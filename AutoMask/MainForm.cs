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

        private Bitmap mask = null;
        private Bitmap photo = null;
        private Image photo_mask = null;

        private string[] PhotoExts = { ".jpg", ".jpeg", ".tif",".tiff", ".bmp", ".png", ".gif" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image RotateImage(Image image)
        {
            if ( image.PropertyIdList.Where( id => id == 0x0112 ).Count() == 0 ) return ( image );

            int flipValue = Convert.ToInt32(image.GetPropertyItem( 0x0112 ).Value[0])-1;
            RotateFlipType flip = (RotateFlipType) flipValue;
            if ( flipValue < 0 || flipValue > 7 ) flip = RotateFlipType.RotateNoneFlipNone;

            //RotateFlipType flip = RotateFlipType.RotateNoneFlipNone;
            //switch ( flipValue )
            //{
            //    case 1:
            //        flip = RotateFlipType.RotateNoneFlipNone;
            //        break;
            //    case 2:
            //        flip = RotateFlipType.RotateNoneFlipX;
            //        break;
            //    case 3:
            //        flip = RotateFlipType.Rotate180FlipNone;
            //        break;
            //    case 4:
            //        flip = RotateFlipType.Rotate180FlipX;
            //        break;
            //    case 5:
            //        flip = RotateFlipType.Rotate90FlipX;
            //        break;
            //    case 6:
            //        flip = RotateFlipType.Rotate90FlipNone;
            //        break;
            //    case 7:
            //        flip = RotateFlipType.Rotate270FlipX;
            //        break;
            //    case 8:
            //        flip = RotateFlipType.Rotate270FlipNone;
            //        break;
            //    default:
            //        flip = RotateFlipType.RotateNoneFlipNone;
            //        break;
            //}
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
            return ( filter.Apply( image as Bitmap ) );
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
                return ( MaskFace( RotateImage( ResizeImage( image, (int) numOutSize.Value ) ), faceSize ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="faceSize"></param>
        /// <param name="Save"></param>
        /// <returns></returns>
        private string MaskFace( string imageFile, int faceSize = 20, bool removeExif=true )
        {
            using ( Image image = new Bitmap( imageFile ) )
            {
                string fe = Path.GetExtension(imageFile);
                string fn = Path.ChangeExtension(imageFile, $"_masked{fe}");
                Image img = MaskFace( image, faceSize );
                if ( removeExif )
                {
                    //img.PropertyItems.
                    foreach ( int id in img.PropertyIdList )
                    {
                        img.RemovePropertyItem( id );
                    }
                }
                img.Save( fn );
                return ( fn );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image MaskFace(Image image, int faceSize=20)
        {
            if ( image == null ) return image;

            detector.MinSize = new Size( faceSize, faceSize );
            detector.UseParallelProcessing = true;
            detector.Suppression = 2;
            detector.SearchMode = (ObjectDetectorSearchMode) cbMode.SelectedValue;
            detector.ScalingMode = (ObjectDetectorScalingMode) cbScaling.SelectedValue;
            if ( image.Width > 1600 || image.Height> 1600)
            {
                detector.SearchMode = ObjectDetectorSearchMode.Average;
                detector.Suppression = 4;
                //detector.ScalingFactor = 1.2f;
            }
            else
            {
                //detector.ScalingFactor = 1.5f;
            }
            detector.ScalingFactor = 1.2f;

            //Stopwatch sw = Stopwatch.StartNew();

            // Process frame to detect objects
            Rectangle[] faces = detector.ProcessFrame(image as Bitmap);
            //detector.

            //sw.Stop();

            if ( faces.Length > 0 )
            {
                RectanglesMarker marker = new RectanglesMarker(faces, Color.Fuchsia);
                //marker.FillColor = Color.WhiteSmoke;
                //pic.Image = marker.Apply( photo );
                photo_mask = image.Clone() as Image;
                using ( Graphics g = Graphics.FromImage( photo_mask ) )
                {
                    foreach ( Rectangle r in marker.Rectangles )
                    {
                        g.DrawImage( mask,
                            r.Left - (float) ( r.Width / 2.0 ),
                            r.Top - (float) ( r.Height / 2.0 ),
                            r.Width * 2,
                            r.Height * 2 );
                    }
                }
                return ( photo_mask );
            }
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
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            if (File.Exists( "mask.png" ) )
            {
                mask = new Bitmap( "mask.png" );
            }
            else
            {
                mask = Icon.ToBitmap();
            }

            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.Image = photo;

            picMask.SizeMode = PictureBoxSizeMode.Zoom;
            picMask.Image = mask;

            cbMode.DataSource = Enum.GetValues( typeof( ObjectDetectorSearchMode ) );
            cbScaling.DataSource = Enum.GetValues( typeof( ObjectDetectorScalingMode ) );

            cbMode.SelectedItem = ObjectDetectorSearchMode.NoOverlap;
            cbScaling.SelectedItem = ObjectDetectorScalingMode.SmallerToGreater;

            HaarCascade cascade = new FaceHaarCascade();
            detector = new HaarObjectDetector( cascade );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMask_Click( object sender, EventArgs e )
        {
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
            if( dlgOpen.ShowDialog(this) == DialogResult.OK)
            {
                if ( mask != null ) mask.Dispose();
                mask = new Bitmap( dlgOpen.FileName );
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
                string f = e.Item.SubItems[1].Text;
                Text = f;
                if(File.Exists(f))
                {
                    if(photo != null) photo.Dispose();
                    photo = ResizeImage( RotateImage( new Bitmap( f ) ), (int) numOutSize.Value ) as Bitmap;
                    picPreview.Image = MaskFace( photo, 25 );
                    tsInfo.Text = $"Size: {picPreview.Image.Width} x {picPreview.Image.Height}";
                }
            }
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

            foreach (string f in flist)
            {
                if( PhotoExts.Contains( Path.GetExtension( f ), StringComparer.CurrentCultureIgnoreCase ) )
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
        private void bgwMask_DoWork( object sender, DoWorkEventArgs e )
        {
            btnMask.Enabled = false;

            int faceSize = (int)numFaceSize.Value;
            bool removeExif = chkRemoveEXIF.Checked;

            //tsProgress.Value = 0;

            foreach ( ListViewItem l in lvFiles.Items )
            {
                if ( bgwMask.CancellationPending ) break;

                //picPreview.Image = MaskFace( photo, (int) numFaceSize.Value );
                string f = l.SubItems[0].Text;
                if ( File.Exists( f ) )
                {
                    MaskFace( f, faceSize, removeExif );
                    bgwMask.ReportProgress( (int) ( l.Index *100.0 / lvFiles.Items.Count ) );
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
