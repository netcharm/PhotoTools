using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ExtensionMethods;
using Media = System.Windows.Media;

namespace DialogTest
{
    public partial class MainForm : Form
    {
        private bool fontApplyTest;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnColorDialogSystem_Click( object sender, EventArgs e )
        {
            ColorDialog dlgColor = new ColorDialog();
            if ( dlgColor.ShowDialog() == DialogResult.OK )
            {
                btnColorDialogSystem.BackColor = dlgColor.Color;
            }
        }

        private void dlgColor_Apply( object sender, EventArgs e )
        {
            fontApplyTest = true;

            //string c = option.TextColor;

            //option.TextColor = dlgColor.Color.ToHtml();
            //Preview();
            //option.TextColor = c;

            fontApplyTest = false;
        }

        private void btnColorDialog_Click( object sender, EventArgs e )
        {
            NetCharm.Common.ColorDialog dlgColor = new NetCharm.Common.ColorDialog();
            dlgColor.Apply += new System.EventHandler( dlgColor_Apply );
            dlgColor.Color = Color.Red;
            if ( dlgColor.ShowDialog() == DialogResult.OK )
            {
                //option.TextColor = dlgColor.Color.ToHtml();
                //Preview();
            }
            else
            {
                //option.TextColor = c;
                //Preview();
            }
        }

        private void dlgColorEx_Apply( object sender, EventArgs e )
        {
            MessageBox.Show( "Color Apply" );
        }

        private void btnColorDilogEx_Click( object sender, EventArgs e )
        {
            //NetCharm.Common.Controls.ColorDialogEx dlgColorEx = new NetCharm.Common.Controls.ColorDialogEx();
            if ( dlgColorEx.ShowDialog( Color.Blue ) == DialogResult.OK )
            {
                var colors = dlgColorEx.CustomColors;
            }
        }

        private void btnFontDialogSystem_Click( object sender, EventArgs e )
        {
            FontDialog dlgFont = new FontDialog();
            dlgFont.ShowDialog();
        }

        private void dlgFont_Apply( object sender, EventArgs e )
        {
            fontApplyTest = true;

            var face = dlgFontEx.TypefaceName + (dlgFontEx.Underline? " Underline" : "") + (dlgFontEx.Strikeout? " Strikeout" : "");
            var sample = "中文Text".ToBitmap( dlgFontEx.FamilyName, face, dlgFontEx.Size, dlgFontEx.Color);
            //picBox.Image = Shadow( sample, Color.DarkGray, 5 );
            //picBox.Image = Blur( sample, 15 );
            picBox.Image = Outline( sample, Color.Black, 5, 1.5 );

            fontApplyTest = false;
        }

        private void btnFontDialog_Click( object sender, EventArgs e )
        {
            NetCharm.Common.FontDialog dlgFont = new NetCharm.Common.FontDialog();
            dlgFont.Apply += new System.EventHandler( dlgFont_Apply );
            //dlgFont.Color = color;
            dlgFont.Font = SystemFonts.DefaultFont;
            dlgFont.FontSize = 12;
            if ( dlgFont.ShowDialog() == DialogResult.OK )
            {
                //
            }
            else
            {
                //option.TextColor = c;
                //Preview();
            }
            return;
        }

        private void btnFontDialogEx_Click( object sender, EventArgs e )
        {
            dlgFontEx.Font = SystemFonts.DefaultFont;
            dlgFontEx.Size = 24;
            if ( dlgFontEx.ShowDialog() == DialogResult.OK )
            {
                var face = dlgFontEx.TypefaceName + (dlgFontEx.Underline? " Underline" : "") + (dlgFontEx.Strikeout? " Strikeout" : "");
                var sample = "中文Text".ToBitmap( dlgFontEx.FamilyName, face, dlgFontEx.Size, dlgFontEx.Color );
                //picBox.Image = Shadow( sample, Color.DarkGray, 5 );
                picBox.Image = Outline( sample, Color.DarkGray, 5 );
            }
            else
            {
                //
            }
        }

        public Bitmap Blur( Bitmap src, double radius = 10 )
        {
            Bitmap result = new Bitmap(src);
            #region Get DPI 
            float dpiX = 96f;
            float dpiY = 96f;

            using ( Graphics g = Graphics.FromHwnd( IntPtr.Zero ) )
            {
                dpiX = g.DpiX;
                dpiY = g.DpiY;
            }
            #endregion

            int width = (int)Math.Ceiling(radius);
            int offset = 4*width;

            #region Create Effect
            var effect = new Media.Effects.BlurEffect();
            effect.Radius = radius;
            #endregion

            #region Draw source bitmap to DrawingVisual
            Media.DrawingVisual drawingVisual = new Media.DrawingVisual();
            drawingVisual.Effect = effect;
            using ( var drawingContext = drawingVisual.RenderOpen() )
            {
                //drawingContext.PushEffect( new Media.Effects.BlurBitmapEffect(), null );
                System.Windows.Rect dRect = new System.Windows.Rect(2*width, 2*width, src.Width, src.Height);
                drawingContext.DrawImage( src.ToBitmapSource(), dRect );
                drawingContext.Close();
            }
            #endregion

            #region Render the DrawingVisual into a RenderTargetBitmap 
            var rtb = new Media.Imaging.RenderTargetBitmap(
              src.Width + offset, src.Height * offset,
              dpiX, dpiY,
              Media.PixelFormats.Pbgra32 );
            rtb.Render( drawingVisual );
            #endregion

            #region Create a System.Drawing.Bitmap 
            var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppArgb );
            #endregion

            #region Copy the RenderTargetBitmap pixels into the bitmap's pixel buffer
            var pdata = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly,
                bitmap.PixelFormat);
            rtb.CopyPixels( System.Windows.Int32Rect.Empty,
                pdata.Scan0, pdata.Stride * pdata.Height, pdata.Stride );
            bitmap.UnlockBits( pdata );
            #endregion

            #region Crop Opaque
            var rect = bitmap.ContentBound();
            rect.Width = rect.Width < 0 ? 1 : rect.Width + 2;
            rect.Height = rect.Height < 0 ? 1 : rect.Height + 2;
            result = new Bitmap( rect.Width, rect.Height, bitmap.PixelFormat );
            using ( var g = Graphics.FromImage( result ) )
            {
                g.DrawImage( bitmap, 0, 0, rect, GraphicsUnit.Pixel );
            }
            #endregion

            return ( result );
        }

        public Bitmap Outline( Bitmap src, Color color, int width, double opacity = 1.0f )
        {
            Bitmap result = new Bitmap(src);
            #region Get DPI 
            float dpiX = 96f;
            float dpiY = 96f;

            using ( Graphics g = Graphics.FromHwnd( IntPtr.Zero ) )
            {
                dpiX = g.DpiX;
                dpiY = g.DpiY;
            }
            #endregion

            int offset = 4*width;

            #region Create Effect
            var effect = new Media.Effects.DropShadowEffect();
            effect.BlurRadius = width;
            effect.Color = color.ToMediaColor();
            effect.Opacity = opacity;
            effect.ShadowDepth = 0;

            //var effect = new Media.Effects.BlurEffect();
            //effect.Radius = 50;
            #endregion

            #region Draw source bitmap to DrawingVisual
            Media.DrawingVisual drawingVisual = new Media.DrawingVisual();

            drawingVisual.Effect = effect;
            using ( var drawingContext = drawingVisual.RenderOpen() )
            {
                System.Windows.Rect dRect = new System.Windows.Rect(2*width, 2*width, src.Width, src.Height);
                drawingContext.DrawImage( src.ToBitmapSource(), dRect );
                drawingContext.Close();
            }
            #endregion

            #region Render the DrawingVisual into a RenderTargetBitmap 
            var rtb = new Media.Imaging.RenderTargetBitmap(
              src.Width + offset, src.Height + offset,
              dpiX, dpiY,
              Media.PixelFormats.Pbgra32 );
            rtb.Render( drawingVisual );
            #endregion

            #region Create a System.Drawing.Bitmap 
            var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppArgb );
            #endregion

            #region Copy the RenderTargetBitmap pixels into the bitmap's pixel buffer
            var pdata = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly,
                bitmap.PixelFormat);
            rtb.CopyPixels( System.Windows.Int32Rect.Empty,
                pdata.Scan0, pdata.Stride * pdata.Height, pdata.Stride );
            bitmap.UnlockBits( pdata );
            #endregion

            #region Crop Transparent Area
            var sts0 = DateTime.Now.Ticks;
            var srcRect = bitmap.ContentBound();
            var ste0 = DateTime.Now.Ticks - sts0;

            var sts1 = DateTime.Now.Ticks;
            var srcRect1 = ContentBound( bitmap );
            var ste1 = DateTime.Now.Ticks - sts1;

            //result = new Bitmap( srcRect.Width, srcRect.Height, bitmap.PixelFormat );
            result = new Bitmap( srcRect1.Width, srcRect1.Height, bitmap.PixelFormat );
            using ( var g = Graphics.FromImage( result ) )
            {
                g.DrawImage( bitmap, 0, 0, srcRect1, GraphicsUnit.Pixel );
            }
            #endregion

            return ( result );
        }

        public Bitmap Shadow( Bitmap src, Color color, int width, double opacity = 0.6f, double angle = 315 )
        {
            Bitmap result = new Bitmap(src);
            #region Get DPI 
            float dpiX = 96f;
            float dpiY = 96f;

            using ( Graphics g = Graphics.FromHwnd( IntPtr.Zero ) )
            {
                dpiX = g.DpiX;
                dpiY = g.DpiY;
            }
            #endregion

            int offset = 4*width;

            #region Create Effect
            var effect = new Media.Effects.DropShadowEffect();
            effect.BlurRadius = width;
            effect.Color = color.ToMediaColor();
            effect.Direction = angle;
            effect.Opacity = opacity;
            effect.ShadowDepth = width;

            //var effect = new Media.Effects.BlurEffect();
            //effect.Radius = 50;
            #endregion

            #region Draw source bitmap to DrawingVisual
            Media.DrawingVisual drawingVisual = new Media.DrawingVisual();

            drawingVisual.Effect = effect;
            using ( var drawingContext = drawingVisual.RenderOpen() )
            {
                System.Windows.Rect dRect = new System.Windows.Rect(2*width, 2*width, src.Width, src.Height);
                drawingContext.DrawImage( src.ToBitmapSource(), dRect );
                drawingContext.Close();
            }
            #endregion

            #region Render the DrawingVisual into a RenderTargetBitmap 
            var rtb = new Media.Imaging.RenderTargetBitmap(
              src.Width + offset, src.Height + offset,
              dpiX, dpiY,
              Media.PixelFormats.Pbgra32 );
            rtb.Render( drawingVisual );
            #endregion

            #region Create a System.Drawing.Bitmap 
            var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppArgb );
            #endregion

            #region Copy the RenderTargetBitmap pixels into the bitmap's pixel buffer
            var pdata = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly,
                bitmap.PixelFormat);
            rtb.CopyPixels( System.Windows.Int32Rect.Empty,
                pdata.Scan0, pdata.Stride * pdata.Height, pdata.Stride );
            bitmap.UnlockBits( pdata );
            #endregion

            #region Crop Transparent Area
            var rect = bitmap.ContentBound();
            result = new Bitmap( rect.Width, rect.Height, bitmap.PixelFormat );
            using ( var g = Graphics.FromImage( result ) )
            {
                g.DrawImage( bitmap, 0, 0, rect, GraphicsUnit.Pixel );
            }
            #endregion

            return ( result );
        }

        public Bitmap Shader( Bitmap src, double radius = 10 )
        {
            Bitmap result = new Bitmap(src);
            #region Get DPI 
            float dpiX = 96f;
            float dpiY = 96f;

            using ( Graphics g = Graphics.FromHwnd( IntPtr.Zero ) )
            {
                dpiX = g.DpiX;
                dpiY = g.DpiY;
            }
            #endregion

            int width = (int)Math.Ceiling(radius);
            int offset = 4*width;

            #region Create Effect
            var effect = new MyShaderEffect();
            //Media.Effects.ShaderRenderMode = Media.Effects.ShaderRenderMode.Auto;
            //effect.
            #endregion

            #region Draw source bitmap to DrawingVisual
            Media.DrawingVisual drawingVisual = new Media.DrawingVisual();
            drawingVisual.Effect = effect;
            using ( var drawingContext = drawingVisual.RenderOpen() )
            {
                //drawingContext.PushEffect( new Media.Effects.BlurBitmapEffect(), null );
                System.Windows.Rect dRect = new System.Windows.Rect(2*width, 2*width, src.Width, src.Height);
                drawingContext.DrawImage( src.ToBitmapSource(), dRect );
                drawingContext.Close();
            }
            #endregion

            #region Render the DrawingVisual into a RenderTargetBitmap 
            var rtb = new Media.Imaging.RenderTargetBitmap(
              src.Width + offset, src.Height * offset,
              dpiX, dpiY,
              Media.PixelFormats.Pbgra32 );
            rtb.Render( drawingVisual );
            #endregion

            #region Create a System.Drawing.Bitmap 
            var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppArgb );
            #endregion

            #region Copy the RenderTargetBitmap pixels into the bitmap's pixel buffer
            var pdata = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly,
                bitmap.PixelFormat);
            rtb.CopyPixels( System.Windows.Int32Rect.Empty,
                pdata.Scan0, pdata.Stride * pdata.Height, pdata.Stride );
            bitmap.UnlockBits( pdata );
            #endregion

            #region Crop Opaque
            var rect = bitmap.ContentBound();
            rect.Width = rect.Width < 0 ? 1 : rect.Width + 2;
            rect.Height = rect.Height < 0 ? 1 : rect.Height + 2;
            result = new Bitmap( rect.Width, rect.Height, bitmap.PixelFormat );
            using ( var g = Graphics.FromImage( result ) )
            {
                g.DrawImage( bitmap, 0, 0, rect, GraphicsUnit.Pixel );
            }
            #endregion

            return ( result );
        }

        public Rectangle ContentBound( Bitmap src, RefColorMode mode = RefColorMode.Alpha)
        {
            Rectangle result = new Rectangle(0, 0, src.Width, src.Height);

            LockBitmap lockbmp = new LockBitmap(src);
            //锁定Bitmap，通过Pixel访问颜色
            lockbmp.LockBits();
            #region Get Ref Color value
            Color cRef = lockbmp.GetPixel(0, 0);
            switch ( mode )
            {
                case RefColorMode.Alpha:
                    cRef = Color.Transparent;
                    break;
                case RefColorMode.TopLeft:
                    cRef = lockbmp.GetPixel( 0, 0 );
                    break;
                case RefColorMode.BottomRight:
                    cRef = lockbmp.GetPixel( lockbmp.Width - 1, lockbmp.Height - 1 );
                    break;
            }
            #endregion

            #region Check pixels value with Ref Value
            int xMin = 0;
            int xMax = lockbmp.Width-1;
            int yMin = 0;
            int yMax = lockbmp.Height-1;
            var w = lockbmp.Width-1;
            var h = lockbmp.Height-1;
            var hh = (int)Math.Ceiling(lockbmp.Height / 2.0f)+1;
            var wh = (int)Math.Ceiling(lockbmp.Width / 2.0f)+1;

            #region Get Bound Top & Bottom
            for ( var y = 0; y < hh; y++ )
            {
                for ( var x = 0; x < w + 1; x++ )
                {
                    var yc = h - y;

                    Color ct = lockbmp.GetPixel( x, y );
                    Color cb = lockbmp.GetPixel( x, yc );
                    switch ( mode )
                    {
                        case RefColorMode.Alpha:
                            if ( ct.A != cRef.A )
                            {
                                if ( yMin == 0 ) yMin = y - 1;
                            }
                            if ( cb.A != cRef.A )
                            {
                                if ( yMax == h ) yMax = yc + 1;
                            }
                            break;
                        default:
                            if ( ct.A != cRef.A || ct.R != cRef.R || ct.G != cRef.G || ct.B != cRef.B )
                            {
                                if ( yMin == 0 ) yMin = y - 1;
                            }
                            if ( cb.A != cRef.A || cb.R != cRef.R || cb.G != cRef.G || cb.B != cRef.B )
                            {
                                if ( yMax == h ) yMax = yc + 1;
                            }
                            break;
                    }
                    if ( yMin != 0 && yMax != h ) break;
                }
                if ( yMin != 0 && yMax != h ) break;
            }
            #endregion

            #region Get Bound Left & Right
            for ( var x = 0; x < wh; x++ )
            {
                for ( var y = yMin + 1; y < yMax; y++ )
                {
                    var xc = w - x;

                    Color cl = lockbmp.GetPixel( x, y );
                    Color cr = lockbmp.GetPixel( xc, y );
                    switch ( mode )
                    {
                        case RefColorMode.Alpha:
                            if ( cl.A != cRef.A )
                            {
                                if ( xMin == 0 ) xMin = x - 1;
                            }
                            if ( cr.A != cRef.A )
                            {
                                if ( xMax == w ) xMax = xc + 1;
                            }
                            break;
                        default:
                            if ( cl.A != cRef.A || cl.R != cRef.R || cl.G != cRef.G || cl.B != cRef.B )
                            {
                                if ( xMin == 0 ) xMin = x - 1;
                            }
                            if ( cr.A != cRef.A || cr.R != cRef.R || cr.G != cRef.G || cr.B != cRef.B )
                            {
                                if ( xMax == w ) xMax = xc + 1;
                            }
                            break;
                    }
                    if ( xMin != 0 && xMax != w ) break;
                }
                if ( xMin != 0 && xMax != w ) break;
            }
            #endregion

            #endregion

            //从内存解锁Bitmap
            lockbmp.UnlockBits();

            #region adjust bound size
            xMin = xMin < 0 ? 0 : xMin;
            xMax = xMax >= src.Width ? src.Width - 1 : xMax;
            yMin = yMin < 0 ? 0 : yMin;
            yMax = yMax >= src.Height ? src.Height - 1 : yMax;
            result.X = xMin;
            result.Y = yMin;
            result.Width = ( xMax - xMin ) < 0 ? src.Width : xMax - xMin;
            result.Height = ( yMax - yMin ) < 0 ? src.Height : yMax - yMin;
            #endregion

            return ( result );
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            #region extracting icon from application to this form window
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            #endregion
        }
    }

    public class MyShaderEffect : Media.Effects.ShaderEffect
    {
        private static Media.Effects.PixelShader _pixelShader =
            new Media.Effects.PixelShader() { UriSource = MakePackUri("shaders/ThresholdEffect.fx.ps") };

        public MyShaderEffect()
        {
            PixelShader = _pixelShader;

            UpdateShaderValue( InputProperty );
            UpdateShaderValue( ThresholdProperty );
            UpdateShaderValue( BlankColorProperty );
        }

        // MakePackUri is a utility method for computing a pack uri
        // for the given resource. 
        public static Uri MakePackUri( string relativeFile )
        {
            Assembly a = typeof(MyShaderEffect).Assembly;

            // Extract the short name.
            string assemblyShortName = a.ToString().Split(',')[0];

            string uriString = "pack://application:,,,/" +
                assemblyShortName +
                ";component/" +
                relativeFile;

            return new Uri( uriString );
        }

        ///////////////////////////////////////////////////////////////////////
        #region Input dependency property

        public Brush Input
        {
            get { return (Brush) GetValue( InputProperty ); }
            set { SetValue( InputProperty, value ); }
        }

        public static readonly DependencyProperty InputProperty =
            Media.Effects.ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(MyShaderEffect), 0);

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region Threshold dependency property

        public double Threshold
        {
            get { return (double) GetValue( ThresholdProperty ); }
            set { SetValue( ThresholdProperty, value ); }
        }

        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(MyShaderEffect),
                    new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)));

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region BlankColor dependency property

        public Color BlankColor
        {
            get { return (Color) GetValue( BlankColorProperty ); }
            set { SetValue( BlankColorProperty, value ); }
        }

        public static readonly DependencyProperty BlankColorProperty =
            DependencyProperty.Register("BlankColor", typeof(Color), typeof(MyShaderEffect),
                    new UIPropertyMetadata(Media.Colors.Transparent, PixelShaderConstantCallback(1)));

        #endregion

    }

    public enum RefColorMode
    {
        Alpha = 0,
        TopLeft = 1,
        BottomRight = 2
    }

    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap( Bitmap source )
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                int PixelCount = Width * Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize( source.PixelFormat );

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if ( Depth != 8 && Depth != 24 && Depth != 32 )
                {
                    throw new ArgumentException( "Only 8, 24 and 32 bpp images are supported." );
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits( rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat );

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                System.Runtime.InteropServices.Marshal.Copy( Iptr, Pixels, 0, Pixels.Length );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                System.Runtime.InteropServices.Marshal.Copy( Pixels, 0, Iptr, Pixels.Length );

                // Unlock bitmap data
                source.UnlockBits( bitmapData );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel( int x, int y )
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if ( i > Pixels.Length - cCount )
                throw new IndexOutOfRangeException();

            if ( Depth == 32 ) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb( a, r, g, b );
            }
            if ( Depth == 24 ) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb( r, g, b );
            }
            if ( Depth == 8 )
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb( c, c, c );
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel( int x, int y, Color color )
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if ( Depth == 32 ) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if ( Depth == 24 ) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if ( Depth == 8 )
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }
    }

}
