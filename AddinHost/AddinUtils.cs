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
    public static class AddinUtils
    {
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
        public static RectangleF AdjustRegion( RectangleF region, System.Drawing.Image image, AnchorStyles side )
        {
            var result = new RectangleF(region.X, region.Y, region.Width, region.Height);
            if ( !side.HasFlag( AnchorStyles.Top ) )
            {
                result.Y = 0;
                result.Height += region.Y;
            }
            if ( !side.HasFlag( AnchorStyles.Bottom ) )
            {
                result.Height = image.Height - result.Y;
            }
            if ( !side.HasFlag( AnchorStyles.Left ) )
            {
                result.X = 0;
                result.Width += region.X;
            }
            if ( !side.HasFlag( AnchorStyles.Right ) )
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
        public static bool GetPosOfRegion( RectangleF region, PointF pos, out ContentAlignment ca, out Cursor cursor )
        {
            bool result = false;
            ca = 0;
            cursor = Cursors.Default;

            float corner = 8f;
            PointF pN = pos;

            if ( region.Width > 0 && region.Height > 0 )
            {
                if ( pN.X > region.X - corner &&
                     pN.X < region.X + corner &&
                     pN.Y > region.Y - corner &&
                     pN.Y < region.Y + corner )
                {
                    ca = ContentAlignment.TopLeft;
                    cursor = Cursors.PanNW;
                    result = true;
                }
                else if ( pN.X > region.X + corner &&
                          pN.X < region.X + region.Width - corner &&
                          pN.Y > region.Y - corner &&
                          pN.Y < region.Y + corner )
                {
                    ca = ContentAlignment.TopCenter;
                    cursor = Cursors.PanNorth;
                    result = true;
                }
                else if ( pN.X > region.X + region.Width - corner &&
                          pN.X < region.X + region.Width + corner &&
                          pN.Y > region.Y - corner &&
                          pN.Y < region.Y + corner )
                {
                    ca = ContentAlignment.TopRight;
                    cursor = Cursors.PanNE;
                    result = true;
                }
                else if ( pN.X > region.X - corner &&
                          pN.X < region.X + corner &&
                          pN.Y > region.Y + corner &&
                          pN.Y < region.Y + region.Height - corner )
                {
                    ca = ContentAlignment.MiddleLeft;
                    cursor = Cursors.PanWest;
                    result = true;
                }
                else if ( pN.X > region.X + region.Width - corner &&
                          pN.X < region.X + region.Width + corner &&
                          pN.Y > region.Y + corner &&
                          pN.Y < region.Y + region.Height - corner )
                {
                    ca = ContentAlignment.MiddleRight;
                    cursor = Cursors.PanEast;
                    result = true;
                }
                else if ( pN.X > region.X - corner &&
                          pN.X < region.X + corner &&
                          pN.Y > region.Y + region.Height - corner &&
                          pN.Y < region.Y + region.Height + corner )
                {
                    ca = ContentAlignment.BottomLeft;
                    cursor = Cursors.PanSW;
                    result = true;
                }
                else if ( pN.X > region.X + corner &&
                          pN.X < region.X + region.Width - corner &&
                          pN.Y > region.Y + region.Height - corner &&
                          pN.Y < region.Y + region.Height + corner )
                {
                    ca = ContentAlignment.BottomCenter;
                    cursor = Cursors.PanSouth;
                    result = true;
                }
                else if ( pN.X > region.X + region.Width - corner &&
                          pN.X < region.X + region.Width + corner &&
                          pN.Y > region.Y + region.Height - corner &&
                          pN.Y < region.Y + region.Height + corner )
                {
                    ca = ContentAlignment.BottomRight;
                    cursor = Cursors.PanSE;
                    result = true;
                }
                else if ( pN.X > region.X + corner &&
                          pN.X < region.X + region.Width - corner &&
                          pN.Y > region.Y + corner &&
                          pN.Y < region.Y + region.Height - corner )
                {
                    ca = ContentAlignment.MiddleCenter;
                    cursor = Cursors.Hand;
                    result = false;
                }
            }
            return ( result );
        }

    }
}
