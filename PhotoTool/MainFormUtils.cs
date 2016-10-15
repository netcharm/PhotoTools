using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image.Addins;
using NGettext.WinForm;

namespace PhotoTool
{
    public partial class MainForm : RibbonForm
    {
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
        private AddinHost addins = new AddinHost();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apps"></param>
        private void AddAddinApp( List<IAddin> apps )
        {
            foreach ( IAddin addin in apps )
            {
                RibbonButton btnAddin = new RibbonButton();
                RibTabMainApp.Items.Add( btnAddin );

                if ( addin.LargeIcon != null )
                    btnAddin.Image = addin.LargeIcon;
                else
                    btnAddin.Image = addins.LargeImage;
                if ( addin.SmallIcon != null )
                    btnAddin.SmallImage = addin.SmallIcon;
                else
                    btnAddin.SmallImage = addins.SmallImage;

                btnAddin.Text = I18N._( addin.DisplayName );
                btnAddin.ToolTip = I18N._( addin.Description );
                btnAddin.ToolTipTitle = I18N._( addin.Author );

                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinAppClick;
            }
            ribbonMain.ResumeLayout( true );
            ribbonMain.PerformLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acts"></param>
        /// <param name="IsExt"></param>
        private void AddAddinAction( List<IAddin> acts, bool IsExt = true )
        {
            foreach ( IAddin addin in acts )
            {
                RibbonButton btnAddin = new RibbonButton();
                if ( string.Equals( addin.Author, "netcharm", StringComparison.CurrentCultureIgnoreCase ) ||
                    addin.Author.StartsWith( "NetCharm ", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    RibTabActInternalList.Buttons.Add( btnAddin );
                    RibTabActInternalDropList.Buttons.Add( btnAddin );
                }
                else
                {
                    RibTabActExternalList.Buttons.Add( btnAddin );
                    RibTabActExternalDropList.Buttons.Add( btnAddin );
                }

                if ( addin.LargeIcon != null )
                    btnAddin.Image = addin.LargeIcon;
                else
                    btnAddin.Image = addins.LargeImage;
                if ( addin.SmallIcon != null )
                    btnAddin.SmallImage = addin.SmallIcon;
                else
                    btnAddin.SmallImage = addins.SmallImage;

                btnAddin.Text = I18N._( addin.DisplayName );
                btnAddin.ToolTip = I18N._( addin.Description );
                btnAddin.ToolTipTitle = I18N._( addin.Author );

                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinActionClick;
            }
            ribbonMain.ResumeLayout( true );
            ribbonMain.PerformLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="IsExt"></param>
        private void AddAddinFilter( List<IAddin> filters, bool IsExt = true )
        {
            foreach ( IAddin addin in filters )
            {
                RibbonButton btnAddin = new RibbonButton();
                if ( string.Equals( addin.Author, "netcharm", StringComparison.CurrentCultureIgnoreCase ) ||
                    addin.Author.StartsWith( "NetCharm ", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    RibTabFilterInternalList.Buttons.Add( btnAddin );
                    RibTabFilterInternalDropList.Buttons.Add( btnAddin );
                }
                else
                {
                    RibTabFilterExternalList.Buttons.Add( btnAddin );
                    RibTabFilterExternalDropList.Buttons.Add( btnAddin );
                }

                if ( addin.LargeIcon != null )
                    btnAddin.Image = addin.LargeIcon;
                else
                    btnAddin.Image = addins.LargeImage;
                if ( addin.SmallIcon != null )
                    btnAddin.SmallImage = addin.SmallIcon;
                else
                    btnAddin.SmallImage = addins.SmallImage;

                btnAddin.Text = I18N._( addin.DisplayName );
                btnAddin.ToolTip = I18N._( addin.Description );
                btnAddin.ToolTipTitle = I18N._( addin.Author );

                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinFilterClick;
            }
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
                    addins.CurrentApp.Show( this );
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
                addins.CurrentAction = addins.Actions[an];
                if ( addins.CurrentAction != null )
                {
                    addins.CurrentAction.ImageData = addins.CurrentApp.ImageData;
                    addins.CurrentAction.Show( this );
                    addins.CurrentApp.ImageData = addins.CurrentAction.ImageData;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddinFilterClick( object sender, EventArgs e )
        {
            string an = ( sender as RibbonButton ).Value;
            if ( addins.CurrentApp != null && addins.Filters.ContainsKey( an ) )
            {
                addins.CurrentFilter = addins.Filters[an];
                if ( addins.CurrentFilter != null )
                {
                    addins.CurrentFilter.ImageData = addins.CurrentApp.ImageData;
                    addins.CurrentFilter.Show( this );
                    addins.CurrentApp.ImageData = addins.CurrentFilter.ImageData;
                    tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height}";
                }
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

            string[] flist = args.Where(f => File.Exists(f) && exts.Contains(Path.GetExtension(f))).ToArray();

            if ( flist.Length == 1 )
            {
                if ( addins.CurrentApp is IAddin )
                {
                    addins.CurrentApp.Open( flist );
                }
                else
                {
                    if ( addins.Apps.ContainsKey( "Editor" ) )
                    {
                        addins.CurrentApp = addins.Apps["Editor"];
                        addins.CurrentApp.Show( this );
                        addins.CurrentApp.Open( flist );
                    }
                }
            }
            else if ( flist.Length > 1 )
            {
                if ( addins.Apps.ContainsKey( "Batch" ) )
                {
                    addins.CurrentApp = addins.Apps["Batch"];
                    addins.CurrentApp.Show( this );
                    addins.CurrentApp.Open( flist );
                }
            }
        }

        #endregion Command Line Arguments Routines

    }
}
