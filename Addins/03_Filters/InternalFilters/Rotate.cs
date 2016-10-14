using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Accord.Imaging.Filters;
using Mono.Addins;
using NetCharm.Image.Addins;

namespace InternalFilters
{
    [Extension]
    class Rotate : AddinBase
    {
        private RotateForm fm = null;
        //private Image ImgSrc = null;
        //private Image ImgDst = null;

        /// <summary>
        /// 
        /// </summary>
        private string _displayName = T("Rotate");
        public override string DisplayName
        {
            get { return ( _( _displayName ) ); }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = T("Rotate Image");
        public override string Description
        {
            get { return ( _( _description ) ); }
            set { _description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Image LargeIcon
        {
            get { return ( Properties.Resources.Rotate_32x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Image SmallIcon
        {
            get { return ( Properties.Resources.Rotate_16x ); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public override void Show( Form parent = null )
        {
            if ( fm == null )
            {
                fm = new RotateForm( this );
                Translate( fm );
                fm.Text = DisplayName;
            }
            if ( fm.ShowDialog() == DialogResult.OK )
            {
                if ( Params.ContainsKey( "Width" ) )
                    Params["Mode"] = fm.GetMode();
                else
                    Params.Add( "Mode", fm.GetMode() );

                if ( Params.ContainsKey( "Angle" ) )
                    Params["Angle"] = fm.GetAngle();
                else
                    Params.Add( "Angle", fm.GetAngle() );

                ImgDst = Apply( ImgSrc );
            }
            else ImgDst = ImgSrc;
            if ( fm != null )
            {
                fm.Dispose();
                fm = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public override Image Apply( Image image )
        {
            if ( image != null )
            {
                double angle = (double)Params["Angle"].Value;
                if ( angle % 90 == 0 )
                {
                    RotateFlipType flip = RotateFlipType.RotateNoneFlipNone;
                    if ( angle % 360 == 000 ) flip = RotateFlipType.RotateNoneFlipNone;
                    else if ( angle % 360 == 090 ) flip = RotateFlipType.Rotate90FlipNone;
                    else if ( angle % 360 == 180 ) flip = RotateFlipType.Rotate180FlipNone;
                    else if ( angle % 360 == 270 ) flip = RotateFlipType.Rotate270FlipNone;

                    Image dst = image.Clone() as Image;
                    dst.RotateFlip( flip );
                    return ( dst );
                }
                else
                {
                    RotateBicubic filter = new RotateBicubic(angle);
                    return ( filter.Apply( image as Bitmap ) );
                }
            }
            return ( image );
        }
    }
}
