namespace DialogTest
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFontDialog = new System.Windows.Forms.Button();
            this.btnColorDialog = new System.Windows.Forms.Button();
            this.btnFontDialogSystem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFontDialog
            // 
            this.btnFontDialog.Location = new System.Drawing.Point(160, 32);
            this.btnFontDialog.Name = "btnFontDialog";
            this.btnFontDialog.Size = new System.Drawing.Size(75, 42);
            this.btnFontDialog.TabIndex = 1;
            this.btnFontDialog.Text = "Font Dialog";
            this.btnFontDialog.UseVisualStyleBackColor = true;
            this.btnFontDialog.Click += new System.EventHandler(this.btnFontDialog_Click);
            // 
            // btnColorDialog
            // 
            this.btnColorDialog.Location = new System.Drawing.Point(32, 32);
            this.btnColorDialog.Name = "btnColorDialog";
            this.btnColorDialog.Size = new System.Drawing.Size(75, 42);
            this.btnColorDialog.TabIndex = 2;
            this.btnColorDialog.Text = "Color Dialog";
            this.btnColorDialog.UseVisualStyleBackColor = true;
            this.btnColorDialog.Click += new System.EventHandler(this.btnColorDialog_Click);
            // 
            // btnFontDialogSystem
            // 
            this.btnFontDialogSystem.Location = new System.Drawing.Point(160, 131);
            this.btnFontDialogSystem.Name = "btnFontDialogSystem";
            this.btnFontDialogSystem.Size = new System.Drawing.Size(75, 42);
            this.btnFontDialogSystem.TabIndex = 3;
            this.btnFontDialogSystem.Text = "Font Dialog";
            this.btnFontDialogSystem.UseVisualStyleBackColor = true;
            this.btnFontDialogSystem.Click += new System.EventHandler(this.btnFontDialogSystem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btnFontDialogSystem);
            this.Controls.Add(this.btnColorDialog);
            this.Controls.Add(this.btnFontDialog);
            this.Name = "MainForm";
            this.Text = "Dialog Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFontDialog;
        private System.Windows.Forms.Button btnColorDialog;
        private System.Windows.Forms.Button btnFontDialogSystem;
    }
}

