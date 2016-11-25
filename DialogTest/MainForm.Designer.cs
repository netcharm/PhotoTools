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
            this.btnColorDialogEx = new System.Windows.Forms.Button();
            this.btnFontDialogEx = new System.Windows.Forms.Button();
            this.btnColorDialogSystem = new System.Windows.Forms.Button();
            this.dlgColorEx = new NetCharm.Common.Controls.ColorDialogEx();
            this.dlgFontEx = new NetCharm.Common.Controls.FontDialogEx();
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
            this.btnFontDialogSystem.Location = new System.Drawing.Point(160, 179);
            this.btnFontDialogSystem.Name = "btnFontDialogSystem";
            this.btnFontDialogSystem.Size = new System.Drawing.Size(75, 53);
            this.btnFontDialogSystem.TabIndex = 3;
            this.btnFontDialogSystem.Text = "System Font Dialog";
            this.btnFontDialogSystem.UseVisualStyleBackColor = true;
            this.btnFontDialogSystem.Click += new System.EventHandler(this.btnFontDialogSystem_Click);
            // 
            // btnColorDialogEx
            // 
            this.btnColorDialogEx.Location = new System.Drawing.Point(32, 80);
            this.btnColorDialogEx.Name = "btnColorDialogEx";
            this.btnColorDialogEx.Size = new System.Drawing.Size(75, 42);
            this.btnColorDialogEx.TabIndex = 4;
            this.btnColorDialogEx.Text = "Color Dialog Ex";
            this.btnColorDialogEx.UseVisualStyleBackColor = true;
            this.btnColorDialogEx.Click += new System.EventHandler(this.btnColorDilogEx_Click);
            // 
            // btnFontDialogEx
            // 
            this.btnFontDialogEx.Location = new System.Drawing.Point(160, 80);
            this.btnFontDialogEx.Name = "btnFontDialogEx";
            this.btnFontDialogEx.Size = new System.Drawing.Size(75, 42);
            this.btnFontDialogEx.TabIndex = 5;
            this.btnFontDialogEx.Text = "Font Dialog Ex";
            this.btnFontDialogEx.UseVisualStyleBackColor = true;
            this.btnFontDialogEx.Click += new System.EventHandler(this.btnFontDialogEx_Click);
            // 
            // btnColorDialogSystem
            // 
            this.btnColorDialogSystem.Location = new System.Drawing.Point(32, 179);
            this.btnColorDialogSystem.Name = "btnColorDialogSystem";
            this.btnColorDialogSystem.Size = new System.Drawing.Size(75, 53);
            this.btnColorDialogSystem.TabIndex = 6;
            this.btnColorDialogSystem.Text = "System Color Dialog";
            this.btnColorDialogSystem.UseVisualStyleBackColor = true;
            this.btnColorDialogSystem.Click += new System.EventHandler(this.btnColorDialogSystem_Click);
            // 
            // dlgColorEx
            // 
            this.dlgColorEx.Color = System.Drawing.Color.Red;
            this.dlgColorEx.ShowApply = true;
            this.dlgColorEx.Apply += new System.EventHandler(this.dlgColorEx_Apply);
            // 
            // dlgFontEx
            // 
            this.dlgFontEx.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dlgFontEx.ShowApply = true;
            this.dlgFontEx.Size = 9F;
            this.dlgFontEx.Strikeout = false;
            this.dlgFontEx.Underline = false;
            this.dlgFontEx.Apply += new System.EventHandler(this.dlgFont_Apply);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 260);
            this.Controls.Add(this.btnColorDialogSystem);
            this.Controls.Add(this.btnFontDialogEx);
            this.Controls.Add(this.btnColorDialogEx);
            this.Controls.Add(this.btnFontDialogSystem);
            this.Controls.Add(this.btnColorDialog);
            this.Controls.Add(this.btnFontDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Dialog Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFontDialog;
        private System.Windows.Forms.Button btnColorDialog;
        private System.Windows.Forms.Button btnFontDialogSystem;
        private NetCharm.Common.Controls.ColorDialogEx dlgColorEx;
        private System.Windows.Forms.Button btnColorDialogEx;
        private System.Windows.Forms.Button btnFontDialogEx;
        private System.Windows.Forms.Button btnColorDialogSystem;
        private NetCharm.Common.Controls.FontDialogEx dlgFontEx;
    }
}

