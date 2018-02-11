namespace NetCharm.Image.Addins.Controls
{
    partial class PreviewBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewBox));
            Cyotek.Windows.Forms.ZoomLevelCollection zoomLevelCollection1 = new Cyotek.Windows.Forms.ZoomLevelCollection();
            this.Preview = new Cyotek.Windows.Forms.ImageBox();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.imageActions = new NetCharm.Common.Controls.ImageActions();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // Preview
            // 
            this.Preview.AllowClickZoom = true;
            this.Preview.AllowDoubleClick = true;
            this.Preview.AllowDrop = true;
            this.Preview.AllowUnfocusedMouseWheel = true;
            this.Preview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.layout.SetColumnSpan(this.Preview, 3);
            resources.ApplyResources(this.Preview, "Preview");
            this.Preview.Name = "Preview";
            this.Preview.ShowPixelGrid = true;
            // 
            // layout
            // 
            resources.ApplyResources(this.layout, "layout");
            this.layout.Controls.Add(this.Preview, 0, 0);
            this.layout.Controls.Add(this.imageActions, 1, 1);
            this.layout.Name = "layout";
            // 
            // imageActions
            // 
            this.imageActions.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.imageActions, "imageActions");
            this.imageActions.ImageBox = null;
            this.imageActions.Name = "imageActions";
            this.imageActions.Source = null;
            this.imageActions.Zoom = 100;
            this.imageActions.ZoomLevels = zoomLevelCollection1;
            // 
            // PreviewBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layout);
            this.MinimumSize = new System.Drawing.Size(150, 175);
            this.Name = "PreviewBox";
            this.layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public Cyotek.Windows.Forms.ImageBox Preview;
        private System.Windows.Forms.TableLayoutPanel layout;
        private NetCharm.Common.Controls.ImageActions imageActions;
    }
}
