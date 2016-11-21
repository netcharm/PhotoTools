using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NetCharm.Image;

namespace ExtensionMethods
{
    public static class NetCharmExtensionMethods
    {
        #region Color Extension Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHtml( this Color color )
        {
            string html = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
            return ( html );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static Color FromHtml( this string html )
        {
            Color c = default(Color);
            if ( html.Length == 6 || html.Length == 8 )
            {
                c = ColorTranslator.FromHtml( $"#{html}" );
            }
            else if ( ( html.Length == 7 || html.Length == 9 ) && html.StartsWith( "#" ) )
            {
                c = ColorTranslator.FromHtml( $"{html}" );
            }
            return ( c );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static Color ToColor( this string html)
        {
            return ( FromHtml( html ) );
        }
        #endregion

        #region Bitmap Extension Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Icon ToIcon( this Image image )
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
        /// <param name="image"></param>
        /// <returns></returns>
        public static Icon ToIcon( this Bitmap image )
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
        /// <param name="image"></param>
        /// <returns></returns>
        public static void AutoRotate( this Image image )
        {
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
                image.RotateFlip( rft );
                pi.Value[0] = (byte) RotateFlipType.RotateNoneFlipNone;
                image.SetPropertyItem( pi );
            }
        }

        #endregion
    }
}
