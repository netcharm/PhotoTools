namespace InternalFilters.Effects
{
    partial class SharpenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharpenForm));
            Cyotek.Windows.Forms.ZoomLevelCollection zoomLevelCollection1 = new Cyotek.Windows.Forms.ZoomLevelCollection();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOriginal = new System.Windows.Forms.Button();
            this.edGaussianThreshold = new System.Windows.Forms.NumericUpDown();
            this.edGaussianSize = new System.Windows.Forms.NumericUpDown();
            this.edGaussianSigma = new System.Windows.Forms.NumericUpDown();
            this.edGdiRatio = new System.Windows.Forms.NumericUpDown();
            this.edGdiAmount = new System.Windows.Forms.NumericUpDown();
            this.imgPreview = new NetCharm.Common.Controls.ImageBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.btnModeGdi = new System.Windows.Forms.RadioButton();
            this.btnModeGaussian = new System.Windows.Forms.RadioButton();
            this.btnModeNormal = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.grpGaussianParams = new System.Windows.Forms.GroupBox();
            this.lblGaussianThreshold = new System.Windows.Forms.Label();
            this.lblGaussianSize = new System.Windows.Forms.Label();
            this.lblGaussianSigma = new System.Windows.Forms.Label();
            this.grpGdiParams = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGdiRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGdiAmount)).BeginInit();
            this.grpMode.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.grpGaussianParams.SuspendLayout();
            this.grpGdiParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOriginal
            // 
            resources.ApplyResources(this.btnOriginal, "btnOriginal");
            this.btnOriginal.Name = "btnOriginal";
            this.toolTip.SetToolTip(this.btnOriginal, resources.GetString("btnOriginal.ToolTip"));
            this.btnOriginal.UseVisualStyleBackColor = true;
            this.btnOriginal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnOriginal_MouseDown);
            this.btnOriginal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnOriginal_MouseUp);
            // 
            // edGaussianThreshold
            // 
            this.edGaussianThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.edGaussianThreshold, "edGaussianThreshold");
            this.edGaussianThreshold.Name = "edGaussianThreshold";
            this.toolTip.SetToolTip(this.edGaussianThreshold, resources.GetString("edGaussianThreshold.ToolTip"));
            this.edGaussianThreshold.ValueChanged += new System.EventHandler(this.edMode_ValueChanged);
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
            5,
            0,
            0,
            0});
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
            // edGdiRatio
            // 
            this.edGdiRatio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edGdiRatio.DecimalPlaces = 2;
            this.edGdiRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.edGdiRatio, "edGdiRatio");
            this.edGdiRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edGdiRatio.Name = "edGdiRatio";
            this.toolTip.SetToolTip(this.edGdiRatio, resources.GetString("edGdiRatio.ToolTip"));
            this.edGdiRatio.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.edGdiRatio.ValueChanged += new System.EventHandler(this.edMode_ValueChanged);
            // 
            // edGdiAmount
            // 
            this.edGdiAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edGdiAmount.DecimalPlaces = 2;
            resources.ApplyResources(this.edGdiAmount, "edGdiAmount");
            this.edGdiAmount.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.edGdiAmount.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.edGdiAmount.Name = "edGdiAmount";
            this.toolTip.SetToolTip(this.edGdiAmount, resources.GetString("edGdiAmount.ToolTip"));
            this.edGdiAmount.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
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
            this.imgPreview.ZoomLevels = zoomLevelCollection1;
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
            // grpMode
            // 
            this.grpMode.Controls.Add(this.btnModeGdi);
            this.grpMode.Controls.Add(this.btnModeGaussian);
            this.grpMode.Controls.Add(this.btnModeNormal);
            this.grpMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.grpMode, "grpMode");
            this.grpMode.Name = "grpMode";
            this.grpMode.TabStop = false;
            // 
            // btnModeGdi
            // 
            resources.ApplyResources(this.btnModeGdi, "btnModeGdi");
            this.btnModeGdi.Name = "btnModeGdi";
            this.btnModeGdi.TabStop = true;
            this.btnModeGdi.UseVisualStyleBackColor = true;
            this.btnModeGdi.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnModeGaussian
            // 
            resources.ApplyResources(this.btnModeGaussian, "btnModeGaussian");
            this.btnModeGaussian.Name = "btnModeGaussian";
            this.btnModeGaussian.TabStop = true;
            this.btnModeGaussian.UseVisualStyleBackColor = true;
            this.btnModeGaussian.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnModeNormal
            // 
            resources.ApplyResources(this.btnModeNormal, "btnModeNormal");
            this.btnModeNormal.Name = "btnModeNormal";
            this.btnModeNormal.TabStop = true;
            this.btnModeNormal.UseVisualStyleBackColor = true;
            this.btnModeNormal.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.grpGaussianParams);
            this.flowLayoutPanel1.Controls.Add(this.grpGdiParams);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
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
            // grpGdiParams
            // 
            this.grpGdiParams.Controls.Add(this.label1);
            this.grpGdiParams.Controls.Add(this.edGdiAmount);
            this.grpGdiParams.Controls.Add(this.label2);
            this.grpGdiParams.Controls.Add(this.edGdiRatio);
            resources.ApplyResources(this.grpGdiParams, "grpGdiParams");
            this.grpGdiParams.Name = "grpGdiParams";
            this.grpGdiParams.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // SharpenForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.imgPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "SharpenForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SharpenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGaussianSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGdiRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edGdiAmount)).EndInit();
            this.grpMode.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.grpGaussianParams.ResumeLayout(false);
            this.grpGdiParams.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private NetCharm.Common.Controls.ImageBox imgPreview;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.RadioButton btnModeGaussian;
        private System.Windows.Forms.RadioButton btnModeNormal;
        private System.Windows.Forms.Button btnOriginal;
        private System.Windows.Forms.RadioButton btnModeGdi;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox grpGaussianParams;
        private System.Windows.Forms.Label lblGaussianThreshold;
        private System.Windows.Forms.NumericUpDown edGaussianThreshold;
        private System.Windows.Forms.Label lblGaussianSize;
        private System.Windows.Forms.NumericUpDown edGaussianSize;
        private System.Windows.Forms.Label lblGaussianSigma;
        private System.Windows.Forms.NumericUpDown edGaussianSigma;
        private System.Windows.Forms.GroupBox grpGdiParams;
        private System.Windows.Forms.NumericUpDown edGdiRatio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown edGdiAmount;
        private System.Windows.Forms.Label label2;
    }
}