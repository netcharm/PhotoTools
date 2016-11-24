namespace NetCharm.Common
{
    partial class ColorDialogEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorDialogEx));
            this.colorWheel = new Cyotek.Windows.Forms.ColorWheel();
            this.colorManager = new Cyotek.Windows.Forms.ColorEditorManager();
            this.colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            this.colorGrid = new Cyotek.Windows.Forms.ColorGrid();
            this.lightnessColorSlider = new Cyotek.Windows.Forms.LightnessColorSlider();
            this.screenColorPicker = new Cyotek.Windows.Forms.ScreenColorPicker();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // colorWheel
            // 
            this.colorWheel.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.colorWheel, "colorWheel");
            this.colorWheel.Name = "colorWheel";
            // 
            // colorManager
            // 
            this.colorManager.ColorEditor = this.colorEditor;
            this.colorManager.ColorGrid = this.colorGrid;
            this.colorManager.ColorWheel = this.colorWheel;
            this.colorManager.LightnessColorSlider = this.lightnessColorSlider;
            this.colorManager.ScreenColorPicker = this.screenColorPicker;
            this.colorManager.ColorChanged += new System.EventHandler(this.colorEditorManager1_ColorChanged);
            // 
            // colorEditor
            // 
            resources.ApplyResources(this.colorEditor, "colorEditor");
            this.colorEditor.Name = "colorEditor";
            this.colorEditor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // colorGrid
            // 
            resources.ApplyResources(this.colorGrid, "colorGrid");
            this.colorGrid.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None;
            this.colorGrid.CellSize = new System.Drawing.Size(10, 11);
            this.colorGrid.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.Palette = Cyotek.Windows.Forms.ColorPalette.Paint;
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
            this.screenColorPicker.Image = global::NetCharm.Image.Addins.Properties.Resources.eyedropper;
            this.screenColorPicker.Name = "screenColorPicker";
            this.screenColorPicker.ShowTextWithSnapshot = true;
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
            this.colorPanel.Name = "colorPanel";
            // 
            // ColorDialog
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
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
    }
}