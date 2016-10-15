using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Accord.Imaging.Filters;
using NetCharm.Image.Addins;

namespace InternalFilters
{
    public partial class RotateForm : Form
    {
        private AddinHost host;
        private IAddin addin;

        private double angle = 0f;
        private RotateFlipType flip = RotateFlipType.RotateNoneFlipNone;

        private Image thumb = null;

        /// <summary>
        /// 
        /// </summary>
        public RotateForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public RotateForm( AddinHost host )
        {
            this.host = host;
            InitializeComponent();
        }
        /// <summary>
        /// ////
        /// </summary>
        /// <param name="filter"></param>
        public RotateForm( IAddin filter )
        {
            this.addin = filter;
            InitializeComponent();
            toolTip.ToolTipTitle = addin.DisplayName;
            AddinUtils.Translate( addin, this, toolTip );

            thumb = CreateThumb( addin.ImageData );
            imgPreview.Image = thumb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal ParamItem GetMode( string name )
        {
            ParamItem pi = new ParamItem();
            pi.Name = name;
            pi.DisplayName = AddinUtils._( addin, name );
            pi.Type = typeof( RotateFlipType );
            pi.Value = flip;
            return ( pi );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal ParamItem GetAngle( string name )
        {
            ParamItem pi = new ParamItem();
            pi.Name = name;
            pi.DisplayName = AddinUtils._( addin, name );
            pi.Type = typeof( double );
            pi.Value = (double) numAngle.Value;
            return ( pi );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal ParamItem GetKeepSize(string name)
        {
            ParamItem pi = new ParamItem();
            pi.Name = name;
            pi.DisplayName = AddinUtils._( addin, name );
            pi.Type = typeof( bool );
            pi.Value = chkKeepSize.Checked;
            return ( pi );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private Image CreateThumb(Image img)
        {
            double thumbSize = 160f;
            //double aspect = (float)img.Width / (float)img.Height;
            double factor = Math.Max(img.Width, img.Height) / thumbSize;
            int w = (int)Math.Round( img.Width / factor );
            int h = (int)Math.Round( img.Height / factor );

            ResizeBicubic filter = new ResizeBicubic(w, h);

            return ( AddinUtils.ProcessImage( filter, img ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flipNew"></param>
        /// <returns></returns>
        private RotateFlipType FlipCalc( RotateFlipType flipNew )
        {
            RotateFlipType oldFlip = RotateFlipType.RotateNoneFlipNone;
            switch (flip)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    oldFlip = flipNew;
                    break;
                case RotateFlipType.Rotate90FlipNone:
                    switch(flipNew)
                    {
                        case RotateFlipType.Rotate90FlipNone:
                            oldFlip = RotateFlipType.Rotate180FlipNone;
                            break;
                        case RotateFlipType.Rotate270FlipNone:
                            oldFlip = RotateFlipType.RotateNoneFlipNone;
                            break;
                        case RotateFlipType.RotateNoneFlipX:
                            oldFlip = RotateFlipType.Rotate90FlipX;
                            break;
                        case RotateFlipType.RotateNoneFlipY:
                            oldFlip = RotateFlipType.Rotate90FlipY;
                            break;
                    }
                    break;
                case RotateFlipType.Rotate180FlipNone:
                    switch ( flipNew )
                    {
                        case RotateFlipType.Rotate90FlipNone:
                            oldFlip = RotateFlipType.Rotate270FlipNone;
                            break;
                        case RotateFlipType.Rotate270FlipNone:
                            oldFlip = RotateFlipType.Rotate90FlipNone;
                            break;
                        case RotateFlipType.RotateNoneFlipX:
                            oldFlip = RotateFlipType.Rotate180FlipX;
                            break;
                        case RotateFlipType.RotateNoneFlipY:
                            oldFlip = RotateFlipType.Rotate180FlipY;
                            break;
                    }
                    break;
                case RotateFlipType.Rotate270FlipNone:
                    switch ( flipNew )
                    {
                        case RotateFlipType.Rotate90FlipNone:
                            oldFlip = RotateFlipType.RotateNoneFlipNone;
                            break;
                        case RotateFlipType.Rotate270FlipNone:
                            oldFlip = RotateFlipType.Rotate180FlipNone;
                            break;
                        case RotateFlipType.RotateNoneFlipX:
                            oldFlip = RotateFlipType.Rotate270FlipX;
                            break;
                        case RotateFlipType.RotateNoneFlipY:
                            oldFlip = RotateFlipType.Rotate270FlipY;
                            break;
                    }
                    break;
                case RotateFlipType.RotateNoneFlipX:
                    switch ( flipNew )
                    {
                        case RotateFlipType.Rotate90FlipNone:
                            oldFlip = RotateFlipType.Rotate90FlipX;
                            break;
                        case RotateFlipType.Rotate270FlipNone:
                            oldFlip = RotateFlipType.Rotate270FlipX;
                            break;
                        case RotateFlipType.RotateNoneFlipX:
                            oldFlip = RotateFlipType.RotateNoneFlipNone;
                            break;
                        case RotateFlipType.RotateNoneFlipY:
                            oldFlip = RotateFlipType.RotateNoneFlipXY;
                            break;
                    }
                    break;
                case RotateFlipType.Rotate90FlipX:
                    switch ( flipNew )
                    {
                        case RotateFlipType.Rotate90FlipNone:
                            oldFlip = RotateFlipType.Rotate180FlipX;
                            break;
                        case RotateFlipType.Rotate270FlipNone:
                            oldFlip = RotateFlipType.RotateNoneFlipX;
                            break;
                        case RotateFlipType.RotateNoneFlipX:
                            oldFlip = RotateFlipType.Rotate90FlipNone;
                            break;
                        case RotateFlipType.RotateNoneFlipY:
                            oldFlip = RotateFlipType.Rotate90FlipXY;
                            break;
                    }
                    break;
                case RotateFlipType.Rotate180FlipX:
                    switch ( flipNew )
                    {
                        case RotateFlipType.Rotate90FlipNone:
                            oldFlip = RotateFlipType.Rotate270FlipX;
                            break;
                        case RotateFlipType.Rotate270FlipNone:
                            oldFlip = RotateFlipType.Rotate90FlipX;
                            break;
                        case RotateFlipType.RotateNoneFlipX:
                            oldFlip = RotateFlipType.Rotate180FlipNone;
                            break;
                        case RotateFlipType.RotateNoneFlipY:
                            oldFlip = RotateFlipType.Rotate180FlipXY;
                            break;
                    }
                    break;
                case RotateFlipType.Rotate270FlipX:
                    switch ( flipNew )
                    {
                        case RotateFlipType.Rotate90FlipNone:
                            oldFlip = RotateFlipType.RotateNoneFlipX;
                            break;
                        case RotateFlipType.Rotate270FlipNone:
                            oldFlip = RotateFlipType.Rotate180FlipX;
                            break;
                        case RotateFlipType.RotateNoneFlipX:
                            oldFlip = RotateFlipType.Rotate270FlipX;
                            break;
                        case RotateFlipType.RotateNoneFlipY:
                            oldFlip = RotateFlipType.Rotate270FlipY;
                            break;
                    }
                    break;
                default:
                    oldFlip = RotateFlipType.RotateNoneFlipNone;
                    break;
            }

            return ( oldFlip );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="flip"></param>
        /// <param name="angle"></param>
        /// <param name="keep"></param>
        /// <returns></returns>
        internal protected static Image RotateImage(Image img, RotateFlipType flip, double angle, bool keep)
        {
            if ( img != null )
            {
                Image dst = img.Clone() as Image;
                double angleNew = angle;
                if ( angleNew < 0 ) angleNew = 360 + angleNew;

                if ( angleNew == 0 )
                {
                    dst.RotateFlip( flip );
                    return ( dst );
                }
                else if ( angleNew % 90 == 0 )
                {
                    RotateFlipType flipT = RotateFlipType.RotateNoneFlipNone;
                    if ( angleNew % 360 == 000 ) flipT = RotateFlipType.RotateNoneFlipNone;
                    else if ( angleNew % 360 == 090 ) flipT = RotateFlipType.Rotate90FlipNone;
                    else if ( angleNew % 360 == 180 ) flipT = RotateFlipType.Rotate180FlipNone;
                    else if ( angleNew % 360 == 270 ) flipT = RotateFlipType.Rotate270FlipNone;

                    dst.RotateFlip( flipT );
                    return ( dst );
                }
                else
                {
                    if( keep )
                    {
                        double a = Math.Abs(angleNew);
                        if ( 0 < a && a < 45 )
                        {
                            //
                        }
                        else if( 45 < a && a < 135)
                        {
                            RotateFlipType flipT = RotateFlipType.Rotate90FlipNone;
                            dst.RotateFlip( flipT );
                            angleNew = a - 90;
                        }
                        else if ( 135 < a && a < 225 )
                        {
                            //
                        }
                        else if ( 225 < a && a < 315 )
                        {
                            RotateFlipType flipT = RotateFlipType.Rotate270FlipNone;
                            dst.RotateFlip( flipT );
                            angleNew = a - 270;
                        }
                        else if ( 315 < a && a < 360 )
                        {
                            //
                        }
                    }
                    RotateBicubic filter = new RotateBicubic(-angleNew, keep);
                    return ( AddinUtils.ProcessImage( filter, dst ) );
                }
            }
            return ( img );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRotate90l_Click( object sender, EventArgs e )
        {
            //angle = ( angle + 270 ) % 360;
            flip = FlipCalc( RotateFlipType.Rotate270FlipNone );
            imgPreview.Image = RotateImage( thumb, flip, angle, chkKeepSize.Checked );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRotate90r_Click( object sender, EventArgs e )
        {
            //angle = ( angle + 90 ) % 360;
            flip = FlipCalc( RotateFlipType.Rotate90FlipNone );
            imgPreview.Image = RotateImage( thumb, flip, angle, chkKeepSize.Checked );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRotateFlipX_Click( object sender, EventArgs e )
        {
            flip = FlipCalc( RotateFlipType.RotateNoneFlipX );
            imgPreview.Image = RotateImage( thumb, flip, angle, chkKeepSize.Checked );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRotateFlipY_Click( object sender, EventArgs e )
        {
            flip = FlipCalc( RotateFlipType.RotateNoneFlipY );
            imgPreview.Image = RotateImage( thumb, flip, angle, chkKeepSize.Checked );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numAngle_ValueChanged( object sender, EventArgs e )
        {
            angle = (double) numAngle.Value % 360;
            numAngle.Value = Convert.ToDecimal( angle );
            imgPreview.Image = RotateImage( thumb, flip, angle, chkKeepSize.Checked );
        }
    }
}
