namespace ImageColors
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Cyotek.Windows.Forms.ZoomLevelCollection zoomLevelCollection1 = new Cyotek.Windows.Forms.ZoomLevelCollection();
            this.colorGrid = new Cyotek.Windows.Forms.ColorGrid();
            this.colorManager = new Cyotek.Windows.Forms.ColorEditorManager();
            this.pnl = new System.Windows.Forms.Panel();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.colorAmount = new NetCharm.Common.Controls.SlideNumber();
            this.imageActions = new NetCharm.Common.Controls.ImageActions();
            this.btnLoad = new System.Windows.Forms.Button();
            this.imageBox = new Cyotek.Windows.Forms.ImageBox();
            this.colorPicker = new Cyotek.Windows.Forms.ScreenColorPicker();
            this.cmSave = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmSaveToCSS = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSaveToPal = new System.Windows.Forms.ToolStripMenuItem();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.pnl.SuspendLayout();
            this.pnlTools.SuspendLayout();
            this.cmSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorGrid
            // 
            this.colorGrid.AutoAddColors = false;
            this.colorGrid.CellSize = new System.Drawing.Size(20, 20);
            this.colorGrid.Columns = 20;
            this.colorGrid.ContextMenuStrip = this.cmSave;
            this.colorGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.colorGrid.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            this.colorGrid.Location = new System.Drawing.Point(450, 0);
            this.colorGrid.Name = "colorGrid";
            this.colorGrid.Palette = Cyotek.Windows.Forms.ColorPalette.None;
            this.colorGrid.ShowCustomColors = false;
            this.colorGrid.Size = new System.Drawing.Size(467, 30);
            this.colorGrid.TabIndex = 1;
            // 
            // colorManager
            // 
            this.colorManager.ColorGrid = this.colorGrid;
            this.colorManager.ScreenColorPicker = this.colorPicker;
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.imageBox);
            this.pnl.Controls.Add(this.pnlTools);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.MinimumSize = new System.Drawing.Size(370, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(450, 529);
            this.pnl.TabIndex = 6;
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.pbar);
            this.pnlTools.Controls.Add(this.colorPicker);
            this.pnlTools.Controls.Add(this.colorAmount);
            this.pnlTools.Controls.Add(this.imageActions);
            this.pnlTools.Controls.Add(this.btnLoad);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTools.Location = new System.Drawing.Point(0, 462);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(450, 67);
            this.pnlTools.TabIndex = 0;
            // 
            // colorAmount
            // 
            this.colorAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorAmount.Caption = "Amount";
            this.colorAmount.DecimalPlaces = 0;
            this.colorAmount.Location = new System.Drawing.Point(159, 6);
            this.colorAmount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.colorAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.colorAmount.Name = "colorAmount";
            this.colorAmount.Size = new System.Drawing.Size(142, 54);
            this.colorAmount.Step = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.colorAmount.TabIndex = 8;
            this.colorAmount.Unit = "";
            this.colorAmount.Value = new decimal(new int[] {
            1553873815,
            40745,
            0,
            720896});
            this.colorAmount.ValueChanged += new System.EventHandler(this.colorAmount_ValueChanged);
            // 
            // imageActions
            // 
            this.imageActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageActions.BackColor = System.Drawing.SystemColors.Control;
            this.imageActions.Location = new System.Drawing.Point(307, 6);
            this.imageActions.MaximumSize = new System.Drawing.Size(156, 24);
            this.imageActions.MinimumSize = new System.Drawing.Size(138, 24);
            this.imageActions.Name = "imageActions";
            this.imageActions.Size = new System.Drawing.Size(138, 24);
            this.imageActions.Source = null;
            this.imageActions.TabIndex = 7;
            this.imageActions.Zoom = 100;
            this.imageActions.ZoomLevels = zoomLevelCollection1;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(3, 32);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox.Location = new System.Drawing.Point(3, 7);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(444, 449);
            this.imageBox.TabIndex = 4;
            // 
            // colorPicker
            // 
            this.colorPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorPicker.Color = System.Drawing.Color.Empty;
            this.colorPicker.Location = new System.Drawing.Point(307, 36);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Size = new System.Drawing.Size(138, 28);
            this.colorPicker.Text = "Color Picker";
            this.colorPicker.ColorChanged += new System.EventHandler(this.colorPicker_ColorChanged);
            // 
            // cmSave
            // 
            this.cmSave.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmSaveToCSS,
            this.cmSaveToPal});
            this.cmSave.Name = "cmSave";
            this.cmSave.Size = new System.Drawing.Size(165, 48);
            // 
            // cmSaveToCSS
            // 
            this.cmSaveToCSS.Name = "cmSaveToCSS";
            this.cmSaveToCSS.Size = new System.Drawing.Size(164, 22);
            this.cmSaveToCSS.Text = "Save As CSS";
            this.cmSaveToCSS.Click += new System.EventHandler(this.cmSaveToCSS_Click);
            // 
            // cmSaveToPal
            // 
            this.cmSaveToPal.Name = "cmSaveToPal";
            this.cmSaveToPal.Size = new System.Drawing.Size(164, 22);
            this.cmSaveToPal.Text = "Save As Palette";
            this.cmSaveToPal.Click += new System.EventHandler(this.cmSaveToPal_Click);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(4, 16);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(74, 10);
            this.pbar.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 529);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.colorGrid);
            this.MinimumSize = new System.Drawing.Size(900, 0);
            this.Name = "MainForm";
            this.Text = "Image Colors";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnl.ResumeLayout(false);
            this.pnlTools.ResumeLayout(false);
            this.cmSave.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Cyotek.Windows.Forms.ColorGrid colorGrid;
        private Cyotek.Windows.Forms.ColorEditorManager colorManager;
        private System.Windows.Forms.Panel pnl;
        private Cyotek.Windows.Forms.ImageBox imageBox;
        private System.Windows.Forms.Panel pnlTools;
        private NetCharm.Common.Controls.SlideNumber colorAmount;
        private NetCharm.Common.Controls.ImageActions imageActions;
        private System.Windows.Forms.Button btnLoad;
        private Cyotek.Windows.Forms.ScreenColorPicker colorPicker;
        private System.Windows.Forms.ContextMenuStrip cmSave;
        private System.Windows.Forms.ToolStripMenuItem cmSaveToCSS;
        private System.Windows.Forms.ToolStripMenuItem cmSaveToPal;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar pbar;
    }
}

