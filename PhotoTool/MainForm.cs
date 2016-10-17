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
        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            I18N i10n = new I18N( null, this, toolTip, new object[] { status, dlgOpen, dlgSave } );
            TranslateRibbon( ribbonMain );
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

            #region Setup Ribbon UI Style & Theme

            ChangeStyle( RibbonOrbStyle.Office_2010 );
            //ChangeTheme( RibbonTheme.Halloween );
            ChangeTheme( RibbonTheme.Black );

            #endregion Setup Ribbon UI Style & Theme

            #region Addin Host Setup

            addins.CommandPropertiesChange += AddinsCommandPropertiesChange;

            addins.Scan();
            AddAddinApp( addins.Apps.Select( kvp => kvp.Value ).ToList() );
            AddAddinAction( addins.Actions.Select( kvp => kvp.Value ).ToList() );
            AddAddinFilter( addins.Filters.Select( kvp => kvp.Value ).ToList() );

            #endregion Addin Host Setup

            #region Process Commnad-Line Parameters

            OpenCmdArgs( ParseCommandLine( Environment.CommandLine ) );

            #endregion Process Commnad-Line Parameters
        }

        #region DrapDrop Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter( object sender, DragEventArgs e )
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop( object sender, DragEventArgs e )
        {
            string[] flist = (string[])e.Data.GetData( DataFormats.FileDrop, true );
            OpenCmdArgs( flist );
        }

        #endregion DragDrop Events

        #region Ribbon UI Events
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

        #endregion Ribbon UI Events

        #region File Command Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdFileOpen_Click( object sender, EventArgs e )
        {
            if ( dlgOpen.ShowDialog() == DialogResult.OK )
            {
                OpenCmdArgs( dlgOpen.FileNames );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdFileSave_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                string fname = I18N._("NewFile");
                object fn = fname;
                addins.CurrentApp.Command( AddinCommand.GetImageName, out fn );
                if ( fn is string && !string.IsNullOrEmpty( (string) fn ) )
                {
                    fname = Path.GetFileNameWithoutExtension( (string) fn);
                }

                dlgSave.FileName = string.Format( $"{fname}_{DateTime.Now.Ticks}.{dlgSave.DefaultExt}" );
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

        #endregion File Command Events

        #region Clipboard Command Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEditCut_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                object data = null;
                addins.CurrentApp.Command( AddinCommand.Cut, out data );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEditCopy_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                object data = null;
                addins.CurrentApp.Command( AddinCommand.Copy, out data );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEditPaste_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                object data = null;
                addins.CurrentApp.Command( AddinCommand.Paste, out data );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEditClear_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                object data = null;
                addins.CurrentApp.Command( AddinCommand.Clear, out data );
            }
        }
        #endregion Clipboard Command Events

        #region Zoom Command Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdViewZoomIn_Click( object sender, EventArgs e )
        {
            if(addins.CurrentApp != null)
            {
                object zoomLevel = 100;
                addins.CurrentApp.Command( AddinCommand.ZoomIn, out zoomLevel );
                tssLabelImageZoom.Text = $"{zoomLevel}%";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdViewZoomOut_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                object zoomLevel = 100;
                addins.CurrentApp.Command( AddinCommand.ZoomOut, out zoomLevel );
                tssLabelImageZoom.Text = $"{zoomLevel}%";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdViewZoomFit_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                object zoomLevel = 100;
                addins.CurrentApp.Command( AddinCommand.ZoomFit, out zoomLevel );
                tssLabelImageZoom.Text = $"{zoomLevel}%";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdViewZoom100_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                object zoomLevel = 100;
                addins.CurrentApp.Command( AddinCommand.Zoom100, out zoomLevel );
                tssLabelImageZoom.Text = $"{zoomLevel}%";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdViewZoomRegion_Click( object sender, EventArgs e )
        {
            if ( addins.CurrentApp != null )
            {
                object zoomLevel = 100;
                addins.CurrentApp.Command( AddinCommand.ZoomRegion, out zoomLevel );
                tssLabelImageZoom.Text = $"{zoomLevel}%";
            }
        }

        #endregion Zoom Command Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddinReScan_Click( object sender, EventArgs e )
        {
            addins.Scan();
            foreach ( KeyValuePair<string, IAddin> kv in addins.Actions )
            {
                //kv.Value.LargeImage
            }
        }

    }
}
