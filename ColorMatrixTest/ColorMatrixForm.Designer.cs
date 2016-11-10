namespace ColorMatrixTest
{
    partial class ColorMatrixForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.colorMatrix = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edMatrix42 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix13 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix12 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix11 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix10 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix04 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix03 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix02 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix01 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix00 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix20 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix21 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix14 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix24 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix23 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix22 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix31 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix30 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix32 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix33 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix34 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.edMatrix40 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix44 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix43 = new System.Windows.Forms.NumericUpDown();
            this.edMatrix41 = new System.Windows.Forms.NumericUpDown();
            this.btnTest = new System.Windows.Forms.Button();
            this.cbGrayMode = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnCopyMatrix = new System.Windows.Forms.Button();
            this.btnOriginal = new System.Windows.Forms.Button();
            this.imgPreview = new System.Windows.Forms.PictureBox();
            this.chkLiveCalc = new System.Windows.Forms.CheckBox();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.colorMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix42)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix04)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix03)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix00)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix43)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // colorMatrix
            // 
            this.colorMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colorMatrix.ColumnCount = 6;
            this.colorMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.colorMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.Controls.Add(this.label10, 0, 5);
            this.colorMatrix.Controls.Add(this.label5, 5, 0);
            this.colorMatrix.Controls.Add(this.label4, 4, 0);
            this.colorMatrix.Controls.Add(this.label3, 3, 0);
            this.colorMatrix.Controls.Add(this.label2, 2, 0);
            this.colorMatrix.Controls.Add(this.edMatrix42, 3, 5);
            this.colorMatrix.Controls.Add(this.edMatrix13, 4, 2);
            this.colorMatrix.Controls.Add(this.edMatrix12, 3, 2);
            this.colorMatrix.Controls.Add(this.edMatrix11, 2, 2);
            this.colorMatrix.Controls.Add(this.edMatrix10, 1, 2);
            this.colorMatrix.Controls.Add(this.edMatrix04, 5, 1);
            this.colorMatrix.Controls.Add(this.edMatrix03, 4, 1);
            this.colorMatrix.Controls.Add(this.edMatrix02, 3, 1);
            this.colorMatrix.Controls.Add(this.edMatrix01, 2, 1);
            this.colorMatrix.Controls.Add(this.edMatrix00, 1, 1);
            this.colorMatrix.Controls.Add(this.edMatrix20, 1, 3);
            this.colorMatrix.Controls.Add(this.edMatrix21, 2, 3);
            this.colorMatrix.Controls.Add(this.edMatrix14, 5, 2);
            this.colorMatrix.Controls.Add(this.edMatrix24, 5, 3);
            this.colorMatrix.Controls.Add(this.edMatrix23, 4, 3);
            this.colorMatrix.Controls.Add(this.edMatrix22, 3, 3);
            this.colorMatrix.Controls.Add(this.edMatrix31, 2, 4);
            this.colorMatrix.Controls.Add(this.edMatrix30, 1, 4);
            this.colorMatrix.Controls.Add(this.edMatrix32, 3, 4);
            this.colorMatrix.Controls.Add(this.edMatrix33, 4, 4);
            this.colorMatrix.Controls.Add(this.edMatrix34, 5, 4);
            this.colorMatrix.Controls.Add(this.label1, 1, 0);
            this.colorMatrix.Controls.Add(this.label6, 0, 1);
            this.colorMatrix.Controls.Add(this.label7, 0, 2);
            this.colorMatrix.Controls.Add(this.label8, 0, 3);
            this.colorMatrix.Controls.Add(this.label9, 0, 4);
            this.colorMatrix.Controls.Add(this.edMatrix40, 1, 5);
            this.colorMatrix.Controls.Add(this.edMatrix44, 5, 5);
            this.colorMatrix.Controls.Add(this.edMatrix43, 4, 5);
            this.colorMatrix.Controls.Add(this.edMatrix41, 2, 5);
            this.colorMatrix.Location = new System.Drawing.Point(13, 454);
            this.colorMatrix.Name = "colorMatrix";
            this.colorMatrix.RowCount = 6;
            this.colorMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.colorMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.colorMatrix.Size = new System.Drawing.Size(475, 149);
            this.colorMatrix.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label10.Location = new System.Drawing.Point(3, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 25);
            this.label10.TabIndex = 34;
            this.label10.Text = "T";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip.SetToolTip(this.label10, "Liner Transform");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label5.Location = new System.Drawing.Point(387, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 24);
            this.label5.TabIndex = 29;
            this.label5.Text = "N/A";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.Location = new System.Drawing.Point(296, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 24);
            this.label4.TabIndex = 28;
            this.label4.Text = "A";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Location = new System.Drawing.Point(205, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 24);
            this.label3.TabIndex = 27;
            this.label3.Text = "B";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Location = new System.Drawing.Point(114, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 24);
            this.label2.TabIndex = 26;
            this.label2.Text = "G";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // edMatrix42
            // 
            this.edMatrix42.DecimalPlaces = 4;
            this.edMatrix42.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix42.Location = new System.Drawing.Point(205, 127);
            this.edMatrix42.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix42.Name = "edMatrix42";
            this.edMatrix42.Size = new System.Drawing.Size(85, 21);
            this.edMatrix42.TabIndex = 22;
            this.edMatrix42.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix42.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix13
            // 
            this.edMatrix13.DecimalPlaces = 4;
            this.edMatrix13.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix13.Location = new System.Drawing.Point(296, 52);
            this.edMatrix13.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix13.Name = "edMatrix13";
            this.edMatrix13.Size = new System.Drawing.Size(85, 21);
            this.edMatrix13.TabIndex = 8;
            this.edMatrix13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix13.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix12
            // 
            this.edMatrix12.DecimalPlaces = 4;
            this.edMatrix12.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix12.Location = new System.Drawing.Point(205, 52);
            this.edMatrix12.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix12.Name = "edMatrix12";
            this.edMatrix12.Size = new System.Drawing.Size(85, 21);
            this.edMatrix12.TabIndex = 7;
            this.edMatrix12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix12.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix11
            // 
            this.edMatrix11.DecimalPlaces = 4;
            this.edMatrix11.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix11.Location = new System.Drawing.Point(114, 52);
            this.edMatrix11.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix11.Name = "edMatrix11";
            this.edMatrix11.Size = new System.Drawing.Size(85, 21);
            this.edMatrix11.TabIndex = 6;
            this.edMatrix11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix11.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edMatrix11.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix10
            // 
            this.edMatrix10.DecimalPlaces = 4;
            this.edMatrix10.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix10.Location = new System.Drawing.Point(23, 52);
            this.edMatrix10.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix10.Name = "edMatrix10";
            this.edMatrix10.Size = new System.Drawing.Size(85, 21);
            this.edMatrix10.TabIndex = 5;
            this.edMatrix10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix10.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix04
            // 
            this.edMatrix04.DecimalPlaces = 4;
            this.edMatrix04.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix04.Location = new System.Drawing.Point(387, 27);
            this.edMatrix04.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix04.Name = "edMatrix04";
            this.edMatrix04.Size = new System.Drawing.Size(85, 21);
            this.edMatrix04.TabIndex = 4;
            this.edMatrix04.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix04.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix03
            // 
            this.edMatrix03.DecimalPlaces = 4;
            this.edMatrix03.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix03.Location = new System.Drawing.Point(296, 27);
            this.edMatrix03.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix03.Name = "edMatrix03";
            this.edMatrix03.Size = new System.Drawing.Size(85, 21);
            this.edMatrix03.TabIndex = 3;
            this.edMatrix03.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix03.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix02
            // 
            this.edMatrix02.DecimalPlaces = 4;
            this.edMatrix02.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix02.Location = new System.Drawing.Point(205, 27);
            this.edMatrix02.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix02.Name = "edMatrix02";
            this.edMatrix02.Size = new System.Drawing.Size(85, 21);
            this.edMatrix02.TabIndex = 2;
            this.edMatrix02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix02.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix01
            // 
            this.edMatrix01.DecimalPlaces = 4;
            this.edMatrix01.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix01.Location = new System.Drawing.Point(114, 27);
            this.edMatrix01.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix01.Name = "edMatrix01";
            this.edMatrix01.Size = new System.Drawing.Size(85, 21);
            this.edMatrix01.TabIndex = 1;
            this.edMatrix01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix01.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix00
            // 
            this.edMatrix00.DecimalPlaces = 4;
            this.edMatrix00.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix00.Location = new System.Drawing.Point(23, 27);
            this.edMatrix00.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix00.Name = "edMatrix00";
            this.edMatrix00.Size = new System.Drawing.Size(85, 21);
            this.edMatrix00.TabIndex = 0;
            this.edMatrix00.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix00.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edMatrix00.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix20
            // 
            this.edMatrix20.DecimalPlaces = 4;
            this.edMatrix20.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix20.Location = new System.Drawing.Point(23, 77);
            this.edMatrix20.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix20.Name = "edMatrix20";
            this.edMatrix20.Size = new System.Drawing.Size(85, 21);
            this.edMatrix20.TabIndex = 10;
            this.edMatrix20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix20.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix21
            // 
            this.edMatrix21.DecimalPlaces = 4;
            this.edMatrix21.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix21.Location = new System.Drawing.Point(114, 77);
            this.edMatrix21.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix21.Name = "edMatrix21";
            this.edMatrix21.Size = new System.Drawing.Size(85, 21);
            this.edMatrix21.TabIndex = 11;
            this.edMatrix21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix21.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix14
            // 
            this.edMatrix14.DecimalPlaces = 4;
            this.edMatrix14.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix14.Location = new System.Drawing.Point(387, 52);
            this.edMatrix14.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix14.Name = "edMatrix14";
            this.edMatrix14.Size = new System.Drawing.Size(85, 21);
            this.edMatrix14.TabIndex = 9;
            this.edMatrix14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix14.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix24
            // 
            this.edMatrix24.DecimalPlaces = 4;
            this.edMatrix24.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix24.Location = new System.Drawing.Point(387, 77);
            this.edMatrix24.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix24.Name = "edMatrix24";
            this.edMatrix24.Size = new System.Drawing.Size(85, 21);
            this.edMatrix24.TabIndex = 14;
            this.edMatrix24.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix24.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix23
            // 
            this.edMatrix23.DecimalPlaces = 4;
            this.edMatrix23.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix23.Location = new System.Drawing.Point(296, 77);
            this.edMatrix23.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix23.Name = "edMatrix23";
            this.edMatrix23.Size = new System.Drawing.Size(85, 21);
            this.edMatrix23.TabIndex = 13;
            this.edMatrix23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix23.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix22
            // 
            this.edMatrix22.DecimalPlaces = 4;
            this.edMatrix22.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix22.Location = new System.Drawing.Point(205, 77);
            this.edMatrix22.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix22.Name = "edMatrix22";
            this.edMatrix22.Size = new System.Drawing.Size(85, 21);
            this.edMatrix22.TabIndex = 12;
            this.edMatrix22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix22.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edMatrix22.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix31
            // 
            this.edMatrix31.DecimalPlaces = 4;
            this.edMatrix31.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix31.Location = new System.Drawing.Point(114, 102);
            this.edMatrix31.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix31.Name = "edMatrix31";
            this.edMatrix31.Size = new System.Drawing.Size(85, 21);
            this.edMatrix31.TabIndex = 16;
            this.edMatrix31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix31.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix30
            // 
            this.edMatrix30.DecimalPlaces = 4;
            this.edMatrix30.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix30.Location = new System.Drawing.Point(23, 102);
            this.edMatrix30.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix30.Name = "edMatrix30";
            this.edMatrix30.Size = new System.Drawing.Size(85, 21);
            this.edMatrix30.TabIndex = 15;
            this.edMatrix30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix30.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix32
            // 
            this.edMatrix32.DecimalPlaces = 4;
            this.edMatrix32.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix32.Location = new System.Drawing.Point(205, 102);
            this.edMatrix32.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix32.Name = "edMatrix32";
            this.edMatrix32.Size = new System.Drawing.Size(85, 21);
            this.edMatrix32.TabIndex = 17;
            this.edMatrix32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix32.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix33
            // 
            this.edMatrix33.DecimalPlaces = 4;
            this.edMatrix33.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix33.Location = new System.Drawing.Point(296, 102);
            this.edMatrix33.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix33.Name = "edMatrix33";
            this.edMatrix33.Size = new System.Drawing.Size(85, 21);
            this.edMatrix33.TabIndex = 18;
            this.edMatrix33.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix33.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edMatrix33.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix34
            // 
            this.edMatrix34.DecimalPlaces = 4;
            this.edMatrix34.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix34.Location = new System.Drawing.Point(387, 102);
            this.edMatrix34.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix34.Name = "edMatrix34";
            this.edMatrix34.Size = new System.Drawing.Size(85, 21);
            this.edMatrix34.TabIndex = 19;
            this.edMatrix34.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix34.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Location = new System.Drawing.Point(23, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 24);
            this.label1.TabIndex = 25;
            this.label1.Text = "R";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Location = new System.Drawing.Point(3, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 25);
            this.label6.TabIndex = 30;
            this.label6.Text = "R";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label7.Location = new System.Drawing.Point(3, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 25);
            this.label7.TabIndex = 31;
            this.label7.Text = "G";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label8.Location = new System.Drawing.Point(3, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 25);
            this.label8.TabIndex = 32;
            this.label8.Text = "B";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label9.Location = new System.Drawing.Point(3, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 25);
            this.label9.TabIndex = 33;
            this.label9.Text = "A";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // edMatrix40
            // 
            this.edMatrix40.DecimalPlaces = 4;
            this.edMatrix40.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix40.Location = new System.Drawing.Point(23, 127);
            this.edMatrix40.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix40.Name = "edMatrix40";
            this.edMatrix40.Size = new System.Drawing.Size(85, 21);
            this.edMatrix40.TabIndex = 20;
            this.edMatrix40.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix40.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix44
            // 
            this.edMatrix44.DecimalPlaces = 4;
            this.edMatrix44.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix44.Location = new System.Drawing.Point(387, 127);
            this.edMatrix44.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix44.Name = "edMatrix44";
            this.edMatrix44.Size = new System.Drawing.Size(85, 21);
            this.edMatrix44.TabIndex = 24;
            this.edMatrix44.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix44.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edMatrix44.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix43
            // 
            this.edMatrix43.DecimalPlaces = 4;
            this.edMatrix43.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix43.Location = new System.Drawing.Point(296, 127);
            this.edMatrix43.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix43.Name = "edMatrix43";
            this.edMatrix43.Size = new System.Drawing.Size(85, 21);
            this.edMatrix43.TabIndex = 23;
            this.edMatrix43.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix43.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // edMatrix41
            // 
            this.edMatrix41.DecimalPlaces = 4;
            this.edMatrix41.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.edMatrix41.Location = new System.Drawing.Point(114, 127);
            this.edMatrix41.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.edMatrix41.Name = "edMatrix41";
            this.edMatrix41.Size = new System.Drawing.Size(85, 21);
            this.edMatrix41.TabIndex = 21;
            this.edMatrix41.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.edMatrix41.ValueChanged += new System.EventHandler(this.edMatrix_ValueChanged);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(707, 529);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 70);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.toolTip.SetToolTip(this.btnTest, "Test Current Color Matrix Value");
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // cbGrayMode
            // 
            this.cbGrayMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGrayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrayMode.DropDownWidth = 128;
            this.cbGrayMode.FormattingEnabled = true;
            this.cbGrayMode.Location = new System.Drawing.Point(632, 478);
            this.cbGrayMode.Name = "cbGrayMode";
            this.cbGrayMode.Size = new System.Drawing.Size(150, 20);
            this.cbGrayMode.TabIndex = 0;
            this.toolTip.SetToolTip(this.cbGrayMode, "Select Pre-Defined Color Matrix ");
            this.cbGrayMode.SelectedIndexChanged += new System.EventHandler(this.cbGrayMode_SelectedIndexChanged);
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::ColorMatrixTest.Properties.Resources.SaveAs_16x;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(632, 529);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(28, 28);
            this.btnSave.TabIndex = 6;
            this.toolTip.SetToolTip(this.btnSave, "Save ColorMatrix File or Image");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Image = global::ColorMatrixTest.Properties.Resources.ImageLoader_16x;
            this.btnOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpen.Location = new System.Drawing.Point(666, 529);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(28, 28);
            this.btnOpen.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnOpen, "Load Image File or ColorMatrix File");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnCopyMatrix
            // 
            this.btnCopyMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyMatrix.Image = global::ColorMatrixTest.Properties.Resources.Copy_16x;
            this.btnCopyMatrix.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCopyMatrix.Location = new System.Drawing.Point(632, 572);
            this.btnCopyMatrix.Name = "btnCopyMatrix";
            this.btnCopyMatrix.Size = new System.Drawing.Size(28, 28);
            this.btnCopyMatrix.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnCopyMatrix, "Copy Current Color Matrix Value to C# Code");
            this.btnCopyMatrix.UseVisualStyleBackColor = true;
            this.btnCopyMatrix.Click += new System.EventHandler(this.btnCopyMatrix_Click);
            this.btnCopyMatrix.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMatrixForm_DragDrop);
            this.btnCopyMatrix.DragEnter += new System.Windows.Forms.DragEventHandler(this.ColorMatrixForm_DragEnter);
            // 
            // btnOriginal
            // 
            this.btnOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOriginal.Image = global::ColorMatrixTest.Properties.Resources.Compare_16x;
            this.btnOriginal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOriginal.Location = new System.Drawing.Point(666, 571);
            this.btnOriginal.Name = "btnOriginal";
            this.btnOriginal.Size = new System.Drawing.Size(28, 28);
            this.btnOriginal.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnOriginal, "View Original Image");
            this.btnOriginal.UseVisualStyleBackColor = true;
            this.btnOriginal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnOriginal_MouseDown);
            this.btnOriginal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnOriginal_MouseUp);
            // 
            // imgPreview
            // 
            this.imgPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgPreview.Location = new System.Drawing.Point(13, 12);
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.Size = new System.Drawing.Size(768, 432);
            this.imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgPreview.TabIndex = 0;
            this.imgPreview.TabStop = false;
            this.toolTip.SetToolTip(this.imgPreview, "Drag Image File To Here");
            this.imgPreview.DoubleClick += new System.EventHandler(this.imgPreview_DoubleClick);
            // 
            // chkLiveCalc
            // 
            this.chkLiveCalc.AutoEllipsis = true;
            this.chkLiveCalc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLiveCalc.Checked = true;
            this.chkLiveCalc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLiveCalc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkLiveCalc.Location = new System.Drawing.Point(632, 503);
            this.chkLiveCalc.Name = "chkLiveCalc";
            this.chkLiveCalc.Size = new System.Drawing.Size(150, 22);
            this.chkLiveCalc.TabIndex = 7;
            this.chkLiveCalc.Text = "Live Calc Values";
            this.chkLiveCalc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.chkLiveCalc, "Immediate Calc the ColorMatrix & Apply to Image (unchecked for big image)");
            this.chkLiveCalc.UseVisualStyleBackColor = true;
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "Image File(*.jpg;*.bmp;*.png;*.tif;*.gif)|*.jpg;*.bmp;*.png;*.tif;*.gif|ColorMatr" +
    "ix File(*.cm)|*.cm|All File(*.*)|*.*";
            this.dlgOpen.Multiselect = true;
            this.dlgOpen.SupportMultiDottedExtensions = true;
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "PNG File(*.png)|*.png|ColorMatrix File(*.cm)|*.cm";
            this.dlgSave.FilterIndex = 2;
            this.dlgSave.SupportMultiDottedExtensions = true;
            // 
            // ColorMatrixForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 615);
            this.Controls.Add(this.chkLiveCalc);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnCopyMatrix);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.cbGrayMode);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.colorMatrix);
            this.Controls.Add(this.imgPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ColorMatrixForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ColorMatrix Test";
            this.Load += new System.EventHandler(this.ColorMatrixForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColorMatrixForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ColorMatrixForm_DragEnter);
            this.colorMatrix.ResumeLayout(false);
            this.colorMatrix.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix42)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix04)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix03)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix00)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix43)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edMatrix41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgPreview;
        private System.Windows.Forms.TableLayoutPanel colorMatrix;
        private System.Windows.Forms.NumericUpDown edMatrix00;
        private System.Windows.Forms.NumericUpDown edMatrix41;
        private System.Windows.Forms.NumericUpDown edMatrix40;
        private System.Windows.Forms.NumericUpDown edMatrix42;
        private System.Windows.Forms.NumericUpDown edMatrix44;
        private System.Windows.Forms.NumericUpDown edMatrix43;
        private System.Windows.Forms.NumericUpDown edMatrix13;
        private System.Windows.Forms.NumericUpDown edMatrix12;
        private System.Windows.Forms.NumericUpDown edMatrix11;
        private System.Windows.Forms.NumericUpDown edMatrix10;
        private System.Windows.Forms.NumericUpDown edMatrix04;
        private System.Windows.Forms.NumericUpDown edMatrix03;
        private System.Windows.Forms.NumericUpDown edMatrix02;
        private System.Windows.Forms.NumericUpDown edMatrix01;
        private System.Windows.Forms.NumericUpDown edMatrix20;
        private System.Windows.Forms.NumericUpDown edMatrix21;
        private System.Windows.Forms.NumericUpDown edMatrix14;
        private System.Windows.Forms.NumericUpDown edMatrix24;
        private System.Windows.Forms.NumericUpDown edMatrix23;
        private System.Windows.Forms.NumericUpDown edMatrix22;
        private System.Windows.Forms.NumericUpDown edMatrix31;
        private System.Windows.Forms.NumericUpDown edMatrix30;
        private System.Windows.Forms.NumericUpDown edMatrix32;
        private System.Windows.Forms.NumericUpDown edMatrix33;
        private System.Windows.Forms.NumericUpDown edMatrix34;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ComboBox cbGrayMode;
        private System.Windows.Forms.Button btnOriginal;
        private System.Windows.Forms.Button btnCopyMatrix;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkLiveCalc;
    }
}

