namespace Base64Png
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.edBase64 = new System.Windows.Forms.TextBox();
            this.btnDecode = new System.Windows.Forms.Button();
            this.btnEncode = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.chkEncPrefix = new System.Windows.Forms.CheckBox();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.radioFormatGIF = new System.Windows.Forms.RadioButton();
            this.radioFormatBMP = new System.Windows.Forms.RadioButton();
            this.radioFormatJPG = new System.Windows.Forms.RadioButton();
            this.radioFormatPNG = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.grpType.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            resources.ApplyResources(this.picPreview, "picPreview");
            this.picPreview.Name = "picPreview";
            this.picPreview.TabStop = false;
            this.toolTip.SetToolTip(this.picPreview, resources.GetString("picPreview.ToolTip"));
            this.picPreview.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.picPreview_LoadCompleted);
            this.picPreview.DoubleClick += new System.EventHandler(this.picPreview_DoubleClick);
            // 
            // edBase64
            // 
            this.edBase64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.edBase64, "edBase64");
            this.edBase64.Name = "edBase64";
            this.edBase64.KeyUp += new System.Windows.Forms.KeyEventHandler(this.edBase64_KeyUp);
            // 
            // btnDecode
            // 
            resources.ApplyResources(this.btnDecode, "btnDecode");
            this.btnDecode.Name = "btnDecode";
            this.toolTip.SetToolTip(this.btnDecode, resources.GetString("btnDecode.ToolTip"));
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // btnEncode
            // 
            resources.ApplyResources(this.btnEncode, "btnEncode");
            this.btnEncode.Name = "btnEncode";
            this.toolTip.SetToolTip(this.btnEncode, resources.GetString("btnEncode.ToolTip"));
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // btnPaste
            // 
            resources.ApplyResources(this.btnPaste, "btnPaste");
            this.btnPaste.Name = "btnPaste";
            this.toolTip.SetToolTip(this.btnPaste, resources.GetString("btnPaste.ToolTip"));
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.toolTip.SetToolTip(this.btnCopy, resources.GetString("btnCopy.ToolTip"));
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // chkEncPrefix
            // 
            resources.ApplyResources(this.chkEncPrefix, "chkEncPrefix");
            this.chkEncPrefix.Name = "chkEncPrefix";
            this.chkEncPrefix.UseVisualStyleBackColor = true;
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.radioFormatGIF);
            this.grpType.Controls.Add(this.radioFormatBMP);
            this.grpType.Controls.Add(this.radioFormatJPG);
            this.grpType.Controls.Add(this.radioFormatPNG);
            resources.ApplyResources(this.grpType, "grpType");
            this.grpType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpType.Name = "grpType";
            this.grpType.TabStop = false;
            // 
            // radioFormatGIF
            // 
            resources.ApplyResources(this.radioFormatGIF, "radioFormatGIF");
            this.radioFormatGIF.Name = "radioFormatGIF";
            this.radioFormatGIF.TabStop = true;
            this.radioFormatGIF.UseVisualStyleBackColor = true;
            // 
            // radioFormatBMP
            // 
            resources.ApplyResources(this.radioFormatBMP, "radioFormatBMP");
            this.radioFormatBMP.Name = "radioFormatBMP";
            this.radioFormatBMP.TabStop = true;
            this.radioFormatBMP.UseVisualStyleBackColor = true;
            // 
            // radioFormatJPG
            // 
            resources.ApplyResources(this.radioFormatJPG, "radioFormatJPG");
            this.radioFormatJPG.Name = "radioFormatJPG";
            this.radioFormatJPG.TabStop = true;
            this.radioFormatJPG.UseVisualStyleBackColor = true;
            // 
            // radioFormatPNG
            // 
            resources.ApplyResources(this.radioFormatPNG, "radioFormatPNG");
            this.radioFormatPNG.Name = "radioFormatPNG";
            this.radioFormatPNG.TabStop = true;
            this.radioFormatPNG.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.chkEncPrefix);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.edBase64);
            this.Controls.Add(this.picPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.TextBox edBase64;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkEncPrefix;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.RadioButton radioFormatGIF;
        private System.Windows.Forms.RadioButton radioFormatBMP;
        private System.Windows.Forms.RadioButton radioFormatJPG;
        private System.Windows.Forms.RadioButton radioFormatPNG;
    }
}

