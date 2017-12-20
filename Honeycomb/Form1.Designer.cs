namespace Honeycomb
{
    partial class frm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRun = new System.Windows.Forms.Button();
            this.wbRun = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(773, 40);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // wbRun
            // 
            this.wbRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wbRun.Location = new System.Drawing.Point(0, 69);
            this.wbRun.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbRun.Name = "wbRun";
            this.wbRun.Size = new System.Drawing.Size(975, 189);
            this.wbRun.TabIndex = 1;
            this.wbRun.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 258);
            this.Controls.Add(this.wbRun);
            this.Controls.Add(this.btnRun);
            this.Name = "frm";
            this.Text = "蜂巢";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.WebBrowser wbRun;
    }
}

