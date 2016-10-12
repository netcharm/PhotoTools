namespace PhotoTool
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.RibTabMain = new System.Windows.Forms.RibbonTab();
            this.RibTabMainFile = new System.Windows.Forms.RibbonPanel();
            this.cmdFileOpen = new System.Windows.Forms.RibbonButton();
            this.cmdFileSave = new System.Windows.Forms.RibbonButton();
            this.RibTabMainEdit = new System.Windows.Forms.RibbonPanel();
            this.cmdEditCut = new System.Windows.Forms.RibbonButton();
            this.cmdEditCopy = new System.Windows.Forms.RibbonButton();
            this.cmdEditPaste = new System.Windows.Forms.RibbonButton();
            this.cmdEditClear = new System.Windows.Forms.RibbonButton();
            this.RibTabEdit = new System.Windows.Forms.RibbonTab();
            this.RibTabAction = new System.Windows.Forms.RibbonTab();
            this.RibTabActManager = new System.Windows.Forms.RibbonPanel();
            this.cmdActionReScan = new System.Windows.Forms.RibbonButton();
            this.RibTabActInternal = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonList1 = new System.Windows.Forms.RibbonButtonList();
            this.ribbonButton3 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton4 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton2 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton5 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton6 = new System.Windows.Forms.RibbonButton();
            this.RibTabFilter = new System.Windows.Forms.RibbonTab();
            this.RibTabFilterManager = new System.Windows.Forms.RibbonPanel();
            this.cmdFilterReScan = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonList2 = new System.Windows.Forms.RibbonButtonList();
            this.ribbonButton7 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton8 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton9 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton10 = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonList3 = new System.Windows.Forms.RibbonButtonList();
            this.ribbonButton11 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton12 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton13 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton14 = new System.Windows.Forms.RibbonButton();
            this.RibTabSetting = new System.Windows.Forms.RibbonTab();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.ribbonMain = new System.Windows.Forms.Ribbon();
            this.ribOrbMiOpen = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribOrbMiSave = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribOptBtnExit = new System.Windows.Forms.RibbonOrbOptionButton();
            this.ribOptBtnOptions = new System.Windows.Forms.RibbonOrbOptionButton();
            this.ribThemeSelect = new System.Windows.Forms.RibbonButton();
            this.cmdThemeBlue = new System.Windows.Forms.RibbonButton();
            this.cmdThemeBlack = new System.Windows.Forms.RibbonButton();
            this.cmdThemeGreen = new System.Windows.Forms.RibbonButton();
            this.cmdThemePurple = new System.Windows.Forms.RibbonButton();
            this.ribStyleSelect = new System.Windows.Forms.RibbonButton();
            this.cmdStyle2007 = new System.Windows.Forms.RibbonButton();
            this.cmdStyle2010 = new System.Windows.Forms.RibbonButton();
            this.cmdStyle2013 = new System.Windows.Forms.RibbonButton();
            this.ribbonOrbOptionButton1 = new System.Windows.Forms.RibbonOrbOptionButton();
            this.status = new System.Windows.Forms.StatusStrip();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RibTabMainApp = new System.Windows.Forms.RibbonPanel();
            this.SuspendLayout();
            // 
            // RibTabMain
            // 
            this.RibTabMain.Panels.Add(this.RibTabMainFile);
            this.RibTabMain.Panels.Add(this.RibTabMainEdit);
            this.RibTabMain.Panels.Add(this.RibTabMainApp);
            resources.ApplyResources(this.RibTabMain, "RibTabMain");
            // 
            // RibTabMainFile
            // 
            this.RibTabMainFile.Items.Add(this.cmdFileOpen);
            this.RibTabMainFile.Items.Add(this.cmdFileSave);
            resources.ApplyResources(this.RibTabMainFile, "RibTabMainFile");
            // 
            // cmdFileOpen
            // 
            this.cmdFileOpen.Image = global::PhotoTool.Properties.Resources.ExportPerformance_32x;
            this.cmdFileOpen.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdFileOpen.SmallImage")));
            resources.ApplyResources(this.cmdFileOpen, "cmdFileOpen");
            // 
            // cmdFileSave
            // 
            this.cmdFileSave.Image = global::PhotoTool.Properties.Resources.Save_32x;
            this.cmdFileSave.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdFileSave.SmallImage")));
            resources.ApplyResources(this.cmdFileSave, "cmdFileSave");
            // 
            // RibTabMainEdit
            // 
            this.RibTabMainEdit.Items.Add(this.cmdEditCut);
            this.RibTabMainEdit.Items.Add(this.cmdEditCopy);
            this.RibTabMainEdit.Items.Add(this.cmdEditPaste);
            this.RibTabMainEdit.Items.Add(this.cmdEditClear);
            resources.ApplyResources(this.RibTabMainEdit, "RibTabMainEdit");
            // 
            // cmdEditCut
            // 
            this.cmdEditCut.Image = global::PhotoTool.Properties.Resources.Cut_32x;
            this.cmdEditCut.SmallImage = global::PhotoTool.Properties.Resources.Marquee_16x;
            resources.ApplyResources(this.cmdEditCut, "cmdEditCut");
            // 
            // cmdEditCopy
            // 
            this.cmdEditCopy.Image = global::PhotoTool.Properties.Resources.Copy_32x;
            this.cmdEditCopy.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdEditCopy.SmallImage")));
            resources.ApplyResources(this.cmdEditCopy, "cmdEditCopy");
            // 
            // cmdEditPaste
            // 
            this.cmdEditPaste.Image = global::PhotoTool.Properties.Resources.Paste_32x;
            this.cmdEditPaste.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdEditPaste.SmallImage")));
            resources.ApplyResources(this.cmdEditPaste, "cmdEditPaste");
            // 
            // cmdEditClear
            // 
            this.cmdEditClear.Image = global::PhotoTool.Properties.Resources.Clear_32x;
            this.cmdEditClear.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdEditClear.SmallImage")));
            resources.ApplyResources(this.cmdEditClear, "cmdEditClear");
            // 
            // RibTabEdit
            // 
            resources.ApplyResources(this.RibTabEdit, "RibTabEdit");
            // 
            // RibTabAction
            // 
            this.RibTabAction.Panels.Add(this.RibTabActManager);
            this.RibTabAction.Panels.Add(this.RibTabActInternal);
            resources.ApplyResources(this.RibTabAction, "RibTabAction");
            // 
            // RibTabActManager
            // 
            this.RibTabActManager.Items.Add(this.cmdActionReScan);
            resources.ApplyResources(this.RibTabActManager, "RibTabActManager");
            // 
            // cmdActionReScan
            // 
            this.cmdActionReScan.Image = global::PhotoTool.Properties.Resources.AddIn_32x;
            this.cmdActionReScan.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdActionReScan.SmallImage")));
            resources.ApplyResources(this.cmdActionReScan, "cmdActionReScan");
            this.cmdActionReScan.Click += new System.EventHandler(this.cmdActionReScan_Click);
            // 
            // RibTabActInternal
            // 
            this.RibTabActInternal.Items.Add(this.ribbonButtonList1);
            resources.ApplyResources(this.RibTabActInternal, "RibTabActInternal");
            // 
            // ribbonButtonList1
            // 
            this.ribbonButtonList1.Buttons.Add(this.ribbonButton3);
            this.ribbonButtonList1.Buttons.Add(this.ribbonButton4);
            this.ribbonButtonList1.Buttons.Add(this.ribbonButton1);
            this.ribbonButtonList1.Buttons.Add(this.ribbonButton2);
            this.ribbonButtonList1.Buttons.Add(this.ribbonButton5);
            this.ribbonButtonList1.Buttons.Add(this.ribbonButton6);
            this.ribbonButtonList1.ButtonsSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.ribbonButtonList1.FlowToBottom = false;
            this.ribbonButtonList1.ItemsSizeInDropwDownMode = new System.Drawing.Size(7, 5);
            resources.ApplyResources(this.ribbonButtonList1, "ribbonButtonList1");
            // 
            // ribbonButton3
            // 
            this.ribbonButton3.Image = global::PhotoTool.Properties.Resources.Marquee_32x;
            this.ribbonButton3.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton3.SmallImage")));
            resources.ApplyResources(this.ribbonButton3, "ribbonButton3");
            // 
            // ribbonButton4
            // 
            this.ribbonButton4.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton4.Image")));
            this.ribbonButton4.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton4.SmallImage")));
            resources.ApplyResources(this.ribbonButton4, "ribbonButton4");
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            resources.ApplyResources(this.ribbonButton1, "ribbonButton1");
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.Image")));
            this.ribbonButton2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.SmallImage")));
            resources.ApplyResources(this.ribbonButton2, "ribbonButton2");
            // 
            // ribbonButton5
            // 
            this.ribbonButton5.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton5.Image")));
            this.ribbonButton5.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton5.SmallImage")));
            resources.ApplyResources(this.ribbonButton5, "ribbonButton5");
            // 
            // ribbonButton6
            // 
            this.ribbonButton6.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton6.Image")));
            this.ribbonButton6.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton6.SmallImage")));
            resources.ApplyResources(this.ribbonButton6, "ribbonButton6");
            // 
            // RibTabFilter
            // 
            this.RibTabFilter.Panels.Add(this.RibTabFilterManager);
            this.RibTabFilter.Panels.Add(this.ribbonPanel1);
            this.RibTabFilter.Panels.Add(this.ribbonPanel2);
            resources.ApplyResources(this.RibTabFilter, "RibTabFilter");
            // 
            // RibTabFilterManager
            // 
            this.RibTabFilterManager.Items.Add(this.cmdFilterReScan);
            resources.ApplyResources(this.RibTabFilterManager, "RibTabFilterManager");
            // 
            // cmdFilterReScan
            // 
            this.cmdFilterReScan.Image = global::PhotoTool.Properties.Resources.AddIn_32x;
            this.cmdFilterReScan.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdFilterReScan.SmallImage")));
            resources.ApplyResources(this.cmdFilterReScan, "cmdFilterReScan");
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.ribbonButtonList2);
            resources.ApplyResources(this.ribbonPanel1, "ribbonPanel1");
            // 
            // ribbonButtonList2
            // 
            this.ribbonButtonList2.Buttons.Add(this.ribbonButton7);
            this.ribbonButtonList2.Buttons.Add(this.ribbonButton8);
            this.ribbonButtonList2.Buttons.Add(this.ribbonButton9);
            this.ribbonButtonList2.Buttons.Add(this.ribbonButton10);
            this.ribbonButtonList2.ButtonsSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.ribbonButtonList2.FlowToBottom = false;
            this.ribbonButtonList2.ItemsSizeInDropwDownMode = new System.Drawing.Size(7, 5);
            resources.ApplyResources(this.ribbonButtonList2, "ribbonButtonList2");
            // 
            // ribbonButton7
            // 
            this.ribbonButton7.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton7.Image")));
            this.ribbonButton7.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton7.SmallImage")));
            resources.ApplyResources(this.ribbonButton7, "ribbonButton7");
            // 
            // ribbonButton8
            // 
            this.ribbonButton8.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton8.Image")));
            this.ribbonButton8.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton8.SmallImage")));
            resources.ApplyResources(this.ribbonButton8, "ribbonButton8");
            // 
            // ribbonButton9
            // 
            this.ribbonButton9.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton9.Image")));
            this.ribbonButton9.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton9.SmallImage")));
            resources.ApplyResources(this.ribbonButton9, "ribbonButton9");
            // 
            // ribbonButton10
            // 
            this.ribbonButton10.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton10.Image")));
            this.ribbonButton10.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton10.SmallImage")));
            resources.ApplyResources(this.ribbonButton10, "ribbonButton10");
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.ribbonButtonList3);
            resources.ApplyResources(this.ribbonPanel2, "ribbonPanel2");
            // 
            // ribbonButtonList3
            // 
            this.ribbonButtonList3.Buttons.Add(this.ribbonButton11);
            this.ribbonButtonList3.Buttons.Add(this.ribbonButton12);
            this.ribbonButtonList3.Buttons.Add(this.ribbonButton13);
            this.ribbonButtonList3.Buttons.Add(this.ribbonButton14);
            this.ribbonButtonList3.ButtonsSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.ribbonButtonList3.FlowToBottom = false;
            this.ribbonButtonList3.ItemsSizeInDropwDownMode = new System.Drawing.Size(7, 5);
            resources.ApplyResources(this.ribbonButtonList3, "ribbonButtonList3");
            // 
            // ribbonButton11
            // 
            this.ribbonButton11.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton11.Image")));
            this.ribbonButton11.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton11.SmallImage")));
            resources.ApplyResources(this.ribbonButton11, "ribbonButton11");
            // 
            // ribbonButton12
            // 
            this.ribbonButton12.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton12.Image")));
            this.ribbonButton12.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton12.SmallImage")));
            resources.ApplyResources(this.ribbonButton12, "ribbonButton12");
            // 
            // ribbonButton13
            // 
            this.ribbonButton13.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton13.Image")));
            this.ribbonButton13.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton13.SmallImage")));
            resources.ApplyResources(this.ribbonButton13, "ribbonButton13");
            // 
            // ribbonButton14
            // 
            this.ribbonButton14.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton14.Image")));
            this.ribbonButton14.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton14.SmallImage")));
            resources.ApplyResources(this.ribbonButton14, "ribbonButton14");
            // 
            // RibTabSetting
            // 
            resources.ApplyResources(this.RibTabSetting, "RibTabSetting");
            // 
            // ribbonMain
            // 
            resources.ApplyResources(this.ribbonMain, "ribbonMain");
            this.ribbonMain.Minimized = false;
            this.ribbonMain.Name = "ribbonMain";
            // 
            // 
            // 
            this.ribbonMain.OrbDropDown.BorderRoundness = 8;
            this.ribbonMain.OrbDropDown.Location = ((System.Drawing.Point)(resources.GetObject("ribbonMain.OrbDropDown.Location")));
            this.ribbonMain.OrbDropDown.MenuItems.Add(this.ribOrbMiOpen);
            this.ribbonMain.OrbDropDown.MenuItems.Add(this.ribOrbMiSave);
            this.ribbonMain.OrbDropDown.Name = "";
            this.ribbonMain.OrbDropDown.OptionItems.Add(this.ribOptBtnExit);
            this.ribbonMain.OrbDropDown.OptionItems.Add(this.ribOptBtnOptions);
            this.ribbonMain.OrbDropDown.OptionItems.Add(this.ribThemeSelect);
            this.ribbonMain.OrbDropDown.OptionItems.Add(this.ribStyleSelect);
            this.ribbonMain.OrbDropDown.RecentItemsCaption = "Recent";
            this.ribbonMain.OrbDropDown.Size = ((System.Drawing.Size)(resources.GetObject("ribbonMain.OrbDropDown.Size")));
            this.ribbonMain.OrbDropDown.TabIndex = ((int)(resources.GetObject("ribbonMain.OrbDropDown.TabIndex")));
            this.ribbonMain.OrbImage = global::PhotoTool.Properties.Resources.Image_16x;
            this.ribbonMain.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010;
            this.ribbonMain.PanelCaptionHeight = 20;
            // 
            // 
            // 
            this.ribbonMain.QuickAcessToolbar.Visible = false;
            this.ribbonMain.RibbonTabFont = new System.Drawing.Font("Segoe UI", 10F);
            this.ribbonMain.Tabs.Add(this.RibTabMain);
            this.ribbonMain.Tabs.Add(this.RibTabEdit);
            this.ribbonMain.Tabs.Add(this.RibTabAction);
            this.ribbonMain.Tabs.Add(this.RibTabFilter);
            this.ribbonMain.Tabs.Add(this.RibTabSetting);
            this.ribbonMain.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbonMain.ThemeColor = System.Windows.Forms.RibbonTheme.Black;
            // 
            // ribOrbMiOpen
            // 
            this.ribOrbMiOpen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribOrbMiOpen.Image = global::PhotoTool.Properties.Resources.ExportPerformance_32x;
            this.ribOrbMiOpen.SmallImage = global::PhotoTool.Properties.Resources.ExportPerformance_32x;
            this.ribOrbMiOpen.Style = System.Windows.Forms.RibbonButtonStyle.SplitDropDown;
            resources.ApplyResources(this.ribOrbMiOpen, "ribOrbMiOpen");
            // 
            // ribOrbMiSave
            // 
            this.ribOrbMiSave.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribOrbMiSave.Image = global::PhotoTool.Properties.Resources.Save_32x;
            this.ribOrbMiSave.SmallImage = global::PhotoTool.Properties.Resources.Save_32x;
            this.ribOrbMiSave.Style = System.Windows.Forms.RibbonButtonStyle.SplitDropDown;
            resources.ApplyResources(this.ribOrbMiSave, "ribOrbMiSave");
            // 
            // ribOptBtnExit
            // 
            this.ribOptBtnExit.Image = global::PhotoTool.Properties.Resources.Exit_16x;
            this.ribOptBtnExit.SmallImage = global::PhotoTool.Properties.Resources.Exit_16x;
            resources.ApplyResources(this.ribOptBtnExit, "ribOptBtnExit");
            this.ribOptBtnExit.Click += new System.EventHandler(this.ribOptBtnExit_Click);
            // 
            // ribOptBtnOptions
            // 
            this.ribOptBtnOptions.Image = global::PhotoTool.Properties.Resources.Settings_32x;
            this.ribOptBtnOptions.SmallImage = global::PhotoTool.Properties.Resources.Settings_16x;
            resources.ApplyResources(this.ribOptBtnOptions, "ribOptBtnOptions");
            // 
            // ribThemeSelect
            // 
            this.ribThemeSelect.DropDownItems.Add(this.cmdThemeBlue);
            this.ribThemeSelect.DropDownItems.Add(this.cmdThemeBlack);
            this.ribThemeSelect.DropDownItems.Add(this.cmdThemeGreen);
            this.ribThemeSelect.DropDownItems.Add(this.cmdThemePurple);
            this.ribThemeSelect.Image = ((System.Drawing.Image)(resources.GetObject("ribThemeSelect.Image")));
            this.ribThemeSelect.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribThemeSelect.SmallImage")));
            this.ribThemeSelect.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            resources.ApplyResources(this.ribThemeSelect, "ribThemeSelect");
            // 
            // cmdThemeBlue
            // 
            this.cmdThemeBlue.CheckedGroup = "0";
            this.cmdThemeBlue.CheckOnClick = true;
            this.cmdThemeBlue.DrawIconsBar = false;
            this.cmdThemeBlue.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.cmdThemeBlue.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemeBlue.Image")));
            this.cmdThemeBlue.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdThemeBlue.SmallImage")));
            resources.ApplyResources(this.cmdThemeBlue, "cmdThemeBlue");
            this.cmdThemeBlue.Value = "1";
            this.cmdThemeBlue.Click += new System.EventHandler(this.cmdThemeBlack_Click);
            // 
            // cmdThemeBlack
            // 
            this.cmdThemeBlack.CheckedGroup = "0";
            this.cmdThemeBlack.CheckOnClick = true;
            this.cmdThemeBlack.DrawIconsBar = false;
            this.cmdThemeBlack.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.cmdThemeBlack.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemeBlack.Image")));
            this.cmdThemeBlack.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdThemeBlack.SmallImage")));
            resources.ApplyResources(this.cmdThemeBlack, "cmdThemeBlack");
            this.cmdThemeBlack.Value = "2";
            this.cmdThemeBlack.Click += new System.EventHandler(this.cmdThemeBlack_Click);
            // 
            // cmdThemeGreen
            // 
            this.cmdThemeGreen.CheckedGroup = "0";
            this.cmdThemeGreen.CheckOnClick = true;
            this.cmdThemeGreen.DrawIconsBar = false;
            this.cmdThemeGreen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.cmdThemeGreen.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemeGreen.Image")));
            this.cmdThemeGreen.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdThemeGreen.SmallImage")));
            resources.ApplyResources(this.cmdThemeGreen, "cmdThemeGreen");
            this.cmdThemeGreen.Value = "3";
            this.cmdThemeGreen.Click += new System.EventHandler(this.cmdThemeBlack_Click);
            // 
            // cmdThemePurple
            // 
            this.cmdThemePurple.Image = ((System.Drawing.Image)(resources.GetObject("cmdThemePurple.Image")));
            this.cmdThemePurple.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdThemePurple.SmallImage")));
            resources.ApplyResources(this.cmdThemePurple, "cmdThemePurple");
            this.cmdThemePurple.Value = "4";
            this.cmdThemePurple.Click += new System.EventHandler(this.cmdThemeBlack_Click);
            // 
            // ribStyleSelect
            // 
            this.ribStyleSelect.DropDownItems.Add(this.cmdStyle2007);
            this.ribStyleSelect.DropDownItems.Add(this.cmdStyle2010);
            this.ribStyleSelect.DropDownItems.Add(this.cmdStyle2013);
            this.ribStyleSelect.Image = ((System.Drawing.Image)(resources.GetObject("ribStyleSelect.Image")));
            this.ribStyleSelect.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribStyleSelect.SmallImage")));
            this.ribStyleSelect.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            resources.ApplyResources(this.ribStyleSelect, "ribStyleSelect");
            // 
            // cmdStyle2007
            // 
            this.cmdStyle2007.CheckedGroup = "0";
            this.cmdStyle2007.CheckOnClick = true;
            this.cmdStyle2007.DrawIconsBar = false;
            this.cmdStyle2007.Image = ((System.Drawing.Image)(resources.GetObject("cmdStyle2007.Image")));
            this.cmdStyle2007.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdStyle2007.SmallImage")));
            resources.ApplyResources(this.cmdStyle2007, "cmdStyle2007");
            this.cmdStyle2007.Value = "0";
            this.cmdStyle2007.Click += new System.EventHandler(this.cmdStyle2010_Click);
            // 
            // cmdStyle2010
            // 
            this.cmdStyle2010.CheckedGroup = "0";
            this.cmdStyle2010.CheckOnClick = true;
            this.cmdStyle2010.DrawIconsBar = false;
            this.cmdStyle2010.Image = ((System.Drawing.Image)(resources.GetObject("cmdStyle2010.Image")));
            this.cmdStyle2010.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdStyle2010.SmallImage")));
            resources.ApplyResources(this.cmdStyle2010, "cmdStyle2010");
            this.cmdStyle2010.Value = "1";
            this.cmdStyle2010.Click += new System.EventHandler(this.cmdStyle2010_Click);
            // 
            // cmdStyle2013
            // 
            this.cmdStyle2013.CheckedGroup = "0";
            this.cmdStyle2013.CheckOnClick = true;
            this.cmdStyle2013.DrawIconsBar = false;
            this.cmdStyle2013.Image = ((System.Drawing.Image)(resources.GetObject("cmdStyle2013.Image")));
            this.cmdStyle2013.SmallImage = ((System.Drawing.Image)(resources.GetObject("cmdStyle2013.SmallImage")));
            resources.ApplyResources(this.cmdStyle2013, "cmdStyle2013");
            this.cmdStyle2013.Value = "2";
            this.cmdStyle2013.Click += new System.EventHandler(this.cmdStyle2010_Click);
            // 
            // ribbonOrbOptionButton1
            // 
            this.ribbonOrbOptionButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonOrbOptionButton1.Image")));
            this.ribbonOrbOptionButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonOrbOptionButton1.SmallImage")));
            resources.ApplyResources(this.ribbonOrbOptionButton1, "ribbonOrbOptionButton1");
            // 
            // status
            // 
            resources.ApplyResources(this.status, "status");
            this.status.Name = "status";
            this.status.ShowItemToolTips = true;
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Tip:";
            // 
            // RibTabMainApp
            // 
            resources.ApplyResources(this.RibTabMainApp, "RibTabMainApp");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.status);
            this.Controls.Add(this.ribbonMain);
            this.HelpButton = true;
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RibbonTab RibTabMain;
        private System.Windows.Forms.RibbonTab RibTabEdit;
        private System.Windows.Forms.RibbonTab RibTabAction;
        private System.Windows.Forms.RibbonTab RibTabFilter;
        private System.Windows.Forms.RibbonPanel RibTabMainFile;
        private System.Windows.Forms.RibbonPanel RibTabMainEdit;
        private System.Windows.Forms.RibbonOrbOptionButton ribOptBtnExit;
        private System.Windows.Forms.RibbonButton cmdFileOpen;
        private System.Windows.Forms.RibbonButton cmdFileSave;
        private System.Windows.Forms.RibbonButton cmdEditCut;
        private System.Windows.Forms.RibbonButton cmdEditCopy;
        private System.Windows.Forms.RibbonButton cmdEditPaste;
        private System.Windows.Forms.RibbonButton cmdEditClear;
        private System.Windows.Forms.RibbonPanel RibTabActManager;
        private System.Windows.Forms.RibbonButton cmdActionReScan;
        private System.Windows.Forms.RibbonPanel RibTabFilterManager;
        private System.Windows.Forms.RibbonButton cmdFilterReScan;
        private System.Windows.Forms.RibbonTab RibTabSetting;
        private System.Windows.Forms.RibbonOrbOptionButton ribOptBtnOptions;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator1;
        private System.Windows.Forms.RibbonButton ribThemeSelect;
        private System.Windows.Forms.RibbonButton cmdThemeBlue;
        private System.Windows.Forms.RibbonButton cmdThemeBlack;
        private System.Windows.Forms.RibbonButton cmdThemeGreen;
        private System.Windows.Forms.RibbonButton ribStyleSelect;
        private System.Windows.Forms.RibbonButton cmdStyle2007;
        private System.Windows.Forms.RibbonButton cmdStyle2010;
        private System.Windows.Forms.RibbonButton cmdStyle2013;
        private System.Windows.Forms.RibbonButton cmdThemePurple;
        private System.Windows.Forms.Ribbon ribbonMain;
        private System.Windows.Forms.RibbonOrbOptionButton ribbonOrbOptionButton1;
        private System.Windows.Forms.RibbonOrbMenuItem ribOrbMiSave;
        private System.Windows.Forms.RibbonOrbMenuItem ribOrbMiOpen;
        private System.Windows.Forms.RibbonPanel RibTabActInternal;
        private System.Windows.Forms.RibbonButtonList ribbonButtonList1;
        private System.Windows.Forms.RibbonButton ribbonButton3;
        private System.Windows.Forms.RibbonButton ribbonButton4;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.RibbonButton ribbonButton2;
        private System.Windows.Forms.RibbonButton ribbonButton5;
        private System.Windows.Forms.RibbonButton ribbonButton6;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButtonList ribbonButtonList2;
        private System.Windows.Forms.RibbonButton ribbonButton7;
        private System.Windows.Forms.RibbonButton ribbonButton8;
        private System.Windows.Forms.RibbonButton ribbonButton9;
        private System.Windows.Forms.RibbonButton ribbonButton10;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButtonList ribbonButtonList3;
        private System.Windows.Forms.RibbonButton ribbonButton11;
        private System.Windows.Forms.RibbonButton ribbonButton12;
        private System.Windows.Forms.RibbonButton ribbonButton13;
        private System.Windows.Forms.RibbonButton ribbonButton14;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RibbonPanel RibTabMainApp;
    }
}

