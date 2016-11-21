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
            this.colorWheel.Color = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 0 ) ) ) ), ( (int) ( ( (byte) ( 0 ) ) ) ), ( (int) ( ( (byte) ( 0 ) ) ) ) );
            this.colorWheel.Location = new System.Drawing.Point( 12, 13 );
            this.colorWheel.Name = "colorWheel";
            this.colorWheel.Size = new System.Drawing.Size( 160, 160 );
            this.colorWheel.TabIndex = 0;
            // 
            // colorManager
            // 
            this.colorManager.ColorEditor = this.colorEditor;
            this.colorManager.ColorGrid = this.colorGrid;
            this.colorManager.ColorWheel = this.colorWheel;
            this.colorManager.LightnessColorSlider = this.lightnessColorSlider;
            this.colorManager.ScreenColorPicker = this.screenColorPicker;
            this.colorManager.ColorChanged += new System.EventHandler( this.colorEditorManager1_ColorChanged );
            // 
            // colorEditor
            // 
            this.colorEditor.Location = new System.Drawing.Point( 12, 178 );
            this.colorEditor.Name = "colorEditor";
            this.colorEditor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.colorEditor.Size = new System.Drawing.Size( 440, 121 );
            this.colorEditor.TabIndex = 2;
            // 
            // colorGrid
            // 
            this.colorGrid.AutoSize = false;
            this.colorGrid.CellBorderStyle = Cyotek.Windows.Forms.ColorCellBorderStyle.None;
            this.colorGrid.CellSize = new System.Drawing.Size( 10, 11 );
            this.colorGrid.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            this.colorGrid.Location = new System.Drawing.Point( 235, 13 );
            this.colorGrid.Margin = new System.Windows.Forms.Padding( 0 );
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.Padding = new System.Windows.Forms.Padding( 0 );
            this.colorGrid.Palette = Cyotek.Windows.Forms.ColorPalette.Paint;
            this.colorGrid.Size = new System.Drawing.Size( 217, 160 );
            this.colorGrid.TabIndex = 1;
            // 
            // lightnessColorSlider
            // 
            this.lightnessColorSlider.Location = new System.Drawing.Point( 200, 13 );
            this.lightnessColorSlider.Name = "lightnessColorSlider";
            this.lightnessColorSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.lightnessColorSlider.Size = new System.Drawing.Size( 21, 160 );
            this.lightnessColorSlider.TabIndex = 8;
            // 
            // screenColorPicker
            // 
            this.screenColorPicker.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.screenColorPicker.Color = System.Drawing.Color.Black;
            this.screenColorPicker.Image = global::NetCharm.Image.Addins.Properties.Resources.eyedropper;
            this.screenColorPicker.Location = new System.Drawing.Point( 472, 109 );
            this.screenColorPicker.Name = "screenColorPicker";
            this.screenColorPicker.ShowTextWithSnapshot = true;
            this.screenColorPicker.Size = new System.Drawing.Size( 64, 64 );
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnApply.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnApply.Location = new System.Drawing.Point( 467, 73 );
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size( 75, 23 );
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler( this.btnApply_Click );
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point( 467, 43 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOk.Location = new System.Drawing.Point( 467, 13 );
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size( 75, 23 );
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // colorPanel
            // 
            this.colorPanel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.colorPanel.Location = new System.Drawing.Point( 472, 226 );
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size( 64, 64 );
            this.colorPanel.TabIndex = 9;
            // 
            // ColorDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size( 554, 311 );
            this.Controls.Add( this.colorPanel );
            this.Controls.Add( this.lightnessColorSlider );
            this.Controls.Add( this.colorGrid );
            this.Controls.Add( this.btnApply );
            this.Controls.Add( this.btnCancel );
            this.Controls.Add( this.btnOk );
            this.Controls.Add( this.screenColorPicker );
            this.Controls.Add( this.colorEditor );
            this.Controls.Add( this.colorWheel );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Color Picker";
            this.Load += new System.EventHandler( this.ColorDialog_Load );
            this.ResumeLayout( false );

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