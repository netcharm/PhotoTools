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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.dlgColorEx = new NetCharm.Common.Controls.ColorDialogEx();
            this.dlgFontEx = new NetCharm.Common.Controls.FontDialogEx();
            this.grpEffect = new System.Windows.Forms.GroupBox();
            this.btnNone = new System.Windows.Forms.RadioButton();
            this.btnGlow = new System.Windows.Forms.RadioButton();
            this.btnOutline = new System.Windows.Forms.RadioButton();
            this.btnShadow = new System.Windows.Forms.RadioButton();
            this.btnBlur = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.grpEffect.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFontDialog
            // 
            this.btnFontDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFontDialog.Location = new System.Drawing.Point(284, 12);
            this.btnFontDialog.Name = "btnFontDialog";
            this.btnFontDialog.Size = new System.Drawing.Size(75, 42);
            this.btnFontDialog.TabIndex = 1;
            this.btnFontDialog.Text = "Font Dialog";
            this.btnFontDialog.UseVisualStyleBackColor = true;
            this.btnFontDialog.Click += new System.EventHandler(this.btnFontDialog_Click);
            // 
            // btnColorDialog
            // 
            this.btnColorDialog.Location = new System.Drawing.Point(32, 12);
            this.btnColorDialog.Name = "btnColorDialog";
            this.btnColorDialog.Size = new System.Drawing.Size(75, 42);
            this.btnColorDialog.TabIndex = 2;
            this.btnColorDialog.Text = "Color Dialog";
            this.btnColorDialog.UseVisualStyleBackColor = true;
            this.btnColorDialog.Click += new System.EventHandler(this.btnColorDialog_Click);
            // 
            // btnFontDialogSystem
            // 
            this.btnFontDialogSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFontDialogSystem.Location = new System.Drawing.Point(284, 310);
            this.btnFontDialogSystem.Name = "btnFontDialogSystem";
            this.btnFontDialogSystem.Size = new System.Drawing.Size(75, 53);
            this.btnFontDialogSystem.TabIndex = 3;
            this.btnFontDialogSystem.Text = "System Font Dialog";
            this.btnFontDialogSystem.UseVisualStyleBackColor = true;
            this.btnFontDialogSystem.Click += new System.EventHandler(this.btnFontDialogSystem_Click);
            // 
            // btnColorDialogEx
            // 
            this.btnColorDialogEx.Location = new System.Drawing.Point(32, 60);
            this.btnColorDialogEx.Name = "btnColorDialogEx";
            this.btnColorDialogEx.Size = new System.Drawing.Size(75, 42);
            this.btnColorDialogEx.TabIndex = 4;
            this.btnColorDialogEx.Text = "Color Dialog Ex";
            this.btnColorDialogEx.UseVisualStyleBackColor = true;
            this.btnColorDialogEx.Click += new System.EventHandler(this.btnColorDilogEx_Click);
            // 
            // btnFontDialogEx
            // 
            this.btnFontDialogEx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFontDialogEx.Location = new System.Drawing.Point(284, 60);
            this.btnFontDialogEx.Name = "btnFontDialogEx";
            this.btnFontDialogEx.Size = new System.Drawing.Size(75, 42);
            this.btnFontDialogEx.TabIndex = 5;
            this.btnFontDialogEx.Text = "Font Dialog Ex";
            this.btnFontDialogEx.UseVisualStyleBackColor = true;
            this.btnFontDialogEx.Click += new System.EventHandler(this.btnFontDialogEx_Click);
            // 
            // btnColorDialogSystem
            // 
            this.btnColorDialogSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnColorDialogSystem.Location = new System.Drawing.Point(32, 310);
            this.btnColorDialogSystem.Name = "btnColorDialogSystem";
            this.btnColorDialogSystem.Size = new System.Drawing.Size(75, 53);
            this.btnColorDialogSystem.TabIndex = 6;
            this.btnColorDialogSystem.Text = "System Color Dialog";
            this.btnColorDialogSystem.UseVisualStyleBackColor = true;
            this.btnColorDialogSystem.Click += new System.EventHandler(this.btnColorDialogSystem_Click);
            // 
            // picBox
            // 
            this.picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBox.BackColor = System.Drawing.Color.White;
            this.picBox.Location = new System.Drawing.Point(32, 109);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(327, 195);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBox.TabIndex = 7;
            this.picBox.TabStop = false;
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
            this.dlgFontEx.UseFont = true;
            this.dlgFontEx.Apply += new System.EventHandler(this.dlgFont_Apply);
            // 
            // grpEffect
            // 
            this.grpEffect.Controls.Add(this.btnNone);
            this.grpEffect.Controls.Add(this.btnGlow);
            this.grpEffect.Controls.Add(this.btnOutline);
            this.grpEffect.Controls.Add(this.btnShadow);
            this.grpEffect.Controls.Add(this.btnBlur);
            this.grpEffect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grpEffect.Location = new System.Drawing.Point(123, 12);
            this.grpEffect.Name = "grpEffect";
            this.grpEffect.Size = new System.Drawing.Size(138, 90);
            this.grpEffect.TabIndex = 12;
            this.grpEffect.TabStop = false;
            this.grpEffect.Text = "Effect";
            // 
            // btnNone
            // 
            this.btnNone.AutoSize = true;
            this.btnNone.Checked = true;
            this.btnNone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNone.Location = new System.Drawing.Point(12, 67);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(46, 16);
            this.btnNone.TabIndex = 4;
            this.btnNone.TabStop = true;
            this.btnNone.Text = "None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnEffect_Click);
            // 
            // btnGlow
            // 
            this.btnGlow.AutoSize = true;
            this.btnGlow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGlow.Location = new System.Drawing.Point(12, 44);
            this.btnGlow.Name = "btnGlow";
            this.btnGlow.Size = new System.Drawing.Size(46, 16);
            this.btnGlow.TabIndex = 3;
            this.btnGlow.Text = "Glow";
            this.btnGlow.UseVisualStyleBackColor = true;
            this.btnGlow.Click += new System.EventHandler(this.btnEffect_Click);
            // 
            // btnOutline
            // 
            this.btnOutline.AutoSize = true;
            this.btnOutline.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOutline.Location = new System.Drawing.Point(67, 21);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new System.Drawing.Size(64, 16);
            this.btnOutline.TabIndex = 2;
            this.btnOutline.Text = "Outline";
            this.btnOutline.UseVisualStyleBackColor = true;
            this.btnOutline.Click += new System.EventHandler(this.btnEffect_Click);
            // 
            // btnShadow
            // 
            this.btnShadow.AutoSize = true;
            this.btnShadow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShadow.Location = new System.Drawing.Point(67, 44);
            this.btnShadow.Name = "btnShadow";
            this.btnShadow.Size = new System.Drawing.Size(58, 16);
            this.btnShadow.TabIndex = 1;
            this.btnShadow.Text = "Shadow";
            this.btnShadow.UseVisualStyleBackColor = true;
            this.btnShadow.Click += new System.EventHandler(this.btnEffect_Click);
            // 
            // btnBlur
            // 
            this.btnBlur.AutoSize = true;
            this.btnBlur.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBlur.Location = new System.Drawing.Point(12, 21);
            this.btnBlur.Name = "btnBlur";
            this.btnBlur.Size = new System.Drawing.Size(46, 16);
            this.btnBlur.TabIndex = 0;
            this.btnBlur.Text = "Blur";
            this.btnBlur.UseVisualStyleBackColor = true;
            this.btnBlur.Click += new System.EventHandler(this.btnEffect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 375);
            this.Controls.Add(this.grpEffect);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.btnColorDialogSystem);
            this.Controls.Add(this.btnFontDialogEx);
            this.Controls.Add(this.btnColorDialogEx);
            this.Controls.Add(this.btnFontDialogSystem);
            this.Controls.Add(this.btnColorDialog);
            this.Controls.Add(this.btnFontDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Dialog Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.grpEffect.ResumeLayout(false);
            this.grpEffect.PerformLayout();
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
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.GroupBox grpEffect;
        private System.Windows.Forms.RadioButton btnGlow;
        private System.Windows.Forms.RadioButton btnOutline;
        private System.Windows.Forms.RadioButton btnShadow;
        private System.Windows.Forms.RadioButton btnBlur;
        private System.Windows.Forms.RadioButton btnNone;
    }
}

