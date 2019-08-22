namespace NetCharm.Common
{
    partial class ColorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorDialog));
            this.colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            this.cmCopyColorValue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiColorValueHexRGB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueHexBGR = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueHexARGB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueHexABGR = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueHexRGBA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueHexBGRA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueHexSharp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiColorValueCssRGB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueCssRGBA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueCssHSL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueCssHSLA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueCssPercent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiColorValueColorRGB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueColorBGR = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorValueSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiColorValueName = new System.Windows.Forms.ToolStripMenuItem();
            this.colorManager = new Cyotek.Windows.Forms.ColorEditorManager();
            this.colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            this.colorGrid = new Cyotek.Windows.Forms.ColorGrid();
            this.cmPalette = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lightnessColorSlider = new Cyotek.Windows.Forms.LightnessColorSlider();
            this.screenColorPicker = new Cyotek.Windows.Forms.ScreenColorPicker();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.btnPalette = new System.Windows.Forms.Button();
            this.cmCopyColorValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorWheel
            // 
            this.colorWheel.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorWheel.ContextMenuStrip = this.cmCopyColorValue;
            resources.ApplyResources(this.colorWheel, "colorWheel");
            this.colorWheel.Name = "colorWheel";
            // 
            // cmCopyColorValue
            // 
            this.cmCopyColorValue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiColorValueHexRGB,
            this.tsmiColorValueHexBGR,
            this.tsmiColorValueHexARGB,
            this.tsmiColorValueHexABGR,
            this.tsmiColorValueHexRGBA,
            this.tsmiColorValueHexBGRA,
            this.tsmiColorValueHexSharp,
            this.tsmiColorValueSep0,
            this.tsmiColorValueCssRGB,
            this.tsmiColorValueCssRGBA,
            this.tsmiColorValueCssHSL,
            this.tsmiColorValueCssHSLA,
            this.tsmiColorValueCssPercent,
            this.tsmiColorValueSep1,
            this.tsmiColorValueColorRGB,
            this.tsmiColorValueColorBGR,
            this.tsmiColorValueSep2,
            this.tsmiColorValueName});
            this.cmCopyColorValue.Name = "cmCopyColorValue";
            resources.ApplyResources(this.cmCopyColorValue, "cmCopyColorValue");
            // 
            // tsmiColorValueHexRGB
            // 
            this.tsmiColorValueHexRGB.Name = "tsmiColorValueHexRGB";
            resources.ApplyResources(this.tsmiColorValueHexRGB, "tsmiColorValueHexRGB");
            this.tsmiColorValueHexRGB.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueHexBGR
            // 
            this.tsmiColorValueHexBGR.Name = "tsmiColorValueHexBGR";
            resources.ApplyResources(this.tsmiColorValueHexBGR, "tsmiColorValueHexBGR");
            this.tsmiColorValueHexBGR.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueHexARGB
            // 
            this.tsmiColorValueHexARGB.Name = "tsmiColorValueHexARGB";
            resources.ApplyResources(this.tsmiColorValueHexARGB, "tsmiColorValueHexARGB");
            this.tsmiColorValueHexARGB.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueHexABGR
            // 
            this.tsmiColorValueHexABGR.Name = "tsmiColorValueHexABGR";
            resources.ApplyResources(this.tsmiColorValueHexABGR, "tsmiColorValueHexABGR");
            this.tsmiColorValueHexABGR.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueHexRGBA
            // 
            this.tsmiColorValueHexRGBA.Name = "tsmiColorValueHexRGBA";
            resources.ApplyResources(this.tsmiColorValueHexRGBA, "tsmiColorValueHexRGBA");
            this.tsmiColorValueHexRGBA.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueHexBGRA
            // 
            this.tsmiColorValueHexBGRA.Name = "tsmiColorValueHexBGRA";
            resources.ApplyResources(this.tsmiColorValueHexBGRA, "tsmiColorValueHexBGRA");
            this.tsmiColorValueHexBGRA.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueHexSharp
            // 
            this.tsmiColorValueHexSharp.Checked = true;
            this.tsmiColorValueHexSharp.CheckOnClick = true;
            this.tsmiColorValueHexSharp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiColorValueHexSharp.Name = "tsmiColorValueHexSharp";
            resources.ApplyResources(this.tsmiColorValueHexSharp, "tsmiColorValueHexSharp");
            this.tsmiColorValueHexSharp.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueSep0
            // 
            this.tsmiColorValueSep0.Name = "tsmiColorValueSep0";
            resources.ApplyResources(this.tsmiColorValueSep0, "tsmiColorValueSep0");
            this.tsmiColorValueSep0.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueCssRGB
            // 
            this.tsmiColorValueCssRGB.Name = "tsmiColorValueCssRGB";
            resources.ApplyResources(this.tsmiColorValueCssRGB, "tsmiColorValueCssRGB");
            this.tsmiColorValueCssRGB.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueCssRGBA
            // 
            this.tsmiColorValueCssRGBA.Name = "tsmiColorValueCssRGBA";
            resources.ApplyResources(this.tsmiColorValueCssRGBA, "tsmiColorValueCssRGBA");
            this.tsmiColorValueCssRGBA.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueCssHSL
            // 
            this.tsmiColorValueCssHSL.Name = "tsmiColorValueCssHSL";
            resources.ApplyResources(this.tsmiColorValueCssHSL, "tsmiColorValueCssHSL");
            this.tsmiColorValueCssHSL.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueCssHSLA
            // 
            this.tsmiColorValueCssHSLA.Name = "tsmiColorValueCssHSLA";
            resources.ApplyResources(this.tsmiColorValueCssHSLA, "tsmiColorValueCssHSLA");
            this.tsmiColorValueCssHSLA.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueCssPercent
            // 
            this.tsmiColorValueCssPercent.Checked = true;
            this.tsmiColorValueCssPercent.CheckOnClick = true;
            this.tsmiColorValueCssPercent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiColorValueCssPercent.Name = "tsmiColorValueCssPercent";
            resources.ApplyResources(this.tsmiColorValueCssPercent, "tsmiColorValueCssPercent");
            this.tsmiColorValueCssPercent.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueSep1
            // 
            this.tsmiColorValueSep1.Name = "tsmiColorValueSep1";
            resources.ApplyResources(this.tsmiColorValueSep1, "tsmiColorValueSep1");
            this.tsmiColorValueSep1.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueColorRGB
            // 
            this.tsmiColorValueColorRGB.Name = "tsmiColorValueColorRGB";
            resources.ApplyResources(this.tsmiColorValueColorRGB, "tsmiColorValueColorRGB");
            this.tsmiColorValueColorRGB.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueColorBGR
            // 
            this.tsmiColorValueColorBGR.Name = "tsmiColorValueColorBGR";
            resources.ApplyResources(this.tsmiColorValueColorBGR, "tsmiColorValueColorBGR");
            this.tsmiColorValueColorBGR.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueSep2
            // 
            this.tsmiColorValueSep2.Name = "tsmiColorValueSep2";
            resources.ApplyResources(this.tsmiColorValueSep2, "tsmiColorValueSep2");
            this.tsmiColorValueSep2.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // tsmiColorValueName
            // 
            this.tsmiColorValueName.Name = "tsmiColorValueName";
            resources.ApplyResources(this.tsmiColorValueName, "tsmiColorValueName");
            this.tsmiColorValueName.Click += new System.EventHandler(this.tsmiColorValueCopy_Click);
            // 
            // colorManager
            // 
            this.colorManager.ColorEditor = this.colorEditor;
            this.colorManager.ColorGrid = this.colorGrid;
            this.colorManager.ColorWheel = this.colorWheel;
            this.colorManager.LightnessColorSlider = this.lightnessColorSlider;
            this.colorManager.ScreenColorPicker = this.screenColorPicker;
            this.colorManager.ColorChanged += new System.EventHandler(this.colorEditorManager_ColorChanged);
            // 
            // colorEditor
            // 
            this.colorEditor.ContextMenuStrip = this.cmCopyColorValue;
            resources.ApplyResources(this.colorEditor, "colorEditor");
            this.colorEditor.Name = "colorEditor";
            this.colorEditor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // colorGrid
            // 
            resources.ApplyResources(this.colorGrid, "colorGrid");
            this.colorGrid.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None;
            this.colorGrid.CellSize = new System.Drawing.Size(10, 11);
            this.colorGrid.ContextMenuStrip = this.cmPalette;
            this.colorGrid.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.Palette = Cyotek.Windows.Forms.ColorPalette.Paint;
            // 
            // cmPalette
            // 
            this.cmPalette.Name = "cmPalette";
            resources.ApplyResources(this.cmPalette, "cmPalette");
            this.cmPalette.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmPalette_ItemClicked);
            // 
            // lightnessColorSlider
            // 
            resources.ApplyResources(this.lightnessColorSlider, "lightnessColorSlider");
            this.lightnessColorSlider.Name = "lightnessColorSlider";
            this.lightnessColorSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            // 
            // screenColorPicker
            // 
            resources.ApplyResources(this.screenColorPicker, "screenColorPicker");
            this.screenColorPicker.Color = System.Drawing.Color.Black;
            this.screenColorPicker.Image = global::NetCharm.Common.Properties.Resources.eyedropper;
            this.screenColorPicker.Name = "screenColorPicker";
            this.screenColorPicker.ShowTextWithSnapshot = true;
            this.screenColorPicker.MouseCaptureChanged += new System.EventHandler(this.screenColorPicker_MouseCaptureChanged);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // colorPanel
            // 
            resources.ApplyResources(this.colorPanel, "colorPanel");
            this.colorPanel.ContextMenuStrip = this.cmCopyColorValue;
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.DoubleClick += new System.EventHandler(this.colorPanel_DoubleClick);
            // 
            // btnPalette
            // 
            resources.ApplyResources(this.btnPalette, "btnPalette");
            this.btnPalette.Name = "btnPalette";
            this.btnPalette.UseVisualStyleBackColor = true;
            this.btnPalette.Click += new System.EventHandler(this.btnPalette_Click);
            // 
            // ColorDialog
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnPalette);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.lightnessColorSlider);
            this.Controls.Add(this.colorGrid);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.screenColorPicker);
            this.Controls.Add(this.colorEditor);
            this.Controls.Add(this.colorWheel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ColorDialog_Load);
            this.cmCopyColorValue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Cyotek.Windows.Forms.ColorWheel colorWheel;
        private Cyotek.Windows.Forms.ColorEditorManager colorManager;
        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private Cyotek.Windows.Forms.ScreenColorPicker screenColorPicker;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Cyotek.Windows.Forms.ColorGrid colorGrid;
        private Cyotek.Windows.Forms.LightnessColorSlider lightnessColorSlider;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.ContextMenuStrip cmPalette;
        private System.Windows.Forms.Button btnPalette;
        private System.Windows.Forms.ContextMenuStrip cmCopyColorValue;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueHexRGB;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueHexBGR;
        private System.Windows.Forms.ToolStripSeparator tsmiColorValueSep0;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueCssRGB;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueCssRGBA;
        private System.Windows.Forms.ToolStripSeparator tsmiColorValueSep1;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueColorRGB;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueColorBGR;
        private System.Windows.Forms.ToolStripSeparator tsmiColorValueSep2;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueName;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueHexRGBA;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueHexBGRA;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueHexARGB;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueHexABGR;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueHexSharp;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueCssHSL;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueCssHSLA;
        private System.Windows.Forms.ToolStripMenuItem tsmiColorValueCssPercent;
    }
}