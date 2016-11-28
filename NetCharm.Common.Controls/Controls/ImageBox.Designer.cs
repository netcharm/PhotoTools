namespace NetCharm.Common.Controls
{
    partial class ImageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageBox));
            this.Viewer = new Cyotek.Windows.Forms.ImageBox();
            this.SuspendLayout();
            // 
            // Viewer
            // 
            resources.ApplyResources(this.Viewer, "Viewer");
            this.Viewer.Name = "Viewer";
            this.Viewer.ShowPixelGrid = true;
            this.Viewer.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
            this.Viewer.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            this.Viewer.SelectionRegionChanged += new System.EventHandler(this.Viewer_SelectionRegionChanged);
            this.Viewer.DoubleClick += new System.EventHandler(this.Viewer_DoubleClick);
            this.Viewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Viewer_MouseDown);
            this.Viewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Viewer_MouseMove);
            this.Viewer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Viewer_MouseUp);
            // 
            // ImageBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Viewer);
            this.Name = "ImageBox";
            this.ResumeLayout(false);

        }

        #endregion

        private Cyotek.Windows.Forms.ImageBox Viewer;
    }
}
