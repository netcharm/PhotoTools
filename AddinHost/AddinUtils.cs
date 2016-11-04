using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Accord.Imaging.Filters;
using NGettext.WinForm;

namespace NetCharm.Image.Addins
{

    public enum SideType
    {
        //
        // 摘要:
        //     该控件未锚定到其容器的任何边缘。
        None = 0,
        //
        // 摘要:
        //     该控件锚定到其容器的上边缘。
        Top = 1,
        //
        // 摘要:
        //     该控件锚定到其容器的下边缘。
        Bottom = 2,
        //
        // 摘要:
        //     该控件锚定到其容器的左边缘。
        Left = 4,
        //
        // 摘要:
        //     该控件锚定到其容器的右边缘。
        Right = 8
    }

    public enum CornerRegionType
    {
        None = 0,
        //
        // 摘要:
        //     内容在垂直方向上顶部对齐，在水平方向上左边对齐。
        TopLeft = 1,
        //
        // 摘要:
        //     内容在垂直方向上顶部对齐，在水平方向上居中对齐。
        TopCenter = 2,
        //
        // 摘要:
        //     内容在垂直方向上顶部对齐，在水平方向上右边对齐。
        TopRight = 4,
        //
        // 摘要:
        //     内容在垂直方向上中间对齐，在水平方向上左边对齐。
        MiddleLeft = 16,
        //
        // 摘要:
        //     内容在垂直方向上中间对齐，在水平方向上居中对齐。
        MiddleCenter = 32,
        //
        // 摘要:
        //     内容在垂直方向上中间对齐，在水平方向上右边对齐。
        MiddleRight = 64,
        //
        // 摘要:
        //     内容在垂直方向上底边对齐，在水平方向上左边对齐。
        BottomLeft = 256,
        //
        // 摘要:
        //     内容在垂直方向上底边对齐，在水平方向上居中对齐。
        BottomCenter = 512,
        //
        // 摘要:
        //     内容在垂直方向上底边对齐，在水平方向上右边对齐。
        BottomRight = 1024
    }

    public class CornerRegion
    {
        private RectangleF _tl;
        public RectangleF TopLeft
        {
            get { return ( _tl ); }
        }
        private RectangleF _tc;
        public RectangleF TopCenter
        {
            get { return ( _tc ); }
        }
        private RectangleF _tr;
        public RectangleF TopRight
        {
            get { return ( _tr ); }
        }
        private RectangleF _ml;
        public RectangleF MiddleLeft
        {
            get { return ( _ml ); }
        }
        private RectangleF _mc;
        public RectangleF MiddleCenter
        {
            get { return ( _mc ); }
        }
        private RectangleF _mr;
        public RectangleF MiddleRight
        {
            get { return ( _mr ); }
        }
        private RectangleF _bl;
        public RectangleF BottomLeft
        {
            get { return ( _bl ); }
        }
        private RectangleF _bc;
        public RectangleF BottomCenter
        {
            get { return ( _bc ); }
        }
        private RectangleF _br;
        public RectangleF BottomRight
        {
            get { return ( _br ); }
        }

        public CornerRegion()
        {
            //
        }

        public CornerRegion( Rectangle region, float ratio = 8 )
        {
            _tl = new RectangleF( region.Left - ratio, region.Top - ratio, ratio * 2, ratio * 2 );
            _tc = new RectangleF( region.Left + ratio, region.Top - ratio, region.Width - ratio * 2, ratio * 2 );
            _tr = new RectangleF( region.Right - ratio, region.Top - ratio, ratio * 2, ratio * 2 );

            _ml = new RectangleF( region.Left - ratio, region.Top + ratio, ratio * 2, region.Height - ratio * 2 );
            _mc = new RectangleF( region.Left + ratio, region.Top + ratio, region.Width - ratio * 2, region.Height - ratio * 2 );
            _mr = new RectangleF( region.Right - ratio, region.Top + ratio, ratio * 2, region.Height - ratio * 2 );

            _bl = new RectangleF( region.Left - ratio, region.Bottom - ratio, ratio * 2, ratio * 2 );
            _bc = new RectangleF( region.Left + ratio, region.Bottom - ratio, region.Width - ratio * 2, ratio * 2 );
            _br = new RectangleF( region.Right - ratio, region.Bottom - ratio, ratio * 2, ratio * 2 );
        }

        public CornerRegion(RectangleF region, float ratio = 8 )
        {
            _tl = new RectangleF( region.Left - ratio, region.Top - ratio, ratio * 2, ratio * 2 );
            _tc = new RectangleF( region.Left + ratio, region.Top - ratio, region.Width - ratio * 2, ratio * 2 );
            _tr = new RectangleF( region.Right - ratio, region.Top - ratio, ratio * 2, ratio * 2 );

            _ml = new RectangleF( region.Left - ratio, region.Top + ratio, ratio * 2, region.Height - ratio * 2 );
            _mc = new RectangleF( region.Left + ratio, region.Top + ratio, region.Width - ratio * 2, region.Height - ratio * 2 );
            _mr = new RectangleF( region.Right - ratio, region.Top + ratio, ratio * 2, region.Height - ratio * 2 );

            _bl = new RectangleF( region.Left - ratio, region.Bottom - ratio, ratio * 2, ratio * 2 );
            _bc = new RectangleF( region.Left + ratio, region.Bottom - ratio, region.Width - ratio * 2, ratio * 2 );
            _br = new RectangleF( region.Right - ratio, region.Bottom - ratio, ratio * 2, ratio * 2 );
        }

        public CornerRegionType GetRegion(PointF p)
        {
            if ( _tl.Contains( p ) )
                return ( CornerRegionType.TopLeft );
            else if ( _tc.Contains( p ) )
                return ( CornerRegionType.TopCenter );
            else if ( _tr.Contains( p ) )
                return ( CornerRegionType.TopRight );
            else if ( _ml.Contains( p ) )
                return ( CornerRegionType.MiddleLeft );
            else if ( _mc.Contains( p ) )
                return ( CornerRegionType.MiddleCenter );
            else if ( _mr.Contains( p ) )
                return ( CornerRegionType.MiddleRight );
            else if ( _bl.Contains( p ) )
                return ( CornerRegionType.BottomLeft );
            else if ( _bc.Contains( p ) )
                return ( CornerRegionType.BottomCenter );
            else if ( _br.Contains( p ) )
                return ( CornerRegionType.BottomRight );

            return ( CornerRegionType.None );
        }
    }

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

        /// <summary>
        /// Fake function for gettext collection msgid
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string T(string t)
        {
            return ( t );
        }

        public static string _( IAddin addin, string t )
        {
            if ( addin is IAddin)
            {
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( addin.Location ) ), "locale" );
                I18N i10n = new I18N( addin.Domain, addinRoot );
                return ( I18N._( i10n.Catalog, t ) );
            }
            return ( t );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public static void Translate( IAddin addin, Form form, ToolTip tooltip = null, object[] extra = null )
        {
            if(addin is IAddin && form is Form)
            {
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( addin.Location ) ), "locale" );

                I18N i10n = new I18N( addin.Domain, addinRoot, form, tooltip, extra );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static System.Drawing.Image ProcessImage( IFilter filter, System.Drawing.Image img )
        {
            if ( filter is IFilter || filter is IAddin )
            {
                System.Drawing.Imaging.PixelFormat[] AlphaFormat = new System.Drawing.Imaging.PixelFormat[]
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

                if ( AlphaFormat.Contains( img.PixelFormat ) )
                {
                    if ( filter is IFilter )
                    {
                        ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                        Bitmap bmpA = filter.Apply(eca.Apply( img as Bitmap ));
                        ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA);

                        Bitmap dst = Accord.Imaging.Image.Clone(img as Bitmap, System.Drawing.Imaging.PixelFormat.Format24bppRgb );
                        dst = Accord.Imaging.Image.Clone( filter.Apply( dst ), System.Drawing.Imaging.PixelFormat.Format32bppArgb );
                        rca.ApplyInPlace( dst );
                        return ( dst );
                    }
                    else if ( filter is IAddin )
                    {
                        ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                        System.Drawing.Image bmpA = (filter as IAddin).Apply(eca.Apply( img as Bitmap ));
                        ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA as Bitmap);

                        Bitmap dst = Accord.Imaging.Image.Clone(img as Bitmap, System.Drawing.Imaging.PixelFormat.Format24bppRgb );
                        dst = Accord.Imaging.Image.Clone( ( filter as IAddin ).Apply( dst as System.Drawing.Image ) as Bitmap, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
                        rca.ApplyInPlace( dst );
                        return ( dst );
                    }
                }
                return ( img );
            }
            else return ( img );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static System.Drawing.Image ProcessImage<T>( T filter, System.Drawing.Image img )
        {
            if ( filter is IFilter || filter is IAddin )
            {
                if ( filter is IFilterInformation && ( filter as IFilterInformation ).FormatTranslations.ContainsKey( img.PixelFormat ) )
                {
                    if ( filter is IFilter )
                    {
                        return ( ( filter as IFilter ).Apply( img as Bitmap ) );
                    }
                    else if ( filter is IAddin )
                    {
                        return ( ( filter as IAddin ).Apply( img ) );
                    }
                }
                else if ( AlphaFormat.Contains( img.PixelFormat ) )
                {
                    if ( filter is IFilter )
                    {
                        ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                        Bitmap bmpA = ((IFilter)filter).Apply(eca.Apply( img as Bitmap ));
                        ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA);

                        Bitmap dst = Accord.Imaging.Image.Clone(img as Bitmap, System.Drawing.Imaging.PixelFormat.Format24bppRgb );
                        dst = Accord.Imaging.Image.Clone( ( (IFilter) filter ).Apply( dst ), System.Drawing.Imaging.PixelFormat.Format32bppArgb );
                        rca.ApplyInPlace( dst );
                        return ( dst );
                    }
                    else if ( filter is IAddin )
                    {
                        ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                        System.Drawing.Image bmpA = (filter as IAddin).Apply(eca.Apply( img as Bitmap ));
                        ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA as Bitmap);

                        Bitmap dst = Accord.Imaging.Image.Clone(img as Bitmap, System.Drawing.Imaging.PixelFormat.Format24bppRgb );
                        dst = Accord.Imaging.Image.Clone( ( filter as IAddin ).Apply( dst as System.Drawing.Image ) as Bitmap, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
                        rca.ApplyInPlace( dst );
                        return ( dst );
                    }
                }
                return ( img );
            }
            else return ( img );
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static System.Drawing.Image CreateThumb( System.Drawing.Image source, Size size )
        {
            double thumbSize = Math.Min(size.Width, size.Height);

            //double aspect = (float)img.Width / (float)img.Height;
            double factor = Math.Max(source.Width, source.Height) / thumbSize;
            int w = (int)Math.Round( source.Width / factor );
            int h = (int)Math.Round( source.Height / factor );

            ResizeBicubic filter = new ResizeBicubic(w, h);

            return ( ProcessImage( filter, source ) );
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
                                if ( x < xMin )
                                    xMin = x - 1;
                                if ( y < yMin )
                                    yMin = y - 1;
                                content = true;
                            }
                            else if ( content && c.A != cRef.A )
                            {
                                if ( x > xMax )
                                    xMax = x + 1;
                                if ( y > yMax )
                                    yMax = y + 1;
                            }
                            break;
                        case OpaqueMode.TopLeft:
                        case OpaqueMode.BottomRight:
                            if ( !content && ( c.R != cRef.R || c.G != cRef.G || c.B != cRef.B ) )
                            {
                                if ( x < xMin )
                                    xMin = x - 1;
                                if ( y < yMin )
                                    yMin = y - 1;
                                content = true;
                            }
                            else if ( content && ( c.R != cRef.R || c.G != cRef.G || c.B != cRef.B ) )
                            {
                                if ( x > xMax )
                                    xMax = x + 1;
                                if ( y > yMax )
                                    yMax = y + 1;
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

    }
}
