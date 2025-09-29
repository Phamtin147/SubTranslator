using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssTranslator
{
    public class AssTranslator
    {
        private readonly string _apiKey;
        private readonly string _model;
        private readonly string _customPrompt;
        private readonly string _languageCode;
        private readonly string _apiProvider;
        private readonly HttpClient _httpClient;
        private readonly int _retryCount;
        private readonly int _retryDelay;
        private readonly int _batchSize;
        
        // API URLs
        private const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/{0}:generateContent?key={1}";
        private const string DeepSeekApiUrl = "https://api.deepseek.com/v1/chat/completions";
        
        private const string DefaultPrompt = @"
Bạn là dịch giả phụ đề chuyên nghiệp.

NHIỆM VỤ:
- Dịch từ tiếng Anh sang {0}
- Tên riêng/vật phẩm/địa danh: kèm Hán Việt trong ngoặc nếu phù hợp
- Phải để ý thật nhưng cách xưng hô như i you để tránh đang thoại của người con trai lớn tuổi hơn nhưng là xưng mình là em
- Tuyệt đối giữ nguyên mọi PLACEHOLDER và TAG trong văn bản: ví dụ {\i1}, {\pos(320,240)} — phải giữ y nguyên
- Giữ nguyên [TAGx] và {\...} nguyên dạng
- Thay \N bằng [BR] khi trả; chương trình sẽ đổi lại

FORMAT TRẢ VỀ:
- Đầu vào: JSON mảng string [""text1"", ""text2"", ...]
- Đầu ra: JSON mảng string [""dịch1"", ""dịch2"", ...]
- CHỈ TRẢ JSON ARRAY, KHÔNG GIẢI THÍCH GÌ KHÁC
- Số lượng phần tử phải khớp với đầu vào

VÍ DỤ:
Input: [""Hello world"", ""How are you?""]
Output: [""Xin chào thế giới"", ""Bạn khỏe không?""]
";

        public event EventHandler<int>? ProgressChanged;
        public event EventHandler<string>? StatusChanged;

        public AssTranslator(string apiKey, string model, string customPrompt, string languageCode, string apiProvider = "Gemini", int retryCount = 100, int retryDelay = 2, int batchSize = 89)
        {
            _apiKey = apiKey;
            _model = model;
            _customPrompt = customPrompt;
            _languageCode = languageCode;
            _apiProvider = apiProvider;
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(2);
            
            // Store retry settings
            _retryCount = retryCount;
            _retryDelay = retryDelay;
            _batchSize = batchSize;
            
            // Set headers based on API provider
            if (apiProvider == "DeepSeek")
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            }
        }

        public async Task<bool> TranslateFile(string inputFilePath, string outputFilePath)
        {
            try
            {
                OnStatusChanged("Đang đọc file phụ đề...");
                var lines = await File.ReadAllLinesAsync(inputFilePath);
                var dialogueLines = new List<string>();
                var nonDialogueLines = new List<string>();
                var dialogueIndices = new List<int>();

                // Tách các dòng dialogue và non-dialogue
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    if (line.StartsWith("Dialogue:"))
                    {
                        dialogueLines.Add(line);
                        dialogueIndices.Add(i);
                    }
                    else
                    {
                        nonDialogueLines.Add(line);
                    }
                }

                OnStatusChanged($"Đã tìm thấy {dialogueLines.Count} dòng thoại cần dịch");

                // Chuẩn bị các batch để dịch
                var batches = new List<List<string>>();
                for (int i = 0; i < dialogueLines.Count; i += _batchSize)
                {
                    batches.Add(dialogueLines.Skip(i).Take(_batchSize).ToList());
                }

                OnStatusChanged($"Chia thành {batches.Count} batch để dịch");

                var translatedDialogues = new List<string>();
                int batchCount = 0;

                // Dịch từng batch
                foreach (var batch in batches)
                {
                    batchCount++;
                    OnStatusChanged($"Đang dịch batch {batchCount}/{batches.Count}...");
                    
                    var extractedTexts = batch.Select(line => ExtractDialogueText(line)).ToList();
                    var translatedTexts = await TranslateTexts(extractedTexts);
                    
                    if (translatedTexts.Count != batch.Count)
                    {
                        OnStatusChanged($"Warning: Số lượng dòng dịch không khớp ({translatedTexts.Count} != {batch.Count})");
                        
                        // Nếu ít hơn, thêm các dòng trống
                        if (translatedTexts.Count < batch.Count)
                        {
                            var missingCount = batch.Count - translatedTexts.Count;
                            for (int i = 0; i < missingCount; i++)
                            {
                                translatedTexts.Add("[DỊCH LỖI]");
                            }
                            OnStatusChanged($"Đã thêm {missingCount} dòng placeholder cho các dòng lỗi");
                        }
                        // Nếu nhiều hơn, chỉ lấy số lượng đúng
                        else if (translatedTexts.Count > batch.Count)
                        {
                            translatedTexts = translatedTexts.Take(batch.Count).ToList();
                            OnStatusChanged($"Đã cắt bớt xuống {batch.Count} dòng");
                        }
                    }

                    for (int i = 0; i < batch.Count; i++)
                    {
                        var originalLine = batch[i];
                        var translatedText = translatedTexts[i].Replace("[BR]", "\\N");
                        var translatedLine = ReplaceDialogueText(originalLine, translatedText);
                        translatedDialogues.Add(translatedLine);
                    }

                    // Cập nhật tiến độ
                    int progressPercentage = (int)((double)batchCount / batches.Count * 100);
                    OnProgressChanged(progressPercentage);
                }

                // Tạo file đầu ra
                var outputLines = new List<string>();
                int dialogueIndex = 0;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (dialogueIndices.Contains(i))
                    {
                        outputLines.Add(translatedDialogues[dialogueIndex]);
                        dialogueIndex++;
                    }
                    else
                    {
                        outputLines.Add(lines[i]);
                    }
                }

                OnStatusChanged("Đang lưu file phụ đề đã dịch...");
                await File.WriteAllLinesAsync(outputFilePath, outputLines);
                OnStatusChanged("Hoàn thành dịch phụ đề!");
                return true;
            }
            catch (Exception ex)
            {
                OnStatusChanged($"Lỗi: {ex.Message}");
                return false;
            }
        }

        private string ExtractDialogueText(string dialogueLine)
        {
            var parts = dialogueLine.Split(',', 10);
            if (parts.Length < 10)
            {
                return string.Empty;
            }
            return parts[9];
        }

        private string ReplaceDialogueText(string dialogueLine, string newText)
        {
            var parts = dialogueLine.Split(',', 10);
            if (parts.Length < 10)
            {
                return dialogueLine;
            }
            parts[9] = newText;
            return string.Join(',', parts);
        }

        private async Task<List<string>> TranslateTexts(List<string> texts)
        {
            try
            {
                string prompt = string.IsNullOrEmpty(_customPrompt) 
                    ? string.Format(DefaultPrompt, _languageCode) 
                    : File.ReadAllText(_customPrompt);

                string requestJson;
                string url;

                if (_apiProvider == "DeepSeek")
                {
                    // DeepSeek API format
                var requestBody = new
                    {
                        model = _model,
                        messages = new[]
                        {
                            new
                            {
                                role = "user",
                                content = $"{prompt}\n\n{JsonSerializer.Serialize(texts)}"
                            }
                        },
                        temperature = 0.2,
                        max_tokens = 8192
                    };

                    requestJson = JsonSerializer.Serialize(requestBody);
                    url = DeepSeekApiUrl;
                }
                else
                {
                    // Gemini API format
                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            role = "user",
                            parts = new[]
                            {
                                new
                                {
                                    text = $"{prompt}\n\n{JsonSerializer.Serialize(texts)}"
                                }
                            }
                        }
                        },
                        generationConfig = new
                        {
                            temperature = 0.2,
                            topP = 0.9,
                            maxOutputTokens = 8192
                        }
                    };

                    requestJson = JsonSerializer.Serialize(requestBody);
                    url = string.Format(GeminiApiUrl, _model, _apiKey);
                }

                var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                
                // Retry mechanism
                int maxRetries = _retryCount;
                HttpResponseMessage? response = null;
                string responseJson = "";
                
                for (int retry = 0; retry < maxRetries; retry++)
                {
                    try
                    {
                        if (retry > 0)
                        {
                            OnStatusChanged($"Retry attempt {retry + 1}/{maxRetries}...");
                        }
                        
                        response = await _httpClient.PostAsync(url, content);
                        responseJson = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            break; // Success, exit retry loop
                        }
                        
                        // Check if it's a retryable error
                        if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable || 
                            response.StatusCode == System.Net.HttpStatusCode.TooManyRequests ||
                            response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            if (retry < maxRetries - 1)
                            {
                                int delaySeconds = Math.Min((retry + 1) * _retryDelay, 60); // Max 60 seconds delay
                                OnStatusChanged($"API Error {response.StatusCode}, retrying in {delaySeconds} seconds... (attempt {retry + 1}/{maxRetries})");
                                await Task.Delay(delaySeconds * 1000); // Exponential backoff with cap
                                continue;
                            }
                        }
                        
                        // Non-retryable error or max retries reached
                        OnStatusChanged($"API Error: {response.StatusCode}\n{responseJson}");
                    throw new Exception($"API Error: {response.StatusCode}");
                    }
                    catch (Exception ex)
                    {
                        if (retry < maxRetries - 1)
                        {
                            int delaySeconds = Math.Min((retry + 1) * _retryDelay, 60); // Max 60 seconds delay
                            OnStatusChanged($"Network error, retrying in {delaySeconds} seconds... (attempt {retry + 1}/{maxRetries})");
                            OnStatusChanged($"Error: {ex.Message}");
                            await Task.Delay(delaySeconds * 1000);
                            continue;
                        }
                        else
                        {
                            OnStatusChanged($"Network error after {maxRetries} attempts: {ex.Message}");
                            throw;
                        }
                    }
                }

                if (response == null || !response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to get response after {maxRetries} attempts");
                }

                var jsonDocument = JsonDocument.Parse(responseJson);
                string responseText;

                if (_apiProvider == "DeepSeek")
                {
                    // DeepSeek response format
                    responseText = jsonDocument.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString() ?? "";
                }
                else
                {
                    // Gemini response format
                    responseText = jsonDocument.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString() ?? "";
                }

                if (string.IsNullOrEmpty(responseText))
                {
                    throw new Exception("Empty response from API");
                }

                // Tìm mảng JSON trong phản hồi với nhiều pattern khác nhau
                List<string> translatedTexts = new List<string>();
                
                // Pattern 1: Mảng string đơn giản ["text1", "text2", ...]
                var match1 = Regex.Match(responseText, @"\[\s*""[^""]*""(?:\s*,\s*""[^""]*"")*\s*\]");
                if (match1.Success)
                {
                    try
                    {
                        var jsonArray = match1.Value;
                        translatedTexts = JsonSerializer.Deserialize<List<string>>(jsonArray) ?? new List<string>();
                        if (translatedTexts.Count > 0)
                        {
                            return translatedTexts;
                        }
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged($"Pattern 1 failed: {ex.Message}");
                    }
                }

                // Pattern 2: Mảng object với id và vi {"id": "L0", "vi": "text"}
                var match2 = Regex.Match(responseText, @"\[\s*\{[^}]*""vi""[^}]*\}(?:\s*,\s*\{[^}]*""vi""[^}]*\})*\s*\]");
                if (match2.Success)
                {
                    try
                    {
                        var jsonArray = match2.Value;
                        var translatedObjects = JsonSerializer.Deserialize<List<TranslatedItem>>(jsonArray, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (translatedObjects != null)
                        {
                            translatedTexts = translatedObjects.Select(x => x.Vi).ToList();
                            if (translatedTexts.Count > 0)
                            {
                                return translatedTexts;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged($"Pattern 2 failed: {ex.Message}");
                    }
                }

                // Pattern 3: Tìm tất cả string trong dấu ngoặc kép (cải thiện)
                var matches = Regex.Matches(responseText, @"""([^""]*)""");
                if (matches.Count > 0)
                {
                    translatedTexts = new List<string>();
                    foreach (Match match in matches)
                    {
                        var text = match.Groups[1].Value;
                        
                        // Lọc bỏ các string không phải bản dịch
                        if (!string.IsNullOrWhiteSpace(text) && 
                            !text.Contains("id") && 
                            !text.Contains("vi") && 
                            !text.Contains("text") &&
                            !text.Contains("input") &&
                            !text.Contains("output") &&
                            !text.Contains("json") &&
                            !text.Contains("array") &&
                            !text.Contains("dịch") &&
                            !text.Contains("translate") &&
                            !text.Contains("response") &&
                            !text.Contains("result") &&
                            !text.Contains("example") &&
                            !text.Contains("ví dụ") &&
                            !text.Contains("format") &&
                            !text.Contains("pattern") &&
                            !text.Contains("error") &&
                            !text.Contains("lỗi") &&
                            !text.Contains("success") &&
                            !text.Contains("thành công") &&
                            !text.Contains("complete") &&
                            !text.Contains("hoàn thành") &&
                            !text.Contains("done") &&
                            !text.Contains("xong") &&
                            text.Length > 3 && // Bỏ qua các string quá ngắn
                            text.Length < 500 && // Bỏ qua các string quá dài (có thể là giải thích)
                            !text.StartsWith("http") && // Bỏ qua URL
                            !text.Contains("@") && // Bỏ qua email
                            !text.Contains("://") && // Bỏ qua URL
                            !Regex.IsMatch(text, @"^\d+$") && // Bỏ qua số thuần
                            !Regex.IsMatch(text, @"^[A-Z_]+$")) // Bỏ qua constant
                        {
                            translatedTexts.Add(text);
                        }
                    }
                    
                    // Kiểm tra số lượng có hợp lý không (không quá chênh lệch với input)
                    if (translatedTexts.Count > 0 && translatedTexts.Count <= texts.Count * 2)
                    {
                        OnStatusChanged($"Extracted {translatedTexts.Count} texts using pattern 3 (input: {texts.Count})");
                        
                        // Nếu số lượng khớp hoặc gần khớp, trả về
                        if (translatedTexts.Count == texts.Count)
                        {
                            return translatedTexts;
                        }
                        // Nếu nhiều hơn, lấy số lượng đúng
                        else if (translatedTexts.Count > texts.Count)
                        {
                            return translatedTexts.Take(texts.Count).ToList();
                        }
                        // Nếu ít hơn, có thể không đủ, nhưng vẫn thử
                        else
                        {
                            OnStatusChanged($"Warning: Pattern 3 found {translatedTexts.Count} texts but expected {texts.Count}");
                            return translatedTexts;
                        }
                    }
                }

                // Pattern 4: Thử parse toàn bộ response như JSON array
                try
                {
                    translatedTexts = JsonSerializer.Deserialize<List<string>>(responseText) ?? new List<string>();
                    if (translatedTexts.Count > 0)
                    {
                        OnStatusChanged($"Parsed entire response as JSON array");
                        return translatedTexts;
                    }
                }
                catch (Exception ex)
                {
                    OnStatusChanged($"Pattern 4 failed: {ex.Message}");
                }

                // Pattern 5: Tìm các dòng có thể là bản dịch (fallback cuối cùng)
                var lines = responseText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                translatedTexts = new List<string>();
                
                foreach (var line in lines)
                {
                    var trimmedLine = line.Trim();
                    
                    // Tìm dòng có chứa text có thể là bản dịch
                    if (trimmedLine.Length > 5 && 
                        trimmedLine.Length < 200 &&
                        !trimmedLine.Contains("```") &&
                        !trimmedLine.Contains("json") &&
                        !trimmedLine.Contains("array") &&
                        !trimmedLine.Contains("input") &&
                        !trimmedLine.Contains("output") &&
                        !trimmedLine.Contains("dịch") &&
                        !trimmedLine.Contains("translate") &&
                        !trimmedLine.StartsWith("[") &&
                        !trimmedLine.StartsWith("{") &&
                        !trimmedLine.StartsWith("\"") &&
                        !trimmedLine.Contains("http") &&
                        !Regex.IsMatch(trimmedLine, @"^\d+$") &&
                        !Regex.IsMatch(trimmedLine, @"^[A-Z_]+$"))
                    {
                        // Loại bỏ các ký tự đặc biệt ở đầu/cuối
                        var cleanText = trimmedLine.Trim('"', '\'', '`', '[', ']', '{', '}', ',');
                        if (cleanText.Length > 3)
                        {
                            translatedTexts.Add(cleanText);
                        }
                    }
                }
                
                // Chỉ sử dụng nếu số lượng hợp lý
                if (translatedTexts.Count > 0 && translatedTexts.Count <= texts.Count)
                {
                    OnStatusChanged($"Pattern 5 found {translatedTexts.Count} texts from lines (input: {texts.Count})");
                return translatedTexts;
                }

                // Nếu tất cả đều thất bại, log response để debug
                OnStatusChanged($"Raw response: {responseText.Substring(0, Math.Min(500, responseText.Length))}...");
                OnStatusChanged($"Full response length: {responseText.Length}");
                
                // Thử một lần nữa với fallback: trả về mảng rỗng và log lỗi
                OnStatusChanged("WARNING: Could not parse response, returning empty array");
                return new List<string>();
            }
            catch (Exception ex)
            {
                OnStatusChanged($"Translation Error: {ex.Message}");
                throw;
            }
        }

        protected virtual void OnProgressChanged(int progressPercentage)
        {
            ProgressChanged?.Invoke(this, progressPercentage);
        }

        protected virtual void OnStatusChanged(string status)
        {
            StatusChanged?.Invoke(this, status);
        }
    }

    public record TranslatedItem(string Id, string Vi);
}