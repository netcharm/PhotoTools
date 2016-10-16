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

            ChangeStyle( RibbonOrbStyle.Office_2010 );
            ChangeTheme( RibbonTheme.Halloween );

            addins.CommandPropertiesChange += Addins_RaiseCommandPropertiesChange;

            addins.Scan();
            AddAddinApp( addins.Apps.Select( kvp => kvp.Value ).ToList() );
            AddAddinAction( addins.Actions.Select( kvp => kvp.Value ).ToList() );
            AddAddinFilter( addins.Filters.Select( kvp => kvp.Value ).ToList() );

            OpenCmdArgs( ParseCommandLine( Environment.CommandLine ) );

        }

        private void Addins_RaiseCommandPropertiesChange( object sender, CommandPropertiesChangeEventArgs e )
        {
            //tssLabelImageName.Text = I18N._( "None" );
            //tssLabelImageSize.Text = "0 x 0";
            //tssLabelImageZoom.Text = "";
            switch ( e.Command)
            {
                case AddinCommand.ZoomLevel:
                    tssLabelImageZoom.Text = $"{e.Property}%";
                    break;
                case AddinCommand.GetImageName:
                    tssLabelImageName.Text = $"{Path.GetFileName( (string) e.Property )}";
                    break;
                case AddinCommand.GetImageSize:
                    tssLabelImageSize.Text = $"{( (Size) e.Property ).Width} x {( (Size) e.Property ).Height}";
                    break;
            }
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdActionReScan_Click( object sender, EventArgs e )
        {
            addins.Scan();
            foreach ( KeyValuePair<string, IAddin> kv in addins.Actions )
            {
                //kv.Value.LargeImage
            }
        }

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

        private void cmdEditCut_Click( object sender, EventArgs e )
        {

        }

        private void cmdEditCopy_Click( object sender, EventArgs e )
        {

        }

        private void cmdEditPaste_Click( object sender, EventArgs e )
        {

        }

        private void cmdEditClear_Click( object sender, EventArgs e )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdViewZoomIn_Click( object sender, EventArgs e )
        {
            if(addins.CurrentApp != null)
            {
                ValueType zoomLevel = 100;
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
                ValueType zoomLevel = 100;
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
                ValueType zoomLevel = 100;
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
                ValueType zoomLevel = 100;
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
                ValueType zoomLevel = 100;
                addins.CurrentApp.Command( AddinCommand.ZoomRegion, out zoomLevel );
                tssLabelImageZoom.Text = $"{zoomLevel}%";
            }
        }

        private void addins_Validating( object sender, CancelEventArgs e )
        {

        }
    }
}
