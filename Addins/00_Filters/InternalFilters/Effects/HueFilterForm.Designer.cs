namespace InternalFilters.Effects
{
    partial class HueFilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HueFilterForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOriginal = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imgPreview = new NetCharm.Image.Addins.ImageBox();
            this.edHue = new NetCharm.Image.Addins.Controls.SlideColorHue();
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
            180,
            0,
            0,
            0});
            this.edHue.ValueChanged += new System.EventHandler(this.edHue_ValueChanged);
            // 
            // HueFilterForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.edHue);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.imgPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "HueFilterForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.HueFilterForm_Load);
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
    }
}