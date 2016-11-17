using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using Accord.Imaging.Filters;
using NetCharm.Image.Addins.Common;
using NGettext.WinForm;

namespace NetCharm.Image.Addins
{
    /// <summary>
    /// 
    /// </summary>
    public static class AddinUtils
    {
        #region PixelFormat catalogs
        /// <summary>
        /// 
        /// </summary>
        public static System.Drawing.Imaging.PixelFormat[] AlphaFormat = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Canonical,
            System.Drawing.Imaging.PixelFormat.Alpha,
            System.Drawing.Imaging.PixelFormat.PAlpha,
            System.Drawing.Imaging.PixelFormat.Format16bppArgb1555,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb,
            System.Drawing.Imaging.PixelFormat.Format32bppPArgb,
            System.Drawing.Imaging.PixelFormat.Format64bppArgb,
            System.Drawing.Imaging.PixelFormat.Format64bppPArgb
        };
        public static System.Drawing.Imaging.PixelFormat[] Format1bpp = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Format1bppIndexed
        };
        public static System.Drawing.Imaging.PixelFormat[] Format4bpp = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Format4bppIndexed
        };
        public static System.Drawing.Imaging.PixelFormat[] Format8bpp = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Format8bppIndexed
        };
        public static System.Drawing.Imaging.PixelFormat[] Format15bpp = new System.Drawing.Imaging.PixelFormat[]
        {            
        };
        public static System.Drawing.Imaging.PixelFormat[] Format16bpp = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Format16bppArgb1555,
            System.Drawing.Imaging.PixelFormat.Format16bppGrayScale,
            System.Drawing.Imaging.PixelFormat.Format16bppRgb555,
            System.Drawing.Imaging.PixelFormat.Format16bppRgb565
        };
        public static System.Drawing.Imaging.PixelFormat[] Format24bpp = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Format24bppRgb
        };
        public static System.Drawing.Imaging.PixelFormat[] Format32bpp = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Format32bppArgb,
            System.Drawing.Imaging.PixelFormat.Format32bppPArgb,
            System.Drawing.Imaging.PixelFormat.Format32bppRgb
        };
        public static System.Drawing.Imaging.PixelFormat[] Format48bpp = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Format48bppRgb
        };
        public static System.Drawing.Imaging.PixelFormat[] Format64bpp = new System.Drawing.Imaging.PixelFormat[]
        {
            System.Drawing.Imaging.PixelFormat.Format64bppArgb,
            System.Drawing.Imaging.PixelFormat.Format64bppPArgb
        };
        #endregion

        #region EXIF file
        public static System.Drawing.Imaging.ImageFormat[] ExifFormat = new System.Drawing.Imaging.ImageFormat[]
        {
            System.Drawing.Imaging.ImageFormat.Exif,
            System.Drawing.Imaging.ImageFormat.Jpeg,
            System.Drawing.Imaging.ImageFormat.Tiff,
            System.Drawing.Imaging.ImageFormat.MemoryBmp
        };
        #endregion

        #region I18N routines
        /// <summary>
        /// Fake function for gettext collection msgid
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string T(string t)
        {
            return ( t );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addin"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string _( IAddin addin, string t )
        {
            if ( addin is IAddin)
            {
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( addin.Location ) ), "locale" );
                I18N i10n = new I18N( addin.Domain, addinRoot );
                return ( I18N._( i10n.Catalog, t ) );
            }
            else
            {
                string path = Assembly.GetExecutingAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( path ) ), "locale" );
                I18N i10n = new I18N( domain, addinRoot );
                return ( I18N._( i10n.Catalog, t ) );
            }
            return ( t );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string _( AddinHost host, string t )
        {
            string path = Assembly.GetExecutingAssembly().Location;
            string domain = Path.GetFileNameWithoutExtension(path);
            string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( path ) ), "locale" );
            I18N i10n = new I18N( domain, addinRoot );
            return ( I18N._( i10n.Catalog, t ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public static void Translate( IAddin addin, Form form, ToolTip tooltip = null, object[] extra = null )
        {
            if ( addin is IAddin && form is Form )
            {
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( addin.Location ) ), "locale" );

                I18N i10n = new I18N( addin.Domain, addinRoot, form, tooltip, extra );
            }
            else if ( form is Form )
            {
                string path = Assembly.GetExecutingAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( path ) ), "locale" );
                I18N i10n = new I18N( domain, addinRoot, form, tooltip, extra );
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<IAddin> ShowAddinsDialog()
        {
            List<IAddin> result = new List<IAddin>();
            SelectAddinForm fm = new SelectAddinForm(AddinHost.GetHost());
            Translate( null, fm );
            if ( fm.ShowDialog() == DialogResult.OK )
            {
                result.AddRange( fm.GetSelectedAddins() );
            }
            return ( result );
        }

        #region Common Image routines
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="iconFile"></param>
        public static void BitmapToIcon( string imageFile, string iconFile )
        {
            // Create a Bitmap object from an image file.
            Bitmap bmp = new Bitmap( imageFile );

            Icon newIcon = BitmapToIcon( bmp );

            //Write Icon to File Stream
            FileStream fs = new FileStream( iconFile, FileMode.OpenOrCreate );
            newIcon.Save( fs );
            fs.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Icon BitmapToIcon( System.Drawing.Image image )
        {
            // Create a Bitmap object from an image file.
            Bitmap bmp = image as Bitmap;
            // Get an Hicon for myBitmap.
            IntPtr Hicon = bmp.GetHicon();
            // Create a new icon from the handle.
            Icon newIcon = Icon.FromHandle(Hicon);

            return ( newIcon );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoder( ImageFormat format )
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach ( ImageCodecInfo codec in codecs )
            {
                if ( codec.FormatID == format.Guid )
                {
                    return codec;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static System.Drawing.Image LoadImage( string filename )
        {
            System.Drawing.Image result = null;

            if ( File.Exists( filename ) )
            {
                using ( FileStream fs = new FileStream( filename, FileMode.Open, FileAccess.Read ) )
                {
                    result = AutoRotate( System.Drawing.Image.FromStream( fs ) );
                }
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="image"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static bool SaveImage( string filename, System.Drawing.Image image, SaveOption option )
        {
            var result = false;

            try
            {
                if ( !( image is System.Drawing.Image ) ) return ( result );

                Bitmap dst = CloneImage(image as Bitmap);
                if ( !option.KeepExif )
                {
                    EXIF.Remove( dst );
                }

                ImageFormat ff = ImageFormat.Jpeg;
                EncoderParameters codecParams = null;

                string fext = Path.GetExtension(filename).ToLower();
                if ( string.Equals( fext, ".jpg", StringComparison.CurrentCultureIgnoreCase ) ||
                     string.Equals( fext, ".jpeg", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    ff = ImageFormat.Jpeg;
                    codecParams = new EncoderParameters( 1 );
                    codecParams.Param[0] = new EncoderParameter( System.Drawing.Imaging.Encoder.Quality, (long)option.Quality );
                }
                else if ( string.Equals( fext, ".tif", StringComparison.CurrentCultureIgnoreCase ) ||
                     string.Equals( fext, ".tiff", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    ff = ImageFormat.Tiff;
                }
                else if ( string.Equals( fext, ".bmp", StringComparison.CurrentCultureIgnoreCase ) )
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
                using ( var fs = new FileStream( filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write ) )
                {
                    if ( codecParams is EncoderParameters )
                    {
                        var codecInfo = GetEncoder( ff );
                        dst.Save( fs, codecInfo, codecParams );
                    }
                    else
                    {
                        dst.Save( fs, ff );
                    }
                    result = true;
                }
            }
            catch ( Exception ex )
            {
                
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static System.Drawing.Image AutoRotate( System.Drawing.Image image )
        {
            System.Drawing.Image result = CloneImage(image);
            if ( image.PropertyIdList.Contains( EXIF.TagID.Orientation ) )
            {
                var pi = image.GetPropertyItem( EXIF.TagID.Orientation );
                RotateFlipType rft = RotateFlipType.RotateNoneFlipNone;
                switch ( pi.Value[0] )
                {
                    case 2:
                        rft = RotateFlipType.RotateNoneFlipX;
                        break;
                    case 3:
                        rft = RotateFlipType.Rotate180FlipNone;
                        break;
                    case 4:
                        rft = RotateFlipType.RotateNoneFlipY;
                        break;
                    case 5:
                        rft = RotateFlipType.Rotate270FlipX;
                        break;
                    case 6:
                        rft = RotateFlipType.Rotate90FlipNone;
                        break;
                    case 7:
                        rft = RotateFlipType.Rotate90FlipX;
                        break;
                    case 8:
                        rft = RotateFlipType.Rotate270FlipNone;
                        break;
                }
                result.RotateFlip( rft );
                pi.Value[0] = (byte) RotateFlipType.RotateNoneFlipNone;
                result.SetPropertyItem( pi );
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static System.Drawing.Image CloneExif( System.Drawing.Image src, System.Drawing.Image dst )
        {
            if ( src is System.Drawing.Image && dst is System.Drawing.Image )
            {
                if ( ExifFormat.Contains( src.RawFormat ) && ExifFormat.Contains( dst.RawFormat ) )
                {
                    foreach ( var item in src.PropertyItems )
                    {
                        dst.SetPropertyItem( item );
                    }
                }
            }
            return ( dst );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public static void RemoveExif( System.Drawing.Image image )
        {
            if ( image is System.Drawing.Image && ExifFormat.Contains( image.RawFormat ) )
            {
                foreach ( var item in image.PropertyItems )
                {
                    image.RemovePropertyItem( item.Id );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static System.Drawing.Image CloneImage( System.Drawing.Image image)
        {
            Bitmap dst = new Bitmap(image.Width, image.Height, image.PixelFormat);
            using ( var g = Graphics.FromImage( dst ) )
            {
                g.DrawImage( image, 0, 0, image.Width, image.Height );
            }
            CloneExif( image, dst );
            return (dst);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap CloneImage( Bitmap image )
        {
            Bitmap dst = new Bitmap( image );
            EXIF.Clone( image, dst );
            return ( dst );
            //return ( CloneImage( image as System.Drawing.Image ) as Bitmap );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="img"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Bitmap ProcessImage( IFilter filter, Bitmap img, bool alpha = true )
        {
            return ( ProcessImage( filter, img as System.Drawing.Image, alpha ) as Bitmap );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static System.Drawing.Image ProcessImage( IFilter filter, System.Drawing.Image img, bool alpha = true )
        {
            if ( filter is IFilter || filter is IAddin )
            {
                if ( AlphaFormat.Contains( img.PixelFormat ) )
                {
                    if ( filter is IFilter )
                    {
                        ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                        Bitmap bmpA = null;
                        if (alpha)
                            bmpA = filter.Apply(eca.Apply( CloneImage(img) as Bitmap ));
                        else
                            bmpA = eca.Apply( CloneImage(img) as Bitmap );
                        ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA);

                        Bitmap dst = Accord.Imaging.Image.Clone(CloneImage(img) as Bitmap, PixelFormat.Format24bppRgb );
                        dst = Accord.Imaging.Image.Clone( filter.Apply( dst ), PixelFormat.Format32bppArgb );
                        rca.ApplyInPlace( dst );
                        bmpA.Dispose();

                        CloneExif( img, dst );
                        return ( dst );
                    }
                    else if ( filter is IAddin )
                    {
                        System.Drawing.Image dst = ( filter as IAddin ).Apply( CloneImage(img) as Bitmap );
                        CloneExif( img, dst );
                        return ( dst );

                        //ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                        //System.Drawing.Image bmpA = (filter as IAddin).Apply(eca.Apply( CloneImage(img) as Bitmap ));
                        //ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA as Bitmap);

                        //Bitmap dst = Accord.Imaging.Image.Clone(CloneImage(img) as Bitmap, PixelFormat.Format24bppRgb );
                        //dst = Accord.Imaging.Image.Clone( ( filter as IAddin ).Apply( dst as System.Drawing.Image ) as Bitmap, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
                        //rca.ApplyInPlace( dst );
                        //bmpA.Dispose();
                        //return ( dst );
                    }
                }
                else if ( filter is IFilterInformation && ( filter as IFilterInformation ).FormatTranslations.ContainsKey( img.PixelFormat ) )
                {
                    Bitmap dst = ( filter as IFilter ).Apply( CloneImage(img) as Bitmap );
                    CloneExif( img, dst );
                    return ( dst );
                }
                return ( img );
            }
            return ( img );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="img"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Bitmap ProcessImage<T>( T filter, Bitmap img, bool alpha = true )
        {
            return ( ProcessImage( filter, img as System.Drawing.Image, alpha ) as Bitmap );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static System.Drawing.Image ProcessImage<T>( T filter, System.Drawing.Image img, bool alpha = true )
        {
            if ( filter is IFilter || filter is IAddin )
            {
                if ( filter is IFilterInformation && ( filter as IFilterInformation ).FormatTranslations.ContainsKey( img.PixelFormat ) )
                {
                    if ( filter is IFilter )
                    {
                        if( ( filter as IFilterInformation ).FormatTranslations.ContainsKey( PixelFormat.Format8bppIndexed ) ||
                            !AlphaFormat.Contains( img.PixelFormat ) )
                        {
                            Bitmap dst = ( filter as IFilter ).Apply( CloneImage(img) as Bitmap );
                            CloneExif( img, dst );
                            return ( dst );
                        }
                        else
                        {
                            ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                            Bitmap bmpA = eca.Apply( CloneImage(img) as Bitmap );
                            ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA);

                            Bitmap dst = Accord.Imaging.Image.Clone(CloneImage(img) as Bitmap, PixelFormat.Format24bppRgb );
                            dst = Accord.Imaging.Image.Clone( ( (IFilter) filter ).Apply( dst ), PixelFormat.Format32bppArgb );
                            rca.ApplyInPlace( dst );
                            bmpA.Dispose();

                            CloneExif( img, dst );
                            return ( dst );
                        }
                    }
                    else if ( filter is IAddin )
                    {
                        System.Drawing.Image dst = ( filter as IAddin ).Apply( CloneImage(img) as Bitmap );
                        CloneExif( img, dst );
                        return ( dst );
                    }
                }
                else if ( AlphaFormat.Contains( img.PixelFormat ) )
                {
                    if ( filter is IFilter )
                    {
                        ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                        Bitmap bmpA = null;
                        if ( alpha )
                            bmpA = ((IFilter)filter).Apply(eca.Apply( CloneImage(img) as Bitmap ));
                        else
                            bmpA = eca.Apply( CloneImage( img ) as Bitmap );
                        ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA);

                        Bitmap dst = Accord.Imaging.Image.Clone(CloneImage(img) as Bitmap, PixelFormat.Format24bppRgb );
                        dst = Accord.Imaging.Image.Clone( ( (IFilter) filter ).Apply( dst ), PixelFormat.Format32bppArgb );
                        rca.ApplyInPlace( dst );
                        bmpA.Dispose();

                        CloneExif( img, dst );
                        return ( dst );
                    }
                    else if ( filter is IAddin )
                    {
                        System.Drawing.Image dst = ( filter as IAddin ).Apply( CloneImage(img) as Bitmap );
                        CloneExif( img, dst );
                        return ( dst );
                    }
                }
                return ( img );
            }
            return ( img );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static System.Drawing.Image CreateThumb( System.Drawing.Image source, Size size )
        {
            if ( source is System.Drawing.Image )
            {
                double thumbSize = Math.Min(size.Width, size.Height);
                double factor = Math.Max(source.Width, source.Height) / thumbSize;
                int w = (int)Math.Round( source.Width / factor );
                int h = (int)Math.Round( source.Height / factor );

                ResizeBicubic filter = new ResizeBicubic(w, h);
                return ( ProcessImage( filter, source ) );
            }
            else
                return ( null );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static RectangleF GetOpaqueBound( System.Drawing.Image img, OpaqueMode mode )
        {
            RectangleF result = new RectangleF(0, 0, img.Width, img.Height);

            Accord.Imaging.UnmanagedImage src = Accord.Imaging.UnmanagedImage.FromManagedImage(img as Bitmap);
            Color cRef = src.GetPixel(0, 0);
            switch ( mode )
            {
                case OpaqueMode.Alpha:
                    cRef = Color.Transparent;
                    break;
                case OpaqueMode.TopLeft:
                    cRef = src.GetPixel( 0, 0 );
                    break;
                case OpaqueMode.BottomRight:
                    cRef = src.GetPixel( src.Width - 1, src.Height - 1 );
                    break;
            }

            bool content = false;
            int xMax = 0;
            int xMin = src.Width-1;
            int yMax = 0;
            int yMin = src.Height-1;
            Color c = src.GetPixel( 0, 0 );
            for ( var y = 1; y < src.Height; y++ )
            {
                content = false;
                for ( var x = 1; x < src.Width; x++ )
                {
                    c = src.GetPixel( x, y );
                    switch ( mode )
                    {
                        case OpaqueMode.Alpha:
                            if ( !content && c.A != cRef.A )
                            {
                                if ( x < xMin ) xMin = x - 1;
                                if ( y < yMin ) yMin = y - 1;
                                content = true;
                            }
                            else if ( content && c.A != cRef.A )
                            {
                                if ( x > xMax ) xMax = x + 1;
                                if ( y > yMax ) yMax = y + 1;
                            }
                            break;
                        case OpaqueMode.TopLeft:
                        case OpaqueMode.BottomRight:
                            if ( !content && ( c.R != cRef.R || c.G != cRef.G || c.B != cRef.B ) )
                            {
                                if ( x < xMin ) xMin = x - 1;
                                if ( y < yMin ) yMin = y - 1;
                                content = true;
                            }
                            else if ( content && ( c.R != cRef.R || c.G != cRef.G || c.B != cRef.B ) )
                            {
                                if ( x > xMax ) xMax = x + 1;
                                if ( y > yMax ) yMax = y + 1;
                            }
                            break;
                    }
                }
            }
            xMin = xMin < 0 ? 0 : xMin;
            xMax = xMax >= src.Width ? src.Width - 1 : xMax;
            yMin = yMin < 0 ? 0 : yMin;
            yMax = yMax >= src.Height ? src.Height - 1 : yMax;
            result.X = xMin;
            result.Y = yMin;
            result.Width = xMax - xMin;
            result.Height = yMax - yMin;
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int GetColorDeep( System.Drawing.Imaging.PixelFormat format )
        {
            if ( Format1bpp.Contains( format ) ) return ( 1 );
            else if ( Format4bpp.Contains( format ) ) return ( 4 );
            else if ( Format8bpp.Contains( format ) ) return ( 8 );
            else if ( Format16bpp.Contains( format ) ) return ( 16 );
            else if ( Format24bpp.Contains( format ) ) return ( 24 );
            else if ( Format32bpp.Contains( format ) ) return ( 32 );
            else if ( Format48bpp.Contains( format ) ) return ( 48 );
            else if ( Format64bpp.Contains( format ) ) return ( 64 );
            else return ( 0 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns></returns>
        public static RectangleF RemapRegion( RectangleF region, System.Drawing.Image src, System.Drawing.Image dst )
        {
            RectangleF result = region;

            int w = src.Width;
            int h = src.Height;
            if ( w > 0 )
            {
                float factor = dst.Width / (float)w;
                //result = RectangleF.Inflate( region, factor * region.Width, factor * region.Height );
                result = new RectangleF(
                    factor * region.X,
                    factor * region.Y,
                    factor * region.Width,
                    factor * region.Height );
            }
            return ( result );
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="image"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static RectangleF AdjustRegion( RectangleF region, System.Drawing.Image image, SideType side )
        {
            var result = new RectangleF(region.X, region.Y, region.Width, region.Height);
            if ( !side.HasFlag( SideType.Top ) )
            {
                result.Y = 0;
                result.Height += region.Y;
            }
            if ( !side.HasFlag( SideType.Bottom ) )
            {
                result.Height = image.Height - result.Y;
            }
            if ( !side.HasFlag( SideType.Left ) )
            {
                result.X = 0;
                result.Width += region.X;
            }
            if ( !side.HasFlag( SideType.Right ) )
            {
                result.Width = image.Width - result.X;
            }
            return ( result );
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="pos"></param>
        /// <param name="ca"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public static bool GetPosOfRegion( RectangleF region, PointF pos, out CornerRegionType ca, out Cursor cursor )
        {
            bool result = false;
            ca = CornerRegionType.None;
            cursor = Cursors.Default;

            float ratio = 8f;
            PointF pN = pos;

            if ( region.Width > 0 && region.Height > 0 )
            {
                CornerRegion cr = new CornerRegion(region, ratio);
                ca = cr.GetRegion( pN );
                switch ( ca )
                {
                    case CornerRegionType.TopLeft:
                        cursor = Cursors.SizeNWSE;
                        result = true;
                        break;
                    case CornerRegionType.TopCenter:
                        cursor = Cursors.SizeNS;
                        result = true;
                        break;
                    case CornerRegionType.TopRight:
                        cursor = Cursors.SizeNESW;
                        result = true;
                        break;
                    case CornerRegionType.MiddleLeft:
                        cursor = Cursors.SizeWE;
                        result = true;
                        break;
                    case CornerRegionType.MiddleCenter:
                        cursor = Cursors.SizeAll;
                        result = true;
                        break;
                    case CornerRegionType.MiddleRight:
                        cursor = Cursors.SizeWE;
                        result = true;
                        break;
                    case CornerRegionType.BottomLeft:
                        cursor = Cursors.SizeNESW;
                        result = true;
                        break;
                    case CornerRegionType.BottomCenter:
                        cursor = Cursors.SizeNS;
                        result = true;
                        break;
                    case CornerRegionType.BottomRight:
                        cursor = Cursors.SizeNWSE;
                        result = true;
                        break;
                    default:
                        cursor = Cursors.Default;
                        result = false;
                        break;
                }
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="region"></param>
        /// <param name="aspect"></param>
        /// <param name="aspectFactor"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static RectangleF MakeAspectRegion( Size size, RectangleF region, float aspect, float aspectFactor, bool force = false )
        {
            RectangleF result = new RectangleF(region.X, region.Y, region.Width, region.Height);

            if ( force || region.Width == 0 || region.Height == 0 )
            {
                result.X = size.Width / 2.0f;
                result.Y = size.Height / 2.0f;
                result.Width = 0;
                result.Height = 0;
                float w = size.Width;
                float h = size.Height;
                if ( aspectFactor >= 1 && size.Width >= size.Height )
                {
                    w = h * aspectFactor;
                    if ( w > size.Width )
                    {
                        w = size.Width;
                        h = w / aspectFactor;
                    }
                }
                else if ( aspectFactor >= 1 && size.Width < size.Height )
                {
                    h = w / aspectFactor;
                    if ( h > size.Height )
                    {
                        h = size.Height;
                        w = h * aspectFactor;
                    }
                }
                else if ( aspectFactor < 1 && size.Width >= size.Height )
                {
                    w = h * aspectFactor;
                    if ( w > size.Width )
                    {
                        w = size.Width;
                        h = w / aspectFactor;
                    }
                }
                else
                {
                    h = w / aspectFactor;
                    if ( h > size.Height )
                    {
                        h = size.Height;
                        w = h * aspectFactor;
                    }
                }
                result.Inflate( w / 2.0f, h / 2.0f );
            }
            return ( result );
        }
        
        #endregion
    }
}
