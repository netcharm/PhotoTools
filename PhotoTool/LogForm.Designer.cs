namespace PhotoTool
{
    partial class LogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.edLog = new System.Windows.Forms.TextBox();
            this.btnAddinError = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // edLog
            // 
            resources.ApplyResources(this.edLog, "edLog");
            this.edLog.Name = "edLog";
            // 
            // btnAddinError
            // 
            resources.ApplyResources(this.btnAddinError, "btnAddinError");
            this.btnAddinError.Name = "btnAddinError";
            this.btnAddinError.UseVisualStyleBackColor = true;
            this.btnAddinError.Click += new System.EventHandler(this.btnAddinError_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // LogForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddinError);
            this.Controls.Add(this.edLog);
            this.HelpButton = true;
            this.Name = "LogForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LogForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edLog;
        private System.Windows.Forms.Button btnAddinError;
        private System.Windows.Forms.Button btnClear;
    }
}