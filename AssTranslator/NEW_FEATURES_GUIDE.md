# Hướng dẫn các tính năng mới

## ✅ Đã thêm thành công:

### 📁 **1. Chọn nhiều file cùng lúc**
- **Thay đổi**: Từ chọn 1 file → chọn nhiều file
- **Cách sử dụng**:
  - Nhấn "Add Files..." để chọn nhiều file .ass
  - Nhấn "Remove" để xóa file đã chọn
  - Nhấn "Clear All" để xóa tất cả
- **Xử lý**: Dịch tuần tự từng file, xuất vào thư mục đã chỉ định

### 📝 **2. Hệ thống Log**
- **Vị trí**: Phần Log ở dưới giao diện
- **Hiển thị**: 
  - Thời gian thực (HH:mm:ss)
  - Hoạt động hiện tại
  - Lỗi và cảnh báo
- **Tự động scroll**: Luôn hiển thị log mới nhất

### ⚙️ **3. Cài đặt Retry**
- **Retry Count**: Số lần thử lại (1-1000, mặc định: 100)
- **Retry Delay**: Thời gian chờ giữa các lần retry (1-300 giây, mặc định: 2)
- **Formula**: Delay = (lần retry + 1) × Retry Delay (tối đa 60 giây)

### 🔢 **4. Batch Size Fibonacci**
- **Dãy Fibonacci**: 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610
- **Mặc định**: 89 (đã test và hoạt động tốt)
- **Lựa chọn**: Chọn batch size phù hợp với file size

### 📂 **5. Output Folder**
- **Thay đổi**: Từ chọn file xuất → chọn thư mục xuất
- **Tên file**: Tự động tạo tên `[tên_gốc].vi.ass`
- **Mặc định**: Desktop

## 🎯 **Cách sử dụng:**

### **Bước 1: Cấu hình**
1. Nhập API Key
2. Chọn Model
3. Chọn ngôn ngữ đích
4. (Tùy chọn) Chọn Custom Prompt

### **Bước 2: Cài đặt nâng cao**
1. **Retry Count**: Để mặc định 100 (hoặc tăng nếu mạng không ổn định)
2. **Retry Delay**: Để mặc định 2 (hoặc tăng nếu API bị rate limit)
3. **Batch Size**: Chọn theo kích thước file:
   - File nhỏ (< 100 dòng): 13, 21, 34
   - File trung bình (100-500 dòng): 55, 89
   - File lớn (> 500 dòng): 144, 233

### **Bước 3: Chọn file**
1. Nhấn "Add Files..." để chọn nhiều file
2. Kiểm tra danh sách file đã chọn
3. Nhấn "Remove" hoặc "Clear All" nếu cần

### **Bước 4: Chọn thư mục xuất**
1. Nhấn "Browse..." để chọn thư mục
2. File sẽ được lưu với tên `[tên_gốc].vi.ass`

### **Bước 5: Bắt đầu dịch**
1. Nhấn "Start Translation"
2. Theo dõi log để xem tiến độ
3. Xem thanh progress bar

## 📊 **Log Messages:**

### **Khi thêm file:**
```
[14:30:15] Đã thêm 3 file(s)
```

### **Khi bắt đầu dịch:**
```
[14:30:20] Đang dịch: movie1.ass
[14:30:25] Đã tìm thấy 150 dòng thoại cần dịch
[14:30:26] Chia thành 2 batch để dịch
```

### **Khi retry:**
```
[14:30:30] API Error 503, retrying in 2 seconds... (attempt 2/100)
[14:30:32] Retry attempt 2/100...
```

### **Khi hoàn thành:**
```
[14:30:45] ✓ Hoàn thành: movie1.ass
[14:30:46] Đang dịch: movie2.ass
[14:31:00] ✓ Hoàn thành: movie2.ass
[14:31:01] Hoàn thành dịch 2/2 files!
```

## ⚠️ **Lưu ý quan trọng:**

### **1. Không sửa code cũ**
- Tất cả tính năng cũ vẫn hoạt động bình thường
- Chỉ thêm tính năng mới, không thay đổi logic cũ

### **2. Tương thích ngược**
- File cũ vẫn có thể dịch được
- Settings cũ vẫn được lưu

### **3. Performance**
- Batch size nhỏ = chậm hơn nhưng ổn định hơn
- Batch size lớn = nhanh hơn nhưng có thể lỗi
- Retry nhiều = tỷ lệ thành công cao hơn

## 🔧 **Troubleshooting:**

### **Nếu gặp lỗi:**
1. **Kiểm tra log**: Xem lỗi cụ thể
2. **Giảm batch size**: Thử batch size nhỏ hơn
3. **Tăng retry**: Tăng số lần retry
4. **Kiểm tra API key**: Đảm bảo API key đúng

### **Nếu chậm:**
1. **Tăng batch size**: Thử batch size lớn hơn
2. **Giảm retry delay**: Giảm thời gian chờ
3. **Kiểm tra mạng**: Đảm bảo kết nối ổn định

## 🚀 **Test ngay:**

```bash
dotnet run
```

1. Chọn 2-3 file .ass nhỏ để test
2. Sử dụng batch size 13 hoặc 21
3. Xem log chi tiết
4. Kiểm tra file xuất

Tất cả tính năng mới đã được thêm thành công và không ảnh hưởng đến chức năng cũ! 🎉

