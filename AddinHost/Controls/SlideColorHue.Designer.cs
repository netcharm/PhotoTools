namespace NetCharm.Image.Addins.Controls
{
    partial class SlideColorHue
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
            this.edValue = new System.Windows.Forms.NumericUpDown();
            this.lblCaption = new System.Windows.Forms.Label();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.slideValue = new Cyotek.Windows.Forms.HueColorSlider();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblTextSuffix = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.edValue)).BeginInit();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // edValue
            // 
            this.edValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.edValue.Location = new System.Drawing.Point(222, 3);
            this.edValue.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.edValue.Name = "edValue";
            this.edValue.Size = new System.Drawing.Size(48, 21);
            this.edValue.TabIndex = 0;
            this.edValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edValue.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.edValue.ValueChanged += new System.EventHandler(this.edValue_ValueChanged);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoEllipsis = true;
            this.lblCaption.AutoSize = true;
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCaption.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblCaption.Location = new System.Drawing.Point(3, 0);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblCaption.Size = new System.Drawing.Size(26, 26);
            this.lblCaption.TabIndex = 2;
            this.lblCaption.Text = "Hue";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // layout
            // 
            this.layout.AutoSize = true;
            this.layout.ColumnCount = 4;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.Controls.Add(this.slideValue, 0, 1);
            this.layout.Controls.Add(this.lblCaption, 0, 0);
            this.layout.Controls.Add(this.lblUnit, 3, 0);
            this.layout.Controls.Add(this.edValue, 2, 0);
            this.layout.Controls.Add(this.lblTextSuffix, 1, 0);
            this.layout.Dock = System.Windows.Forms.DockStyle.Top;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.RowCount = 2;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.Size = new System.Drawing.Size(279, 52);
            this.layout.TabIndex = 4;
            // 
            // slideValue
            // 
            this.layout.SetColumnSpan(this.slideValue, 4);
            this.slideValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slideValue.Location = new System.Drawing.Point(3, 29);
            this.slideValue.Name = "slideValue";
            this.slideValue.Size = new System.Drawing.Size(273, 20);
            this.slideValue.TabIndex = 6;
            this.slideValue.Value = 180F;
            this.slideValue.ValueChanged += new System.EventHandler(this.slideValue_ValueChanged);
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUnit.Location = new System.Drawing.Point(273, 0);
            this.lblUnit.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblUnit.Size = new System.Drawing.Size(3, 26);
            this.lblUnit.TabIndex = 4;
            this.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextSuffix
            // 
            this.lblTextSuffix.AutoEllipsis = true;
            this.lblTextSuffix.AutoSize = true;
            this.lblTextSuffix.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTextSuffix.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTextSuffix.Location = new System.Drawing.Point(31, 0);
            this.lblTextSuffix.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.lblTextSuffix.Name = "lblTextSuffix";
            this.lblTextSuffix.Size = new System.Drawing.Size(11, 26);
            this.lblTextSuffix.TabIndex = 5;
            this.lblTextSuffix.Text = ":";
            this.lblTextSuffix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SlideColorHue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layout);
            this.Name = "SlideColorHue";
            this.Size = new System.Drawing.Size(279, 58);
            this.Load += new System.EventHandler(this.ParamNumber_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edValue)).EndInit();
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown edValue;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblTextSuffix;
        private Cyotek.Windows.Forms.HueColorSlider slideValue;
    }
}
