using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Mono.Addins;
using NetCharm.Image.Addins;


namespace InternalFilters.Effects
{
    public enum GrayscaleMode
    {
        None = 0,
        BT709,
        RMY,
        Y,
        Grayscale_1,
        Grayscale_2,
        Sepia_1,
        Sepia_2,
        Sepia_3,
        TawawaBlue,
        TawawaDarkBlue,
        TawawaOrange,
        TawawaDarkOrange,
        Sepia,
        BlackWhite,
        ComicHigh,
        ComicLow,
        HiSat,
        LoSat,
        Invert,
        LomoGraph,
        Polaroid,
        Custom
    }

    [Extension]
    class Grayscale : BaseAddinEffect
    {
        GrayscaleForm fm = null;

        #region Properties override
        /// <summary>
        /// 
        /// </summary>
        public override AddinType Type
        {
            get { return ( AddinType.Effect ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _name = "Grayscale";
        public override string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayname = T("Grayscale");
        public override string DisplayName
        {
            get { return _( _displayname ); }
            set { _displayname = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override string GroupName
        {
            get { return ( "Color" ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayGroupName = T("Color");
        public override string DisplayGroupName
        {
            get { return _( _displayGroupName ); }
            set { _displayGroupName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = T("Convert Image to Grayscale");
        public override string Description
        {
            get { return _( _description ); }
            set { _description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Image LargeIcon
        {
            get { return Properties.Resources.Grayscale_32x; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Image SmallIcon
        {
            get { return Properties.Resources.Grayscale_16x; }
        }

        #endregion

        #region Method override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected override void GetParams( Form form )
        {
            if ( Params.ContainsKey( "GrayscaleMode" ) )
                Params["GrayscaleMode"] = ( form as GrayscaleForm ).ParamGrayscaleMode;
            else
                Params.Add( "GrayscaleMode", ( form as GrayscaleForm ).ParamGrayscaleMode );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="img"></param>
        protected override void SetParams( Form form, Image img = null )
        {
            if ( Params.ContainsKey( "GrayscaleMode" ) )
                ( form as GrayscaleForm ).ParamGrayscaleMode = Params["GrayscaleMode"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public override void Show( Form parent = null, bool setup = false )
        {
            _success = false;
            if ( fm == null )
            {
                fm = new GrayscaleForm( this );
                fm.host = Host;
                fm.Text = DisplayName;
                fm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                fm.MaximizeBox = false;
                fm.MinimizeBox = false;
                fm.ShowIcon = false;
                fm.ShowInTaskbar = false;
                fm.StartPosition = FormStartPosition.CenterParent;

                Translate( fm );
                SetParams( fm, ImgSrc );
                Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.GetImageSelection, 0 ) );
            }
            if ( fm.ShowDialog() == DialogResult.OK )
            {
                Success = true;
                GetParams( fm );
                if ( !setup )
                {
                    ImgDst = Apply( ImgSrc );
                    Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.SetImageSelection, new RectangleF( 0, 0, 0, 0 ) ) );
                }
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
            GrayscaleMode grayscaleMode = GrayscaleMode.BT709;
            if ( Params.ContainsKey( "GrayscaleMode" ) )
                grayscaleMode = (GrayscaleMode) Params["GrayscaleMode"].Value;

            var dst = image.Clone();
            switch ( grayscaleMode )
            {
                case GrayscaleMode.BT709:
                case GrayscaleMode.RMY:
                case GrayscaleMode.Y:
                case GrayscaleMode.Sepia_1:
                case GrayscaleMode.Sepia_2:
                case GrayscaleMode.Sepia_3:
                case GrayscaleMode.Grayscale_1:
                case GrayscaleMode.Grayscale_2:
                case GrayscaleMode.BlackWhite:
                case GrayscaleMode.ComicHigh:
                case GrayscaleMode.ComicLow:
                case GrayscaleMode.HiSat:
                case GrayscaleMode.LoSat:
                case GrayscaleMode.LomoGraph:
                case GrayscaleMode.Invert:
                case GrayscaleMode.Polaroid:
                case GrayscaleMode.Custom:
                    dst = Gray( image, grayscaleMode );
                    break;
                case GrayscaleMode.Sepia:
                    dst = AddinUtils.ProcessImage( new Accord.Imaging.Filters.Sepia(), image, false );
                    break;
                case GrayscaleMode.TawawaBlue:
                    dst = Tawawa( image, false, false );
                    break;
                case GrayscaleMode.TawawaDarkBlue:
                    dst = Tawawa( image, true, false );
                    break;
                case GrayscaleMode.TawawaOrange:
                    dst = Tawawa( image, false, true );
                    break;
                case GrayscaleMode.TawawaDarkOrange:
                    dst = Tawawa( image, true, true );
                    break;
                default:
                    break;
            }
            return ( dst as Image );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool Command( AddinCommand cmd, out object result, params object[] args )
        {
            return base.Command( cmd, out result, args );
        }
        #endregion

        #region Gray / Tawawa routines
        /// <summary>
        /// 
        /// </summary>
        internal Dictionary<GrayscaleMode, ColorMatrix> GrayscaleMatrix = new Dictionary<GrayscaleMode, ColorMatrix>()
        {
            #region Fill ColorMatrix List
            { GrayscaleMode.BT709, new ColorMatrix()
                {
                    Matrix00 = 0.2125f,
                    Matrix01 = 0.2125f,
                    Matrix02 = 0.2125f,

                    Matrix10 = 0.7154f,
                    Matrix11 = 0.7154f,
                    Matrix12 = 0.7154f,

                    Matrix20 = 0.0721f,
                    Matrix21 = 0.0721f,
                    Matrix22 = 0.0721f
                }
            },
            { GrayscaleMode.RMY, new ColorMatrix()
                {
                    Matrix00 = 0.5000f,
                    Matrix01 = 0.5000f,
                    Matrix02 = 0.5000f,

                    Matrix10 = 0.4190f,
                    Matrix11 = 0.4190f,
                    Matrix12 = 0.4190f,

                    Matrix20 = 0.0810f,
                    Matrix21 = 0.0810f,
                    Matrix22 = 0.0810f
                }
            },
            { GrayscaleMode.Y, new ColorMatrix()
                {
                    Matrix00 = 0.2990f,
                    Matrix01 = 0.2990f,
                    Matrix02 = 0.2990f,

                    Matrix10 = 0.5870f,
                    Matrix11 = 0.5870f,
                    Matrix12 = 0.5870f,

                    Matrix20 = 0.1140f,
                    Matrix21 = 0.1140f,
                    Matrix22 = 0.1140f,
                }
            },
            { GrayscaleMode.Grayscale_1, new ColorMatrix()
                {
                    Matrix00 = 0.5000f,
                    Matrix01 = 0.5000f,
                    Matrix02 = 0.5000f,

                    Matrix10 = 0.5000f,
                    Matrix11 = 0.5000f,
                    Matrix12 = 0.5000f,

                    Matrix20 = 0.5000f,
                    Matrix21 = 0.5000f,
                    Matrix22 = 0.5000f,
                }
            },
            { GrayscaleMode.Grayscale_2, new ColorMatrix()
                {
                    Matrix00 = 0.330f,
                    Matrix01 = 0.330f,
                    Matrix02 = 0.330f,

                    Matrix10 = 0.590f,
                    Matrix11 = 0.590f,
                    Matrix12 = 0.590f,

                    Matrix20 = 0.110f,
                    Matrix21 = 0.110f,
                    Matrix22 = 0.110f,

                    Matrix33 = 1.000f,

                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.Sepia_1, new ColorMatrix()
                {
                    Matrix00 = 0.393f,
                    Matrix01 = 0.349f,
                    Matrix02 = 0.272f,

                    Matrix10 = 0.769f,
                    Matrix11 = 0.686f,
                    Matrix12 = 0.534f,

                    Matrix20 = 0.189f,
                    Matrix21 = 0.168f,
                    Matrix22 = 0.131f,
                }
            },
            { GrayscaleMode.Sepia_2, new ColorMatrix()
                {
                    Matrix00 = 0.393f,
                    Matrix01 = 0.349f,
                    Matrix02 = 0.299f,

                    Matrix10 = 0.769f,
                    Matrix11 = 0.686f,
                    Matrix12 = 0.534f,

                    Matrix20 = 0.189f,
                    Matrix21 = 0.168f,
                    Matrix22 = 0.131f,
                }
            },
            { GrayscaleMode.Sepia_3, new ColorMatrix()
                {
                    Matrix00 = 0.340f,
                    Matrix01 = 0.330f,
                    Matrix02 = 0.330f,
                    Matrix04 = 30.00f,

                    Matrix10 = 0.330f,
                    Matrix11 = 0.340f,
                    Matrix12 = 0.330f,

                    Matrix20 = 0.330f,
                    Matrix21 = 0.330f,
                    Matrix22 = 0.334f,
                    Matrix24 = 20.00f,
                }
            },
            { GrayscaleMode.BlackWhite, new ColorMatrix()
                {
                    Matrix00 = 1.500f,
                    Matrix01 = 1.500f,
                    Matrix02 = 1.500f,

                    Matrix10 = 1.500f,
                    Matrix11 = 1.500f,
                    Matrix12 = 1.500f,

                    Matrix20 = 1.500f,
                    Matrix21 = 1.500f,
                    Matrix22 = 1.500f,

                    Matrix33 = 1.000f,

                    Matrix40 = -1.00f,
                    Matrix41 = -1.00f,
                    Matrix42 = -1.00f,
                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.ComicHigh, new ColorMatrix()
                {
                    Matrix00 = 2.000f,
                    Matrix01 = -0.50f,
                    Matrix02 = -0.50f,

                    Matrix10 = -0.50f,
                    Matrix11 = 2.000f,
                    Matrix12 = -0.50f,

                    Matrix20 = -0.50f,
                    Matrix21 = -0.50f,
                    Matrix22 = 2.000f,

                    Matrix33 = 1.000f,

                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.ComicLow, new ColorMatrix()
                {
                    Matrix00 = 1.000f,

                    Matrix11 = 1.000f,

                    Matrix22 = 1.000f,

                    Matrix33 = 1.000f,

                    Matrix40 = 0.075f,
                    Matrix41 = 0.075f,
                    Matrix42 = 0.075f,
                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.HiSat, new ColorMatrix()
                {
                    Matrix00 = 3.000f,
                    Matrix01 = -1.00f,
                    Matrix02 = -1.00f,

                    Matrix10 = -1.00f,
                    Matrix11 = 3.000f,
                    Matrix12 = -1.00f,

                    Matrix20 = -1.00f,
                    Matrix21 = -1.00f,
                    Matrix22 = 3.000f,

                    Matrix33 = 1.000f,

                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.LoSat, new ColorMatrix()
                {
                    Matrix00 = 1.000f,

                    Matrix11 = 1.000f,

                    Matrix22 = 1.000f,

                    Matrix33 = 1.000f,

                    Matrix40 = 0.100f,
                    Matrix41 = 0.100f,
                    Matrix42 = 0.100f,
                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.Invert, new ColorMatrix()
                {
                    Matrix00 = -1.000f,

                    Matrix11 = -1.000f,

                    Matrix22 = -1.000f,

                    Matrix33 = 1.000f,

                    Matrix40 = 1.000f,
                    Matrix41 = 1.000f,
                    Matrix42 = 1.000f,
                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.LomoGraph, new ColorMatrix()
                {
                    Matrix00 = 1.500f,

                    Matrix11 = 1.450f,

                    Matrix22 = 1.090f,

                    Matrix33 = 1.000f,

                    Matrix40 = -0.10f,
                    Matrix41 = 0.050f,
                    Matrix42 = -0.08f,
                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.Polaroid, new ColorMatrix()
                {
                    Matrix00 = 1.6380f,
                    Matrix01 = -0.062f,
                    Matrix02 = -0.262f,

                    Matrix10 = -0.122f,
                    Matrix11 = 1.3780f,
                    Matrix12 = -0.122f,

                    Matrix20 = 1.0160f,
                    Matrix21 = -0.016f,
                    Matrix22 = 1.3830f,

                    Matrix33 = 1.000f,

                    Matrix40 = 0.060f,
                    Matrix41 = -0.05f,
                    Matrix42 = -0.05f,
                    Matrix44 = 1.000f
                }
            },
            { GrayscaleMode.Custom, new ColorMatrix()
                {
                    Matrix00 = 0.330f,
                    Matrix01 = 0.330f,
                    Matrix02 = 0.330f,

                    Matrix10 = 0.590f,
                    Matrix11 = 0.590f,
                    Matrix12 = 0.590f,

                    Matrix20 = 0.110f,
                    Matrix21 = 0.110f,
                    Matrix22 = 0.110f,

                    Matrix33 = 1.000f,

                    Matrix44 = 1.000f
                }
            }
            #endregion
        };

        /// <summary>
        ///
        /// BT709: 0.2125, 0.7154, 0.0721 
        /// RMY  : 0.5000, 0.4190, 0.0810
        /// Y    : 0.2990, 0.5870, 0.1140
        /// Half : 0.5000, 0.5000, 0.5000
        /// Sepia:
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="mode"></param>
        /// <returns></returns>            
        internal Image Gray( Image image, GrayscaleMode mode = GrayscaleMode.BT709 )
        {
            #region Fill ColorMatrix List
            if ( GrayscaleMatrix.Count == 0 )
            {
                GrayscaleMatrix.Add( GrayscaleMode.BT709, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix00 = 0.2125f;
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix01 = 0.2125f;
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix02 = 0.2125f;
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix10 = 0.7154f;
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix11 = 0.7154f;
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix12 = 0.7154f;
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix20 = 0.0721f;
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix21 = 0.0721f;
                GrayscaleMatrix[GrayscaleMode.BT709].Matrix22 = 0.0721f;

                GrayscaleMatrix.Add( GrayscaleMode.RMY, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix00 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix01 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix02 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix10 = 0.4190f;
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix11 = 0.4190f;
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix12 = 0.4190f;
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix20 = 0.0810f;
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix21 = 0.0810f;
                GrayscaleMatrix[GrayscaleMode.RMY].Matrix22 = 0.0810f;

                GrayscaleMatrix.Add( GrayscaleMode.Y, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.Y].Matrix00 = 0.2990f;
                GrayscaleMatrix[GrayscaleMode.Y].Matrix01 = 0.2990f;
                GrayscaleMatrix[GrayscaleMode.Y].Matrix02 = 0.2990f;
                GrayscaleMatrix[GrayscaleMode.Y].Matrix10 = 0.5870f;
                GrayscaleMatrix[GrayscaleMode.Y].Matrix11 = 0.5870f;
                GrayscaleMatrix[GrayscaleMode.Y].Matrix12 = 0.5870f;
                GrayscaleMatrix[GrayscaleMode.Y].Matrix20 = 0.1140f;
                GrayscaleMatrix[GrayscaleMode.Y].Matrix21 = 0.1140f;
                GrayscaleMatrix[GrayscaleMode.Y].Matrix22 = 0.1140f;

                GrayscaleMatrix.Add( GrayscaleMode.Grayscale_1, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix00 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix01 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix02 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix10 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix11 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix12 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix20 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix21 = 0.5000f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_1].Matrix22 = 0.5000f;

                GrayscaleMatrix.Add( GrayscaleMode.Sepia_1, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix00 = 0.393f;
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix01 = 0.349f;
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix02 = 0.272f;
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix10 = 0.769f;
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix11 = 0.686f;
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix12 = 0.534f;
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix20 = 0.189f;
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix21 = 0.168f;
                GrayscaleMatrix[GrayscaleMode.Sepia_1].Matrix22 = 0.131f;

                GrayscaleMatrix.Add( GrayscaleMode.Sepia_2, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix00 = 0.393f;
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix01 = 0.349f;
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix02 = 0.299f;
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix10 = 0.769f;
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix11 = 0.686f;
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix12 = 0.534f;
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix20 = 0.189f;
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix21 = 0.168f;
                GrayscaleMatrix[GrayscaleMode.Sepia_2].Matrix22 = 0.131f;

                GrayscaleMatrix.Add( GrayscaleMode.Sepia_3, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix00 = 0.340f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix01 = 0.330f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix02 = 0.330f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix04 = 30.00f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix10 = 0.330f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix11 = 0.340f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix12 = 0.330f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix20 = 0.330f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix21 = 0.330f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix22 = 0.334f;
                GrayscaleMatrix[GrayscaleMode.Sepia_3].Matrix24 = 20.00f;

                GrayscaleMatrix.Add( GrayscaleMode.TawawaBlue, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix00 = 0.2125f;
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix01 = 0.2125f;
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix02 = 0.2125f;
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix10 = 0.7154f;
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix11 = 0.7154f;
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix12 = 0.7154f;
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix20 = 0.0721f;
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix21 = 0.0721f;
                GrayscaleMatrix[GrayscaleMode.TawawaBlue].Matrix22 = 0.0721f;

                GrayscaleMatrix.Add( GrayscaleMode.Grayscale_2, new ColorMatrix() );
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix00 = 0.300f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix01 = 0.300f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix02 = 0.300f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix10 = 0.590f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix11 = 0.590f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix12 = 0.590f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix20 = 0.110f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix21 = 0.110f;
                GrayscaleMatrix[GrayscaleMode.Grayscale_2].Matrix22 = 0.110f;
            }
            #endregion

            if ( !GrayscaleMatrix.ContainsKey( mode ) )
            {
                return ( image );
            }

            ImageAttributes a = new ImageAttributes();
            ColorMatrix c = GrayscaleMatrix[mode];
            a.SetColorMatrix( c, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );

            Bitmap src = AddinUtils.CloneImage(image) as Bitmap;
            if ( src.PixelFormat != PixelFormat.Format32bppArgb )
                src = Accord.Imaging.Image.Clone( src, PixelFormat.Format32bppArgb );
            Bitmap dst = new Bitmap( src.Width, src.Height, src.PixelFormat );

            using ( var g = Graphics.FromImage( dst ) )
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage( src,
                             new Rectangle( 0, 0, src.Width, src.Height ),
                             0, 0, src.Width, src.Height,
                             GraphicsUnit.Pixel,
                             a );
            }

            AddinUtils.CloneExif( src, dst );
            return ( dst );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="swap"></param>
        /// <returns></returns>
        internal Image Tawawa( Image image, bool dark = true, bool swap = false )
        {
            Bitmap src = AddinUtils.CloneImage(image) as Bitmap;
            if ( image.PixelFormat != PixelFormat.Format32bppArgb )
                src = Accord.Imaging.Image.Clone( image as Bitmap, PixelFormat.Format32bppArgb );

            Accord.Imaging.UnmanagedImage dst = Accord.Imaging.UnmanagedImage.FromManagedImage(src);
            if ( !dark && !swap )
            {
                #region Tawawa Blue
                for ( int h = 0; h < dst.Height; h++ )
                {
                    for ( int w = 0; w < dst.Width; w++ )
                    {
                        Color pcSrc = dst.GetPixel(w, h);

                        double y = 0;
                        y = pcSrc.R * 0.25 + pcSrc.G * 0.65 + pcSrc.B * 0.25;
                        y = y / 255 * 200 + 55;
                        if ( y > 255 ) y = 255;
                        int iy = (int)Math.Round(y);

                        int r = (int)Math.Round(iy > 85 ? ( ( y - 85 ) / 255 * 340 ) : 0);
                        int g = iy;
                        int b = iy > 135 ? 255 : g + 120;

                        Color pcDst = Color.FromArgb( pcSrc.A, r, g, b );
                        dst.SetPixel( w, h, pcDst );
                    }
                }
                #endregion
            }
            else if ( dark && !swap )
            {
                #region Tawawa Dark Blue
                for ( int h = 0; h < dst.Height; h++ )
                {
                    for ( int w = 0; w < dst.Width; w++ )
                    {
                        Color pcSrc = dst.GetPixel(w, h);

                        double y = 0;
                        y = pcSrc.R * 0.25 + pcSrc.G * 0.60 + pcSrc.B * 0.15;
                        y = y / 255 * 200 + 55;
                        if ( y > 255 ) y = 255;
                        int iy = (int)Math.Round(y);

                        int r = (int)Math.Round(iy > 85 ? ( ( y - 85 ) / 255 * 340 ) : 0);
                        int g = iy;
                        int b = iy > 135 ? 255 : g + 120;

                        Color pcDst = Color.FromArgb( pcSrc.A, r, g, b );
                        dst.SetPixel( w, h, pcDst );
                    }
                }
                #endregion
            }
            else if( !dark && swap)
            {
                #region Tawawa Orange
                for ( int h = 0; h < dst.Height; h++ )
                {
                    for ( int w = 0; w < dst.Width; w++ )
                    {
                        Color pcSrc = dst.GetPixel(w, h);

                        double y = 0;
                        y = pcSrc.R * 0.33 + pcSrc.G * 0.55 + pcSrc.B * 0.20;
                        y = y / 255 * 200 + 55;
                        if ( y > 255 ) y = 255;
                        int iy = (int)Math.Round(y);

                        int r = (int)Math.Round(iy > 85 ? ( ( y - 85 ) / 255 * 340 ) : 0);
                        int g = iy;
                        int b = iy > 135 ? 255 : g + 120;

                        Color pcDst = Color.FromArgb( pcSrc.A, b, g, r );
                        dst.SetPixel( w, h, pcDst );
                    }
                }
                #endregion
            }
            else
            {
                #region Tawawa Dark Orange
                for ( int h = 0; h < dst.Height; h++ )
                {
                    for ( int w = 0; w < dst.Width; w++ )
                    {
                        Color pcSrc = dst.GetPixel(w, h);
                        double y = 0;
                        y = pcSrc.R * 0.3 + pcSrc.G * 0.59 + pcSrc.B * 0.11;
                        y = y / 255 * 200 + 55;
                        if ( y > 255 ) y = 255;
                        int iy = (int)Math.Round(y);

                        int r = (int)Math.Round(iy > 85 ? ( ( y - 85 ) / 255 * 340 ) : 0);
                        int g = iy;
                        int b = iy > 135 ? 255 : g + 120;

                        Color pcDst = Color.FromArgb( pcSrc.A, b, g, r );
                        dst.SetPixel( w, h, pcDst );
                    }
                }
                #endregion
            }

            Bitmap dstBmp = dst.ToManagedImage();
            //var filter = new Accord.Imaging.Filters.BrightnessCorrection(10);
            //filter.ApplyInPlace( dstBmp );

            AddinUtils.CloneExif( image, dstBmp );
            src.Dispose();
            dst.Dispose();
            return ( dstBmp );
        }

        #endregion
    }
}
