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
            this.cmdFileSepApply = new System.Windows.Forms.RibbonSeparator();
            this.cmdFileApply = new System.Windows.Forms.RibbonButton();
            this.cmdFileApplyAll = new System.Windows.Forms.RibbonButton();
            this.RibTabMainEdit = new System.Windows.Forms.RibbonPanel();
            this.cmdEditUndo = new System.Windows.Forms.RibbonButton();
            this.cmdEditRedo = new System.Windows.Forms.RibbonButton();
            this.RibTabMainClipboard = new System.Windows.Forms.RibbonPanel();
            this.cmdEditCut = new System.Windows.Forms.RibbonButton();
            this.cmdEditCopy = new System.Windows.Forms.RibbonButton();
            this.cmdEditPaste = new System.Windows.Forms.RibbonButton();
            this.cmdEditClear = new System.Windows.Forms.RibbonButton();
            this.RibTabMainView = new System.Windows.Forms.RibbonPanel();
            this.cmdViewZoomIn = new System.Windows.Forms.RibbonButton();
            this.cmdViewZoomOut = new System.Windows.Forms.RibbonButton();
            this.cmdViewZoomFit = new System.Windows.Forms.RibbonButton();
            this.cmdViewZoom100 = new System.Windows.Forms.RibbonButton();
            this.cmdViewZoomRegion = new System.Windows.Forms.RibbonButton();
            this.RibTabMainApp = new System.Windows.Forms.RibbonPanel();
            this.RibTabAction = new System.Windows.Forms.RibbonTab();
            this.RibTabActInternal = new System.Windows.Forms.RibbonPanel();
            this.RibTabActExternal = new System.Windows.Forms.RibbonPanel();
            this.RibTabEffect = new System.Windows.Forms.RibbonTab();
            this.RibTabEffectInternal = new System.Windows.Forms.RibbonPanel();
            this.RibTabEffectExternal = new System.Windows.Forms.RibbonPanel();
            this.RibTabSystem = new System.Windows.Forms.RibbonTab();
            this.RibTabSysAddinManager = new System.Windows.Forms.RibbonPanel();
            this.cmdAddinReScan = new System.Windows.Forms.RibbonButton();
            this.RibTabSysTools = new System.Windows.Forms.RibbonPanel();
            this.cmdLogger = new System.Windows.Forms.RibbonButton();
            this.RibTabSysAbout = new System.Windows.Forms.RibbonPanel();
            this.cmdAbout = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.status = new System.Windows.Forms.StatusStrip();
            this.tssLabelImageName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLabelImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLabelImageZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLabelImageInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
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
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // RibTabMain
            // 
            this.RibTabMain.Panels.Add(this.RibTabMainFile);
            this.RibTabMain.Panels.Add(this.RibTabMainEdit);
            this.RibTabMain.Panels.Add(this.RibTabMainClipboard);
            this.RibTabMain.Panels.Add(this.RibTabMainView);
            this.RibTabMain.Panels.Add(this.RibTabMainApp);
            resources.ApplyResources(this.RibTabMain, "RibTabMain");
            // 
            // RibTabMainFile
            // 
            this.RibTabMainFile.Items.Add(this.cmdFileOpen);
            this.RibTabMainFile.Items.Add(this.cmdFileSave);
            this.RibTabMainFile.Items.Add(this.cmdFileSepApply);
            this.RibTabMainFile.Items.Add(this.cmdFileApply);
            this.RibTabMainFile.Items.Add(this.cmdFileApplyAll);
            resources.ApplyResources(this.RibTabMainFile, "RibTabMainFile");
            // 
            // cmdFileOpen
            // 
            this.cmdFileOpen.Image = global::PhotoTool.Properties.Resources.Open_32x;
            this.cmdFileOpen.SmallImage = global::PhotoTool.Properties.Resources.Open_16x;
            resources.ApplyResources(this.cmdFileOpen, "cmdFileOpen");
            this.cmdFileOpen.Click += new System.EventHandler(this.cmdFileOpen_Click);
            // 
            // cmdFileSave
            // 
            this.cmdFileSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileSave.Image")));
            this.cmdFileSave.SmallImage = global::PhotoTool.Properties.Resources.Save_16x;
            resources.ApplyResources(this.cmdFileSave, "cmdFileSave");
            this.cmdFileSave.Click += new System.EventHandler(this.cmdFileSave_Click);
            // 
            // cmdFileApply
            // 
            this.cmdFileApply.Image = global::PhotoTool.Properties.Resources.RunChecked_32x;
            this.cmdFileApply.SmallImage = global::PhotoTool.Properties.Resources.RunChecked_16x;
            resources.ApplyResources(this.cmdFileApply, "cmdFileApply");
            // 
            // cmdFileApplyAll
            // 
            this.cmdFileApplyAll.Image = global::PhotoTool.Properties.Resources.Run_32x;
            this.cmdFileApplyAll.SmallImage = global::PhotoTool.Properties.Resources.Run_16x;
            resources.ApplyResources(this.cmdFileApplyAll, "cmdFileApplyAll");
            // 
            // RibTabMainEdit
            // 
            this.RibTabMainEdit.Items.Add(this.cmdEditUndo);
            this.RibTabMainEdit.Items.Add(this.cmdEditRedo);
            resources.ApplyResources(this.RibTabMainEdit, "RibTabMainEdit");
            // 
            // cmdEditUndo
            // 
            this.cmdEditUndo.Image = global::PhotoTool.Properties.Resources.Undo_32x;
            this.cmdEditUndo.SmallImage = global::PhotoTool.Properties.Resources.Undo_16x;
            resources.ApplyResources(this.cmdEditUndo, "cmdEditUndo");
            this.cmdEditUndo.Click += new System.EventHandler(this.cmdEditUndo_Click);
            // 
            // cmdEditRedo
            // 
            this.cmdEditRedo.Image = global::PhotoTool.Properties.Resources.Redo_32x;
            this.cmdEditRedo.SmallImage = global::PhotoTool.Properties.Resources.Redo_16x;
            resources.ApplyResources(this.cmdEditRedo, "cmdEditRedo");
            this.cmdEditRedo.Click += new System.EventHandler(this.cmdEditRedo_Click);
            // 
            // RibTabMainClipboard
            // 
            this.RibTabMainClipboard.Items.Add(this.cmdEditCut);
            this.RibTabMainClipboard.Items.Add(this.cmdEditCopy);
            this.RibTabMainClipboard.Items.Add(this.cmdEditPaste);
            this.RibTabMainClipboard.Items.Add(this.cmdEditClear);
            resources.ApplyResources(this.RibTabMainClipboard, "RibTabMainClipboard");
            // 
            // cmdEditCut
            // 
            this.cmdEditCut.Image = ((System.Drawing.Image)(resources.GetObject("cmdEditCut.Image")));
            this.cmdEditCut.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.cmdEditCut.SmallImage = global::PhotoTool.Properties.Resources.Cut_16x;
            resources.ApplyResources(this.cmdEditCut, "cmdEditCut");
            this.cmdEditCut.ToolTipTitle = "Cut";
            this.cmdEditCut.Click += new System.EventHandler(this.cmdEditCut_Click);
            // 
            // cmdEditCopy
            // 
            this.cmdEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmdEditCopy.Image")));
            this.cmdEditCopy.SmallImage = global::PhotoTool.Properties.Resources.Copy_16x;
            resources.ApplyResources(this.cmdEditCopy, "cmdEditCopy");
            this.cmdEditCopy.Click += new System.EventHandler(this.cmdEditCopy_Click);
            // 
            // cmdEditPaste
            // 
            this.cmdEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("cmdEditPaste.Image")));
            this.cmdEditPaste.SmallImage = global::PhotoTool.Properties.Resources.Paste_16x;
            resources.ApplyResources(this.cmdEditPaste, "cmdEditPaste");
            this.cmdEditPaste.Click += new System.EventHandler(this.cmdEditPaste_Click);
            // 
            // cmdEditClear
            // 
            this.cmdEditClear.Image = global::PhotoTool.Properties.Resources.Clear_32x;
            this.cmdEditClear.SmallImage = global::PhotoTool.Properties.Resources.Clear_16x;
            resources.ApplyResources(this.cmdEditClear, "cmdEditClear");
            this.cmdEditClear.Click += new System.EventHandler(this.cmdEditClear_Click);
            // 
            // RibTabMainView
            // 
            this.RibTabMainView.Items.Add(this.cmdViewZoomIn);
            this.RibTabMainView.Items.Add(this.cmdViewZoomOut);
            this.RibTabMainView.Items.Add(this.cmdViewZoomFit);
            this.RibTabMainView.Items.Add(this.cmdViewZoom100);
            this.RibTabMainView.Items.Add(this.cmdViewZoomRegion);
            resources.ApplyResources(this.RibTabMainView, "RibTabMainView");
            // 
            // cmdViewZoomIn
            // 
            this.cmdViewZoomIn.Image = global::PhotoTool.Properties.Resources.ZoomIn_32x;
            this.cmdViewZoomIn.SmallImage = global::PhotoTool.Properties.Resources.ZoomIn_16x;
            resources.ApplyResources(this.cmdViewZoomIn, "cmdViewZoomIn");
            this.cmdViewZoomIn.Click += new System.EventHandler(this.cmdViewZoomIn_Click);
            // 
            // cmdViewZoomOut
            // 
            this.cmdViewZoomOut.Image = global::PhotoTool.Properties.Resources.ZoomOut_32x;
            this.cmdViewZoomOut.SmallImage = global::PhotoTool.Properties.Resources.ZoomOut_16x;
            resources.ApplyResources(this.cmdViewZoomOut, "cmdViewZoomOut");
            this.cmdViewZoomOut.Click += new System.EventHandler(this.cmdViewZoomOut_Click);
            // 
            // cmdViewZoomFit
            // 
            this.cmdViewZoomFit.Image = global::PhotoTool.Properties.Resources.ZoomToFit_32x;
            this.cmdViewZoomFit.SmallImage = global::PhotoTool.Properties.Resources.ZoomToFit_16x;
            resources.ApplyResources(this.cmdViewZoomFit, "cmdViewZoomFit");
            this.cmdViewZoomFit.Click += new System.EventHandler(this.cmdViewZoomFit_Click);
            // 
            // cmdViewZoom100
            // 
            this.cmdViewZoom100.Image = global::PhotoTool.Properties.Resources.ZoomOriginalSize_32x;
            this.cmdViewZoom100.SmallImage = global::PhotoTool.Properties.Resources.ZoomOriginalSize_16x;
            resources.ApplyResources(this.cmdViewZoom100, "cmdViewZoom100");
            this.cmdViewZoom100.Click += new System.EventHandler(this.cmdViewZoom100_Click);
            // 
            // cmdViewZoomRegion
            // 
            this.cmdViewZoomRegion.Image = global::PhotoTool.Properties.Resources.ZoomToRegion_32x;
            this.cmdViewZoomRegion.SmallImage = global::PhotoTool.Properties.Resources.ZoomToRegion_16x;
            resources.ApplyResources(this.cmdViewZoomRegion, "cmdViewZoomRegion");
            this.cmdViewZoomRegion.Click += new System.EventHandler(this.cmdViewZoomRegion_Click);
            // 
            // RibTabMainApp
            // 
            resources.ApplyResources(this.RibTabMainApp, "RibTabMainApp");
            // 
            // RibTabAction
            // 
            this.RibTabAction.Panels.Add(this.RibTabActInternal);
            this.RibTabAction.Panels.Add(this.RibTabActExternal);
            resources.ApplyResources(this.RibTabAction, "RibTabAction");
            // 
            // RibTabActInternal
            // 
            resources.ApplyResources(this.RibTabActInternal, "RibTabActInternal");
            // 
            // RibTabActExternal
            // 
            resources.ApplyResources(this.RibTabActExternal, "RibTabActExternal");
            // 
            // RibTabEffect
            // 
            this.RibTabEffect.Panels.Add(this.RibTabEffectInternal);
            this.RibTabEffect.Panels.Add(this.RibTabEffectExternal);
            resources.ApplyResources(this.RibTabEffect, "RibTabEffect");
            // 
            // RibTabEffectInternal
            // 
            resources.ApplyResources(this.RibTabEffectInternal, "RibTabEffectInternal");
            // 
            // RibTabEffectExternal
            // 
            resources.ApplyResources(this.RibTabEffectExternal, "RibTabEffectExternal");
            // 
            // RibTabSystem
            // 
            this.RibTabSystem.Panels.Add(this.RibTabSysAddinManager);
            this.RibTabSystem.Panels.Add(this.RibTabSysTools);
            this.RibTabSystem.Panels.Add(this.RibTabSysAbout);
            resources.ApplyResources(this.RibTabSystem, "RibTabSystem");
            // 
            // RibTabSysAddinManager
            // 
            this.RibTabSysAddinManager.Items.Add(this.cmdAddinReScan);
            resources.ApplyResources(this.RibTabSysAddinManager, "RibTabSysAddinManager");
            // 
            // cmdAddinReScan
            // 
            this.cmdAddinReScan.Image = global::PhotoTool.Properties.Resources.AddIn_32x;
            this.cmdAddinReScan.SmallImage = global::PhotoTool.Properties.Resources.AddIn_16x;
            resources.ApplyResources(this.cmdAddinReScan, "cmdAddinReScan");
            this.cmdAddinReScan.Click += new System.EventHandler(this.cmdAddinReScan_Click);
            // 
            // RibTabSysTools
            // 
            this.RibTabSysTools.Items.Add(this.cmdLogger);
            resources.ApplyResources(this.RibTabSysTools, "RibTabSysTools");
            // 
            // cmdLogger
            // 
            this.cmdLogger.Image = global::PhotoTool.Properties.Resources.Log_32x;
            this.cmdLogger.SmallImage = global::PhotoTool.Properties.Resources.Log_16x;
            resources.ApplyResources(this.cmdLogger, "cmdLogger");
            this.cmdLogger.Click += new System.EventHandler(this.cmdLogger_Click);
            // 
            // RibTabSysAbout
            // 
            this.RibTabSysAbout.Items.Add(this.cmdAbout);
            resources.ApplyResources(this.RibTabSysAbout, "RibTabSysAbout");
            // 
            // cmdAbout
            // 
            this.cmdAbout.Image = global::PhotoTool.Properties.Resources.About_32x;
            this.cmdAbout.SmallImage = global::PhotoTool.Properties.Resources.About_16x;
            resources.ApplyResources(this.cmdAbout, "cmdAbout");
            this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssLabelImageName,
            this.tssLabelImageSize,
            this.tssLabelImageZoom,
            this.tssLabelImageInfo});
            resources.ApplyResources(this.status, "status");
            this.status.Name = "status";
            this.status.ShowItemToolTips = true;
            // 
            // tssLabelImageName
            // 
            this.tssLabelImageName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLabelImageName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tssLabelImageName.Name = "tssLabelImageName";
            resources.ApplyResources(this.tssLabelImageName, "tssLabelImageName");
            // 
            // tssLabelImageSize
            // 
            this.tssLabelImageSize.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLabelImageSize.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tssLabelImageSize.Name = "tssLabelImageSize";
            resources.ApplyResources(this.tssLabelImageSize, "tssLabelImageSize");
            // 
            // tssLabelImageZoom
            // 
            this.tssLabelImageZoom.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLabelImageZoom.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tssLabelImageZoom.Name = "tssLabelImageZoom";
            resources.ApplyResources(this.tssLabelImageZoom, "tssLabelImageZoom");
            // 
            // tssLabelImageInfo
            // 
            this.tssLabelImageInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssLabelImageInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tssLabelImageInfo.Name = "tssLabelImageInfo";
            resources.ApplyResources(this.tssLabelImageInfo, "tssLabelImageInfo");
            this.tssLabelImageInfo.Spring = true;
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Tip:";
            // 
            // dlgOpen
            // 
            this.dlgOpen.DefaultExt = "jpg";
            resources.ApplyResources(this.dlgOpen, "dlgOpen");
            this.dlgOpen.Multiselect = true;
            this.dlgOpen.ShowHelp = true;
            this.dlgOpen.SupportMultiDottedExtensions = true;
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "jpg";
            resources.ApplyResources(this.dlgSave, "dlgSave");
            this.dlgSave.ShowHelp = true;
            this.dlgSave.SupportMultiDottedExtensions = true;
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
            this.ribbonMain.OrbImage = ((System.Drawing.Image)(resources.GetObject("ribbonMain.OrbImage")));
            this.ribbonMain.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010;
            this.ribbonMain.PanelCaptionHeight = 20;
            // 
            // 
            // 
            this.ribbonMain.QuickAcessToolbar.Visible = false;
            this.ribbonMain.RibbonTabFont = new System.Drawing.Font("Segoe UI", 10F);
            this.ribbonMain.Tabs.Add(this.RibTabMain);
            this.ribbonMain.Tabs.Add(this.RibTabAction);
            this.ribbonMain.Tabs.Add(this.RibTabEffect);
            this.ribbonMain.Tabs.Add(this.RibTabSystem);
            this.ribbonMain.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbonMain.ThemeColor = System.Windows.Forms.RibbonTheme.Black;
            // 
            // ribOrbMiOpen
            // 
            this.ribOrbMiOpen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribOrbMiOpen.Image = global::PhotoTool.Properties.Resources.Open_32x;
            this.ribOrbMiOpen.SmallImage = global::PhotoTool.Properties.Resources.Open_32x;
            this.ribOrbMiOpen.Style = System.Windows.Forms.RibbonButtonStyle.SplitDropDown;
            resources.ApplyResources(this.ribOrbMiOpen, "ribOrbMiOpen");
            this.ribOrbMiOpen.Click += new System.EventHandler(this.cmdFileOpen_Click);
            // 
            // ribOrbMiSave
            // 
            this.ribOrbMiSave.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribOrbMiSave.Image = ((System.Drawing.Image)(resources.GetObject("ribOrbMiSave.Image")));
            this.ribOrbMiSave.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribOrbMiSave.SmallImage")));
            this.ribOrbMiSave.Style = System.Windows.Forms.RibbonButtonStyle.SplitDropDown;
            resources.ApplyResources(this.ribOrbMiSave, "ribOrbMiSave");
            this.ribOrbMiSave.Click += new System.EventHandler(this.cmdFileSave_Click);
            // 
            // ribOptBtnExit
            // 
            this.ribOptBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("ribOptBtnExit.Image")));
            this.ribOptBtnExit.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribOptBtnExit.SmallImage")));
            resources.ApplyResources(this.ribOptBtnExit, "ribOptBtnExit");
            this.ribOptBtnExit.Click += new System.EventHandler(this.ribOptBtnExit_Click);
            // 
            // ribOptBtnOptions
            // 
            this.ribOptBtnOptions.Image = ((System.Drawing.Image)(resources.GetObject("ribOptBtnOptions.Image")));
            this.ribOptBtnOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribOptBtnOptions.SmallImage")));
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
            // MainForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.status);
            this.Controls.Add(this.ribbonMain);
            this.HelpButton = true;
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RibbonTab RibTabMain;
        private System.Windows.Forms.RibbonTab RibTabAction;
        private System.Windows.Forms.RibbonTab RibTabEffect;
        private System.Windows.Forms.RibbonPanel RibTabMainFile;
        private System.Windows.Forms.RibbonPanel RibTabMainClipboard;
        private System.Windows.Forms.RibbonOrbOptionButton ribOptBtnExit;
        private System.Windows.Forms.RibbonButton cmdFileOpen;
        private System.Windows.Forms.RibbonButton cmdFileSave;
        private System.Windows.Forms.RibbonButton cmdEditCut;
        private System.Windows.Forms.RibbonButton cmdEditCopy;
        private System.Windows.Forms.RibbonButton cmdEditPaste;
        private System.Windows.Forms.RibbonButton cmdEditClear;
        private System.Windows.Forms.RibbonTab RibTabSystem;
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
        private System.Windows.Forms.RibbonOrbMenuItem ribOrbMiSave;
        private System.Windows.Forms.RibbonOrbMenuItem ribOrbMiOpen;
        private System.Windows.Forms.RibbonPanel RibTabActInternal;
        private System.Windows.Forms.RibbonPanel RibTabEffectInternal;
        private System.Windows.Forms.RibbonPanel RibTabEffectExternal;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RibbonPanel RibTabMainApp;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.RibbonPanel RibTabActExternal;
        private System.Windows.Forms.ToolStripStatusLabel tssLabelImageName;
        private System.Windows.Forms.ToolStripStatusLabel tssLabelImageSize;
        private System.Windows.Forms.ToolStripStatusLabel tssLabelImageInfo;
        private System.Windows.Forms.RibbonPanel RibTabMainView;
        private System.Windows.Forms.RibbonButton cmdViewZoomIn;
        private System.Windows.Forms.RibbonButton cmdViewZoomOut;
        private System.Windows.Forms.RibbonButton cmdViewZoomFit;
        private System.Windows.Forms.RibbonButton cmdViewZoom100;
        private System.Windows.Forms.RibbonButton cmdViewZoomRegion;
        private System.Windows.Forms.ToolStripStatusLabel tssLabelImageZoom;
        private System.Windows.Forms.RibbonPanel RibTabSysAddinManager;
        private System.Windows.Forms.RibbonButton cmdAddinReScan;
        private System.Windows.Forms.RibbonButton cmdFileApply;
        private System.Windows.Forms.RibbonSeparator cmdFileSepApply;
        private System.Windows.Forms.RibbonPanel RibTabSysTools;
        private System.Windows.Forms.RibbonButton cmdLogger;
        private System.Windows.Forms.RibbonPanel RibTabSysAbout;
        private System.Windows.Forms.RibbonButton cmdAbout;
        private System.Windows.Forms.RibbonPanel RibTabMainEdit;
        private System.Windows.Forms.RibbonButton cmdEditUndo;
        private System.Windows.Forms.RibbonButton cmdEditRedo;
        private System.Windows.Forms.RibbonButton cmdFileApplyAll;
    }
}

