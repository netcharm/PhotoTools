namespace AutoMask
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.btnOrig = new System.Windows.Forms.Button();
            this.btnMask = new System.Windows.Forms.Button();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.picMask = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.numFaceSize = new System.Windows.Forms.NumericUpDown();
            this.chkRemoveEXIF = new System.Windows.Forms.CheckBox();
            this.bgwMask = new System.ComponentModel.BackgroundWorker();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.cbScaling = new System.Windows.Forms.ComboBox();
            this.numOutSize = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.cmFileList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiFileListClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileListAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileListRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileListMaskSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileListMaskAll = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaceSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOutSize)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.cmFileList.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPreview.Location = new System.Drawing.Point(273, 70);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(519, 478);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            // 
            // lvFiles
            // 
            this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colPath});
            this.lvFiles.ContextMenuStrip = this.cmFileList;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(0, 70);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.ShowItemToolTips = true;
            this.lvFiles.Size = new System.Drawing.Size(267, 478);
            this.lvFiles.TabIndex = 3;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvFiles_ItemSelectionChanged);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.numOutSize);
            this.pnlTools.Controls.Add(this.cbScaling);
            this.pnlTools.Controls.Add(this.cbMode);
            this.pnlTools.Controls.Add(this.chkRemoveEXIF);
            this.pnlTools.Controls.Add(this.numFaceSize);
            this.pnlTools.Controls.Add(this.picMask);
            this.pnlTools.Controls.Add(this.btnOrig);
            this.pnlTools.Controls.Add(this.btnMask);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(792, 64);
            this.pnlTools.TabIndex = 4;
            // 
            // btnOrig
            // 
            this.btnOrig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrig.Location = new System.Drawing.Point(631, 7);
            this.btnOrig.Name = "btnOrig";
            this.btnOrig.Size = new System.Drawing.Size(75, 23);
            this.btnOrig.TabIndex = 4;
            this.btnOrig.Text = "Original";
            this.btnOrig.UseVisualStyleBackColor = true;
            this.btnOrig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnOrig_MouseDown);
            this.btnOrig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnOrig_MouseUp);
            // 
            // btnMask
            // 
            this.btnMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMask.Location = new System.Drawing.Point(712, 4);
            this.btnMask.Name = "btnMask";
            this.btnMask.Size = new System.Drawing.Size(75, 56);
            this.btnMask.TabIndex = 3;
            this.btnMask.Text = "MASK !!";
            this.btnMask.UseVisualStyleBackColor = true;
            this.btnMask.Click += new System.EventHandler(this.btnMask_Click);
            // 
            // colName
            // 
            this.colName.Text = "File Name";
            this.colName.Width = 160;
            // 
            // colPath
            // 
            this.colPath.Text = "File Path";
            this.colPath.Width = 320;
            // 
            // picMask
            // 
            this.picMask.Location = new System.Drawing.Point(4, 4);
            this.picMask.Name = "picMask";
            this.picMask.Size = new System.Drawing.Size(56, 56);
            this.picMask.TabIndex = 5;
            this.picMask.TabStop = false;
            this.toolTip.SetToolTip(this.picMask, "Double Click to change the mask image");
            this.picMask.DoubleClick += new System.EventHandler(this.picMask_DoubleClick);
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Note";
            // 
            // numFaceSize
            // 
            this.numFaceSize.Location = new System.Drawing.Point(81, 4);
            this.numFaceSize.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numFaceSize.Name = "numFaceSize";
            this.numFaceSize.Size = new System.Drawing.Size(79, 21);
            this.numFaceSize.TabIndex = 6;
            this.numFaceSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip.SetToolTip(this.numFaceSize, "Minimized Face Size");
            this.numFaceSize.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numFaceSize.ValueChanged += new System.EventHandler(this.numFaceSize_ValueChanged);
            // 
            // chkRemoveEXIF
            // 
            this.chkRemoveEXIF.AutoSize = true;
            this.chkRemoveEXIF.Location = new System.Drawing.Point(279, 6);
            this.chkRemoveEXIF.Name = "chkRemoveEXIF";
            this.chkRemoveEXIF.Size = new System.Drawing.Size(90, 16);
            this.chkRemoveEXIF.TabIndex = 7;
            this.chkRemoveEXIF.Text = "Remove EXIF";
            this.chkRemoveEXIF.UseVisualStyleBackColor = true;
            // 
            // bgwMask
            // 
            this.bgwMask.WorkerReportsProgress = true;
            this.bgwMask.WorkerSupportsCancellation = true;
            this.bgwMask.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMask_DoWork);
            this.bgwMask.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwMask_ProgressChanged);
            this.bgwMask.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwMask_RunWorkerCompleted);
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Location = new System.Drawing.Point(80, 37);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(80, 20);
            this.cbMode.TabIndex = 9;
            this.cbMode.SelectionChangeCommitted += new System.EventHandler(this.cbMode_SelectionChangeCommitted);
            // 
            // cbScaling
            // 
            this.cbScaling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScaling.FormattingEnabled = true;
            this.cbScaling.Location = new System.Drawing.Point(166, 37);
            this.cbScaling.Name = "cbScaling";
            this.cbScaling.Size = new System.Drawing.Size(101, 20);
            this.cbScaling.TabIndex = 10;
            this.cbScaling.SelectionChangeCommitted += new System.EventHandler(this.cbScaling_SelectionChangeCommitted);
            // 
            // numOutSize
            // 
            this.numOutSize.Location = new System.Drawing.Point(279, 36);
            this.numOutSize.Maximum = new decimal(new int[] {
            2400,
            0,
            0,
            0});
            this.numOutSize.Minimum = new decimal(new int[] {
            320,
            0,
            0,
            0});
            this.numOutSize.Name = "numOutSize";
            this.numOutSize.Size = new System.Drawing.Size(90, 21);
            this.numOutSize.TabIndex = 11;
            this.numOutSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip.SetToolTip(this.numOutSize, "Output image large side size");
            this.numOutSize.Value = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numOutSize.ValueChanged += new System.EventHandler(this.numOutSize_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsInfo,
            this.tsProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsInfo
            // 
            this.tsInfo.Name = "tsInfo";
            this.tsInfo.Size = new System.Drawing.Size(675, 17);
            this.tsInfo.Spring = true;
            this.tsInfo.Text = "OK";
            this.tsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsProgress
            // 
            this.tsProgress.Name = "tsProgress";
            this.tsProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // cmFileList
            // 
            this.cmFileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileListAdd,
            this.toolStripMenuItem1,
            this.tsmiFileListRemove,
            this.tsmiFileListClear,
            this.toolStripMenuItem2,
            this.tsmiFileListMaskSelected,
            this.tsmiFileListMaskAll});
            this.cmFileList.Name = "cmFileList";
            this.cmFileList.Size = new System.Drawing.Size(153, 148);
            // 
            // tsmiFileListClear
            // 
            this.tsmiFileListClear.Name = "tsmiFileListClear";
            this.tsmiFileListClear.Size = new System.Drawing.Size(152, 22);
            this.tsmiFileListClear.Text = "Clear";
            this.tsmiFileListClear.Click += new System.EventHandler(this.tsmiFileListClear_Click);
            // 
            // tsmiFileListAdd
            // 
            this.tsmiFileListAdd.Name = "tsmiFileListAdd";
            this.tsmiFileListAdd.Size = new System.Drawing.Size(152, 22);
            this.tsmiFileListAdd.Text = "Add";
            this.tsmiFileListAdd.Click += new System.EventHandler(this.tsmiFileListAdd_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiFileListRemove
            // 
            this.tsmiFileListRemove.Name = "tsmiFileListRemove";
            this.tsmiFileListRemove.Size = new System.Drawing.Size(152, 22);
            this.tsmiFileListRemove.Text = "Remove";
            this.tsmiFileListRemove.Click += new System.EventHandler(this.tsmiFileListRemove_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiFileListMaskSelected
            // 
            this.tsmiFileListMaskSelected.Name = "tsmiFileListMaskSelected";
            this.tsmiFileListMaskSelected.Size = new System.Drawing.Size(152, 22);
            this.tsmiFileListMaskSelected.Text = "Mask Selected";
            this.tsmiFileListMaskSelected.Click += new System.EventHandler(this.tsmiFileListMaskSelected_Click);
            // 
            // tsmiFileListMaskAll
            // 
            this.tsmiFileListMaskAll.Name = "tsmiFileListMaskAll";
            this.tsmiFileListMaskAll.Size = new System.Drawing.Size(152, 22);
            this.tsmiFileListMaskAll.Text = "Mask All";
            this.tsmiFileListMaskAll.Click += new System.EventHandler(this.tsmiFileListMaskAll_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlTools);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.picPreview);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Auto Mask Face(s)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaceSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOutSize)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.cmFileList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.Button btnOrig;
        private System.Windows.Forms.Button btnMask;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.PictureBox picMask;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NumericUpDown numFaceSize;
        private System.Windows.Forms.CheckBox chkRemoveEXIF;
        private System.ComponentModel.BackgroundWorker bgwMask;
        private System.Windows.Forms.ComboBox cbScaling;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.NumericUpDown numOutSize;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsInfo;
        private System.Windows.Forms.ToolStripProgressBar tsProgress;
        private System.Windows.Forms.ContextMenuStrip cmFileList;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileListAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileListRemove;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileListClear;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileListMaskSelected;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileListMaskAll;
    }
}

