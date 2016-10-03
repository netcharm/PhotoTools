using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Accord.Imaging;
using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;
using AutoMask.common;

namespace AutoMask
{
    public partial class MainForm : Form
    {
        private HaarObjectDetector detector;

        private Image mask = null;
        private Image photo = null;
        private Image photo_mask = null;
        private Image erase = null;

        private string[] PhotoExts = { ".jpg", ".jpeg", ".tif",".tiff", ".bmp", ".png", ".gif" };

        private ListViewItem[] files = null;
        private ObjectDetectorSearchMode SearchMode = ObjectDetectorSearchMode.NoOverlap;
        private ObjectDetectorScalingMode ScalingMode = ObjectDetectorScalingMode.SmallerToGreater;
        private int faceSize = 25;
        private int OutSize = 1200;
        private bool GrayFirst = false;

        private Dictionary<PointF, Image> faceList = new Dictionary<PointF, Image>();

        private Stopwatch watch = Stopwatch.StartNew();

        private bool mouseDown = false;
        private ActionMode action = ActionMode.None;

        private float faceSizeAverage = 25.0F;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maskFile"></param>
        /// <returns></returns>
        private Image LoadMask(string maskFile)
        {
            using ( Image m = Accord.Imaging.Image.FromFile( maskFile ) )
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
            //using ( Image image = Accord.Imaging.Image.FromFile( imageFile ) )
            using ( Image image = ImageFast.FromFile( imageFile ) )
            {
                return ( MaskFace( ResizeImage( RotateImage( image ), OutSize ), faceSize ) );
            }
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
            string fd = Path.GetDirectoryName(imageFile);
            string fe = Path.GetExtension(imageFile);
            fn = Path.Combine( fd, $"{Path.GetFileNameWithoutExtension( imageFile )}_masked{fe}" );

            using ( Image src = ResizeImage( RotateImage( Accord.Imaging.Image.FromFile( imageFile ) ), OutSize ) )
            {
                //using ( Image dst = src.Clone() as Image )
                using ( Image dst = new Bitmap( src.Width, src.Height, PixelFormat.Format32bppArgb ) )
                {
                    using ( Graphics g = Graphics.FromImage( dst ) )
                    {
                        g.DrawImage( src, 0, 0, src.Width, src.Height );
                        g.DrawImage( MaskFace( src, faceSize ), 0, 0, src.Width, src.Height );
                    }
                    if ( !removeExif )
                    {
                        foreach ( PropertyItem pi in src.PropertyItems )
                        {
                            dst.SetPropertyItem( pi );
                        }
                    }
                    else
                    {
                        foreach ( int id in dst.PropertyIdList )
                        {
                            dst.RemovePropertyItem( id );
                        }
                    }
                    dst.Save( fn, ImageFormat.Jpeg );
                }
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
                dst.PixelFormat != PixelFormat.Format8bppIndexed )
            {
                dst = new Grayscale( 0.2125, 0.7154, 0.0721 ).Apply( dst as Bitmap ) as Image;
            }
            Rectangle[] faces = detector.ProcessFrame(dst as Bitmap);
            if ( faces.Length <= 0 )
            {
                detector.SearchMode = ObjectDetectorSearchMode.Average;
                //faces = detector.ProcessFrame( image as Bitmap );
                faces = detector.ProcessFrame( dst as Bitmap );
            }
            #endregion
            faceSizeAverage = (float)faces.Average( o => o.Width );

            #region Create detected face image list
            faceList.Clear();
            if ( faces.Length > 0 )
            {
                float factorX = 1.75f;
                float factorY = 1.75f;
                foreach ( Rectangle r in faces )
                {
                    PointF k = new PointF(
                        r.Left + (float) ( r.Width * ( 1 - factorX ) / 2.0f ),
                        r.Top + (float) ( r.Height * ( 1 - factorY ) / 2.0f )
                    );
                    Image v = new Bitmap( 
                        (int)Math.Ceiling(r.Width*factorX), 
                        (int)Math.Ceiling(r.Height*factorY), 
                        PixelFormat.Format32bppArgb 
                    );
                    using ( Graphics g = Graphics.FromImage( v ) )
                    {
                        g.DrawImage( mask, 0, 0, v.Width, v.Height );
                    }
                    faceList.Add( k, v);
                }
            }
            #endregion

            #region Draw mask to photo
            if ( faces.Length > 0 )
            {
                photo_mask = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );
                using ( Graphics g = Graphics.FromImage( photo_mask ) )
                {
                    float factorX = 1.75f;
                    float factorY = 1.75f;
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

        /// <summary>
        /// 
        /// </summary>
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
            watch.Stop();

            #region extracting icon from application to this form window
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            #endregion
            
            #region Load default Mask Image
            if ( File.Exists( "mask.png" ) )
            {
                mask = LoadMask( "mask.png" );
            }
            else
            {
                mask = Icon.ToBitmap();
            }
            erase = new Bitmap( mask.Width, mask.Height, PixelFormat.Format32bppArgb );
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
        private void btnMaskAll_Click( object sender, EventArgs e )
        {
            btnMaskAll.Enabled = false;
            //btnMaskAll.BackColor = Color.DimGray;
            btnMaskAll.BackColor = SystemColors.GrayText;

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
                picPreview.Image = null;// photo;
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
                mask = LoadMask( dlgOpen.FileName );
                erase = new Bitmap( mask.Width, mask.Height, PixelFormat.Format32bppArgb );
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
                if (File.Exists(ff))
                {
                    watch.Restart();
                    if (photo != null) photo.Dispose();
                    using ( Image of = Accord.Imaging.Image.FromFile( ff ) )
                    //using ( Image of = ImageFast.FromFile( ff ) )
                    {
                        photo = ResizeImage( RotateImage( of ), OutSize ) as Bitmap;

                        picPreview.BackgroundImage = photo;
                        picPreview.BackgroundImageLayout = ImageLayout.Zoom;

                        picPreview.Image = MaskFace( photo, faceSize );
                        picPreview.SizeMode = PictureBoxSizeMode.Zoom;
                        
                        tsInfoFileName.Text = $"{fn}";
                        tsInfoFileSize.Text = $"Image: {of.Width} x {of.Height}";
                        tsInfoPreviewSize.Text = $"View: {picPreview.Image.Width} x {picPreview.Image.Height}";
                        //tsInfo.Text = "OK";
                    }
                    watch.Stop();
                    tsInfo.Text = $"Cost {watch.ElapsedTicks / 1000000.0:n3}s";
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
            btnMaskAll.Enabled = false;
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
            btnMaskAll.PerformClick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMask_DoWork( object sender, DoWorkEventArgs e )
        {
            watch.Restart();

            btnMaskAll.Enabled = false;

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
            btnMaskAll.BackColor = SystemColors.Control;
            btnMaskAll.Enabled = true;
            watch.Stop();
            tsInfo.Text = $"Cost {watch.ElapsedTicks / 1000000.0:n3}s";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaskAtPreview_Click( object sender, EventArgs e )
        {
            //if()
            //btnMaskAtPreview.FlatStyle = FlatStyle.Flat;
            btnMaskAtPreview.BackColor = Color.DeepSkyBlue;
            btnEraseAtPreview.BackColor = SystemColors.Control;
            action = ActionMode.Mask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEraseAtPreview_Click( object sender, EventArgs e )
        {
            btnMaskAtPreview.BackColor = SystemColors.Control;
            btnEraseAtPreview.BackColor = Color.DeepSkyBlue;
            action = ActionMode.Erase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click( object sender, EventArgs e )
        {
            //
        }

        private void picPreview_MouseDown( object sender, MouseEventArgs e )
        {
            if ( picPreview.Image == null )
            {
                mouseDown = true;
                return;
            }
            mouseDown = true;
        }

        private void picPreview_MouseUp( object sender, MouseEventArgs e )
        {
            if ( mouseDown )
            {
                if ( action == ActionMode.Mask )
                {

                }
                else if ( action == ActionMode.Erase )
                {
                    DrawErase( e.Location );
                }
                photo_mask = picPreview.Image;
            }
            mouseDown = false;
        }

        private void picPreview_MouseLeave( object sender, EventArgs e )
        {
            if(mouseDown)
                photo_mask = picPreview.Image;
            mouseDown = false;
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Gets the mouse position over the image when the <span class="code-SummaryComment"><see cref="PictureBox">PictureBox's</span>
        /// <span class="code-SummaryComment"></see> <see cref="PictureBox.SizeMode">SizeMode</see> is set to Zoom</span>
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="coordinates">Point to translate</param></span>
        /// <span class="code-SummaryComment"><returns>A point relative to the top left corner of the </span>
        /// <span class="code-SummaryComment"><see cref="PictureBox.Image">Image</see></span>
        /// If the Image is null, no translation is performed
        /// <span class="code-SummaryComment"></returns></span>
        protected Point TranslateZoomMousePosition( Point coordinates )
        {
            // test to make sure our image is not null
            if ( picPreview.Image == null ) return coordinates;
            // Make sure our control width and height are not 0 and our 
            // image width and height are not 0
            if ( Width == 0 || Height == 0 || picPreview.Image.Width == 0 || picPreview.Image.Height == 0 ) return coordinates;
            // This is the one that gets a little tricky. Essentially, need to check 
            // the aspect ratio of the image to the aspect ratio of the control
            // to determine how it is being rendered
            float imageAspect = (float)picPreview.Image.Width / picPreview.Image.Height;
            float controlAspect = (float)picPreview.Width / picPreview.Height;
            float newX = coordinates.X;
            float newY = coordinates.Y;
            if ( imageAspect > controlAspect )
            {
                // This means that we are limited by width, 
                // meaning the image fills up the entire control from left to right
                float ratioWidth = (float)picPreview.Image.Width / picPreview.Width;
                newX *= ratioWidth;
                float scale = (float)picPreview.Width / picPreview.Image.Width;
                float displayHeight = scale * picPreview.Image.Height;
                float diffHeight = picPreview.Height - displayHeight;
                diffHeight /= 2;
                newY -= diffHeight;
                newY /= scale;
            }
            else
            {
                // This means that we are limited by height, 
                // meaning the image fills up the entire control from top to bottom
                float ratioHeight = (float)picPreview.Image.Height / picPreview.Height;
                newY *= ratioHeight;
                float scale = (float)picPreview.Height / picPreview.Image.Height;
                float displayWidth = scale * picPreview.Image.Width;
                float diffWidth = picPreview.Width - displayWidth;
                diffWidth /= 2;
                newX -= diffWidth;
                newX /= scale;
            }
            return new Point( (int) newX, (int) newY );
        }

        private void DrawErase( Point coordinates )
        {
            using ( Graphics gd = Graphics.FromImage( picPreview.Image ) )
            {
                PointF pos = TranslateZoomMousePosition(coordinates);

                RectangleF rd = new RectangleF();
                rd.X = pos.X - faceSizeAverage / 2.0f;
                rd.Y = pos.Y - faceSizeAverage / 2.0f;
                rd.Height = faceSizeAverage;
                rd.Width = faceSizeAverage;

                Rectangle rs = new Rectangle();
                rs.X = 0;
                rs.Y = 0;
                rs.Height = mask.Width;
                rs.Width = mask.Height;

                using ( Graphics g = Graphics.FromImage( erase ) )
                {
                    Rectangle r = new Rectangle();
                    r.X = 0;
                    r.Y = 0;
                    r.Height = mask.Width;
                    r.Width = mask.Height;

                    g.DrawImage( picPreview.BackgroundImage, r, rd, GraphicsUnit.Pixel );
                }

                var bitsMask = (mask as Bitmap).LockBits(rs, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                var bitErase = (erase as Bitmap).LockBits(rs, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                unsafe
                {
                    for ( int y = 0; y < mask.Height; y++ )
                    {
                        byte* ptrMask = (byte*) bitsMask.Scan0 + y * bitsMask.Stride;
                        byte* ptrOutput = (byte*) bitErase.Scan0 + y * bitErase.Stride;
                        for ( int x = 0; x < mask.Width; x++ )
                        {
                            ptrOutput[4 * x + 3] = ptrMask[4 * x + 3];        // alpha
                        }
                    }
                }
                ( mask as Bitmap ).UnlockBits( bitsMask );
                ( erase as Bitmap ).UnlockBits( bitsMask );
                //erase.Save( "test_01.png" );

                gd.DrawImage( erase, rd, rs, GraphicsUnit.Pixel );
            }
            picPreview.Invalidate();
        }

        private void picPreview_MouseMove( object sender, MouseEventArgs e )
        {
            if ( mouseDown )
            {
                if ( action == ActionMode.Mask )
                {

                }
                else if ( action == ActionMode.Erase )
                {
                    DrawErase( e.Location );
                }
            }
        }

        private void picPreview_Paint( object sender, PaintEventArgs e )
        {

        }
    }
}
