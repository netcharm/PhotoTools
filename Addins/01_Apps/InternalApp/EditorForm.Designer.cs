namespace InternalFilters
{
    partial class EditorForm
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
            this.imgEditor = new Cyotek.Windows.Forms.ImageBox();
            this.SuspendLayout();
            // 
            // imgEditor
            // 
            this.imgEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imgEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgEditor.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.imgEditor.Location = new System.Drawing.Point(0, 0);
            this.imgEditor.Name = "imgEditor";
            this.imgEditor.ShowPixelGrid = true;
            this.imgEditor.Size = new System.Drawing.Size(573, 439);
            this.imgEditor.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
            this.imgEditor.TabIndex = 0;
            this.imgEditor.Text = "Image Editor";
            this.imgEditor.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 439);
            this.Controls.Add(this.imgEditor);
            this.Name = "EditorForm";
            this.Text = "Image Editor";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Cyotek.Windows.Forms.ImageBox imgEditor;
    }
}