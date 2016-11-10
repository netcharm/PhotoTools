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
        Grayscale_3,
        Sepia_1,
        Sepia_2,
        Sepia_3,
        BlackWhite,
        ComicHigh,
        ComicLow,
        HiSat,
        LoSat,
        Invert,
        LomoGraph,
        Polaroid,
        Sepia,
        TestMatrix,
        TawawaBlue,
        TawawaDarkBlue,
        TawawaOrange,
        TawawaDarkOrange,
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
            Params["GrayscaleMode"] = ( form as GrayscaleForm ).ParamGrayscaleMode;
            Params["ColorMatrix"] = ( form as GrayscaleForm ).ParamColorMatrix;
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
            if ( Params.ContainsKey( "ColorMatrix" ) )
                ( form as GrayscaleForm ).ParamColorMatrix = Params["ColorMatrix"];
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
            ColorMatrix cm = null;
            if ( Params.ContainsKey( "ColorMatrix" ) )
                cm = (ColorMatrix) Params["ColorMatrix"].Value;

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
                case GrayscaleMode.Grayscale_3:
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
                case GrayscaleMode.TestMatrix:
                    dst = Gray( image, grayscaleMode, cm );
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
        internal Dictionary<GrayscaleMode, ColorMatrix> GrayscaleMatrix = new Dictionary<GrayscaleMode, ColorMatrix>();

        internal void InitColorMatrix()
        {
            GrayscaleMatrix.Clear();
            #region Init ColorMatrix for Grayscal Mode

            #region BT709
            GrayscaleMatrix.Add( GrayscaleMode.BT709, new ColorMatrix( new[]{
                new float[] { 0.2125f, 0.2125f, 0.2125f, 0, 0},        // red scaling factor
                new float[] { 0.7154f, 0.7154f, 0.7154f, 0, 0},        // green scaling factor
                new float[] { 0.0721f, 0.0721f, 0.0721f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            }));
            #endregion
            #region RMY
            GrayscaleMatrix.Add( GrayscaleMode.RMY, new ColorMatrix( new[]{
                new float[] { 0.5000f, 0.5000f, 0.5000f, 0, 0},        // red scaling factor
                new float[] { 0.4190f, 0.4190f, 0.4190f, 0, 0},        // green scaling factor
                new float[] { 0.0810f, 0.0810f, 0.0810f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Y
            GrayscaleMatrix.Add( GrayscaleMode.Y, new ColorMatrix( new[]{
                new float[] { 0.2990f, 0.2990f, 0.2990f, 0, 0},        // red scaling factor
                new float[] { 0.5870f, 0.5870f, 0.5870f, 0, 0},        // green scaling factor
                new float[] { 0.1140f, 0.1140f, 0.1140f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Grayscale_1
            GrayscaleMatrix.Add( GrayscaleMode.Grayscale_1, new ColorMatrix( new[]{
                new float[] { 0.5000f, 0.5000f, 0.5000f, 0, 0},        // red scaling factor
                new float[] { 0.5000f, 0.5000f, 0.5000f, 0, 0},        // green scaling factor
                new float[] { 0.5000f, 0.5000f, 0.5000f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Grayscale_2
            GrayscaleMatrix.Add( GrayscaleMode.Grayscale_2, new ColorMatrix( new[]{
                new float[] { 0.3300f, 0.3300f, 0.3300f, 0, 0},        // red scaling factor
                new float[] { 0.5900f, 0.5900f, 0.5900f, 0, 0},        // green scaling factor
                new float[] { 0.1100f, 0.1100f, 0.1100f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Grayscale_3
            GrayscaleMatrix.Add( GrayscaleMode.Grayscale_3, new ColorMatrix( new[]{
                new float[] { 0.3300f, 0.3300f, 0.3300f, 0, 0},        // red scaling factor
                new float[] { 0.3300f, 0.3300f, 0.3300f, 0, 0},        // green scaling factor
                new float[] { 0.3300f, 0.3300f, 0.3300f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Sepia_1
            GrayscaleMatrix.Add( GrayscaleMode.Sepia_1, new ColorMatrix( new[]{
                new float[] { 0.3930f, 0.3490f, 0.2720f, 0, 0},        // red scaling factor
                new float[] { 0.7690f, 0.6860f, 0.5340f, 0, 0},        // green scaling factor
                new float[] { 0.1890f, 0.1680f, 0.1310f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Sepia_2
            GrayscaleMatrix.Add( GrayscaleMode.Sepia_2, new ColorMatrix( new[]{
                new float[] { 0.3930f, 0.3490f, 0.2990f, 0, 0},        // red scaling factor
                new float[] { 0.7690f, 0.6860f, 0.5340f, 0, 0},        // green scaling factor
                new float[] { 0.1890f, 0.1680f, 0.1310f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Sepia_3
            GrayscaleMatrix.Add( GrayscaleMode.Sepia_3, new ColorMatrix( new[]{
                new float[] { 0.340f, 0.330f, 0.330f, 0, 30.00f},        // red scaling factor
                new float[] { 0.330f, 0.340f, 0.330f, 0,      0},        // green scaling factor
                new float[] { 0.330f, 0.330f, 0.334f, 0, 20.00f},        // blue scaling factor
                new float[] {      0,      0,      0, 1,      0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0,      1}         // three translations
            } ) );
            #endregion
            #region BlackWhite
            GrayscaleMatrix.Add( GrayscaleMode.BlackWhite, new ColorMatrix( new[]{
                new float[] { 1.500f, 1.500f, 1.500f, 0, 0},        // red scaling factor
                new float[] { 1.500f, 1.500f, 1.500f, 0, 0},        // green scaling factor
                new float[] { 1.500f, 1.500f, 1.500f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] { -1.00f, -1.00f, -1.00f, 0, 1}         // three translations
            } ) );
            #endregion
            #region ComicHigh
            GrayscaleMatrix.Add( GrayscaleMode.ComicHigh, new ColorMatrix( new[]{
                new float[] { 2.000f, -0.50f, -0.50f, 0, 0},        // red scaling factor
                new float[] { -0.50f, 2.000f, -0.50f, 0, 0},        // green scaling factor
                new float[] { -0.50f, -0.50f, 2.000f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region ComicLow
            GrayscaleMatrix.Add( GrayscaleMode.ComicLow, new ColorMatrix( new[]{
                new float[] { 1.000f,      0,      0, 0, 0},        // red scaling factor
                new float[] {      0, 1.000f,      0, 0, 0},        // green scaling factor
                new float[] {      0,      0, 1.000f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] { 0.075f, 0.075f, 0.075f, 0, 1}         // three translations
            } ) );
            #endregion
            #region HiSat
            GrayscaleMatrix.Add( GrayscaleMode.HiSat, new ColorMatrix( new[]{
                new float[] { 3.000f, -1.00f, -1.00f, 0, 0},        // red scaling factor
                new float[] { -1.00f, 3.000f, -1.00f, 0, 0},        // green scaling factor
                new float[] { -1.00f, -1.00f, 3.000f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region LoSat
            GrayscaleMatrix.Add( GrayscaleMode.LoSat, new ColorMatrix( new[]{
                new float[] { 1.000f,      0,      0, 0, 0},        // red scaling factor
                new float[] {      0, 1.000f,      0, 0, 0},        // green scaling factor
                new float[] {      0,      0, 1.000f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] { 0.100f, 0.100f, 0.100f, 0, 1}         // three translations
            } ) );
            #endregion
            #region Invert
            GrayscaleMatrix.Add( GrayscaleMode.Invert, new ColorMatrix( new[]{
                new float[] { -1.00f,      0,      0, 0, 0},        // red scaling factor
                new float[] {      0, -1.00f,      0, 0, 0},        // green scaling factor
                new float[] {      0,      0, -1.00f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] { 1.000f, 1.000f, 1.000f, 0, 1}         // three translations
            } ) );
            #endregion
            #region LomoGraph
            GrayscaleMatrix.Add( GrayscaleMode.LomoGraph, new ColorMatrix( new[]{
                new float[] { 1.500f,      0,      0, 0, 0},        // red scaling factor
                new float[] {      0, 1.450f,      0, 0, 0},        // green scaling factor
                new float[] {      0,      0, 1.090f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] { -0.10f, 0.050f, -0.08f, 0, 1}         // three translations
            } ) );
            #endregion
            #region Polaroid
            GrayscaleMatrix.Add( GrayscaleMode.Polaroid, new ColorMatrix( new[]{
                new float[] {  1.638f, -0.062f, -0.262f, 0, 0},        // red scaling factor
                new float[] { -0.122f, 1.3780f, -0.122f, 0, 0},        // green scaling factor
                new float[] { 1.0160f, -0.016f, 1.3830f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] { 0.0600f, -0.050f, -0.050f, 0, 1}         // three translations
            } ) );
            #endregion
            #region Custom
            GrayscaleMatrix.Add( GrayscaleMode.Custom, new ColorMatrix( new[]{
                new float[] { 0.330f, 0.330f, 0.330f, 0, 0},        // red scaling factor
                new float[] { 0.590f, 0.590f, 0.590f, 0, 0},        // green scaling factor
                new float[] { 0.110f, 0.110f, 0.110f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region TestMatrix
            GrayscaleMatrix.Add( GrayscaleMode.TestMatrix, new ColorMatrix( new[]{
                new float[] { 0.250f,      0,      0, 0, 0},        // red scaling factor
                new float[] { 0.250f, 0.650f,      0, 0, 0},        // green scaling factor
                new float[] { 0.250f,      0, 0.350f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] { 0.075f, 0.075f, 0.075f, 0, 1}         // three translations
            } ) );
            #endregion

            #endregion
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="image"></param>
        /// <param name="mode"></param>
        /// <returns></returns>            
        internal Image Gray( Image image, GrayscaleMode mode = GrayscaleMode.BT709, ColorMatrix cm = null )
        {
            #region Fill ColorMatrix List
            if ( GrayscaleMatrix.Count == 0 )
            {
                InitColorMatrix();
            }
            #endregion

            if ( !GrayscaleMatrix.ContainsKey( mode ) )
            {
                return ( image );
            }

            ImageAttributes a = new ImageAttributes();
            ColorMatrix c = GrayscaleMatrix[mode];
            if ( mode == GrayscaleMode.TestMatrix && cm is ColorMatrix ) c = cm;
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
