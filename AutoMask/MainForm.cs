using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;

namespace AutoMask
{
    public partial class MainForm : Form
    {
        private HaarObjectDetector detector;

        private Image mask = null;
        private Image photo = null;
        private Image photo_mask = null;

        private string[] PhotoExts = { ".jpg", ".jpeg", ".tif",".tiff", ".bmp", ".png", ".gif" };

        private ListViewItem[] files = null;
        private ObjectDetectorSearchMode SearchMode = ObjectDetectorSearchMode.NoOverlap;
        private ObjectDetectorScalingMode ScalingMode = ObjectDetectorScalingMode.SmallerToGreater;
        private int faceSize = 25;
        private int OutSize = 1200;
        private bool GrayFirst = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maskFile"></param>
        /// <returns></returns>
        private Image LoadMask(string maskFile)
        {
            using ( Image m = new Bitmap( maskFile ) )
            {
                int largeSize = Math.Max(m.Width, m.Height);
                Image mc = new Bitmap( largeSize, largeSize, PixelFormat.Format32bppArgb );
                using ( Graphics g = Graphics.FromImage( mc ) )
                {
                    RectangleF r = new RectangleF();
                    r.X = ( mc.Width - m.Width ) / 2.0f;
                    r.Y = ( mc.Height - m.Height ) / 2.0f;
                    r.Height = m.Height;
                    r.Width = m.Width;
                    g.DrawImage( m, r );
                }
                return ( mc );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image RotateImage(Image image)
        {
            if ( image.PropertyIdList.Where( id => id == 0x0112 ).Count() == 0 ) return ( image );

            int flipValue = Convert.ToInt32(image.GetPropertyItem( 0x0112 ).Value[0]);

            RotateFlipType flip = RotateFlipType.RotateNoneFlipNone;
            switch ( flipValue )
            {
                case 1:
                    flip = RotateFlipType.RotateNoneFlipNone;
                    break;
                case 2:
                    flip = RotateFlipType.RotateNoneFlipX;
                    break;
                case 3:
                    flip = RotateFlipType.Rotate180FlipNone;
                    break;
                case 4:
                    flip = RotateFlipType.Rotate180FlipX;
                    break;
                case 5:
                    flip = RotateFlipType.Rotate90FlipX;
                    break;
                case 6:
                    flip = RotateFlipType.Rotate90FlipNone;
                    break;
                case 7:
                    flip = RotateFlipType.Rotate270FlipX;
                    break;
                case 8:
                    flip = RotateFlipType.Rotate270FlipNone;
                    break;
                default:
                    flip = RotateFlipType.RotateNoneFlipNone;
                    break;
            }
            image.RotateFlip( flip );
            PropertyItem pi = image.GetPropertyItem(0x0112);
            pi.Value[0] = 0x01;
            image.SetPropertyItem( pi );
            return ( image );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Image ResizeImage(Image image, int newSize)
        {
            float largeSize = Math.Max(image.Width, image.Height);
            float factor = (float)newSize/largeSize;

            ResizeBicubic filter = new ResizeBicubic((int)Math.Round(image.Width*factor), (int)Math.Round(image.Height*factor) );
            Image dst = filter.Apply( image as Bitmap );
            foreach(PropertyItem item in image.PropertyItems )
            {
                dst.SetPropertyItem(item);
            }
            return ( dst );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="faceSize"></param>
        /// <returns></returns>
        private Image MaskFace( string imageFile, int faceSize = 20 )
        {
            using ( Image image = new Bitmap( imageFile ) )
            {
                return ( MaskFace( ResizeImage( RotateImage( image ), OutSize ), faceSize ) );
            }
            //return ( MaskFace( ResizeImage( RotateImage( new Bitmap( imageFile ) ), OutSize ), faceSize ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="faceSize"></param>
        /// <param name="Save"></param>
        /// <returns></returns>
        private string MaskFace( string imageFile, int faceSize = 20, bool removeExif = true )
        {
            string fn = imageFile;

            //using ( Image image = MaskFace( imageFile, faceSize ) )
            {
                Image image = MaskFace( imageFile, faceSize );

                string fd = Path.GetDirectoryName(imageFile);
                string fe = Path.GetExtension(imageFile);
                fn = Path.Combine( fd, $"{Path.GetFileNameWithoutExtension( imageFile )}_masked{fe}" );

                if ( removeExif )
                {
                    foreach ( int id in image.PropertyIdList )
                    {
                        image.RemovePropertyItem( id );
                    }
                }
                image.Save( fn, ImageFormat.Jpeg );
            }
            return ( fn );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image MaskFace(Image image, int faceSize=20)
        {
            if ( image == null ) return image;

            #region Setting Face Detector
            detector.MinSize = new Size( faceSize, faceSize );
            detector.UseParallelProcessing = true;
            detector.Suppression = 4;
            detector.SearchMode = SearchMode;
            detector.ScalingMode = ScalingMode;
            if ( image.Width > 1600 || image.Height > 1600 )
            {
                detector.SearchMode = ObjectDetectorSearchMode.Average;
            }
            detector.ScalingFactor = 1.2f;
            #endregion

            #region Detecting face(s)
            // Process frame to detect objects
            Image dst = image.Clone() as Image;
            if ( GrayFirst &&
                dst.PixelFormat != PixelFormat.Format1bppIndexed &&
                dst.PixelFormat != PixelFormat.Format4bppIndexed &&
                dst.PixelFormat != PixelFormat.Format8bppIndexed)
            {
                dst = new Grayscale( 0.2125, 0.7154, 0.0721 ).Apply( dst as Bitmap ) as Image; 
            }
            Rectangle[] faces = detector.ProcessFrame(dst as Bitmap);
            if(faces.Length<=0)
            {
                detector.SearchMode = ObjectDetectorSearchMode.Average;
                //faces = detector.ProcessFrame( image as Bitmap );
                faces = detector.ProcessFrame( dst as Bitmap );
            }
            #endregion

            #region Draw mask to photo
            if ( faces.Length > 0 )
            {
                //RectanglesMarker marker = new RectanglesMarker(faces, Color.Fuchsia);
                if( image.PixelFormat == PixelFormat.Format1bppIndexed || 
                    image.PixelFormat == PixelFormat.Format4bppIndexed || 
                    image.PixelFormat == PixelFormat.Format8bppIndexed )
                {
                    photo_mask = new GrayscaleToRGB().Apply( image.Clone() as Bitmap );
                }
                else photo_mask = image.Clone() as Image;
                using ( Graphics g = Graphics.FromImage( photo_mask ) )
                {
                    float factorX = 1.75f;
                    float factorY = 1.75f;
                    //foreach ( Rectangle r in marker.Rectangles )
                    foreach ( Rectangle r in faces )
                    {
                        g.DrawImage( mask,
                            r.Left + (float) ( r.Width * ( 1 - factorX ) / 2.0f ),
                            r.Top + (float) ( r.Height * ( 1 - factorY ) / 2.0f ),
                            r.Width * factorX,
                            r.Height * factorY );
                    }
                }
                return ( photo_mask );
            }
            #endregion
            return ( image );
        }

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load( object sender, EventArgs e )
        {
            #region extracting icon from application to this form window
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            if (File.Exists( "mask.png" ) )
            {
                //mask = new Bitmap( "mask.png" );
                mask = LoadMask( "mask.png" );
            }
            else
            {
                mask = Icon.ToBitmap();
            }
            #endregion

            #region Add file(s) from command line args
            string[] flist = Environment.GetCommandLineArgs();
            lvFiles.Items.Clear();
            foreach ( string f in flist )
            {
                if ( PhotoExts.Contains( Path.GetExtension( f ), StringComparer.CurrentCultureIgnoreCase ) )
                {
                    if ( lvFiles.Items.Count == 0 || lvFiles.FindItemWithText( f, true, 0 ) == null )
                    {
                        ListViewItem fItem = new ListViewItem( new string[] { Path.GetFileName( f ), f } );
                        lvFiles.Items.Add( fItem );
                    }
                }
            }
            #endregion

            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.Image = photo;

            picMask.SizeMode = PictureBoxSizeMode.Zoom;
            picMask.Image = mask;

            cbMode.DataSource = Enum.GetValues( typeof( ObjectDetectorSearchMode ) );
            cbScaling.DataSource = Enum.GetValues( typeof( ObjectDetectorScalingMode ) );

            cbMode.SelectedItem = ObjectDetectorSearchMode.NoOverlap;
            cbScaling.SelectedItem = ObjectDetectorScalingMode.SmallerToGreater;

            SearchMode = ObjectDetectorSearchMode.NoOverlap;
            ScalingMode = ObjectDetectorScalingMode.SmallerToGreater;

            OutSize = (int) numOutSize.Value;
            faceSize = (int) numFaceSize.Value;

            GrayFirst = chkGrayDetect.Checked;

            HaarCascade cascade = new FaceHaarCascade();
            detector = new HaarObjectDetector( cascade );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMode_SelectionChangeCommitted( object sender, EventArgs e )
        {
            SearchMode = (ObjectDetectorSearchMode) cbMode.SelectedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbScaling_SelectionChangeCommitted( object sender, EventArgs e )
        {
            ScalingMode = (ObjectDetectorScalingMode) cbScaling.SelectedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numFaceSize_ValueChanged( object sender, EventArgs e )
        {
            faceSize = (int) numFaceSize.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numOutSize_ValueChanged( object sender, EventArgs e )
        {
            OutSize = (int) numOutSize.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkGrayDetect_CheckedChanged( object sender, EventArgs e )
        {
            GrayFirst = chkGrayDetect.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter( object sender, DragEventArgs e )
        {
            //e.Effect = DragDropEffects.Link;
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

            foreach ( string f in flist )
            {
                if ( PhotoExts.Contains( Path.GetExtension( f ), StringComparer.CurrentCultureIgnoreCase ) )
                {
                    if ( lvFiles.Items.Count == 0 || lvFiles.FindItemWithText( f, true, 0 ) == null )
                    {
                        ListViewItem fItem = new ListViewItem( new string[] { Path.GetFileName( f ), f } );
                        lvFiles.Items.Add( fItem );
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMask_Click( object sender, EventArgs e )
        {
            btnMask.Enabled = false;
            files = new ListViewItem[lvFiles.Items.Count];
            lvFiles.Items.CopyTo( files, 0 );

            bgwMask.RunWorkerAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrig_MouseDown( object sender, MouseEventArgs e )
        {
            if ( photo != null )
                picPreview.Image = photo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrig_MouseUp( object sender, MouseEventArgs e )
        {
            if ( photo_mask != null )
                picPreview.Image = photo_mask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picMask_DoubleClick( object sender, EventArgs e )
        {
            //
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "PNG Image(*.png)|*.png|Bitmap Image(*.bmp)|*.bmp|GIF Image(*.gif)|*.gif|All Supported Image(*.png;*.bmp;*.gif)|*.png;*.bmp;*.gif";
            dlgOpen.FilterIndex = 4;
            dlgOpen.DefaultExt = ".png";
            dlgOpen.Multiselect = false;
            dlgOpen.CheckFileExists = true;
            dlgOpen.CheckPathExists = true;
            if ( dlgOpen.ShowDialog(this) == DialogResult.OK)
            {
                if ( mask != null ) mask.Dispose();
                //mask = new Bitmap( dlgOpen.FileName );
                mask = LoadMask( dlgOpen.FileName );
                picMask.Image = mask;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvFiles_ItemSelectionChanged( object sender, ListViewItemSelectionChangedEventArgs e )
        {
            if(e.ItemIndex>=0 && e.Item != null && e.IsSelected )
            {
                string fn = e.Item.SubItems[0].Text;
                string ff = e.Item.SubItems[1].Text;
                //Text = f;
                if (File.Exists(ff))
                {
                    if(photo != null) photo.Dispose();
                    using ( Image of = new Bitmap( ff ) )
                    {
                        photo = ResizeImage( RotateImage( of ), OutSize ) as Bitmap;
                        picPreview.Image = MaskFace( photo, faceSize );

                        tsInfoFileName.Text = $"{fn}";
                        tsInfoFileSize.Text = $"Image: {of.Width} x {of.Height}";
                        tsInfoPreviewSize.Text = $"View: {picPreview.Image.Width} x {picPreview.Image.Height}";
                        tsInfo.Text = "OK";
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiFileListAdd_Click( object sender, EventArgs e )
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "PNG Image(*.png)|*.png|Bitmap Image(*.bmp)|*.bmp|GIF Image(*.gif)|*.gif|All Supported Image(*.png;*.bmp;*.gif)|*.png;*.bmp;*.gif";
            dlgOpen.FilterIndex = 4;
            dlgOpen.DefaultExt = ".jpg";
            dlgOpen.Multiselect = true;
            dlgOpen.CheckFileExists = true;
            dlgOpen.CheckPathExists = true;
            if ( dlgOpen.ShowDialog( this ) == DialogResult.OK )
            {
                foreach ( string f in dlgOpen.FileNames )
                {
                    if ( PhotoExts.Contains( Path.GetExtension( f ), StringComparer.CurrentCultureIgnoreCase ) )
                    {
                        if ( lvFiles.Items.Count == 0 || lvFiles.FindItemWithText( f, true, 0 ) == null )
                        {
                            ListViewItem fItem = new ListViewItem( new string[] { Path.GetFileName( f ), f } );
                            lvFiles.Items.Add( fItem );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiFileListRemove_Click( object sender, EventArgs e )
        {
            foreach ( ListViewItem l in lvFiles.SelectedItems )
            {
                l.Remove();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiFileListClear_Click( object sender, EventArgs e )
        {
            lvFiles.Items.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiFileListMaskSelected_Click( object sender, EventArgs e )
        {
            btnMask.Enabled = false;
            files = new ListViewItem[lvFiles.SelectedItems.Count];
            for ( int i = 0; i < files.Length; i++ )
            {
                files[i] = lvFiles.SelectedItems[i];
            }
            bgwMask.RunWorkerAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiFileListMaskAll_Click( object sender, EventArgs e )
        {
            btnMask.PerformClick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMask_DoWork( object sender, DoWorkEventArgs e )
        {
            btnMask.Enabled = false;

            int faceSize = (int)numFaceSize.Value;
            bool removeExif = chkRemoveEXIF.Checked;

            for(int i=0; i< files.Length; i++ )
            {
                if ( bgwMask.CancellationPending ) break;

                string f = files[i].SubItems[1].Text;
                if ( File.Exists( f ) )
                {
                    MaskFace( f, faceSize, removeExif );
                    bgwMask.ReportProgress( (int) ( ( i + 1 ) * 100.0 / files.Length ) );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMask_ProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            tsProgress.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMask_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            tsProgress.Value = 100;
            btnMask.Enabled = true;
        }

    }
}
