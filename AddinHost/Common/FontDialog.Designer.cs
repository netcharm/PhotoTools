namespace NetCharm.Common
{
    partial class FontDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontDialog));
            this.layoutFont = new System.Windows.Forms.TableLayoutPanel();
            this.edFamily = new System.Windows.Forms.TextBox();
            this.lvFamily = new System.Windows.Forms.ListView();
            this.edStyle = new System.Windows.Forms.TextBox();
            this.lvStyle = new System.Windows.Forms.ListView();
            this.edSize = new System.Windows.Forms.TextBox();
            this.lbSize = new System.Windows.Forms.ListBox();
            this.grpEffect = new System.Windows.Forms.GroupBox();
            this.colorGrid = new Cyotek.Windows.Forms.ColorGrid();
            this.chkEffectUnderline = new System.Windows.Forms.CheckBox();
            this.chkEffectStrikeout = new System.Windows.Forms.CheckBox();
            this.grpSample = new System.Windows.Forms.GroupBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.cbCharset = new System.Windows.Forms.ComboBox();
            this.lblFamily = new System.Windows.Forms.Label();
            this.lblFace = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.layoutFont.SuspendLayout();
            this.grpEffect.SuspendLayout();
            this.grpSample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutFont
            // 
            resources.ApplyResources(this.layoutFont, "layoutFont");
            this.layoutFont.Controls.Add(this.edFamily, 0, 1);
            this.layoutFont.Controls.Add(this.lvFamily, 0, 2);
            this.layoutFont.Controls.Add(this.edStyle, 1, 1);
            this.layoutFont.Controls.Add(this.lvStyle, 1, 2);
            this.layoutFont.Controls.Add(this.edSize, 2, 1);
            this.layoutFont.Controls.Add(this.lbSize, 2, 2);
            this.layoutFont.Controls.Add(this.grpEffect, 0, 3);
            this.layoutFont.Controls.Add(this.grpSample, 1, 3);
            this.layoutFont.Controls.Add(this.cbCharset, 1, 4);
            this.layoutFont.Controls.Add(this.lblFamily, 0, 0);
            this.layoutFont.Controls.Add(this.lblFace, 1, 0);
            this.layoutFont.Controls.Add(this.lblSize, 2, 0);
            this.layoutFont.Name = "layoutFont";
            // 
            // edFamily
            // 
            resources.ApplyResources(this.edFamily, "edFamily");
            this.edFamily.Name = "edFamily";
            this.edFamily.TextChanged += new System.EventHandler(this.edFamily_TextChanged);
            // 
            // lvFamily
            // 
            resources.ApplyResources(this.lvFamily, "lvFamily");
            this.lvFamily.FullRowSelect = true;
            this.lvFamily.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvFamily.HideSelection = false;
            this.lvFamily.MultiSelect = false;
            this.lvFamily.Name = "lvFamily";
            this.lvFamily.OwnerDraw = true;
            this.lvFamily.ShowItemToolTips = true;
            this.lvFamily.UseCompatibleStateImageBehavior = false;
            this.lvFamily.View = System.Windows.Forms.View.Details;
            this.lvFamily.VirtualMode = true;
            this.lvFamily.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvFamily_DrawItem);
            this.lvFamily.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvFamily_RetrieveVirtualItem);
            this.lvFamily.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.lvFamily_SearchForVirtualItem);
            this.lvFamily.SelectedIndexChanged += new System.EventHandler(this.lvFamily_SelectedIndexChanged);
            this.lvFamily.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvFamily_KeyPress);
            // 
            // edStyle
            // 
            resources.ApplyResources(this.edStyle, "edStyle");
            this.edStyle.Name = "edStyle";
            this.edStyle.ReadOnly = true;
            // 
            // lvStyle
            // 
            resources.ApplyResources(this.lvStyle, "lvStyle");
            this.lvStyle.FullRowSelect = true;
            this.lvStyle.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvStyle.HideSelection = false;
            this.lvStyle.MultiSelect = false;
            this.lvStyle.Name = "lvStyle";
            this.lvStyle.OwnerDraw = true;
            this.lvStyle.ShowItemToolTips = true;
            this.lvStyle.UseCompatibleStateImageBehavior = false;
            this.lvStyle.View = System.Windows.Forms.View.Details;
            this.lvStyle.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvStyle_DrawItem);
            this.lvStyle.SelectedIndexChanged += new System.EventHandler(this.lvStyle_SelectedIndexChanged);
            // 
            // edSize
            // 
            resources.ApplyResources(this.edSize, "edSize");
            this.edSize.Name = "edSize";
            this.edSize.TextChanged += new System.EventHandler(this.edSize_TextChanged);
            // 
            // lbSize
            // 
            resources.ApplyResources(this.lbSize, "lbSize");
            this.lbSize.FormattingEnabled = true;
            this.lbSize.Name = "lbSize";
            this.lbSize.SelectedIndexChanged += new System.EventHandler(this.lbSize_SelectedIndexChanged);
            // 
            // grpEffect
            // 
            this.grpEffect.Controls.Add(this.colorGrid);
            this.grpEffect.Controls.Add(this.chkEffectUnderline);
            this.grpEffect.Controls.Add(this.chkEffectStrikeout);
            resources.ApplyResources(this.grpEffect, "grpEffect");
            this.grpEffect.Name = "grpEffect";
            this.layoutFont.SetRowSpan(this.grpEffect, 2);
            this.grpEffect.TabStop = false;
            // 
            // colorGrid
            // 
            resources.ApplyResources(this.colorGrid, "colorGrid");
            this.colorGrid.Columns = 12;
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.Palette = Cyotek.Windows.Forms.ColorPalette.Paint;
            this.colorGrid.ColorChanged += new System.EventHandler(this.colorGrid_ColorChanged);
            // 
            // chkEffectUnderline
            // 
            resources.ApplyResources(this.chkEffectUnderline, "chkEffectUnderline");
            this.chkEffectUnderline.Name = "chkEffectUnderline";
            this.chkEffectUnderline.UseVisualStyleBackColor = true;
            this.chkEffectUnderline.CheckedChanged += new System.EventHandler(this.chkEffect_CheckedChanged);
            // 
            // chkEffectStrikeout
            // 
            resources.ApplyResources(this.chkEffectStrikeout, "chkEffectStrikeout");
            this.chkEffectStrikeout.Name = "chkEffectStrikeout";
            this.chkEffectStrikeout.UseVisualStyleBackColor = true;
            this.chkEffectStrikeout.CheckedChanged += new System.EventHandler(this.chkEffect_CheckedChanged);
            // 
            // grpSample
            // 
            this.layoutFont.SetColumnSpan(this.grpSample, 2);
            this.grpSample.Controls.Add(this.picPreview);
            resources.ApplyResources(this.grpSample, "grpSample");
            this.grpSample.Name = "grpSample";
            this.grpSample.TabStop = false;
            // 
            // picPreview
            // 
            resources.ApplyResources(this.picPreview, "picPreview");
            this.picPreview.Name = "picPreview";
            this.picPreview.TabStop = false;
            // 
            // cbCharset
            // 
            this.layoutFont.SetColumnSpan(this.cbCharset, 2);
            resources.ApplyResources(this.cbCharset, "cbCharset");
            this.cbCharset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCharset.FormattingEnabled = true;
            this.cbCharset.Name = "cbCharset";
            // 
            // lblFamily
            // 
            this.lblFamily.AutoEllipsis = true;
            resources.ApplyResources(this.lblFamily, "lblFamily");
            this.lblFamily.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblFamily.Name = "lblFamily";
            // 
            // lblFace
            // 
            this.lblFace.AutoEllipsis = true;
            resources.ApplyResources(this.lblFace, "lblFace");
            this.lblFace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblFace.Name = "lblFace";
            // 
            // lblSize
            // 
            this.lblSize.AutoEllipsis = true;
            resources.ApplyResources(this.lblSize, "lblSize");
            this.lblSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSize.Name = "lblSize";
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
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // FontDialog
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.layoutFont);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FontDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FontDialog_Load);
            this.layoutFont.ResumeLayout(false);
            this.layoutFont.PerformLayout();
            this.grpEffect.ResumeLayout(false);
            this.grpSample.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutFont;
        private System.Windows.Forms.TextBox edFamily;
        private System.Windows.Forms.ListView lvFamily;
        private System.Windows.Forms.TextBox edStyle;
        private System.Windows.Forms.ListView lvStyle;
        private System.Windows.Forms.TextBox edSize;
        private System.Windows.Forms.ListBox lbSize;
        private System.Windows.Forms.GroupBox grpEffect;
        private System.Windows.Forms.GroupBox grpSample;
        private System.Windows.Forms.ComboBox cbCharset;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox chkEffectUnderline;
        private System.Windows.Forms.CheckBox chkEffectStrikeout;
        private Cyotek.Windows.Forms.ColorGrid colorGrid;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label lblFamily;
        private System.Windows.Forms.Label lblFace;
        private System.Windows.Forms.Label lblSize;
    }
}