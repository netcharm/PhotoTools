using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetCharm.Image.Addins;
using NGettext.WinForm;

namespace PhotoTool
{
    public partial class MainForm : RibbonForm
    {
        private AddinHost addins = new AddinHost();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        private void ChangeTheme( RibbonTheme theme = RibbonTheme.Normal )
        {
            switch(theme)
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

            if( ribbonMain.OrbStyle == RibbonOrbStyle.Office_2007 )
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apps"></param>
        private void AddAddinApp(List<IAddin> apps)
        {
            //ComponentResourceManager resources = new ComponentResourceManager( this.GetType());
            //ribbonMain.SuspendLayout();
            foreach ( IAddin addin in apps)
            {
                //RibbonButton btnAddin = new RibbonButton(Path.GetFileNameWithoutExtension(addin.Name));
                RibbonButton btnAddin = new RibbonButton();
                RibTabMainApp.Items.Add( btnAddin );

                if ( addin.LargeIcon != null)
                    btnAddin.Image = addin.LargeIcon;
                else
                    btnAddin.Image = addins.LargeImage;
                if ( addin.SmallIcon != null )
                    btnAddin.SmallImage = addin.SmallIcon;
                else
                    btnAddin.SmallImage = addins.SmallImage;

                btnAddin.Text = I18N._(addin.DisplayName);
                btnAddin.ToolTip = I18N._(addin.Description);
                btnAddin.ToolTipTitle = I18N._(addin.Author);

                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinAppClick;

                //resources.ApplyResources( btnAddin, addin.Name );
            }
            ribbonMain.ResumeLayout( true );
            ribbonMain.PerformLayout();
        }

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

                btnAddin.Text = I18N._(addin.DisplayName);
                btnAddin.ToolTip = I18N._( addin.Description);
                btnAddin.ToolTipTitle = I18N._( addin.Author);

                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinActionClick;
            }
            ribbonMain.ResumeLayout( true );
            ribbonMain.PerformLayout();
        }

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

                btnAddin.Text = I18N._( addin.DisplayName);
                btnAddin.ToolTip = I18N._( addin.Description);
                btnAddin.ToolTipTitle = I18N._( addin.Author);

                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinFilterClick;
            }
            ribbonMain.ResumeLayout( true );
            ribbonMain.PerformLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddinAppClick(object sender, EventArgs e)
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
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            I18N i10n = new I18N( null, this );
            TranslateRibbon( ribbonMain );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private void TranslateRibbonItems(RibbonItemCollection items)
        {
            if ( items == null || items.Count <= 0 ) return;

            foreach ( RibbonItem item in items )
            {
                item.Text = I18N._( item.Text );
                item.ToolTip = I18N._( item.ToolTip );
                item.ToolTipTitle = I18N._( item.ToolTipTitle );

                if( item.GetType().ToString().EndsWith( "RibbonButton", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    var btn = item as RibbonButton;
                    if (btn.Style != RibbonButtonStyle.Normal)
                    {
                        TranslateRibbonItems( btn.DropDownItems );
                    }
                }
                else if( item.GetType().ToString().EndsWith("RibbonButtonList", StringComparison.CurrentCultureIgnoreCase))
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
        public void TranslateRibbon(Ribbon rib)
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
            foreach ( RibbonTab tab in rib.Tabs)
            {
                tab.Text = I18N._( tab.Text );
                tab.ToolTip = I18N._( tab.ToolTip );
                tab.ToolTipTitle = I18N._( tab.ToolTipTitle );

                #region Ribbon Panel in Tab
                foreach (RibbonPanel panel in tab.Panels)
                {
                    panel.Text = I18N._( panel.Text );
                    foreach( RibbonItem item in panel.Items)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load( object sender, EventArgs e )
        {
            //
            #region extracting icon from application to this form window
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            #endregion

            ChangeStyle( RibbonOrbStyle.Office_2010 );
            ChangeTheme( RibbonTheme.Halloween );

            addins.Scan();
            AddAddinApp( addins.Apps.Select( kvp => kvp.Value ).ToList() );
            AddAddinAction( addins.Actions.Select( kvp => kvp.Value ).ToList() );
            AddAddinFilter( addins.Filters.Select( kvp => kvp.Value ).ToList() );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemeBlack_Click( object sender, EventArgs e )
        {
            var obj = sender as RibbonButton;
            var value = Int32.Parse( obj.Value );
            ChangeTheme( (RibbonTheme) value );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStyle2010_Click( object sender, EventArgs e )
        {
            var obj = sender as RibbonButton;
            var value = Int32.Parse( obj.Value );
            ChangeStyle( (RibbonOrbStyle) value );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribOptBtnExit_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void cmdActionReScan_Click( object sender, EventArgs e )
        {
            addins.Scan();
            foreach( KeyValuePair<string, IAddin> kv in addins.Actions)
            {
                //kv.Value.LargeImage
            }
        }

        private void cmdFileOpen_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                if ( dlgOpen.ShowDialog() == DialogResult.OK )
                {
                    addins.CurrentApp.ImageData = new Bitmap( dlgOpen.FileName );
                    tssLabelImageName.Text = Path.GetFileName( dlgOpen.FileName );
                    tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height}";
                }
            }
        }

        private void cmdFileSave_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                dlgSave.FileName = string.Format( $"{Path.GetFileNameWithoutExtension(dlgOpen.FileName)}_{DateTime.Now.Ticks}.{dlgSave.DefaultExt}" );
                if ( dlgSave.ShowDialog() == DialogResult.OK )
                {
                    if ( addins.CurrentApp.ImageData != null )
                    {
                        ImageFormat ff = ImageFormat.Jpeg;
                        string fext = Path.GetExtension(dlgSave.FileName).ToLower();
                        if ( string.Equals( fext, ".jpg", StringComparison.CurrentCultureIgnoreCase ) || 
                             string.Equals( fext, ".jpeg", StringComparison.CurrentCultureIgnoreCase ))
                        {
                            ff = ImageFormat.Jpeg;
                        }
                        else if ( string.Equals( fext, ".tif", StringComparison.CurrentCultureIgnoreCase ) ||
                             string.Equals( fext, ".tiff", StringComparison.CurrentCultureIgnoreCase ) )
                        {
                            ff = ImageFormat.Tiff;
                        }
                        else if ( string.Equals( fext, ".bmp", StringComparison.CurrentCultureIgnoreCase ))
                        {
                            ff = ImageFormat.Bmp;
                        }
                        else if ( string.Equals( fext, ".png", StringComparison.CurrentCultureIgnoreCase ) )
                        {
                            ff = ImageFormat.Png;
                        }
                        else if ( string.Equals( fext, ".gif", StringComparison.CurrentCultureIgnoreCase ) )
                        {
                            ff = ImageFormat.Gif;
                        }
                        addins.CurrentApp.ImageData.Save( dlgSave.FileName, ff );
                    }
                }
            }
        }
    }
}
