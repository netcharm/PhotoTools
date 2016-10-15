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

            addins.Scan();
            AddAddinApp( addins.Apps.Select( kvp => kvp.Value ).ToList() );
            AddAddinAction( addins.Actions.Select( kvp => kvp.Value ).ToList() );
            AddAddinFilter( addins.Filters.Select( kvp => kvp.Value ).ToList() );

            OpenCmdArgs( ParseCommandLine( Environment.CommandLine ) );
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
            foreach( KeyValuePair<string, IAddin> kv in addins.Actions)
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
            if ( addins.CurrentApp != null )
            {
                if ( dlgOpen.ShowDialog() == DialogResult.OK )
                {
                    if( addins.CurrentApp.SupportMultiFile )
                    {
                        addins.CurrentApp.Open( dlgOpen.FileNames );
                    }
                    else
                    {
                        addins.CurrentApp.Open( dlgOpen.FileName );
                    }
                    tssLabelImageName.Text = Path.GetFileName( dlgOpen.FileName );
                    if( addins.CurrentApp.ImageData is Image)
                    {
                        tssLabelImageSize.Text = $"{addins.CurrentApp.ImageData.Width} x {addins.CurrentApp.ImageData.Height}";
                    }
                }
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

    }
}
