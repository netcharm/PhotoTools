namespace BatchProcess
{
    partial class BatchProcessForm
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
            this.lvFiles = new System.Windows.Forms.ListView();
            this.lvColFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsFileList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSep00 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSep01 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiProcessSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProcessAll = new System.Windows.Forms.ToolStripMenuItem();
            this.imgPreview = new Cyotek.Windows.Forms.ImageBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cmsFileList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFiles
            // 
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvColFile,
            this.lvColPath});
            this.lvFiles.ContextMenuStrip = this.cmsFileList;
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(0, 0);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(240, 564);
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvFiles_ItemSelectionChanged);
            // 
            // lvColFile
            // 
            this.lvColFile.Text = "File";
            this.lvColFile.Width = 120;
            // 
            // lvColPath
            // 
            this.lvColPath.Text = "Full Path";
            this.lvColPath.Width = 360;
            // 
            // cmsFileList
            // 
            this.cmsFileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddImage,
            this.tsmiRemoveImage,
            this.tsmiSep00,
            this.tsmiClear,
            this.tsmiSep01,
            this.tsmiProcessSelected,
            this.tsmiProcessAll});
            this.cmsFileList.Name = "cmsFileList";
            this.cmsFileList.Size = new System.Drawing.Size(167, 126);
            // 
            // tsmiAddImage
            // 
            this.tsmiAddImage.Name = "tsmiAddImage";
            this.tsmiAddImage.Size = new System.Drawing.Size(166, 22);
            this.tsmiAddImage.Text = "Add Image";
            this.tsmiAddImage.Click += new System.EventHandler(this.tsmiAddImage_Click);
            // 
            // tsmiRemoveImage
            // 
            this.tsmiRemoveImage.Name = "tsmiRemoveImage";
            this.tsmiRemoveImage.Size = new System.Drawing.Size(166, 22);
            this.tsmiRemoveImage.Text = "Remove Image";
            this.tsmiRemoveImage.Click += new System.EventHandler(this.tsmiRemoveImage_Click);
            // 
            // tsmiSep00
            // 
            this.tsmiSep00.Name = "tsmiSep00";
            this.tsmiSep00.Size = new System.Drawing.Size(163, 6);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(166, 22);
            this.tsmiClear.Text = "Clear";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // tsmiSep01
            // 
            this.tsmiSep01.Name = "tsmiSep01";
            this.tsmiSep01.Size = new System.Drawing.Size(163, 6);
            // 
            // tsmiProcessSelected
            // 
            this.tsmiProcessSelected.Name = "tsmiProcessSelected";
            this.tsmiProcessSelected.Size = new System.Drawing.Size(166, 22);
            this.tsmiProcessSelected.Text = "Process Selected";
            this.tsmiProcessSelected.Click += new System.EventHandler(this.tsmiProcessSelected_Click);
            // 
            // tsmiProcessAll
            // 
            this.tsmiProcessAll.Name = "tsmiProcessAll";
            this.tsmiProcessAll.Size = new System.Drawing.Size(166, 22);
            this.tsmiProcessAll.Text = "Process All";
            this.tsmiProcessAll.Click += new System.EventHandler(this.tsmiProcessAll_Click);
            // 
            // imgPreview
            // 
            this.imgPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgPreview.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.imgPreview.Location = new System.Drawing.Point(243, 242);
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.ShowPixelGrid = true;
            this.imgPreview.Size = new System.Drawing.Size(472, 318);
            this.imgPreview.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
            this.imgPreview.TabIndex = 1;
            this.imgPreview.Text = "Preview";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(240, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 236);
            this.panel1.TabIndex = 2;
            // 
            // dlgOpen
            // 
            this.dlgOpen.DefaultExt = "jpg";
            this.dlgOpen.Filter = "Image File(*.jpg;*.bmp;*.png;*.tif;*.gif)|*.jpg;*.bmp;*.png;*.tif;*.gif|All File(" +
    "*.*)|*.*";
            this.dlgOpen.Multiselect = true;
            this.dlgOpen.ShowHelp = true;
            this.dlgOpen.SupportMultiDottedExtensions = true;
            this.dlgOpen.Title = "Add Image(s)";
            // 
            // BatchProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 564);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imgPreview);
            this.Controls.Add(this.lvFiles);
            this.Name = "BatchProcessForm";
            this.ShowInTaskbar = false;
            this.Text = "Batch Image Processing";
            this.Load += new System.EventHandler(this.BatchProcessForm_Load);
            this.cmsFileList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader lvColFile;
        private System.Windows.Forms.ColumnHeader lvColPath;
        private Cyotek.Windows.Forms.ImageBox imgPreview;
        private System.Windows.Forms.ContextMenuStrip cmsFileList;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveImage;
        private System.Windows.Forms.ToolStripSeparator tsmiSep00;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private System.Windows.Forms.ToolStripSeparator tsmiSep01;
        private System.Windows.Forms.ToolStripMenuItem tsmiProcessSelected;
        private System.Windows.Forms.ToolStripMenuItem tsmiProcessAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.ToolTip toolTip;
    }
}