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
                    Params["Mode"] = fm.GetMode( "Width" );
                else
                    Params.Add( "Mode", fm.GetMode( "Width" ) );

                if ( Params.ContainsKey( "Angle" ) )
                    Params["Angle"] = fm.GetAngle( "Angle" );
                else
                    Params.Add( "Angle", fm.GetAngle( "Angle" ) );

                if ( Params.ContainsKey( "KeepSize" ) )
                    Params["KeepSize"] = fm.GetKeepSize( "KeepSize" );
                else
                    Params.Add( "KeepSize", fm.GetKeepSize( "KeepSize" ) );

                ImgDst = Apply( ImgSrc );
            }
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
                RotateFlipType flip = (RotateFlipType)Params["Mode"].Value;
                double angle = (double)Params["Angle"].Value;
                bool keep = (bool)Params["KeepSize"].Value;

                Image dst = RotateForm.RotateImage( image, flip, angle, keep );
                return ( dst );

                //if ( angle < 0 ) angle = 360 + angle;

                //if ( angle == 0)
                //{
                //    RotateFlipType flip = (RotateFlipType)Params["Mode"].Value;
                //    dst.RotateFlip( flip );
                //    return ( dst );
                //}
                //else if ( angle % 90 == 0 )
                //{
                //    RotateFlipType flip = RotateFlipType.RotateNoneFlipNone;
                //    if ( angle % 360 == 000 ) flip = RotateFlipType.RotateNoneFlipNone;
                //    else if ( angle % 360 == 090 ) flip = RotateFlipType.Rotate90FlipNone;
                //    else if ( angle % 360 == 180 ) flip = RotateFlipType.Rotate180FlipNone;
                //    else if ( angle % 360 == 270 ) flip = RotateFlipType.Rotate270FlipNone;

                //    dst.RotateFlip( flip );
                //    return ( dst );
                //}
                //else
                //{
                //    bool keep = (bool)Params["KeepSize"].Value;
                //    RotateBicubic filter = new RotateBicubic(-angle, keep);
                //    return ( filter.Apply( dst as Bitmap ) );
                //}
            }
            return ( image );
        }
    }
}
