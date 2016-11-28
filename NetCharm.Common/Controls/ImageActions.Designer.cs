namespace NetCharm.Common.Controls
{
    partial class ImageActions
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmsZoomLevel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsAction = new System.Windows.Forms.ToolStrip();
            this.tsbtnOriginal = new System.Windows.Forms.ToolStripButton();
            this.tss0 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tscbZoomLevels = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsZoomLevel
            // 
            this.cmsZoomLevel.Name = "cmsZoomLevel";
            this.cmsZoomLevel.Size = new System.Drawing.Size(61, 4);
            this.cmsZoomLevel.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsZoomLevel_ItemClicked);
            // 
            // tsAction
            // 
            this.tsAction.AllowMerge = false;
            this.tsAction.BackColor = System.Drawing.Color.Transparent;
            this.tsAction.CanOverflow = false;
            this.tsAction.ContextMenuStrip = this.cmsZoomLevel;
            this.tsAction.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOriginal,
            this.tss0,
            this.tsbtnZoomOut,
            this.tscbZoomLevels,
            this.tsbtnZoomIn});
            this.tsAction.Location = new System.Drawing.Point(0, 0);
            this.tsAction.Name = "tsAction";
            this.tsAction.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsAction.Size = new System.Drawing.Size(138, 25);
            this.tsAction.TabIndex = 38;
            // 
            // tsbtnOriginal
            // 
            this.tsbtnOriginal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOriginal.Image = global::NetCharm.Common.Properties.Resources.Compare_16x;
            this.tsbtnOriginal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOriginal.Name = "tsbtnOriginal";
            this.tsbtnOriginal.Size = new System.Drawing.Size(23, 22);
            this.tsbtnOriginal.Text = "View Original";
            this.tsbtnOriginal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbtnOriginal_MouseDown);
            this.tsbtnOriginal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tsbtnOriginal_MouseUp);
            // 
            // tss0
            // 
            this.tss0.Name = "tss0";
            this.tss0.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnZoomOut
            // 
            this.tsbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomOut.Image = global::NetCharm.Common.Properties.Resources.ZoomOut_16x;
            this.tsbtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomOut.Name = "tsbtnZoomOut";
            this.tsbtnZoomOut.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsbtnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbtnZoomOut.Text = "Zoom Out";
            this.tsbtnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // tscbZoomLevels
            // 
            this.tscbZoomLevels.AutoSize = false;
            this.tscbZoomLevels.AutoToolTip = true;
            this.tscbZoomLevels.Name = "tscbZoomLevels";
            this.tscbZoomLevels.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tscbZoomLevels.Size = new System.Drawing.Size(56, 20);
            this.tscbZoomLevels.ToolTipText = "Zoom Levels";
            this.tscbZoomLevels.SelectedIndexChanged += new System.EventHandler(this.tscbZoomLevels_SelectedIndexChanged);
            this.tscbZoomLevels.TextChanged += new System.EventHandler(this.tscbZoomLevels_TextChanged);
            // 
            // tsbtnZoomIn
            // 
            this.tsbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomIn.Image = global::NetCharm.Common.Properties.Resources.ZoomIn_16x;
            this.tsbtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomIn.Name = "tsbtnZoomIn";
            this.tsbtnZoomIn.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsbtnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsbtnZoomIn.Text = "Zoom In";
            this.tsbtnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // ImageActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tsAction);
            this.MaximumSize = new System.Drawing.Size(156, 24);
            this.MinimumSize = new System.Drawing.Size(138, 24);
            this.Name = "ImageActions";
            this.Size = new System.Drawing.Size(138, 24);
            this.Load += new System.EventHandler(this.ImageActions_Load);
            this.tsAction.ResumeLayout(false);
            this.tsAction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsZoomLevel;
        private System.Windows.Forms.ToolStrip tsAction;
        private System.Windows.Forms.ToolStripButton tsbtnOriginal;
        private System.Windows.Forms.ToolStripSeparator tss0;
        private System.Windows.Forms.ToolStripButton tsbtnZoomOut;
        private System.Windows.Forms.ToolStripComboBox tscbZoomLevels;
        private System.Windows.Forms.ToolStripButton tsbtnZoomIn;
    }
}
