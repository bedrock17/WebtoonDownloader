namespace Webtoon
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderSelect = new System.Windows.Forms.Button();
            this.startPage = new System.Windows.Forms.TextBox();
            this.endPage = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.StartButton = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.RichTextBox();
            this.folderPath = new System.Windows.Forms.TextBox();
            this.URLTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // folderSelect
            // 
            this.folderSelect.Location = new System.Drawing.Point(717, 45);
            this.folderSelect.Name = "folderSelect";
            this.folderSelect.Size = new System.Drawing.Size(120, 23);
            this.folderSelect.TabIndex = 0;
            this.folderSelect.Text = "FolderSelect";
            this.folderSelect.UseVisualStyleBackColor = true;
            this.folderSelect.Click += new System.EventHandler(this.folderSelect_Click);
            // 
            // startPage
            // 
            this.startPage.Location = new System.Drawing.Point(683, 12);
            this.startPage.Name = "startPage";
            this.startPage.Size = new System.Drawing.Size(78, 25);
            this.startPage.TabIndex = 2;
            this.startPage.Text = "1";
            // 
            // endPage
            // 
            this.endPage.Location = new System.Drawing.Point(767, 12);
            this.endPage.Name = "endPage";
            this.endPage.Size = new System.Drawing.Size(70, 25);
            this.endPage.TabIndex = 3;
            this.endPage.Text = "10";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 387);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(824, 23);
            this.progressBar.TabIndex = 4;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(761, 77);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(76, 47);
            this.StartButton.TabIndex = 5;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(12, 77);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(743, 300);
            this.output.TabIndex = 6;
            this.output.Text = " ";
         
            // 
            // folderPath
            // 
            this.folderPath.Location = new System.Drawing.Point(12, 44);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(699, 25);
            this.folderPath.TabIndex = 7;
            // 
            // URLTextBox
            // 
            this.URLTextBox.Location = new System.Drawing.Point(13, 12);
            this.URLTextBox.Name = "URLTextBox";
            this.URLTextBox.Size = new System.Drawing.Size(664, 25);
            this.URLTextBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(762, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 67);
            this.button1.TabIndex = 8;
            this.button1.Text = "Make HTML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 422);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.output);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.endPage);
            this.Controls.Add(this.startPage);
            this.Controls.Add(this.URLTextBox);
            this.Controls.Add(this.folderSelect);
            this.Name = "Form1";
            this.Text = "WebtoonDownloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button folderSelect;
        private System.Windows.Forms.TextBox startPage;
        private System.Windows.Forms.TextBox endPage;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.TextBox folderPath;
        private System.Windows.Forms.TextBox URLTextBox;
        private System.Windows.Forms.Button button1;
    }
}

