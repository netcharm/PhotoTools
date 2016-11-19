using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image.Addins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NGettext.WinForm;

namespace PhotoTool
{
    public partial class MainForm : RibbonForm
    {
        internal string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

        private Dictionary<string, object> settings = new Dictionary<string, object>();
        private LogForm fmLog;

        #region Style / Theme Change Routine
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        private void ChangeTheme( RibbonTheme theme = RibbonTheme.Normal )
        {
            switch ( theme )
            {
                case RibbonTheme.Black:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableBlack();
                    break;
                case RibbonTheme.Green:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableGreen();
                    break;
                case RibbonTheme.Halloween:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableHalloween();
                    break;
                case RibbonTheme.JellyBelly:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableJellyBelly();
                    break;
                case RibbonTheme.Purple:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTablePurple();
                    break;
                case RibbonTheme.Blue:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableBlue();
                    break;
                case RibbonTheme.Normal:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTable();
                    break;
                default:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTable();
                    break;
            }

            if ( ribbonMain.OrbStyle == RibbonOrbStyle.Office_2007 )
            {
                status.BackColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonBackground );
                status.ForeColor = Theme.ColorTable.GetColor( RibbonColorPart.Text );
            }
            else if ( ribbonMain.OrbStyle == RibbonOrbStyle.Office_2010 )
            {
                status.BackColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonBackground );
                status.ForeColor = Theme.ColorTable.GetColor( RibbonColorPart.Text );
            }
            else if ( ribbonMain.OrbStyle == RibbonOrbStyle.Office_2013 )
            {
                status.BackColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonBackground_2013 );
                status.ForeColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonItemText_2013 );
            }
            Theme.ThemeColor = theme;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        private void ChangeStyle( RibbonOrbStyle style = RibbonOrbStyle.Office_2010 )
        {
            ribbonMain.OrbStyle = style;
            if ( ribbonMain.OrbStyle == RibbonOrbStyle.Office_2007 )
            {
                status.BackColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonBackground );
                status.ForeColor = Theme.ColorTable.GetColor( RibbonColorPart.Text );
            }
            else if ( ribbonMain.OrbStyle == RibbonOrbStyle.Office_2010 )
            {
                status.BackColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonBackground );
                status.ForeColor = Theme.ColorTable.GetColor( RibbonColorPart.Text );
            }
            else if ( ribbonMain.OrbStyle == RibbonOrbStyle.Office_2013 )
            {
                status.BackColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonBackground_2013 );
                status.ForeColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonItemText_2013 );
            }
        }

        #endregion Style / Theme Change Routine

        #region Addin Loading Routine
        internal Dictionary<string, RibbonPanel> CustomPanelApp = new Dictionary<string, RibbonPanel>();
        internal Dictionary<string, RibbonPanel> CustomPanelAction = new Dictionary<string, RibbonPanel>();
        internal Dictionary<string, RibbonPanel> CustomPanelEffect = new Dictionary<string, RibbonPanel>();

        private AddinHost addins = new AddinHost();

        internal void FixedMdiSize()
        {
            this.Height -= 1;
            this.Height += 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apps"></param>
        private void AddAddinApp( List<IAddin> apps )
        {
            RibTabMainApp.Items.Clear();
            CustomPanelApp.Clear();

            foreach ( IAddin addin in apps )
            {
                RibbonButton btnAddin = new RibbonButton();
                RibTabMainApp.Items.Add( btnAddin );

                if ( addin.LargeIcon != null )
                    btnAddin.Image = addin.LargeIcon;
                else
                    btnAddin.Image = addins.LargeApplicationImage;
                if ( addin.SmallIcon != null )
                    btnAddin.SmallImage = addin.SmallIcon;
                else
                    btnAddin.SmallImage = addins.SmallApplicationImage;

                btnAddin.Text = I18N._( addin.DisplayName );
                btnAddin.ToolTip = I18N._( addin.Description );
                btnAddin.ToolTipTitle = I18N._( addin.Author );
                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinAppClick;

                #region fetch subitems of addin
                object result = null;
                addin.Command( AddinCommand.SubItems, out result );
                if(result is Dictionary<string, object> )
                {
                    foreach(var item in ( result as Dictionary<string, object> ))
                    {
                        //var smi = new RibbonButton( item.Key );
                        //smi.SmallImage = item.Value.
                        //btnAddin.DropDownItems.Add( smi );
                        //b
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acts"></param>
        /// <param name="IsExt"></param>
        private void AddAddinAction( List<IAddin> acts, bool IsExt = true )
        {
            RibTabActInternal.Items.Clear();
            RibTabActExternal.Items.Clear();

            #region Clear Custom Actions Panel
            foreach ( var kv in CustomPanelAction )
            {
                if ( kv.Value is RibbonPanel )
                {
                    RibTabAction.Panels.Remove( kv.Value );
                    kv.Value.Dispose();
                }
            }
            CustomPanelAction.Clear();
            #endregion

            #region Add addin to Command Panels
            foreach ( IAddin addin in acts )
            {
                if ( !addin.Visible ) continue;

                RibbonPanel targetPanel = RibTabActExternal;
                if ( !string.IsNullOrEmpty( addin.CategoryName ) )
                {
                    if ( !CustomPanelAction.ContainsKey( addin.CategoryName ) )
                        CustomPanelAction.Add( addin.CategoryName, new RibbonPanel( I18N._( addin.DisplayCategoryName ) ) );
                    targetPanel = CustomPanelAction[addin.CategoryName];
                }
                else if ( string.Equals( addin.Author, "netcharm", StringComparison.CurrentCultureIgnoreCase ) ||
                    addin.Author.StartsWith( "NetCharm ", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    targetPanel = RibTabActInternal;
                }
                else
                {
                    targetPanel = RibTabActExternal;
                }

                RibbonButton btnAddin = new RibbonButton();
                targetPanel.Items.Add( btnAddin );

                if ( addin.LargeIcon != null )
                    btnAddin.Image = addin.LargeIcon;
                else
                    btnAddin.Image = addins.LargeActionImage;
                    //btnAddin.Image = addins.LargeImage;
                if ( addin.SmallIcon != null )
                    btnAddin.SmallImage = addin.SmallIcon;
                else
                    btnAddin.SmallImage = addins.SmallActionImage;
                    //btnAddin.SmallImage = addins.SmallImage;

                btnAddin.Text = I18N._( addin.DisplayName );
                btnAddin.ToolTip = I18N._( addin.Description );
                btnAddin.ToolTipTitle = I18N._( addin.Author );
                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinActionClick;

                #region fetch subitems of addin
                object result = null;
                addin.Command( AddinCommand.SubItems, out result );
                if ( result is List<AddinSubItem> && ( result as List<AddinSubItem> ).Count > 0 )
                {
                    foreach ( var item in ( result as List<AddinSubItem> ) )
                    {
                        var smi = new RibbonButton( item.Name );
                        smi.Text = item.DisplayName;
                        smi.Image = item.LargeIcon;
                        smi.SmallImage = item.SmallIcon;
                        smi.Value = item.Name;
                        //smi.Click += AddinActionSubItemClick;

                        btnAddin.DropDownItems.Add( smi );
                    }
                    //btnAddin.DrawIconsBar = false;
                    btnAddin.Style = RibbonButtonStyle.SplitDropDown;
                    btnAddin.DropDownItemClicked += AddinActionSubItemClick;
                }
                #endregion
            }
            #endregion

            #region Refresh Panels Visible state
            int c = 0;
            foreach ( var kv in CustomPanelAction )
            {
                RibTabAction.Panels.Insert( c, kv.Value );
                c++;
            }
            if ( RibTabActInternal.Items.Count == 0 )
                RibTabActInternal.Visible = false;
            else
                RibTabActInternal.Visible = true;
            if ( RibTabActExternal.Items.Count == 0 )
                RibTabActExternal.Visible = false;
            else
                RibTabActExternal.Visible = true;
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="IsExt"></param>
        private void AddAddinEffect( List<IAddin> filters, bool IsExt = true )
        {
            RibTabEffectInternal.Items.Clear();
            RibTabEffectExternal.Items.Clear();

            #region Clear Custom Actions Panel
            foreach (var kv in CustomPanelEffect )
            {
                if ( kv.Value is RibbonPanel)
                {
                    RibTabEffect.Panels.Remove( kv.Value );
                    kv.Value.Dispose();
                }
            }
            CustomPanelEffect.Clear();
            #endregion

            #region Add addin to Command Panels
            foreach ( IAddin addin in filters )
            {
                if ( !addin.Visible ) continue;

                RibbonPanel targetPanel = RibTabEffectExternal;
                if(!string.IsNullOrEmpty(addin.CategoryName))
                {
                    if(!CustomPanelEffect.ContainsKey( addin.CategoryName ) )
                        CustomPanelEffect.Add( addin.CategoryName, new RibbonPanel( I18N._(addin.DisplayCategoryName) ) );
                    targetPanel = CustomPanelEffect[addin.CategoryName];
                }
                else if ( string.Equals( addin.Author, "netcharm", StringComparison.CurrentCultureIgnoreCase ) ||
                    addin.Author.StartsWith( "NetCharm ", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    targetPanel = RibTabEffectInternal;
                }
                else
                {
                    targetPanel = RibTabEffectExternal;
                }

                RibbonButton btnAddin = new RibbonButton();
                targetPanel.Items.Add( btnAddin );

                if ( addin.LargeIcon != null )
                    btnAddin.Image = addin.LargeIcon;
                else
                    btnAddin.Image = addins.LargeEffectImage;
                if ( addin.SmallIcon != null )
                    btnAddin.SmallImage = addin.SmallIcon;
                else
                    btnAddin.SmallImage = addins.SmallEffectImage;

                btnAddin.Text = I18N._( addin.DisplayName );
                btnAddin.ToolTip = I18N._( addin.Description );
                btnAddin.ToolTipTitle = I18N._( addin.Author );
                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinEffectClick;

                #region fetch subitems of addin
                object result = null;
                addin.Command( AddinCommand.SubItems, out result );
                if ( result is List<AddinSubItem> && ( result as List<AddinSubItem> ).Count > 0 )
                {
                    foreach ( var item in ( result as List<AddinSubItem> ) )
                    {
                        var smi = new RibbonButton( item.Name );
                        smi.Text = item.DisplayName;
                        smi.Image = item.LargeIcon;
                        smi.SmallImage = item.SmallIcon;
                        smi.Value = item.Name;
                        //smi.Click += AddinActionSubItemClick;

                        btnAddin.DropDownItems.Add( smi );
                    }
                    //btnAddin.DrawIconsBar = false;
                    btnAddin.Style = RibbonButtonStyle.SplitDropDown;
                    btnAddin.DropDownItemClicked += AddinEffectSubItemClick;
                }
                #endregion
            }
            #endregion

            #region Refresh Panels Visible state
            int c = 0;
            foreach ( var kv in CustomPanelEffect )
            {
                RibTabEffect.Panels.Insert( c, kv.Value );
                c++;
            }
            if ( RibTabEffectInternal.Items.Count == 0 )
                RibTabEffectInternal.Visible = false;
            else
                RibTabEffectInternal.Visible = true;
            if ( RibTabEffectExternal.Items.Count == 0 )
                RibTabEffectExternal.Visible = false;
            else
                RibTabEffectExternal.Visible = true;
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddinAppClick( object sender, EventArgs e )
        {
            string an = ( sender as RibbonButton ).Value;
            if ( addins.Apps.ContainsKey( an ) )
            {
                addins.CurrentApp = addins.Apps[an];
                if ( addins.CurrentApp != null )
                {
                    addins.CurrentApp.Show( this, false );
                    FixedMdiSize();
                }
            }
            cmdFileApply.Visible = addins.CurrentApp is IAddin ? addins.CurrentApp.SupportMultiFile : false;
            cmdFileApplyAll.Visible = addins.CurrentApp is IAddin ? addins.CurrentApp.SupportMultiFile : false;
            cmdFileSepApply.Visible = addins.CurrentApp is IAddin ? addins.CurrentApp.SupportMultiFile : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddinAppSubItemClick( object sender, EventArgs e )
        {
            string an = ( sender as RibbonButton ).Value;
            string ans = ( sender as RibbonButton ).SelectedValue;
            if ( addins.CurrentApp != null && addins.Apps.ContainsKey( an ) )
            {
                addins.CurrentFilter = addins.Apps[an];
                if ( addins.CurrentFilter != null )
                {
                    addins.CurrentFilter.ImageData = addins.CurrentApp.ImageData;
                    object data = null;
                    addins.CurrentFilter.Command( AddinCommand.SubItems, out data, ans );
                    if ( addins.CurrentFilter.Success )
                    {
                        addins.CurrentApp.ImageData = addins.CurrentFilter.ImageData;
                    }

                    int bits = AddinUtils.GetColorDeep(addins.CurrentApp.ImageData.PixelFormat);
                    tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height} x {bits}";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddinActionClick( object sender, EventArgs e )
        {
            string an = ( sender as RibbonButton ).Value;
            if ( addins.CurrentApp != null && addins.Actions.ContainsKey( an ) )
            {
                addins.CurrentFilter = addins.Actions[an];
                if ( addins.CurrentFilter != null )
                {
                    addins.CurrentFilter.ImageData = addins.CurrentApp.ImageData;
                    addins.CurrentFilter.Show( this, false );
                    if ( addins.CurrentFilter.Success )
                    {
                        addins.CurrentApp.ImageData = addins.CurrentFilter.ImageData;
                    }

                    int bits = AddinUtils.GetColorDeep(addins.CurrentApp.ImageData.PixelFormat);
                    tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height} x {bits}";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddinActionSubItemClick( object sender, EventArgs e )
        {
            string an = ( sender as RibbonButton ).Value;
            string ans = ( sender as RibbonButton ).SelectedValue;
            if ( addins.CurrentApp != null && addins.Actions.ContainsKey( an ) )
            {
                addins.CurrentFilter = addins.Actions[an];
                if ( addins.CurrentFilter != null )
                {
                    addins.CurrentFilter.ImageData = addins.CurrentApp.ImageData;
                    object data = null;
                    addins.CurrentFilter.Command( AddinCommand.SubItems, out data, ans );
                    if ( addins.CurrentFilter.Success )
                    {
                        addins.CurrentApp.ImageData = addins.CurrentFilter.ImageData;
                    }

                    int bits = AddinUtils.GetColorDeep(addins.CurrentApp.ImageData.PixelFormat);
                    tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height} x {bits}";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddinEffectClick( object sender, EventArgs e )
        {
            string an = ( sender as RibbonButton ).Value;
            if ( addins.CurrentApp != null && addins.Effects.ContainsKey( an ) )
            {
                addins.CurrentFilter = addins.Effects[an];
                if ( addins.CurrentFilter != null )
                {
                    addins.CurrentFilter.ImageData = addins.CurrentApp.ImageData;
                    addins.CurrentFilter.Show( this, false );
                    if ( addins.CurrentFilter.Success )
                    {
                        addins.CurrentApp.ImageData = addins.CurrentFilter.ImageData;
                    }

                    int bits = AddinUtils.GetColorDeep(addins.CurrentApp.ImageData.PixelFormat);
                    tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height} x {bits}";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddinEffectSubItemClick( object sender, EventArgs e )
        {
            string an = ( sender as RibbonButton ).Value;
            string ans = ( sender as RibbonButton ).SelectedValue;
            if ( addins.CurrentApp != null && addins.Effects.ContainsKey( an ) )
            {
                addins.CurrentFilter = addins.Effects[an];
                if ( addins.CurrentFilter != null )
                {
                    addins.CurrentFilter.ImageData = addins.CurrentApp.ImageData;
                    object data = null;
                    addins.CurrentFilter.Command( AddinCommand.SubItems, out data, ans );
                    if ( addins.CurrentFilter.Success )
                    {
                        addins.CurrentApp.ImageData = addins.CurrentFilter.ImageData;
                    }

                    int bits = AddinUtils.GetColorDeep(addins.CurrentApp.ImageData.PixelFormat);
                    tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height} x {bits}";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddinsCommandPropertiesChange( object sender, CommandPropertiesChangeEventArgs e )
        {
            switch ( e.Command )
            {
                case AddinCommand.Undo:
                    if ( e.Property is bool )
                    {
                        cmdEditUndo.Enabled = (bool) e.Property;
                        cmdQUndo.Enabled = (bool) e.Property;
                    }
                    break;
                case AddinCommand.Redo:
                    if ( e.Property is bool )
                    {
                        cmdEditRedo.Enabled = (bool) e.Property;
                        cmdQRedo.Enabled = (bool) e.Property;
                    }
                    break;
                case AddinCommand.Log:
                    if ( fmLog is LogForm && !fmLog.IsDisposed  )
                    {
                        fmLog.Log( $"{e.Property}" );
                    }
                    break;
                case AddinCommand.ZoomLevel:
                    if ( e.Property is int || e.Property is decimal || e.Property is double || e.Property is float )
                    {
                        tssLabelImageZoom.Text = $"{e.Property}%";
                    }
                    break;
                case AddinCommand.GetImageName:
                    if ( e.Property is string )
                    {
                        tssLabelImageName.Text = $"{Path.GetFileName( (string) e.Property )}";
                    }
                    break;
                case AddinCommand.GetImageSize:
                    if ( e.Property is Size )
                    {
                        tssLabelImageSize.Text = $"{( (Size) e.Property ).Width} x {( (Size) e.Property ).Height}";
                    }
                    break;
                case AddinCommand.GetImageSelection:
                    object data = null;
                    object selection = null;
                    addins.CurrentApp.Command( AddinCommand.GetImageSelection, out selection );
                    addins.CurrentFilter.Command( AddinCommand.SetImageSelection, out data, selection );
                    break;
                case AddinCommand.SetImageSelection:
                    addins.CurrentApp.Command( AddinCommand.SetImageSelection, out data, e.Property );
                    break;
                case AddinCommand.ApplyTiming:
                    tssLabelTimeCost.Text = $"{(float)e.Property:F4}s";
                    break;
                default:
                    break;
            }
            if( addins.CurrentApp is IAddin && addins.CurrentApp.ImageData is Image)
            {
                int bits = AddinUtils.GetColorDeep(addins.CurrentApp.ImageData.PixelFormat);
                tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height} x {bits}";
            }
        }

        #endregion Addin Loading Routine

        #region Ribbon Localization Routines
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private void TranslateRibbonItems( RibbonItemCollection items )
        {
            if ( items == null || items.Count <= 0 ) return;

            foreach ( RibbonItem item in items )
            {
                item.Text = I18N._( item.Text );
                item.ToolTip = I18N._( item.ToolTip );
                item.ToolTipTitle = I18N._( item.ToolTipTitle );

                if ( item.GetType().ToString().EndsWith( "RibbonButton", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    var btn = item as RibbonButton;
                    if ( btn.Style != RibbonButtonStyle.Normal )
                    {
                        TranslateRibbonItems( btn.DropDownItems );
                    }
                }
                else if ( item.GetType().ToString().EndsWith( "RibbonButtonList", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    TranslateRibbonItems( item as RibbonButtonList );
                }
                else if ( item.GetType().ToString().EndsWith( "RibbonButtonCollection", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    //TranslateRibbonItems( (RibbonItemCollection) item. );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private void TranslateRibbonItems( RibbonButtonCollection items )
        {
            if ( items == null || items.Count() <= 0 ) return;

            foreach ( RibbonButton item in items )
            {
                item.Text = I18N._( item.Text );
                item.ToolTip = I18N._( item.ToolTip );
                item.ToolTipTitle = I18N._( item.ToolTipTitle );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private void TranslateRibbonItems( RibbonButtonList items )
        {
            if ( items == null || items.Buttons.Count <= 0 ) return;

            foreach ( RibbonButton item in items.Buttons )
            {
                item.Text = I18N._( item.Text );
                item.ToolTip = I18N._( item.ToolTip );
                item.ToolTipTitle = I18N._( item.ToolTipTitle );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rib"></param>
        public void TranslateRibbon( Ribbon rib )
        {
            rib.Text = I18N._( rib.Text );

            #region Ribbon QuickAccessToolbar
            rib.QuickAcessToolbar.Text = I18N._( rib.QuickAcessToolbar.Text );
            rib.QuickAcessToolbar.ToolTip = I18N._( rib.QuickAcessToolbar.ToolTip );
            rib.QuickAcessToolbar.ToolTipTitle = I18N._( rib.QuickAcessToolbar.ToolTipTitle );
            rib.QuickAcessToolbar.DropDownButton.Text = I18N._( rib.QuickAcessToolbar.DropDownButton.Text );
            rib.QuickAcessToolbar.DropDownButton.ToolTip = I18N._( rib.QuickAcessToolbar.DropDownButton.ToolTip );
            rib.QuickAcessToolbar.DropDownButton.ToolTipTitle = I18N._( rib.QuickAcessToolbar.DropDownButton.ToolTipTitle );
            TranslateRibbonItems( rib.QuickAcessToolbar.Items );
            //foreach ( RibbonItem item in rib.QuickAcessToolbar.Items )
            //{
            //    item.Text = I18N._( item.Text );
            //    item.ToolTip = I18N._( item.ToolTip );
            //    item.ToolTipTitle = I18N._( item.ToolTipTitle );
            //}
            TranslateRibbonItems( rib.QuickAcessToolbar.DropDownButtonItems );
            //foreach ( RibbonItem item in rib.QuickAcessToolbar.DropDownButtonItems )
            //{
            //    item.Text = I18N._( item.Text );
            //    item.ToolTip = I18N._( item.ToolTip );
            //    item.ToolTipTitle = I18N._( item.ToolTipTitle );
            //}
            #endregion

            #region Ribbon OrbDropDown
            rib.OrbDropDown.Text = I18N._( rib.QuickAcessToolbar.Text );
            TranslateRibbonItems( rib.OrbDropDown.MenuItems );
            TranslateRibbonItems( rib.OrbDropDown.OptionItems );

            rib.OrbDropDown.RecentItemsCaption = I18N._( rib.OrbDropDown.RecentItemsCaption );
            TranslateRibbonItems( rib.OrbDropDown.RecentItems );
            #endregion

            #region Ribbon Tabs
            foreach ( RibbonTab tab in rib.Tabs )
            {
                tab.Text = I18N._( tab.Text );
                tab.ToolTip = I18N._( tab.ToolTip );
                tab.ToolTipTitle = I18N._( tab.ToolTipTitle );

                #region Ribbon Panel in Tab
                foreach ( RibbonPanel panel in tab.Panels )
                {
                    panel.Text = I18N._( panel.Text );
                    foreach ( RibbonItem item in panel.Items )
                    {
                        item.Text = I18N._( item.Text );
                        item.ToolTip = I18N._( item.ToolTip );
                        item.ToolTipTitle = I18N._( item.ToolTipTitle );
                    }
                }
                #endregion
            }
            #endregion
        }

        #endregion Ribbon Localization Routines

        #region Ribbon Help Routine & Orb Command Events
        private void RecentItemAdd( string filename )
        {
            //ribbonMain.OrbDropDown.RecentItems.Capacity = 15;
            //ribbonMain.OrbDropDown.RecentItems.TrimExcess();

            RibbonItem result = ribbonMain.OrbDropDown.RecentItems.Find(
                delegate(RibbonItem ri)
                {
                    return string.Equals(ri.Value, filename, StringComparison.CurrentCultureIgnoreCase);
                }
            );
            if ( result != null )
            {
                ribbonMain.OrbDropDown.RecentItems.Remove( result );
            }

            ribbonMain.OrbDropDown.RecentItems.Insert( 0, new RibbonButton( Path.GetFileName(filename) ) );
            ribbonMain.OrbDropDown.RecentItems.First().Value = filename;
            ribbonMain.OrbDropDown.RecentItems.First().Click += RecentItem_Click;

            if( ribbonMain.OrbDropDown.RecentItems.Count >= 15)
            {
                ribbonMain.OrbDropDown.RecentItems.RemoveRange( 15, ribbonMain.OrbDropDown.RecentItems.Count - 15 );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecentItem_Click( object sender, EventArgs e )
        {
            string fn = ( sender as RibbonButton ).Value;
            string[] flist = new string[] { fn };
            OpenCmdArgs( flist );
        }
        #endregion

        #region Command Line Arguments Routines
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetFilenameExtension( ImageFormat format )
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault( x => x.FormatID == format.Guid ).FilenameExtension;
            //ImageFormat.Jpeg.FileExtensionFromEncoder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdline"></param>
        /// <returns></returns>
        private string[] ParseCommandLine( string cmdline )
        {
            List<string> args = new List<string>();

            string[] cmds = cmdline.Split( new char[] { ' ' } );
            string arg = "";
            foreach ( string cmd in cmds )
            {
                if ( cmd.StartsWith( "\"" ) && cmd.EndsWith( "\"" ) )
                {
                    args.Add( cmd.Trim( new char[] { '\"', ' ' } ) );
                    arg = "";
                }
                else if ( cmd.StartsWith( "\"" ) )
                {
                    arg = cmd + " ";
                }
                else if ( cmd.EndsWith( "\"" ) )
                {
                    arg += cmd;
                    args.Add( arg.Trim( new char[] { '\"', ' ' } ) );
                    arg = "";
                }
                else if ( !string.IsNullOrEmpty( arg ) )
                {
                    arg += cmd + " ";
                }
                else
                {
                    if ( !string.IsNullOrEmpty( cmd ) )
                    {
                        args.Add( cmd );
                    }
                    arg = "";
                }
#if DEBUG
                Console.WriteLine( $"Curent ARG: {cmd}, Parsed ARG: {arg}" );
#endif
            }
            return ( args.GetRange( 1, args.Count - 1 ).ToArray() );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private void OpenCmdArgs(string[] args)
        {
            string[] exts = new string[] { ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".bmp", ".gif" };

            string[] flist = args.Where(f => File.Exists(f) && exts.Contains(Path.GetExtension(f).ToLower())).ToArray();
            object result = true;

            if ( flist.Length == 1 )
            {
                if ( addins.CurrentApp is IAddin )
                {
                    addins.CurrentApp.Command( AddinCommand.Open, out result, flist );
                }
                else
                {
                    if ( addins.Apps.ContainsKey( "Editor" ) )
                    {
                        addins.CurrentApp = addins.Apps["Editor"];
                        addins.CurrentApp.Show( this, false );
                        addins.CurrentApp.Command( AddinCommand.Open, out result, flist );
                    }
                }
            }
            else if ( flist.Length > 1 )
            {
                if ( addins.Apps.ContainsKey( "Batch" ) )
                {
                    addins.CurrentApp = addins.Apps["Batch"];
                    addins.CurrentApp.Show( this, false );
                    addins.CurrentApp.Command( AddinCommand.Open, out result, flist );
                }
            }

            if(flist.Length>0)
            {
                tssLabelImageName.Text = Path.GetFileName( flist[0] );
                tssLabelImageName.ToolTipText = $"{I18N._( "Image File" )}: {flist[0]}";
                if ( addins.CurrentApp.ImageData is Image )
                {
                    object imgSize = new Size(0, 0);
                    object imgFormat = PixelFormat.Format32bppArgb;
                    addins.CurrentApp.Command( AddinCommand.GetImageSize, out imgSize );
                    addins.CurrentApp.Command( AddinCommand.GetImageColors, out imgFormat );

                    int bits = AddinUtils.GetColorDeep((PixelFormat) imgFormat);
                    tssLabelImageSize.Text = $"{( (Size) imgSize ).Width} x {( (Size) imgSize ).Height} x {bits}";

                    object zoomLevel = 100;
                    addins.CurrentApp.Command( AddinCommand.ZoomLevel, out zoomLevel );
                    tssLabelImageZoom.Text = $"{zoomLevel}%";

                    RecentItemAdd( flist[0] );
                }
            }
            else
            {
                tssLabelImageName.Text = I18N._( "None" );
                tssLabelImageSize.Text = I18N._( "None" );
                tssLabelImageZoom.Text = I18N._( "None" );
            }
            cmdFileApply.Visible = addins.CurrentApp is IAddin ? addins.CurrentApp.SupportMultiFile : false;
            cmdFileApplyAll.Visible = addins.CurrentApp is IAddin ? addins.CurrentApp.SupportMultiFile : false;
            cmdFileSepApply.Visible = addins.CurrentApp is IAddin ? addins.CurrentApp.SupportMultiFile : false;

            FixedMdiSize();
        }

        #endregion Command Line Arguments Routines

        #region System Settint Load & Save routines
        /// <summary>
        /// 
        /// </summary>
        private void Setting_Load()
        {
            string jfile = Path.Combine(AppPath, "settings.json");
            if ( File.Exists( jfile ) )
            {
                string json = File.ReadAllText(jfile);
                settings = JsonConvert.DeserializeObject<Dictionary<string, object>>( json );

                #region RecentItem
                if ( settings.ContainsKey( "RecentItem" ) )
                {
                    foreach ( string item in ( settings["RecentItem"] as JArray ).Reverse().ToList() )
                    {
                        RecentItemAdd( item );
                    }
                }
                #endregion

                #region Ribbon Style & Theme
                if ( settings.ContainsKey( "ThemeStyle" ) && settings["ThemeStyle"] != null )
                    ChangeStyle( (RibbonOrbStyle) Enum.Parse( typeof( RibbonOrbStyle ), settings["ThemeStyle"].ToString() ) );
                if ( settings.ContainsKey( "ThemeColor" ) && settings["ThemeColor"] != null )
                    ChangeTheme( (RibbonTheme) Enum.Parse( typeof( RibbonTheme ), settings["ThemeColor"].ToString() ) );
                #endregion

                #region Window Position & Size
                if ( settings.ContainsKey( "WindowTop" ) && settings["WindowTop"] != null )
                    this.Top = Convert.ToInt32( settings["WindowTop"] );
                if ( settings.ContainsKey( "WindowLeft" ) && settings["WindowLeft"] != null )
                    this.Left = Convert.ToInt32( settings["WindowLeft"] );
                if ( settings.ContainsKey( "WindowWidth" ) && settings["WindowWidth"] != null )
                    this.Width = Convert.ToInt32( settings["WindowWidth"] );
                if ( settings.ContainsKey( "WindowHeight" ) && settings["WindowHeight"] != null )
                    this.Height = Convert.ToInt32( settings["WindowHeight"] );
                if ( settings.ContainsKey( "WindowState" ) && settings["WindowState"] != null )
                    this.WindowState = (FormWindowState) Enum.Parse( typeof( FormWindowState ), settings["WindowState"].ToString() );
                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Setting_Save()
        {
            settings["RecentItem"] = ribbonMain.OrbDropDown.RecentItems.Select( o => o.Value ).Take( 20 );
            settings["ThemeStyle"] = Theme.ThemeStyle.ToString();
            settings["ThemeColor"] = Theme.ThemeColor.ToString();
            settings["WindowTop"] = this.Top;
            settings["WindowLeft"] = this.Left;
            settings["WindowWidth"] = this.Width;
            settings["WindowHeight"] = this.Height;
            settings["WindowState"] = this.WindowState;

            string json = JsonConvert.SerializeObject( settings, Formatting.Indented );
            File.WriteAllText( Path.Combine( AppPath, "settings.json" ), json );
        }
        #endregion
    }
}
