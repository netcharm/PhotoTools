namespace InternalFilters
{
    partial class ResizeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResizeForm));
            this.edWidth = new System.Windows.Forms.NumericUpDown();
            this.edHeight = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.chkAspect = new System.Windows.Forms.CheckBox();
            this.cbResizeMethod = new System.Windows.Forms.ComboBox();
            this.labelMethod = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.edWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // edWidth
            // 
            resources.ApplyResources(this.edWidth, "edWidth");
            this.edWidth.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.edWidth.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.edWidth.Name = "edWidth";
            this.edWidth.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.edWidth.ValueChanged += new System.EventHandler(this.edWidth_ValueChanged);
            // 
            // edHeight
            // 
            resources.ApplyResources(this.edHeight, "edHeight");
            this.edHeight.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.edHeight.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.edHeight.Name = "edHeight";
            this.edHeight.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.edHeight.ValueChanged += new System.EventHandler(this.edWidth_ValueChanged);
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
            // labelWidth
            // 
            resources.ApplyResources(this.labelWidth, "labelWidth");
            this.labelWidth.Name = "labelWidth";
            // 
            // labelHeight
            // 
            resources.ApplyResources(this.labelHeight, "labelHeight");
            this.labelHeight.Name = "labelHeight";
            // 
            // chkAspect
            // 
            this.chkAspect.Checked = true;
            this.chkAspect.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.chkAspect, "chkAspect");
            this.chkAspect.Name = "chkAspect";
            this.chkAspect.UseVisualStyleBackColor = true;
            // 
            // cbResizeMethod
            // 
            this.cbResizeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbResizeMethod, "cbResizeMethod");
            this.cbResizeMethod.FormattingEnabled = true;
            this.cbResizeMethod.Items.AddRange(new object[] {
            resources.GetString("cbResizeMethod.Items"),
            resources.GetString("cbResizeMethod.Items1"),
            resources.GetString("cbResizeMethod.Items2")});
            this.cbResizeMethod.Name = "cbResizeMethod";
            // 
            // labelMethod
            // 
            resources.ApplyResources(this.labelMethod, "labelMethod");
            this.labelMethod.Name = "labelMethod";
            // 
            // ResizeForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.labelMethod);
            this.Controls.Add(this.cbResizeMethod);
            this.Controls.Add(this.chkAspect);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.edHeight);
            this.Controls.Add(this.edWidth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "ResizeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ResizeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown edWidth;
        private System.Windows.Forms.NumericUpDown edHeight;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.CheckBox chkAspect;
        private System.Windows.Forms.ComboBox cbResizeMethod;
        private System.Windows.Forms.Label labelMethod;
    }
}