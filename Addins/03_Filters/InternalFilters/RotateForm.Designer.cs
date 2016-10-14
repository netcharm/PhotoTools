namespace InternalFilters
{
    partial class RotateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RotateForm));
            this.groupMode = new System.Windows.Forms.GroupBox();
            this.btnRotateFlipY = new System.Windows.Forms.Button();
            this.btnRotateFlipX = new System.Windows.Forms.Button();
            this.btnRotate90r = new System.Windows.Forms.Button();
            this.btnRotate90l = new System.Windows.Forms.Button();
            this.groupAngle = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip();
            this.groupMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMode
            // 
            this.groupMode.Controls.Add(this.btnRotateFlipY);
            this.groupMode.Controls.Add(this.btnRotateFlipX);
            this.groupMode.Controls.Add(this.btnRotate90r);
            this.groupMode.Controls.Add(this.btnRotate90l);
            resources.ApplyResources(this.groupMode, "groupMode");
            this.groupMode.Name = "groupMode";
            this.groupMode.TabStop = false;
            // 
            // btnRotateFlipY
            // 
            resources.ApplyResources(this.btnRotateFlipY, "btnRotateFlipY");
            this.btnRotateFlipY.Image = global::InternalFilters.Properties.Resources.FlipY_32x;
            this.btnRotateFlipY.Name = "btnRotateFlipY";
            this.toolTip.SetToolTip(this.btnRotateFlipY, resources.GetString("btnRotateFlipY.ToolTip"));
            this.btnRotateFlipY.UseVisualStyleBackColor = true;
            // 
            // btnRotateFlipX
            // 
            resources.ApplyResources(this.btnRotateFlipX, "btnRotateFlipX");
            this.btnRotateFlipX.Image = global::InternalFilters.Properties.Resources.FlipX_32x;
            this.btnRotateFlipX.Name = "btnRotateFlipX";
            this.toolTip.SetToolTip(this.btnRotateFlipX, resources.GetString("btnRotateFlipX.ToolTip"));
            this.btnRotateFlipX.UseVisualStyleBackColor = true;
            // 
            // btnRotate90r
            // 
            resources.ApplyResources(this.btnRotate90r, "btnRotate90r");
            this.btnRotate90r.Image = global::InternalFilters.Properties.Resources.RotateR_32x;
            this.btnRotate90r.Name = "btnRotate90r";
            this.toolTip.SetToolTip(this.btnRotate90r, resources.GetString("btnRotate90r.ToolTip"));
            this.btnRotate90r.UseVisualStyleBackColor = true;
            // 
            // btnRotate90l
            // 
            resources.ApplyResources(this.btnRotate90l, "btnRotate90l");
            this.btnRotate90l.Image = global::InternalFilters.Properties.Resources.RotateL_32x;
            this.btnRotate90l.Name = "btnRotate90l";
            this.toolTip.SetToolTip(this.btnRotate90l, resources.GetString("btnRotate90l.ToolTip"));
            this.btnRotate90l.UseVisualStyleBackColor = true;
            // 
            // groupAngle
            // 
            resources.ApplyResources(this.groupAngle, "groupAngle");
            this.groupAngle.Name = "groupAngle";
            this.groupAngle.TabStop = false;
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
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // RotateForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupAngle);
            this.Controls.Add(this.groupMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.Name = "RotateForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.groupMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.GroupBox groupAngle;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRotate90l;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnRotateFlipY;
        private System.Windows.Forms.Button btnRotateFlipX;
        private System.Windows.Forms.Button btnRotate90r;
    }
}