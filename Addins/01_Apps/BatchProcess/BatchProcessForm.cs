using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetCharm.Image.Addins;
using ExtensionMethods;

namespace BatchProcess
{
    public partial class BatchProcessForm : Form
    {
        private AddinHost Host = null;
        private IAddin addin;
        private Image _image = null;

        /// <summary>
        /// 
        /// </summary>
        public Image ImageData
        {
            get { return ( imgPreview.Image ); }
            internal set { imgPreview.Image = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SelectedFile
        {
            get
            {
                var file = string.Empty;
                if ( lvFiles.FocusedItem is ListViewItem )
                {
                    file = lvFiles.FocusedItem.SubItems[1].Text;
                    if ( !File.Exists( file ) ) file = string.Empty;
                }
                return ( file );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] SelectedFiles
        {
            get
            {
                var filelist = new List<string>();
                foreach(int idx in lvFiles.SelectedIndices)
                {
                    var file = lvFiles.Items[idx].SubItems[1].Text;
                    if ( System.IO.File.Exists( file ) )
                        filelist.Add(file);
                }
                return ( filelist.ToArray() );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] AllFiles
        {
            get
            {
                var filelist = new List<string>();
                foreach ( ListViewItem item in lvFiles.Items )
                {
                    var file = item.SubItems[1].Text;
                    if ( File.Exists( file ) )
                        filelist.Add( file );
                }
                return ( filelist.ToArray() );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        protected internal void AddFiles( string file )
        {
            lvFiles.Items.Add( new ListViewItem( new string[] { Path.GetFileName( file ), file } ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        protected internal void AddFiles( string[] files )
        {
            foreach ( string f in files )
            {
                lvFiles.Items.Add( new ListViewItem( new string[] { Path.GetFileName( f ), f } ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoomMode"></param>
        /// <returns></returns>
        public ValueType Zoom( AddinCommand zoomMode )
        {
            switch ( zoomMode )
            {
                case AddinCommand.ZoomIn:
                    imgPreview.ZoomIn();
                    break;
                case AddinCommand.ZoomOut:
                    imgPreview.ZoomOut();
                    break;
                case AddinCommand.ZoomRegion:
                    imgPreview.ZoomToRegion( imgPreview.SelectionRegion );
                    break;
                case AddinCommand.ZoomFit:
                    imgPreview.ZoomToFit();
                    break;
                case AddinCommand.Zoom100:
                    imgPreview.Zoom = 100;
                    break;
                case AddinCommand.ZoomLevel:
                    break;
            }
            return ( imgPreview.Zoom );
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Preview()
        {


        }

        /// <summary>
        /// 
        /// </summary>
        public BatchProcessForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public BatchProcessForm(AddinHost host)
        {
            Host = host;
            InitializeComponent();

            AddinUtils.Translate( addin, this, toolTip, new object[] { cmsFileList, dlgOpen } );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addin"></param>
        public BatchProcessForm( IAddin addin )
        {
            this.addin = addin;
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatchProcessForm_Load( object sender, EventArgs e )
        {
            AddinUtils.Translate( addin, this, toolTip, new object[] { cmsFileList, dlgOpen } );

            //
            //this.MinimizeBox = false;
            //this.MaximizeBox = false;
            //this.ControlBox = false;
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddImage_Click( object sender, EventArgs e )
        {
            if(dlgOpen.ShowDialog() == DialogResult.OK)
            {
                AddFiles( dlgOpen.FileNames );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRemoveImage_Click( object sender, EventArgs e )
        {
            foreach(ListViewItem item in lvFiles.SelectedItems)
            {
                lvFiles.Items.Remove( item );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiClear_Click( object sender, EventArgs e )
        {
            lvFiles.Items.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiProcessSelected_Click( object sender, EventArgs e )
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiProcessAll_Click( object sender, EventArgs e )
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvFiles_ItemSelectionChanged( object sender, ListViewItemSelectionChangedEventArgs e )
        {
            if( imgPreview.Image != null) imgPreview.Image.Dispose();

            //imgPreview.Image = new Bitmap( e.Item.SubItems[1].Text );
            //lvAddins.Image = imgPreview.Image;
            _image = AddinUtils.LoadImage( e.Item.SubItems[1].Text );
            imgPreview.Image = _image;
            lvAddins.Image = _image;
        }

        private void lvAddins_ValueChanged( object sender, EventArgs e )
        {
            //
            //lvAddins.FilterParams;
            imgPreview.Image = addin.Apply( ImageData );
        }
    }
}
