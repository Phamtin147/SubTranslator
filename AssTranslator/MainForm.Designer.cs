namespace AssTranslator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblApiKey = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.cboModel = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.lblInputFiles = new System.Windows.Forms.Label();
            this.lstInputFiles = new System.Windows.Forms.ListBox();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.lblOutputFile = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.lblCustomPrompt = new System.Windows.Forms.Label();
            this.txtCustomPrompt = new System.Windows.Forms.TextBox();
            this.btnBrowseCustomPrompt = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblRetryCount = new System.Windows.Forms.Label();
            this.numRetryCount = new System.Windows.Forms.NumericUpDown();
            this.lblRetryDelay = new System.Windows.Forms.Label();
            this.numRetryDelay = new System.Windows.Forms.NumericUpDown();
            this.lblBatchSize = new System.Windows.Forms.Label();
            this.cboBatchSize = new System.Windows.Forms.ComboBox();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.lstInputFiles = new System.Windows.Forms.ListBox();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnRemoveFile = new System.Windows.Forms.Button();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.lblInputFiles = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(12, 15);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(48, 13);
            this.lblApiKey.TabIndex = 0;
            this.lblApiKey.Text = "API Key:";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(150, 12);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.PasswordChar = '*';
            this.txtApiKey.Size = new System.Drawing.Size(420, 20);
            this.txtApiKey.TabIndex = 1;
            this.txtApiKey.TextChanged += new System.EventHandler(this.txtApiKey_TextChanged);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(12, 55);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(39, 13);
            this.lblModel.TabIndex = 2;
            this.lblModel.Text = "Model:";
            // 
            // cboModel
            // 
            this.cboModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModel.FormattingEnabled = true;
            this.cboModel.Location = new System.Drawing.Point(150, 52);
            this.cboModel.Name = "cboModel";
            this.cboModel.Size = new System.Drawing.Size(420, 21);
            this.cboModel.TabIndex = 3;
            this.cboModel.SelectedIndexChanged += new System.EventHandler(this.cboModel_SelectedIndexChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(12, 95);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(58, 13);
            this.lblLanguage.TabIndex = 4;
            this.lblLanguage.Text = "Language:";
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(150, 92);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(420, 21);
            this.cboLanguage.TabIndex = 5;
            this.cboLanguage.SelectedIndexChanged += new System.EventHandler(this.cboLanguage_SelectedIndexChanged);
            // 
            // lblInputFiles
            // 
            this.lblInputFiles.AutoSize = true;
            this.lblInputFiles.Location = new System.Drawing.Point(12, 135);
            this.lblInputFiles.Name = "lblInputFiles";
            this.lblInputFiles.Size = new System.Drawing.Size(60, 13);
            this.lblInputFiles.TabIndex = 6;
            this.lblInputFiles.Text = "Input Files:";
            // 
            // lstInputFiles
            // 
            this.lstInputFiles.FormattingEnabled = true;
            this.lstInputFiles.Location = new System.Drawing.Point(150, 132);
            this.lstInputFiles.Name = "lstInputFiles";
            this.lstInputFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstInputFiles.Size = new System.Drawing.Size(341, 95);
            this.lstInputFiles.TabIndex = 7;
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(497, 132);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(75, 23);
            this.btnAddFiles.TabIndex = 8;
            this.btnAddFiles.Text = "Add Files...";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // btnRemoveFile
            // 
            this.btnRemoveFile.Location = new System.Drawing.Point(497, 161);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveFile.TabIndex = 9;
            this.btnRemoveFile.Text = "Remove";
            this.btnRemoveFile.UseVisualStyleBackColor = true;
            this.btnRemoveFile.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // btnClearFiles
            // 
            this.btnClearFiles.Location = new System.Drawing.Point(497, 190);
            this.btnClearFiles.Name = "btnClearFiles";
            this.btnClearFiles.Size = new System.Drawing.Size(75, 23);
            this.btnClearFiles.TabIndex = 10;
            this.btnClearFiles.Text = "Clear All";
            this.btnClearFiles.UseVisualStyleBackColor = true;
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);
            // 
            // lblOutputFile
            // 
            this.lblOutputFile.AutoSize = true;
            this.lblOutputFile.Location = new System.Drawing.Point(12, 240);
            this.lblOutputFile.Name = "lblOutputFile";
            this.lblOutputFile.Size = new System.Drawing.Size(75, 13);
            this.lblOutputFile.TabIndex = 11;
            this.lblOutputFile.Text = "Output Folder:";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(150, 237);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.ReadOnly = true;
            this.txtOutputFile.Size = new System.Drawing.Size(341, 20);
            this.txtOutputFile.TabIndex = 12;
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Location = new System.Drawing.Point(497, 235);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutput.TabIndex = 13;
            this.btnBrowseOutput.Text = "Browse...";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // lblCustomPrompt
            // 
            this.lblCustomPrompt.AutoSize = true;
            this.lblCustomPrompt.Location = new System.Drawing.Point(12, 270);
            this.lblCustomPrompt.Name = "lblCustomPrompt";
            this.lblCustomPrompt.Size = new System.Drawing.Size(85, 13);
            this.lblCustomPrompt.TabIndex = 14;
            this.lblCustomPrompt.Text = "Custom Prompt:";
            // 
            // txtCustomPrompt
            // 
            this.txtCustomPrompt.Location = new System.Drawing.Point(150, 267);
            this.txtCustomPrompt.Name = "txtCustomPrompt";
            this.txtCustomPrompt.ReadOnly = true;
            this.txtCustomPrompt.Size = new System.Drawing.Size(341, 20);
            this.txtCustomPrompt.TabIndex = 15;
            // 
            // btnBrowseCustomPrompt
            // 
            this.btnBrowseCustomPrompt.Location = new System.Drawing.Point(497, 265);
            this.btnBrowseCustomPrompt.Name = "btnBrowseCustomPrompt";
            this.btnBrowseCustomPrompt.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCustomPrompt.TabIndex = 16;
            this.btnBrowseCustomPrompt.Text = "Browse...";
            this.btnBrowseCustomPrompt.UseVisualStyleBackColor = true;
            this.btnBrowseCustomPrompt.Click += new System.EventHandler(this.btnBrowseCustomPrompt_Click);
            // 
            // lblRetryCount
            // 
            this.lblRetryCount.AutoSize = true;
            this.lblRetryCount.Location = new System.Drawing.Point(12, 300);
            this.lblRetryCount.Name = "lblRetryCount";
            this.lblRetryCount.Size = new System.Drawing.Size(65, 13);
            this.lblRetryCount.TabIndex = 17;
            this.lblRetryCount.Text = "Retry Count:";
            // 
            // numRetryCount
            // 
            this.numRetryCount.Location = new System.Drawing.Point(150, 297);
            this.numRetryCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRetryCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRetryCount.Name = "numRetryCount";
            this.numRetryCount.Size = new System.Drawing.Size(100, 20);
            this.numRetryCount.TabIndex = 18;
            this.numRetryCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblRetryDelay
            // 
            this.lblRetryDelay.AutoSize = true;
            this.lblRetryDelay.Location = new System.Drawing.Point(270, 300);
            this.lblRetryDelay.Name = "lblRetryDelay";
            this.lblRetryDelay.Size = new System.Drawing.Size(68, 13);
            this.lblRetryDelay.TabIndex = 19;
            this.lblRetryDelay.Text = "Retry Delay:";
            // 
            // numRetryDelay
            // 
            this.numRetryDelay.Location = new System.Drawing.Point(350, 297);
            this.numRetryDelay.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numRetryDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRetryDelay.Name = "numRetryDelay";
            this.numRetryDelay.Size = new System.Drawing.Size(100, 20);
            this.numRetryDelay.TabIndex = 20;
            this.numRetryDelay.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblBatchSize
            // 
            this.lblBatchSize.AutoSize = true;
            this.lblBatchSize.Location = new System.Drawing.Point(12, 330);
            this.lblBatchSize.Name = "lblBatchSize";
            this.lblBatchSize.Size = new System.Drawing.Size(61, 13);
            this.lblBatchSize.TabIndex = 21;
            this.lblBatchSize.Text = "Batch Size:";
            // 
            // cboBatchSize
            // 
            this.cboBatchSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBatchSize.FormattingEnabled = true;
            this.cboBatchSize.Location = new System.Drawing.Point(150, 327);
            this.cboBatchSize.Name = "cboBatchSize";
            this.cboBatchSize.Size = new System.Drawing.Size(200, 21);
            this.cboBatchSize.TabIndex = 22;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(12, 360);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(28, 13);
            this.lblLog.TabIndex = 23;
            this.lblLog.Text = "Log:";
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(15, 380);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(555, 95);
            this.lstLog.TabIndex = 24;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 360);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(555, 23);
            this.progressBar.TabIndex = 25;
            this.progressBar.Visible = false;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTranslate.Location = new System.Drawing.Point(15, 485);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(555, 40);
            this.btnTranslate.TabIndex = 26;
            this.btnTranslate.Text = "Start Translation";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 535);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 27;
            this.lblStatus.Text = "Status";
            this.lblStatus.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 580);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.cboBatchSize);
            this.Controls.Add(this.lblBatchSize);
            this.Controls.Add(this.numRetryDelay);
            this.Controls.Add(this.lblRetryDelay);
            this.Controls.Add(this.numRetryCount);
            this.Controls.Add(this.lblRetryCount);
            this.Controls.Add(this.btnBrowseCustomPrompt);
            this.Controls.Add(this.txtCustomPrompt);
            this.Controls.Add(this.lblCustomPrompt);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.lblOutputFile);
            this.Controls.Add(this.btnClearFiles);
            this.Controls.Add(this.btnRemoveFile);
            this.Controls.Add(this.btnAddFiles);
            this.Controls.Add(this.lstInputFiles);
            this.Controls.Add(this.lblInputFiles);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.cboModel);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.lblApiKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASS Subtitle Translator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numRetryCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetryDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.ComboBox cboModel;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Label lblInputFiles;
        private System.Windows.Forms.ListBox lstInputFiles;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnRemoveFile;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.Label lblOutputFile;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Label lblCustomPrompt;
        private System.Windows.Forms.TextBox txtCustomPrompt;
        private System.Windows.Forms.Button btnBrowseCustomPrompt;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.NumericUpDown numRetryCount;
        private System.Windows.Forms.Label lblRetryDelay;
        private System.Windows.Forms.NumericUpDown numRetryDelay;
        private System.Windows.Forms.Label lblBatchSize;
        private System.Windows.Forms.ComboBox cboBatchSize;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.Label lblStatus;
    }
}