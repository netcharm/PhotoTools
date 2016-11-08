namespace InternalFilters.Effects
{
    partial class HslFilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HslFilterForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.scpPicker = new Cyotek.Windows.Forms.ScreenColorPicker();
            this.btnOriginal = new System.Windows.Forms.CheckBox();
            this.cbGrayMode = new System.Windows.Forms.ComboBox();
            this.edTol = new NetCharm.Image.Addins.Controls.SlideNumber();
            this.edLum = new NetCharm.Image.Addins.Controls.SlideColorLum();
            this.edSat = new NetCharm.Image.Addins.Controls.SlideColorSat();
            this.edHue = new NetCharm.Image.Addins.Controls.SlideColorHue();
            this.imgPreview = new NetCharm.Image.Addins.ImageBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // scpPicker
            // 
            this.scpPicker.Color = System.Drawing.Color.Empty;
            this.scpPicker.Image = global::InternalFilters.Properties.Resources.eyedropper;
            resources.ApplyResources(this.scpPicker, "scpPicker");
            this.scpPicker.Name = "scpPicker";
            this.scpPicker.ColorChanged += new System.EventHandler(this.scpPicker_ColorChanged);
            this.scpPicker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scpPicker_MouseDown);
            this.scpPicker.MouseMove += new System.Windows.Forms.MouseEventHandler(this.scpPicker_MouseMove);
            this.scpPicker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scpPicker_MouseUp);
            // 
            // btnOriginal
            // 
            resources.ApplyResources(this.btnOriginal, "btnOriginal");
            this.btnOriginal.Name = "btnOriginal";
            this.toolTip.SetToolTip(this.btnOriginal, resources.GetString("btnOriginal.ToolTip"));
            this.btnOriginal.UseVisualStyleBackColor = true;
            this.btnOriginal.Click += new System.EventHandler(this.btnOriginal_Click);
            // 
            // cbGrayMode
            // 
            this.cbGrayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrayMode.FormattingEnabled = true;
            resources.ApplyResources(this.cbGrayMode, "cbGrayMode");
            this.cbGrayMode.Name = "cbGrayMode";
            this.toolTip.SetToolTip(this.cbGrayMode, resources.GetString("cbGrayMode.ToolTip"));
            this.cbGrayMode.SelectedIndexChanged += new System.EventHandler(this.cbGrayMode_SelectedIndexChanged);
            // 
            // edTol
            // 
            this.edTol.Caption = "Tolerance";
            this.edTol.DecimalPlaces = 1;
            resources.ApplyResources(this.edTol, "edTol");
            this.edTol.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.edTol.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edTol.Name = "edTol";
            this.edTol.Step = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.edTol.Unit = "%";
            this.edTol.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.edTol.ValueChanged += new System.EventHandler(this.edHsl_ValueChanged);
            // 
            // edLum
            // 
            this.edLum.Caption = "Luminance";
            this.edLum.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.edLum.DecimalPlaces = 2;
            resources.ApplyResources(this.edLum, "edLum");
            this.edLum.Name = "edLum";
            this.edLum.NubColor = System.Drawing.Color.Black;
            this.edLum.NubSize = new System.Drawing.Size(8, 8);
            this.edLum.NubStyle = Cyotek.Windows.Forms.ColorSliderNubStyle.BottomRight;
            this.edLum.ShowValueDivider = false;
            this.edLum.Step = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edLum.Unit = "";
            this.edLum.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.edLum.ValueChanged += new System.EventHandler(this.edHsl_ValueChanged);
            // 
            // edSat
            // 
            this.edSat.Caption = "Saturation";
            this.edSat.Color = System.Drawing.Color.Black;
            this.edSat.DecimalPlaces = 2;
            resources.ApplyResources(this.edSat, "edSat");
            this.edSat.Name = "edSat";
            this.edSat.NubColor = System.Drawing.Color.Black;
            this.edSat.NubSize = new System.Drawing.Size(8, 8);
            this.edSat.NubStyle = Cyotek.Windows.Forms.ColorSliderNubStyle.BottomRight;
            this.edSat.ShowValueDivider = false;
            this.edSat.Step = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edSat.Unit = "";
            this.edSat.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.edSat.ValueChanged += new System.EventHandler(this.edHsl_ValueChanged);
            // 
            // edHue
            // 
            this.edHue.Caption = "Hue";
            this.edHue.DecimalPlaces = 0;
            resources.ApplyResources(this.edHue, "edHue");
            this.edHue.Name = "edHue";
            this.edHue.NubColor = System.Drawing.Color.Black;
            this.edHue.NubSize = new System.Drawing.Size(8, 8);
            this.edHue.NubStyle = Cyotek.Windows.Forms.ColorSliderNubStyle.BottomRight;
            this.edHue.ShowValueDivider = false;
            this.edHue.Step = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edHue.Unit = "";
            this.edHue.Value = new decimal(new int[] {
            181,
            0,
            0,
            0});
            this.edHue.ValueChanged += new System.EventHandler(this.edHsl_ValueChanged);
            // 
            // imgPreview
            // 
            this.imgPreview.Image = null;
            resources.ApplyResources(this.imgPreview, "imgPreview");
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.SelectionColor = System.Drawing.SystemColors.Highlight;
            this.imgPreview.SelectionKeepAspect = false;
            this.imgPreview.SelectionRegion = ((System.Drawing.RectangleF)(resources.GetObject("imgPreview.SelectionRegion")));
            this.imgPreview.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
            this.imgPreview.Zoom = 100;
            // 
            // HslFilterForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.cbGrayMode);
            this.Controls.Add(this.scpPicker);
            this.Controls.Add(this.edTol);
            this.Controls.Add(this.edLum);
            this.Controls.Add(this.edSat);
            this.Controls.Add(this.edHue);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.imgPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "HslFilterForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.HslFilterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private NetCharm.Image.Addins.ImageBox imgPreview;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox btnOriginal;
        private NetCharm.Image.Addins.Controls.SlideColorHue edHue;
        private NetCharm.Image.Addins.Controls.SlideColorSat edSat;
        private NetCharm.Image.Addins.Controls.SlideColorLum edLum;
        private NetCharm.Image.Addins.Controls.SlideNumber edTol;
        private Cyotek.Windows.Forms.ScreenColorPicker scpPicker;
        private System.Windows.Forms.ComboBox cbGrayMode;
    }
}