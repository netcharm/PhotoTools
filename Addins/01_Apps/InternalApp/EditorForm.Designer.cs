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
            this.imgEditor.AllowDoubleClick = true;
            this.imgEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imgEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgEditor.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.imgEditor.Location = new System.Drawing.Point(0, 0);
            this.imgEditor.Name = "imgEditor";
            this.imgEditor.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle;
            this.imgEditor.ShowPixelGrid = true;
            this.imgEditor.Size = new System.Drawing.Size(573, 439);
            this.imgEditor.TabIndex = 0;
            this.imgEditor.Text = "Image Editor";
            this.imgEditor.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            this.imgEditor.ImageChanged += new System.EventHandler(this.imgEditor_ImageChanged);
            this.imgEditor.Selected += new System.EventHandler<System.EventArgs>(this.imgEditor_Selected);
            this.imgEditor.Selecting += new System.EventHandler<Cyotek.Windows.Forms.ImageBoxCancelEventArgs>(this.imgEditor_Selecting);
            this.imgEditor.ZoomChanged += new System.EventHandler(this.imgEditor_ZoomChanged);
            this.imgEditor.Zoomed += new System.EventHandler<Cyotek.Windows.Forms.ImageBoxZoomEventArgs>(this.imgEditor_Zoomed);
            this.imgEditor.SizeChanged += new System.EventHandler(this.imgEditor_SizeChanged);
            this.imgEditor.Click += new System.EventHandler(this.imgEditor_Click);
            this.imgEditor.DoubleClick += new System.EventHandler(this.imgEditor_DoubleClick);
            this.imgEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.imgEditor_KeyDown);
            this.imgEditor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.imgEditor_KeyUp);
            this.imgEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgEditor_MouseDown);
            this.imgEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgEditor_MouseMove);
            this.imgEditor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgEditor_MouseUp);
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