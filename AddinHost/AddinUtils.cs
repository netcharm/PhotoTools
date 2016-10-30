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
    }
}
