namespace InternalFilters.Effects
{
    partial class BlurForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlurForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOriginal = new System.Windows.Forms.CheckBox();
            this.edGaussianSize = new System.Windows.Forms.NumericUpDown();
            this.edGaussianSigma = new System.Windows.Forms.NumericUpDown();
            this.edGaussianThreshold = new System.Windows.Forms.NumericUpDown();
            this.edBoxSize = new System.Windows.Forms.NumericUpDown();
            this.btnModeBox = new System.Windows.Forms.RadioButton();
            this.btnModeGaussian = new System.Windows.Forms.RadioButton();
            this.btnModeNormal = new System.Windows.Forms.RadioButton();
            this.imgPreview = new NetCharm.Image.Addins.ImageBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpGaussianParams = new System.Windows.Forms.GroupBox();
            this.lblGaussianThreshold = new System.Windows.Forms.Label();
            this.lblGaussianSize = new System.Windows.Forms.Label();
            this.lblGaussianSigma = new System.Windows.Forms.Label();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.grpBoxParams = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edBoxSize)).BeginInit();
            this.grpGaussianParams.SuspendLayout();
            this.grpMode.SuspendLayout();
            this.grpBoxParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOriginal
            // 
            resources.ApplyResources(this.btnOriginal, "btnOriginal");
            this.btnOriginal.Name = "btnOriginal";
            this.toolTip.SetToolTip(this.btnOriginal, resources.GetString("btnOriginal.ToolTip"));
            this.btnOriginal.UseVisualStyleBackColor = true;
            this.btnOriginal.Click += new System.EventHandler(this.btnOriginal_Click);
            // 
            // edGaussianSize
            // 
            this.edGaussianSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.edGaussianSize, "edGaussianSize");
            this.edGaussianSize.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.edGaussianSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.edGaussianSize.Name = "edGaussianSize";
            this.toolTip.SetToolTip(this.edGaussianSize, resources.GetString("edGaussianSize.ToolTip"));
            this.edGaussianSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.edGaussianSize.ValueChanged += new System.EventHandler(this.edMode_ValueChanged);
            // 
            // edGaussianSigma
            // 
            this.edGaussianSigma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edGaussianSigma.DecimalPlaces = 2;
            this.edGaussianSigma.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.edGaussianSigma, "edGaussianSigma");
            this.edGaussianSigma.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            this.edGaussianSigma.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.edGaussianSigma.Name = "edGaussianSigma";
            this.toolTip.SetToolTip(this.edGaussianSigma, resources.GetString("edGaussianSigma.ToolTip"));
            this.edGaussianSigma.Value = new decimal(new int[] {
            14,
            0,
            0,
            65536});
            this.edGaussianSigma.ValueChanged += new System.EventHandler(this.edMode_ValueChanged);
            // 
            // edGaussianThreshold
            // 
            this.edGaussianThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.edGaussianThreshold, "edGaussianThreshold");
            this.edGaussianThreshold.Name = "edGaussianThreshold";
            this.toolTip.SetToolTip(this.edGaussianThreshold, resources.GetString("edGaussianThreshold.ToolTip"));
            this.edGaussianThreshold.ValueChanged += new System.EventHandler(this.edMode_ValueChanged);
            // 
            // edBoxSize
            // 
            this.edBoxSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.edBoxSize, "edBoxSize");
            this.edBoxSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.edBoxSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.edBoxSize.Name = "edBoxSize";
            this.toolTip.SetToolTip(this.edBoxSize, resources.GetString("edBoxSize.ToolTip"));
            this.edBoxSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.edBoxSize.ValueChanged += new System.EventHandler(this.edMode_ValueChanged);
            // 
            // btnModeBox
            // 
            resources.ApplyResources(this.btnModeBox, "btnModeBox");
            this.btnModeBox.Name = "btnModeBox";
            this.btnModeBox.TabStop = true;
            this.toolTip.SetToolTip(this.btnModeBox, resources.GetString("btnModeBox.ToolTip"));
            this.btnModeBox.UseVisualStyleBackColor = true;
            this.btnModeBox.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnModeGaussian
            // 
            resources.ApplyResources(this.btnModeGaussian, "btnModeGaussian");
            this.btnModeGaussian.Name = "btnModeGaussian";
            this.btnModeGaussian.TabStop = true;
            this.toolTip.SetToolTip(this.btnModeGaussian, resources.GetString("btnModeGaussian.ToolTip"));
            this.btnModeGaussian.UseVisualStyleBackColor = true;
            this.btnModeGaussian.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnModeNormal
            // 
            resources.ApplyResources(this.btnModeNormal, "btnModeNormal");
            this.btnModeNormal.Name = "btnModeNormal";
            this.btnModeNormal.TabStop = true;
            this.toolTip.SetToolTip(this.btnModeNormal, resources.GetString("btnModeNormal.ToolTip"));
            this.btnModeNormal.UseVisualStyleBackColor = true;
            this.btnModeNormal.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // imgPreview
            // 
            resources.ApplyResources(this.imgPreview, "imgPreview");
            this.imgPreview.Image = null;
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.SelectionColor = System.Drawing.SystemColors.Highlight;
            this.imgPreview.SelectionKeepAspect = false;
            this.imgPreview.SelectionRegion = ((System.Drawing.RectangleF)(resources.GetObject("imgPreview.SelectionRegion")));
            this.imgPreview.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
            this.imgPreview.Zoom = 100;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpGaussianParams
            // 
            this.grpGaussianParams.Controls.Add(this.lblGaussianThreshold);
            this.grpGaussianParams.Controls.Add(this.edGaussianThreshold);
            this.grpGaussianParams.Controls.Add(this.lblGaussianSize);
            this.grpGaussianParams.Controls.Add(this.edGaussianSize);
            this.grpGaussianParams.Controls.Add(this.lblGaussianSigma);
            this.grpGaussianParams.Controls.Add(this.edGaussianSigma);
            this.grpGaussianParams.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.grpGaussianParams, "grpGaussianParams");
            this.grpGaussianParams.Name = "grpGaussianParams";
            this.grpGaussianParams.TabStop = false;
            // 
            // lblGaussianThreshold
            // 
            resources.ApplyResources(this.lblGaussianThreshold, "lblGaussianThreshold");
            this.lblGaussianThreshold.Name = "lblGaussianThreshold";
            // 
            // lblGaussianSize
            // 
            resources.ApplyResources(this.lblGaussianSize, "lblGaussianSize");
            this.lblGaussianSize.Name = "lblGaussianSize";
            // 
            // lblGaussianSigma
            // 
            resources.ApplyResources(this.lblGaussianSigma, "lblGaussianSigma");
            this.lblGaussianSigma.Name = "lblGaussianSigma";
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.btnModeBox);
            this.grpMode.Controls.Add(this.btnModeGaussian);
            this.grpMode.Controls.Add(this.btnModeNormal);
            this.grpMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.grpMode, "grpMode");
            this.grpMode.Name = "grpMode";
            this.grpMode.TabStop = false;
            // 
            // grpBoxParams
            // 
            this.grpBoxParams.Controls.Add(this.edBoxSize);
            resources.ApplyResources(this.grpBoxParams, "grpBoxParams");
            this.grpBoxParams.Name = "grpBoxParams";
            this.grpBoxParams.TabStop = false;
            // 
            // BlurForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.grpBoxParams);
            this.Controls.Add(this.grpGaussianParams);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.imgPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "BlurForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.BlurForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edBoxSize)).EndInit();
            this.grpGaussianParams.ResumeLayout(false);
            this.grpMode.ResumeLayout(false);
            this.grpBoxParams.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private NetCharm.Image.Addins.ImageBox imgPreview;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox btnOriginal;
        private System.Windows.Forms.GroupBox grpGaussianParams;
        private System.Windows.Forms.Label lblGaussianSize;
        private System.Windows.Forms.NumericUpDown edGaussianSize;
        private System.Windows.Forms.Label lblGaussianSigma;
        private System.Windows.Forms.NumericUpDown edGaussianSigma;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.RadioButton btnModeGaussian;
        private System.Windows.Forms.RadioButton btnModeNormal;
        private System.Windows.Forms.Label lblGaussianThreshold;
        private System.Windows.Forms.NumericUpDown edGaussianThreshold;
        private System.Windows.Forms.GroupBox grpBoxParams;
        private System.Windows.Forms.NumericUpDown edBoxSize;
        private System.Windows.Forms.RadioButton btnModeBox;
    }
}