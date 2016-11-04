namespace InternalFilters
{
    partial class CropForm
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnModeSelection = new System.Windows.Forms.RadioButton();
            this.btnModeTrans = new System.Windows.Forms.RadioButton();
            this.btnModeTopLeft = new System.Windows.Forms.RadioButton();
            this.btnModeBottomRight = new System.Windows.Forms.RadioButton();
            this.chkSideT = new System.Windows.Forms.CheckBox();
            this.chkSideB = new System.Windows.Forms.CheckBox();
            this.chkSideL = new System.Windows.Forms.CheckBox();
            this.chkSideR = new System.Windows.Forms.CheckBox();
            this.btnModeAspect = new System.Windows.Forms.RadioButton();
            this.cbAspect = new System.Windows.Forms.ComboBox();
            this.edAspectW = new System.Windows.Forms.NumericUpDown();
            this.edAspectH = new System.Windows.Forms.NumericUpDown();
            this.imgPreview = new Cyotek.Windows.Forms.ImageBox();
            this.grpCropMode = new System.Windows.Forms.GroupBox();
            this.grpCropSide = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grpCropAspect = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.edAspectW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edAspectH)).BeginInit();
            this.grpCropMode.SuspendLayout();
            this.grpCropSide.SuspendLayout();
            this.grpCropAspect.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // btnModeSelection
            // 
            this.btnModeSelection.AutoEllipsis = true;
            this.btnModeSelection.Checked = true;
            this.btnModeSelection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModeSelection.Location = new System.Drawing.Point(9, 14);
            this.btnModeSelection.Name = "btnModeSelection";
            this.btnModeSelection.Size = new System.Drawing.Size(140, 20);
            this.btnModeSelection.TabIndex = 0;
            this.btnModeSelection.TabStop = true;
            this.btnModeSelection.Text = "Selection";
            this.toolTip.SetToolTip(this.btnModeSelection, "Selection Area");
            this.btnModeSelection.UseVisualStyleBackColor = true;
            this.btnModeSelection.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnModeTrans
            // 
            this.btnModeTrans.AutoEllipsis = true;
            this.btnModeTrans.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModeTrans.Location = new System.Drawing.Point(9, 39);
            this.btnModeTrans.Name = "btnModeTrans";
            this.btnModeTrans.Size = new System.Drawing.Size(140, 20);
            this.btnModeTrans.TabIndex = 1;
            this.btnModeTrans.Text = "Transparent";
            this.toolTip.SetToolTip(this.btnModeTrans, "Transparent Pixels");
            this.btnModeTrans.UseVisualStyleBackColor = true;
            this.btnModeTrans.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnModeTopLeft
            // 
            this.btnModeTopLeft.AutoEllipsis = true;
            this.btnModeTopLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModeTopLeft.Location = new System.Drawing.Point(9, 64);
            this.btnModeTopLeft.Name = "btnModeTopLeft";
            this.btnModeTopLeft.Size = new System.Drawing.Size(140, 20);
            this.btnModeTopLeft.TabIndex = 2;
            this.btnModeTopLeft.Text = "Top Left";
            this.toolTip.SetToolTip(this.btnModeTopLeft, "Top Left Pixel Color");
            this.btnModeTopLeft.UseVisualStyleBackColor = true;
            this.btnModeTopLeft.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnModeBottomRight
            // 
            this.btnModeBottomRight.AutoEllipsis = true;
            this.btnModeBottomRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModeBottomRight.Location = new System.Drawing.Point(9, 89);
            this.btnModeBottomRight.Name = "btnModeBottomRight";
            this.btnModeBottomRight.Size = new System.Drawing.Size(140, 20);
            this.btnModeBottomRight.TabIndex = 4;
            this.btnModeBottomRight.Text = "Bottom Right";
            this.toolTip.SetToolTip(this.btnModeBottomRight, "Bottom Right Pixel Color");
            this.btnModeBottomRight.UseVisualStyleBackColor = true;
            this.btnModeBottomRight.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // chkSideT
            // 
            this.chkSideT.AutoEllipsis = true;
            this.chkSideT.Checked = true;
            this.chkSideT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSideT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkSideT.Location = new System.Drawing.Point(9, 20);
            this.chkSideT.Name = "chkSideT";
            this.chkSideT.Size = new System.Drawing.Size(64, 20);
            this.chkSideT.TabIndex = 0;
            this.chkSideT.Text = "Top";
            this.toolTip.SetToolTip(this.chkSideT, "Top Side");
            this.chkSideT.UseVisualStyleBackColor = true;
            this.chkSideT.Click += new System.EventHandler(this.chkSide_Click);
            // 
            // chkSideB
            // 
            this.chkSideB.AutoEllipsis = true;
            this.chkSideB.Checked = true;
            this.chkSideB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSideB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkSideB.Location = new System.Drawing.Point(9, 46);
            this.chkSideB.Name = "chkSideB";
            this.chkSideB.Size = new System.Drawing.Size(64, 20);
            this.chkSideB.TabIndex = 1;
            this.chkSideB.Text = "Bottom";
            this.toolTip.SetToolTip(this.chkSideB, "Bottom Side");
            this.chkSideB.UseVisualStyleBackColor = true;
            this.chkSideB.Click += new System.EventHandler(this.chkSide_Click);
            // 
            // chkSideL
            // 
            this.chkSideL.AutoEllipsis = true;
            this.chkSideL.Checked = true;
            this.chkSideL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSideL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkSideL.Location = new System.Drawing.Point(85, 20);
            this.chkSideL.Name = "chkSideL";
            this.chkSideL.Size = new System.Drawing.Size(64, 20);
            this.chkSideL.TabIndex = 2;
            this.chkSideL.Text = "Left";
            this.toolTip.SetToolTip(this.chkSideL, "Left Side");
            this.chkSideL.UseVisualStyleBackColor = true;
            this.chkSideL.Click += new System.EventHandler(this.chkSide_Click);
            // 
            // chkSideR
            // 
            this.chkSideR.AutoEllipsis = true;
            this.chkSideR.Checked = true;
            this.chkSideR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSideR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkSideR.Location = new System.Drawing.Point(85, 46);
            this.chkSideR.Name = "chkSideR";
            this.chkSideR.Size = new System.Drawing.Size(64, 20);
            this.chkSideR.TabIndex = 3;
            this.chkSideR.Text = "Right";
            this.toolTip.SetToolTip(this.chkSideR, "Right Side");
            this.chkSideR.UseVisualStyleBackColor = true;
            this.chkSideR.Click += new System.EventHandler(this.chkSide_Click);
            // 
            // btnModeAspect
            // 
            this.btnModeAspect.AutoEllipsis = true;
            this.btnModeAspect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModeAspect.Location = new System.Drawing.Point(10, 114);
            this.btnModeAspect.Name = "btnModeAspect";
            this.btnModeAspect.Size = new System.Drawing.Size(140, 20);
            this.btnModeAspect.TabIndex = 5;
            this.btnModeAspect.Text = "Aspect Ratio";
            this.toolTip.SetToolTip(this.btnModeAspect, "Custom Aspect Ratio");
            this.btnModeAspect.UseVisualStyleBackColor = true;
            this.btnModeAspect.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // cbAspect
            // 
            this.cbAspect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAspect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbAspect.FormattingEnabled = true;
            this.cbAspect.Location = new System.Drawing.Point(10, 18);
            this.cbAspect.Name = "cbAspect";
            this.cbAspect.Size = new System.Drawing.Size(139, 20);
            this.cbAspect.TabIndex = 0;
            this.toolTip.SetToolTip(this.cbAspect, "Pre-Defined Aspect Ratio List");
            this.cbAspect.SelectedIndexChanged += new System.EventHandler(this.cbAspect_SelectedIndexChanged);
            // 
            // edAspectW
            // 
            this.edAspectW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edAspectW.Location = new System.Drawing.Point(10, 47);
            this.edAspectW.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edAspectW.Name = "edAspectW";
            this.edAspectW.Size = new System.Drawing.Size(53, 21);
            this.edAspectW.TabIndex = 1;
            this.edAspectW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip.SetToolTip(this.edAspectW, "Aspect Ratio of Width & Height");
            this.edAspectW.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edAspectW.ValueChanged += new System.EventHandler(this.edAspect_ValueChanged);
            // 
            // edAspectH
            // 
            this.edAspectH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edAspectH.Location = new System.Drawing.Point(96, 47);
            this.edAspectH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edAspectH.Name = "edAspectH";
            this.edAspectH.Size = new System.Drawing.Size(53, 21);
            this.edAspectH.TabIndex = 2;
            this.edAspectH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip.SetToolTip(this.edAspectH, "Aspect Ratio of Width & Height");
            this.edAspectH.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edAspectH.ValueChanged += new System.EventHandler(this.edAspect_ValueChanged);
            // 
            // imgPreview
            // 
            this.imgPreview.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Image;
            this.imgPreview.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.imgPreview.InvertMouse = true;
            this.imgPreview.Location = new System.Drawing.Point(185, 13);
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.SelectionColor = System.Drawing.Color.PaleTurquoise;
            this.imgPreview.ShowPixelGrid = true;
            this.imgPreview.Size = new System.Drawing.Size(256, 256);
            this.imgPreview.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
            this.imgPreview.TabIndex = 4;
            this.imgPreview.Text = "Image Preview";
            this.imgPreview.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            this.imgPreview.SelectionRegionChanged += new System.EventHandler(this.imgPreview_SelectionRegionChanged);
            this.imgPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgPreview_MouseDown);
            this.imgPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgPreview_MouseMove);
            this.imgPreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgPreview_MouseUp);
            // 
            // grpCropMode
            // 
            this.grpCropMode.Controls.Add(this.btnModeAspect);
            this.grpCropMode.Controls.Add(this.btnModeBottomRight);
            this.grpCropMode.Controls.Add(this.btnModeTopLeft);
            this.grpCropMode.Controls.Add(this.btnModeTrans);
            this.grpCropMode.Controls.Add(this.btnModeSelection);
            this.grpCropMode.Location = new System.Drawing.Point(13, 13);
            this.grpCropMode.Name = "grpCropMode";
            this.grpCropMode.Size = new System.Drawing.Size(160, 140);
            this.grpCropMode.TabIndex = 0;
            this.grpCropMode.TabStop = false;
            this.grpCropMode.Text = "Crop Mode";
            // 
            // grpCropSide
            // 
            this.grpCropSide.Controls.Add(this.chkSideR);
            this.grpCropSide.Controls.Add(this.chkSideL);
            this.grpCropSide.Controls.Add(this.chkSideB);
            this.grpCropSide.Controls.Add(this.chkSideT);
            this.grpCropSide.Location = new System.Drawing.Point(13, 159);
            this.grpCropSide.Name = "grpCropSide";
            this.grpCropSide.Size = new System.Drawing.Size(160, 78);
            this.grpCropSide.TabIndex = 1;
            this.grpCropSide.TabStop = false;
            this.grpCropSide.Text = "Crop Side";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(231, 295);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(321, 295);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // grpCropAspect
            // 
            this.grpCropAspect.Controls.Add(this.label1);
            this.grpCropAspect.Controls.Add(this.edAspectH);
            this.grpCropAspect.Controls.Add(this.edAspectW);
            this.grpCropAspect.Controls.Add(this.cbAspect);
            this.grpCropAspect.Location = new System.Drawing.Point(13, 244);
            this.grpCropAspect.Name = "grpCropAspect";
            this.grpCropAspect.Size = new System.Drawing.Size(160, 76);
            this.grpCropAspect.TabIndex = 5;
            this.grpCropAspect.TabStop = false;
            this.grpCropAspect.Text = "Crop Aspect";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(67, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "x";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CropForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(450, 332);
            this.Controls.Add(this.grpCropAspect);
            this.Controls.Add(this.imgPreview);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpCropSide);
            this.Controls.Add(this.grpCropMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "CropForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crop";
            this.Load += new System.EventHandler(this.CropForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edAspectW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edAspectH)).EndInit();
            this.grpCropMode.ResumeLayout(false);
            this.grpCropSide.ResumeLayout(false);
            this.grpCropAspect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox grpCropMode;
        private System.Windows.Forms.RadioButton btnModeBottomRight;
        private System.Windows.Forms.RadioButton btnModeTopLeft;
        private System.Windows.Forms.RadioButton btnModeTrans;
        private System.Windows.Forms.RadioButton btnModeSelection;
        private System.Windows.Forms.GroupBox grpCropSide;
        private System.Windows.Forms.CheckBox chkSideR;
        private System.Windows.Forms.CheckBox chkSideL;
        private System.Windows.Forms.CheckBox chkSideB;
        private System.Windows.Forms.CheckBox chkSideT;
        private System.Windows.Forms.RadioButton btnModeAspect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Cyotek.Windows.Forms.ImageBox imgPreview;
        private System.Windows.Forms.GroupBox grpCropAspect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown edAspectH;
        private System.Windows.Forms.NumericUpDown edAspectW;
        private System.Windows.Forms.ComboBox cbAspect;
    }
}