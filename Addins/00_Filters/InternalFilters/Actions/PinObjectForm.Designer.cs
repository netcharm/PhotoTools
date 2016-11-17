namespace InternalFilters.Actions
{
    partial class PinObjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PinObjectForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpCommonSetting = new System.Windows.Forms.GroupBox();
            this.pnlPosMode = new System.Windows.Forms.Panel();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkTile = new System.Windows.Forms.CheckBox();
            this.btnPosRandom = new System.Windows.Forms.Button();
            this.lvFilters = new System.Windows.Forms.ListView();
            this.pnlEffectTools = new System.Windows.Forms.Panel();
            this.btnEffectAdd = new System.Windows.Forms.Button();
            this.btnEffectRemove = new System.Windows.Forms.Button();
            this.btnEffectUp = new System.Windows.Forms.Button();
            this.btnEffectDown = new System.Windows.Forms.Button();
            this.btnOriginal = new System.Windows.Forms.CheckBox();
            this.pnlCustom = new System.Windows.Forms.Panel();
            this.csSelect = new NetCharm.Image.Addins.Controls.CornerSide();
            this.slideBlend = new NetCharm.Image.Addins.Controls.SlideNumber();
            this.slideOpaque = new NetCharm.Image.Addins.Controls.SlideNumber();
            this.imgPreview = new NetCharm.Image.Addins.ImageBox();
            this.ilSmall = new System.Windows.Forms.ImageList(this.components);
            this.ilLarge = new System.Windows.Forms.ImageList(this.components);
            this.grpCommonSetting.SuspendLayout();
            this.pnlPosMode.SuspendLayout();
            this.pnlEffectTools.SuspendLayout();
            this.SuspendLayout();
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
            // grpCommonSetting
            // 
            resources.ApplyResources(this.grpCommonSetting, "grpCommonSetting");
            this.grpCommonSetting.Controls.Add(this.pnlPosMode);
            this.grpCommonSetting.Controls.Add(this.slideBlend);
            this.grpCommonSetting.Controls.Add(this.slideOpaque);
            this.grpCommonSetting.Name = "grpCommonSetting";
            this.grpCommonSetting.TabStop = false;
            // 
            // pnlPosMode
            // 
            this.pnlPosMode.Controls.Add(this.chkEnabled);
            this.pnlPosMode.Controls.Add(this.chkTile);
            this.pnlPosMode.Controls.Add(this.btnPosRandom);
            this.pnlPosMode.Controls.Add(this.csSelect);
            resources.ApplyResources(this.pnlPosMode, "pnlPosMode");
            this.pnlPosMode.Name = "pnlPosMode";
            // 
            // chkEnabled
            // 
            resources.ApplyResources(this.chkEnabled, "chkEnabled");
            this.chkEnabled.AutoEllipsis = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // chkTile
            // 
            resources.ApplyResources(this.chkTile, "chkTile");
            this.chkTile.AutoEllipsis = true;
            this.chkTile.Name = "chkTile";
            this.chkTile.UseVisualStyleBackColor = true;
            // 
            // btnPosRandom
            // 
            resources.ApplyResources(this.btnPosRandom, "btnPosRandom");
            this.btnPosRandom.Name = "btnPosRandom";
            this.btnPosRandom.UseVisualStyleBackColor = true;
            // 
            // lvFilters
            // 
            resources.ApplyResources(this.lvFilters, "lvFilters");
            this.lvFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvFilters.FullRowSelect = true;
            this.lvFilters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvFilters.HideSelection = false;
            this.lvFilters.LargeImageList = this.ilLarge;
            this.lvFilters.Name = "lvFilters";
            this.lvFilters.SmallImageList = this.ilSmall;
            this.lvFilters.UseCompatibleStateImageBehavior = false;
            this.lvFilters.View = System.Windows.Forms.View.List;
            this.lvFilters.VirtualMode = true;
            this.lvFilters.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvFilters_RetrieveVirtualItem);
            // 
            // pnlEffectTools
            // 
            resources.ApplyResources(this.pnlEffectTools, "pnlEffectTools");
            this.pnlEffectTools.Controls.Add(this.btnEffectDown);
            this.pnlEffectTools.Controls.Add(this.btnEffectUp);
            this.pnlEffectTools.Controls.Add(this.btnEffectRemove);
            this.pnlEffectTools.Controls.Add(this.btnEffectAdd);
            this.pnlEffectTools.Name = "pnlEffectTools";
            // 
            // btnEffectAdd
            // 
            resources.ApplyResources(this.btnEffectAdd, "btnEffectAdd");
            this.btnEffectAdd.Image = global::InternalFilters.Properties.Resources.effect_add_24x;
            this.btnEffectAdd.Name = "btnEffectAdd";
            this.toolTip.SetToolTip(this.btnEffectAdd, resources.GetString("btnEffectAdd.ToolTip"));
            this.btnEffectAdd.UseVisualStyleBackColor = true;
            this.btnEffectAdd.Click += new System.EventHandler(this.btnEffectAdd_Click);
            // 
            // btnEffectRemove
            // 
            resources.ApplyResources(this.btnEffectRemove, "btnEffectRemove");
            this.btnEffectRemove.Image = global::InternalFilters.Properties.Resources.effect_remove_24x;
            this.btnEffectRemove.Name = "btnEffectRemove";
            this.toolTip.SetToolTip(this.btnEffectRemove, resources.GetString("btnEffectRemove.ToolTip"));
            this.btnEffectRemove.UseVisualStyleBackColor = true;
            this.btnEffectRemove.Click += new System.EventHandler(this.btnEffectRemove_Click);
            // 
            // btnEffectUp
            // 
            resources.ApplyResources(this.btnEffectUp, "btnEffectUp");
            this.btnEffectUp.Image = global::InternalFilters.Properties.Resources.effect_up_24x;
            this.btnEffectUp.Name = "btnEffectUp";
            this.toolTip.SetToolTip(this.btnEffectUp, resources.GetString("btnEffectUp.ToolTip"));
            this.btnEffectUp.UseVisualStyleBackColor = true;
            this.btnEffectUp.Click += new System.EventHandler(this.btnEffectUp_Click);
            // 
            // btnEffectDown
            // 
            resources.ApplyResources(this.btnEffectDown, "btnEffectDown");
            this.btnEffectDown.Image = global::InternalFilters.Properties.Resources.effect_down_24x;
            this.btnEffectDown.Name = "btnEffectDown";
            this.toolTip.SetToolTip(this.btnEffectDown, resources.GetString("btnEffectDown.ToolTip"));
            this.btnEffectDown.UseVisualStyleBackColor = true;
            this.btnEffectDown.Click += new System.EventHandler(this.btnEffectDown_Click);
            // 
            // btnOriginal
            // 
            resources.ApplyResources(this.btnOriginal, "btnOriginal");
            this.btnOriginal.Name = "btnOriginal";
            this.toolTip.SetToolTip(this.btnOriginal, resources.GetString("btnOriginal.ToolTip"));
            this.btnOriginal.UseVisualStyleBackColor = true;
            this.btnOriginal.Click += new System.EventHandler(this.btnOriginal_Click);
            // 
            // pnlCustom
            // 
            resources.ApplyResources(this.pnlCustom, "pnlCustom");
            this.pnlCustom.Name = "pnlCustom";
            // 
            // csSelect
            // 
            resources.ApplyResources(this.csSelect, "csSelect");
            this.csSelect.CornetRegion = NetCharm.Image.Addins.CornerRegionType.None;
            this.csSelect.Name = "csSelect";
            // 
            // slideBlend
            // 
            this.slideBlend.Caption = "Blend";
            this.slideBlend.DecimalPlaces = 0;
            resources.ApplyResources(this.slideBlend, "slideBlend");
            this.slideBlend.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.slideBlend.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.slideBlend.Name = "slideBlend";
            this.slideBlend.Step = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.slideBlend.Unit = "%";
            this.slideBlend.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // slideOpaque
            // 
            this.slideOpaque.Caption = "Opaque";
            this.slideOpaque.DecimalPlaces = 0;
            resources.ApplyResources(this.slideOpaque, "slideOpaque");
            this.slideOpaque.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.slideOpaque.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.slideOpaque.Name = "slideOpaque";
            this.slideOpaque.Step = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.slideOpaque.Unit = "%";
            this.slideOpaque.Value = new decimal(new int[] {
            100,
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
            // 
            // ilSmall
            // 
            this.ilSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilSmall, "ilSmall");
            this.ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ilLarge
            // 
            this.ilLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilLarge, "ilLarge");
            this.ilLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // PinObjectForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlCustom);
            this.Controls.Add(this.pnlEffectTools);
            this.Controls.Add(this.lvFilters);
            this.Controls.Add(this.grpCommonSetting);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.imgPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "PinObjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.StampObjectForm_Load);
            this.grpCommonSetting.ResumeLayout(false);
            this.pnlPosMode.ResumeLayout(false);
            this.pnlEffectTools.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private NetCharm.Image.Addins.ImageBox imgPreview;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox btnOriginal;
        private System.Windows.Forms.GroupBox grpCommonSetting;
        private NetCharm.Image.Addins.Controls.SlideNumber slideBlend;
        private NetCharm.Image.Addins.Controls.SlideNumber slideOpaque;
        private System.Windows.Forms.Panel pnlPosMode;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkTile;
        private System.Windows.Forms.Button btnPosRandom;
        private NetCharm.Image.Addins.Controls.CornerSide csSelect;
        private System.Windows.Forms.ListView lvFilters;
        private System.Windows.Forms.Panel pnlEffectTools;
        private System.Windows.Forms.Button btnEffectDown;
        private System.Windows.Forms.Button btnEffectUp;
        private System.Windows.Forms.Button btnEffectRemove;
        private System.Windows.Forms.Button btnEffectAdd;
        private System.Windows.Forms.Panel pnlCustom;
        private System.Windows.Forms.ImageList ilSmall;
        private System.Windows.Forms.ImageList ilLarge;
    }
}