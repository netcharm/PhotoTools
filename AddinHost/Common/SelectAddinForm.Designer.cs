namespace NetCharm.Image.Addins.Common
{
    partial class SelectAddinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAddinForm));
            this.lvAddins = new System.Windows.Forms.ListView();
            this.ilLarge = new System.Windows.Forms.ImageList(this.components);
            this.ilSmall = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpGroupMode = new System.Windows.Forms.GroupBox();
            this.btnGroupCategory = new System.Windows.Forms.RadioButton();
            this.btnGroupType = new System.Windows.Forms.RadioButton();
            this.btnGroupNone = new System.Windows.Forms.RadioButton();
            this.grpGroupMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvAddins
            // 
            resources.ApplyResources(this.lvAddins, "lvAddins");
            this.lvAddins.LargeImageList = this.ilLarge;
            this.lvAddins.Name = "lvAddins";
            this.lvAddins.SmallImageList = this.ilSmall;
            this.lvAddins.UseCompatibleStateImageBehavior = false;
            this.lvAddins.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvAddins_RetrieveVirtualItem);
            this.lvAddins.DoubleClick += new System.EventHandler(this.lvAddins_DoubleClick);
            // 
            // ilLarge
            // 
            this.ilLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilLarge, "ilLarge");
            this.ilLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ilSmall
            // 
            this.ilSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilSmall, "ilSmall");
            this.ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpGroupMode
            // 
            resources.ApplyResources(this.grpGroupMode, "grpGroupMode");
            this.grpGroupMode.Controls.Add(this.btnGroupCategory);
            this.grpGroupMode.Controls.Add(this.btnGroupType);
            this.grpGroupMode.Controls.Add(this.btnGroupNone);
            this.grpGroupMode.Name = "grpGroupMode";
            this.grpGroupMode.TabStop = false;
            // 
            // btnGroupCategory
            // 
            resources.ApplyResources(this.btnGroupCategory, "btnGroupCategory");
            this.btnGroupCategory.Name = "btnGroupCategory";
            this.btnGroupCategory.TabStop = true;
            this.btnGroupCategory.UseVisualStyleBackColor = true;
            this.btnGroupCategory.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // btnGroupType
            // 
            resources.ApplyResources(this.btnGroupType, "btnGroupType");
            this.btnGroupType.Name = "btnGroupType";
            this.btnGroupType.TabStop = true;
            this.btnGroupType.UseVisualStyleBackColor = true;
            this.btnGroupType.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // btnGroupNone
            // 
            resources.ApplyResources(this.btnGroupNone, "btnGroupNone");
            this.btnGroupNone.Name = "btnGroupNone";
            this.btnGroupNone.TabStop = true;
            this.btnGroupNone.UseVisualStyleBackColor = true;
            this.btnGroupNone.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // SelectAddinForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.grpGroupMode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lvAddins);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "SelectAddinForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SelectAddinForm_Load);
            this.grpGroupMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvAddins;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList ilLarge;
        private System.Windows.Forms.ImageList ilSmall;
        private System.Windows.Forms.GroupBox grpGroupMode;
        private System.Windows.Forms.RadioButton btnGroupCategory;
        private System.Windows.Forms.RadioButton btnGroupType;
        private System.Windows.Forms.RadioButton btnGroupNone;
    }
}