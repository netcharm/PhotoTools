using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Accord.Imaging.Filters;

namespace NetCharm.Image.Addins
{
    public static class AddinUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static System.Drawing.Image ProcessImage( IFilter filter, System.Drawing.Image img )
        {
            System.Drawing.Imaging.PixelFormat[] AlphaFormat = new System.Drawing.Imaging.PixelFormat[]
            {
                System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                System.Drawing.Imaging.PixelFormat.Format16bppArgb1555,
                System.Drawing.Imaging.PixelFormat.Format32bppPArgb,
                System.Drawing.Imaging.PixelFormat.Format64bppArgb,
                System.Drawing.Imaging.PixelFormat.Format64bppPArgb
            };

            if ( AlphaFormat.Contains( img.PixelFormat ) )
            {
                ExtractChannel eca = new ExtractChannel(Accord.Imaging.RGB.A);
                Bitmap bmpA = filter.Apply(eca.Apply( img as Bitmap ));
                ReplaceChannel rca = new ReplaceChannel(Accord.Imaging.RGB.A, bmpA);

                Bitmap dst = Accord.Imaging.Image.Clone(img as Bitmap, System.Drawing.Imaging.PixelFormat.Format24bppRgb );
                dst = Accord.Imaging.Image.Clone( filter.Apply( dst ), System.Drawing.Imaging.PixelFormat.Format32bppArgb );
                rca.ApplyInPlace( dst );

                return ( dst );
            }
            return ( img );
        }
    }
}
