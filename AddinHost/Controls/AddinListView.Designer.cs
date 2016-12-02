namespace NetCharm.Image.Addins.Controls
{
    partial class AddinListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddinListView));
            this.btnEffectDown = new System.Windows.Forms.Button();
            this.btnEffectUp = new System.Windows.Forms.Button();
            this.btnEffectRemove = new System.Windows.Forms.Button();
            this.lvFilters = new System.Windows.Forms.ListView();
            this.flowEffects = new System.Windows.Forms.FlowLayoutPanel();
            this.flowEffectTool = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEffectAdd = new System.Windows.Forms.Button();
            this.ilSmall = new System.Windows.Forms.ImageList(this.components);
            this.ilLarge = new System.Windows.Forms.ImageList(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.flowEffects.SuspendLayout();
            this.flowEffectTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEffectDown
            // 
            resources.ApplyResources(this.btnEffectDown, "btnEffectDown");
            this.btnEffectDown.Image = global::NetCharm.Image.Addins.Properties.Resources.effect_down_24x;
            this.btnEffectDown.Name = "btnEffectDown";
            this.btnEffectDown.UseVisualStyleBackColor = true;
            this.btnEffectDown.Click += new System.EventHandler(this.btnEffectDown_Click);
            // 
            // btnEffectUp
            // 
            resources.ApplyResources(this.btnEffectUp, "btnEffectUp");
            this.btnEffectUp.Image = global::NetCharm.Image.Addins.Properties.Resources.effect_up_24x;
            this.btnEffectUp.Name = "btnEffectUp";
            this.btnEffectUp.UseVisualStyleBackColor = true;
            this.btnEffectUp.Click += new System.EventHandler(this.btnEffectUp_Click);
            // 
            // btnEffectRemove
            // 
            resources.ApplyResources(this.btnEffectRemove, "btnEffectRemove");
            this.btnEffectRemove.Image = global::NetCharm.Image.Addins.Properties.Resources.effect_remove_24x;
            this.btnEffectRemove.Name = "btnEffectRemove";
            this.btnEffectRemove.UseVisualStyleBackColor = true;
            this.btnEffectRemove.Click += new System.EventHandler(this.btnEffectRemove_Click);
            // 
            // lvFilters
            // 
            resources.ApplyResources(this.lvFilters, "lvFilters");
            this.lvFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvFilters.CheckBoxes = true;
            this.lvFilters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvFilters.HideSelection = false;
            this.lvFilters.Name = "lvFilters";
            this.lvFilters.ShowItemToolTips = true;
            this.lvFilters.UseCompatibleStateImageBehavior = false;
            this.lvFilters.View = System.Windows.Forms.View.List;
            this.lvFilters.VirtualMode = true;
            this.lvFilters.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvFilters_RetrieveVirtualItem);
            this.lvFilters.DoubleClick += new System.EventHandler(this.lvFilters_DoubleClick);
            this.lvFilters.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvFilters_KeyPress);
            this.lvFilters.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvFilters_MouseClick);
            // 
            // flowEffects
            // 
            resources.ApplyResources(this.flowEffects, "flowEffects");
            this.flowEffects.Controls.Add(this.lvFilters);
            this.flowEffects.Controls.Add(this.flowEffectTool);
            this.flowEffects.Name = "flowEffects";
            // 
            // flowEffectTool
            // 
            this.flowEffectTool.Controls.Add(this.btnEffectAdd);
            this.flowEffectTool.Controls.Add(this.btnEffectRemove);
            this.flowEffectTool.Controls.Add(this.btnEffectUp);
            this.flowEffectTool.Controls.Add(this.btnEffectDown);
            resources.ApplyResources(this.flowEffectTool, "flowEffectTool");
            this.flowEffectTool.Name = "flowEffectTool";
            // 
            // btnEffectAdd
            // 
            resources.ApplyResources(this.btnEffectAdd, "btnEffectAdd");
            this.btnEffectAdd.Image = global::NetCharm.Image.Addins.Properties.Resources.effect_add_24x;
            this.btnEffectAdd.Name = "btnEffectAdd";
            this.btnEffectAdd.UseVisualStyleBackColor = true;
            this.btnEffectAdd.Click += new System.EventHandler(this.btnEffectAdd_Click);
            // 
            // ilSmall
            // 
            this.ilSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilSmall, "ilSmall");
            this.ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ilLarge
            // 
            this.ilLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilLarge, "ilLarge");
            this.ilLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // AddinListView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowEffects);
            this.MinimumSize = new System.Drawing.Size(210, 210);
            this.Name = "AddinListView";
            this.Resize += new System.EventHandler(this.AddinListView_Resize);
            this.flowEffects.ResumeLayout(false);
            this.flowEffectTool.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lvFilters;
        private System.Windows.Forms.Button btnEffectDown;
        private System.Windows.Forms.Button btnEffectUp;
        private System.Windows.Forms.Button btnEffectRemove;
        private System.Windows.Forms.Button btnEffectAdd;
        private System.Windows.Forms.FlowLayoutPanel flowEffects;
        private System.Windows.Forms.FlowLayoutPanel flowEffectTool;
        private System.Windows.Forms.ImageList ilSmall;
        private System.Windows.Forms.ImageList ilLarge;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
