using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ColorMatrixTest
{
    public enum GrayscaleMode
    {
        None = 0,
        BT709,
        BT601,
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
        Kodachrome,
        LomoGraph_1,
        LomoGraph_2,
        Polaroid_1,
        Polaroid_2,
        Achromatomaly,
        Achromatopsia,
        Deuteranomaly,
        Deuteranopia,
        Protanomaly,
        Protanopia,
        Tritanomaly,
        Tritanopia,
        TestMatrix,
        Custom
    }
    public partial class ColorMatrixForm : Form
    {
        internal string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

        string[] exts_image = new string[] { ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".bmp", ".gif" };
        string[] exts_cm = new string[] { ".cm" };

        private Image ImgSrc = null;
        private Image ImgDst = null;

        #region Gray / Tawawa routines
        /// <summary>
        /// 
        /// </summary>
        internal Dictionary<GrayscaleMode, ColorMatrix> GrayscaleMatrix = new Dictionary<GrayscaleMode, ColorMatrix>();

        /// <summary>
        /// 
        /// </summary>
        internal void InitColorMatrix()
        {
            GrayscaleMatrix.Clear();
            #region Init ColorMatrix for Grayscal Mode

            #region None
            GrayscaleMatrix.Add( GrayscaleMode.None, new ColorMatrix( new[]{
                new float[] { 1, 0, 0, 0, 0},        // red scaling factor
                new float[] { 0, 1, 0, 0, 0},        // green scaling factor
                new float[] { 0, 0, 1, 0, 0},        // blue scaling factor
                new float[] { 0, 0, 0, 1, 0},        // alpha scaling factor
                new float[] { 0, 0, 0, 0, 1}         // three translations
            } ) );
            #endregion
            #region BT709
            GrayscaleMatrix.Add( GrayscaleMode.BT709, new ColorMatrix( new[]{
                new float[] { 0.2125f, 0.2125f, 0.2125f, 0, 0},        // red scaling factor
                new float[] { 0.7154f, 0.7154f, 0.7154f, 0, 0},        // green scaling factor
                new float[] { 0.0721f, 0.0721f, 0.0721f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region BT601
            GrayscaleMatrix.Add( GrayscaleMode.BT601, new ColorMatrix( new[]{
                new float[] { 0.2990f, 0.2990f, 0.2990f, 0, 0},        // red scaling factor
                new float[] { 0.5870f, 0.5870f, 0.5870f, 0, 0},        // green scaling factor
                new float[] { 0.1140f, 0.1140f, 0.1140f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
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
            #region Kodachrome
            GrayscaleMatrix.Add( GrayscaleMode.Kodachrome, new ColorMatrix( new[]{
                new float[] { 0.6997023f, 0, 0, 0, 0},        // red scaling factor
                new float[] { 0, 0.4609577f, 0, 0, 0},        // green scaling factor
                new float[] { 0, 0, 0.397218f, 0, 0},        // blue scaling factor
                new float[] { 0, 0, 0, 1, 0},        // alpha scaling factor
                new float[] { 0.005f, -0.005f, 0.005f, 0, 1}         // three translations
            } ) );
            #endregion
            #region LomoGraph_1
            GrayscaleMatrix.Add( GrayscaleMode.LomoGraph_1, new ColorMatrix( new[]{
                new float[] { 1.500f,      0,      0, 0, 0},        // red scaling factor
                new float[] {      0, 1.450f,      0, 0, 0},        // green scaling factor
                new float[] {      0,      0, 1.090f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] { -0.10f, 0.050f, -0.08f, 0, 1}         // three translations
            } ) );
            #endregion
            #region LomoGraph_2
            GrayscaleMatrix.Add( GrayscaleMode.LomoGraph_2, new ColorMatrix( new[]{
                new float[] { 1.500f,      0,      0, 0, 0},        // red scaling factor
                new float[] {      0, 1.450f,      0, 0, 0},        // green scaling factor
                new float[] {      0,      0, 1.110f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] { -0.10f, 0.000f, -0.08f, 0, 1}         // three translations
            } ) );
            #endregion
            #region Polaroid_1
            GrayscaleMatrix.Add( GrayscaleMode.Polaroid_1, new ColorMatrix( new[]{
                new float[] {  1.638f, -0.062f, -0.262f, 0, 0},        // red scaling factor
                new float[] { -0.122f, 1.3780f, -0.122f, 0, 0},        // green scaling factor
                new float[] { 1.0160f, -0.016f, 1.3830f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] { 0.0600f, -0.050f, -0.050f, 0, 1}         // three translations
            } ) );
            #endregion
            #region Polaroid_2
            GrayscaleMatrix.Add( GrayscaleMode.Polaroid_2, new ColorMatrix( new[]{
                new float[] {  1.538f, -0.062f, -0.262f, 0, 0},        // red scaling factor
                new float[] { -0.022f, 1.5780f, -0.022f, 0, 0},        // green scaling factor
                new float[] { 0.2160f, -0.160f, 1.5831f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] { 0.0200f, -0.050f, -0.050f, 0, 1}         // three translations
            } ) );
            #endregion
            #region Achromatomaly,
            GrayscaleMatrix.Add( GrayscaleMode.Achromatomaly, new ColorMatrix( new[]{
                new float[] { 0.618f, 0.163f, 0.163f, 0, 0},        // red scaling factor
                new float[] { 0.320f, 0.775f, 0.320f, 0, 0},        // green scaling factor
                new float[] { 0.062f, 0.062f, 0.516f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Achromatopsia,
            GrayscaleMatrix.Add( GrayscaleMode.Achromatopsia, new ColorMatrix( new[]{
                new float[] { 0.299f, 0.299f, 0.299f, 0, 0},        // red scaling factor
                new float[] { 0.587f, 0.587f, 0.587f, 0, 0},        // green scaling factor
                new float[] { 0.114f, 0.114f, 0.114f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Deuteranomaly,
            GrayscaleMatrix.Add( GrayscaleMode.Deuteranomaly, new ColorMatrix( new[]{
                new float[] {  1.538f, -0.062f, -0.262f, 0, 0},        // red scaling factor
                new float[] { -0.022f, 1.5780f, -0.022f, 0, 0},        // green scaling factor
                new float[] { 0.2160f, -0.160f, 1.5831f, 0, 0},        // blue scaling factor
                new float[] {       0,       0,       0, 1, 0},        // alpha scaling factor
                new float[] {       0,       0,       0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Deuteranopia,
            GrayscaleMatrix.Add( GrayscaleMode.Deuteranopia, new ColorMatrix( new[]{
                new float[] { 0.800f, 0.258f,      0, 0, 0},        // red scaling factor
                new float[] { 0.200f, 0.742f, 0.142f, 0, 0},        // green scaling factor
                new float[] {      0,      0, 0.858f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Protanomaly,
            GrayscaleMatrix.Add( GrayscaleMode.Protanomaly, new ColorMatrix( new[]{
                new float[] { 0.817f, 0.333f,      0, 0, 0},        // red scaling factor
                new float[] { 0.183f, 0.667f, 0.125f, 0, 0},        // green scaling factor
                new float[] {      0,      0, 0.875f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Protanopia,
            GrayscaleMatrix.Add( GrayscaleMode.Protanopia, new ColorMatrix( new[]{
                new float[] { 0.567f, 0.558f,      0, 0, 0},        // red scaling factor
                new float[] { 0.433f, 0.442f, 0.242f, 0, 0},        // green scaling factor
                new float[] {      0,      0, 0.758f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Tritanomaly,
            GrayscaleMatrix.Add( GrayscaleMode.Tritanomaly, new ColorMatrix( new[]{
                new float[] { 0.967f,      0,      0, 0, 0},        // red scaling factor
                new float[] { 0.330f, 0.733f, 0.183f, 0, 0},        // green scaling factor
                new float[] {      0, 0.267f, 0.817f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
            } ) );
            #endregion
            #region Tritanopia,
            GrayscaleMatrix.Add( GrayscaleMode.Tritanopia, new ColorMatrix( new[]{
                new float[] { 0.950f,      0,      0, 0, 0},        // red scaling factor
                new float[] { 0.050f, 0.433f, 0.475f, 0, 0},        // green scaling factor
                new float[] {      0, 0.567f, 0.525f, 0, 0},        // blue scaling factor
                new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                new float[] {      0,      0,      0, 0, 1}         // three translations
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
                new float[] {   0.5500f,   0.2500f,   0.4500f,   0.0000f,   0.0000f },        // red scaling factor
                new float[] {   0.7500f,   0.6300f,   0.4000f,   0.0000f,   0.0000f },        // green scaling factor
                new float[] {   0.1500f,   0.2500f,   0.9000f,   0.0000f,   0.0000f },        // blue scaling factor
                new float[] {   0.0000f,   0.0000f,   0.0000f,   1.0000f,   0.0000f },        // alpha scaling factor
                new float[] {  -0.4330f,   0.0000f,   0.4700f,   0.0000f,   1.0000f }         // three translations
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
            if ( cm is ColorMatrix ) c = cm;
            a.SetColorMatrix( c, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );

            Bitmap dst = new Bitmap( image.Width, image.Height, PixelFormat.Format32bppArgb );

            using ( var g = Graphics.FromImage( dst ) )
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage( image,
                             new Rectangle( 0, 0, image.Width, image.Height ),
                             0, 0, image.Width, image.Height,
                             GraphicsUnit.Pixel,
                             a );
            }
            return ( dst );
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageSize"></param>
        /// <param name="patternSize"></param>
        /// <returns></returns>
        internal Image MakePatternImage( Size imageSize, int patternSize = 8 )
        {
            Bitmap pat = new Bitmap(patternSize*2, patternSize*2, PixelFormat.Format32bppArgb);
            using ( var g = Graphics.FromImage( pat ) )
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                SolidBrush bb = new SolidBrush(Color.Silver);
                SolidBrush wb = new SolidBrush(Color.White);

                g.FillRectangle( bb, 0, 0, patternSize, patternSize );
                g.FillRectangle( bb, patternSize, patternSize, patternSize, patternSize );

                g.FillRectangle( wb, 0, patternSize, patternSize, patternSize );
                g.FillRectangle( wb, patternSize, 0, patternSize, patternSize );
            }

            Bitmap bg = new Bitmap(imageSize.Width, imageSize.Height, PixelFormat.Format32bppArgb);
            using ( var g = Graphics.FromImage( bg ) )
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                TextureBrush pb = new TextureBrush(pat);
                g.FillRectangle( pb, 0, 0, imageSize.Width, imageSize.Height );
            }
            return ( bg );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private void LoadImage( string[] args )
        {
            string[] flist = args.Where(f => File.Exists(f) && exts_image.Contains(Path.GetExtension(f).ToLower())).ToArray();

            if(flist.Length > 0)
            {
                using ( FileStream fs = new FileStream( flist[0], FileMode.Open, FileAccess.Read ) )
                {
                    ImgSrc = Image.FromStream( fs );
                }
                if(ImgSrc.Width > imgPreview.ClientSize.Width || ImgSrc.Height > imgPreview.ClientSize.Height )
                {
                    imgPreview.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    imgPreview.SizeMode = PictureBoxSizeMode.CenterImage;
                }
                btnTest.PerformClick();
                //imgPreview.Image = ImgSrc;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveImage( string fileName )
        {
            if(imgPreview.Image is Image)
            {
                imgPreview.Image.Save( fileName );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private void LoadMatrix( string[] args)
        {
            string[] exts = new string[] { ".cm" };
            string[] flist = args.Where(f => File.Exists(f) && exts.Contains(Path.GetExtension(f).ToLower())).ToArray();

            if ( flist.Length > 0 )
            {
                var json = File.ReadAllText( $"{dlgOpen.FileName}" );

                JavaScriptSerializer serializer  = new JavaScriptSerializer();
                var cm = serializer.Deserialize(json , typeof(ColorMatrix));
                SetMatrix( (ColorMatrix) cm );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cm"></param>
        private void SaveMatrix(string file, ColorMatrix cm)
        {
            JavaScriptSerializer serializer  = new JavaScriptSerializer();
            var json = serializer.Serialize(cm);
            File.WriteAllText( Path.Combine( AppPath, $"{file}" ), json );
        }

        private void LoadFile(string[] args)
        {
            if ( exts_image.Contains( Path.GetExtension( args[0] ).ToLower() ) )
            {
                LoadImage( args );
            }
            else if ( exts_cm.Contains( Path.GetExtension( args[0] ).ToLower() ) )
            {
                cbGrayMode.Text = "TestMatrix";
                LoadMatrix( args );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ColorMatrix GetMatrix()
        {
            var cm = new ColorMatrix();
            cm.Matrix00 = (float) Convert.ToDouble( edMatrix00.Value );
            cm.Matrix01 = (float) Convert.ToDouble( edMatrix01.Value );
            cm.Matrix02 = (float) Convert.ToDouble( edMatrix02.Value );
            cm.Matrix03 = (float) Convert.ToDouble( edMatrix03.Value );
            cm.Matrix04 = (float) Convert.ToDouble( edMatrix04.Value );

            cm.Matrix10 = (float) Convert.ToDouble( edMatrix10.Value );
            cm.Matrix11 = (float) Convert.ToDouble( edMatrix11.Value );
            cm.Matrix12 = (float) Convert.ToDouble( edMatrix12.Value );
            cm.Matrix13 = (float) Convert.ToDouble( edMatrix13.Value );
            cm.Matrix14 = (float) Convert.ToDouble( edMatrix14.Value );

            cm.Matrix20 = (float) Convert.ToDouble( edMatrix20.Value );
            cm.Matrix21 = (float) Convert.ToDouble( edMatrix21.Value );
            cm.Matrix22 = (float) Convert.ToDouble( edMatrix22.Value );
            cm.Matrix23 = (float) Convert.ToDouble( edMatrix23.Value );
            cm.Matrix24 = (float) Convert.ToDouble( edMatrix24.Value );

            cm.Matrix30 = (float) Convert.ToDouble( edMatrix30.Value );
            cm.Matrix31 = (float) Convert.ToDouble( edMatrix31.Value );
            cm.Matrix32 = (float) Convert.ToDouble( edMatrix32.Value );
            cm.Matrix33 = (float) Convert.ToDouble( edMatrix33.Value );
            cm.Matrix34 = (float) Convert.ToDouble( edMatrix34.Value );

            cm.Matrix40 = (float) Convert.ToDouble( edMatrix40.Value );
            cm.Matrix41 = (float) Convert.ToDouble( edMatrix41.Value );
            cm.Matrix42 = (float) Convert.ToDouble( edMatrix42.Value );
            cm.Matrix43 = (float) Convert.ToDouble( edMatrix43.Value );
            cm.Matrix44 = (float) Convert.ToDouble( edMatrix44.Value );

            return ( cm );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cm"></param>
        private void SetMatrix( ColorMatrix cm )
        {
            if ( cm is ColorMatrix )
            {
                edMatrix00.Value = Convert.ToDecimal( cm.Matrix00 );
                edMatrix01.Value = Convert.ToDecimal( cm.Matrix01 );
                edMatrix02.Value = Convert.ToDecimal( cm.Matrix02 );
                edMatrix03.Value = Convert.ToDecimal( cm.Matrix03 );
                edMatrix04.Value = Convert.ToDecimal( cm.Matrix04 );

                edMatrix10.Value = Convert.ToDecimal( cm.Matrix10 );
                edMatrix11.Value = Convert.ToDecimal( cm.Matrix11 );
                edMatrix12.Value = Convert.ToDecimal( cm.Matrix12 );
                edMatrix13.Value = Convert.ToDecimal( cm.Matrix13 );
                edMatrix14.Value = Convert.ToDecimal( cm.Matrix14 );

                edMatrix20.Value = Convert.ToDecimal( cm.Matrix20 );
                edMatrix21.Value = Convert.ToDecimal( cm.Matrix21 );
                edMatrix22.Value = Convert.ToDecimal( cm.Matrix22 );
                edMatrix23.Value = Convert.ToDecimal( cm.Matrix23 );
                edMatrix24.Value = Convert.ToDecimal( cm.Matrix24 );

                edMatrix30.Value = Convert.ToDecimal( cm.Matrix30 );
                edMatrix31.Value = Convert.ToDecimal( cm.Matrix31 );
                edMatrix32.Value = Convert.ToDecimal( cm.Matrix32 );
                edMatrix33.Value = Convert.ToDecimal( cm.Matrix33 );
                edMatrix34.Value = Convert.ToDecimal( cm.Matrix34 );

                edMatrix40.Value = Convert.ToDecimal( cm.Matrix40 );
                edMatrix41.Value = Convert.ToDecimal( cm.Matrix41 );
                edMatrix42.Value = Convert.ToDecimal( cm.Matrix42 );
                edMatrix43.Value = Convert.ToDecimal( cm.Matrix43 );
                edMatrix44.Value = Convert.ToDecimal( cm.Matrix44 );
            }
        }

        public ColorMatrixForm()
        {
            InitializeComponent();
        }

        private void ColorMatrixForm_Load( object sender, EventArgs e )
        {
            #region extracting icon from application to this form window
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            #endregion

            toolTip.ToolTipTitle = this.Text;

            var cmtip_r = "r = R * M00 + G * M10 + B * M20 + A * M30 + M40 * 255";
            var cmtip_g = "g = R * M01 + G * M11 + B * M21 + A * M31 + M41 * 255";
            var cmtip_b = "b = R * M02 + G * M12 + B * M22 + A * M32 + M42 * 255";
            var cmtip_a = "a = R * M03 + G * M13 + B * M23 + A * M33 + M43 * 255";
            var cmtip = $"{cmtip_r}{Environment.NewLine}{cmtip_g}{Environment.NewLine}{cmtip_b}{Environment.NewLine}{cmtip_a}";
            toolTip.SetToolTip( colorMatrix, cmtip );

            imgPreview.BackgroundImage = MakePatternImage( imgPreview.Size );

            cbGrayMode.DataSource = Enum.GetValues( typeof( GrayscaleMode ) );
            InitColorMatrix();
        }

        #region DrapDrop Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorMatrixForm_DragEnter( object sender, DragEventArgs e )
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorMatrixForm_DragDrop( object sender, DragEventArgs e )
        {
            string[] flist = (string[])e.Data.GetData( DataFormats.FileDrop, true );

            LoadFile( flist );
            //LoadImage( flist );
        }

        #endregion DragDrop Events

        private void imgPreview_DoubleClick( object sender, EventArgs e )
        {
            btnOpen.PerformClick();
        }

        private void cbGrayMode_SelectedIndexChanged( object sender, EventArgs e )
        {
            var grayscaleMode = GrayscaleMode.None;
            Enum.TryParse( cbGrayMode.SelectedValue.ToString(), out grayscaleMode );
            if ( GrayscaleMatrix.ContainsKey( grayscaleMode ) )
            {
                var c = GrayscaleMatrix[grayscaleMode];
                SetMatrix( c );
            }
            btnTest.PerformClick();
        }

        private void btnCopyMatrix_Click( object sender, EventArgs e )
        {
            StringBuilder sb = new StringBuilder();

            var cm = GetMatrix();

            sb.AppendLine( $"var cm = new ColorMatrix( new[]{{" );
            sb.AppendLine( $"    new float[] {{ {cm.Matrix00,8:0.0000}f, {cm.Matrix01,8:0.0000}f, {cm.Matrix02,8:0.0000}f, {cm.Matrix03,8:0.0000}f, {cm.Matrix04,8:0.0000}f }},        // red scaling factor" );
            sb.AppendLine( $"    new float[] {{ {cm.Matrix10,8:0.0000}f, {cm.Matrix11,8:0.0000}f, {cm.Matrix12,8:0.0000}f, {cm.Matrix13,8:0.0000}f, {cm.Matrix14,8:0.0000}f }},        // green scaling factor" );
            sb.AppendLine( $"    new float[] {{ {cm.Matrix20,8:0.0000}f, {cm.Matrix21,8:0.0000}f, {cm.Matrix22,8:0.0000}f, {cm.Matrix23,8:0.0000}f, {cm.Matrix24,8:0.0000}f }},        // blue scaling factor" );
            sb.AppendLine( $"    new float[] {{ {cm.Matrix30,8:0.0000}f, {cm.Matrix31,8:0.0000}f, {cm.Matrix32,8:0.0000}f, {cm.Matrix33,8:0.0000}f, {cm.Matrix34,8:0.0000}f }},        // alpha scaling factor" );
            sb.AppendLine( $"    new float[] {{ {cm.Matrix40,8:0.0000}f, {cm.Matrix41,8:0.0000}f, {cm.Matrix42,8:0.0000}f, {cm.Matrix43,8:0.0000}f, {cm.Matrix44,8:0.0000}f }}         // three translations" );
            sb.AppendLine( $"}} );" );

            Clipboard.SetText( sb.ToString() );                
        }

        private void btnTest_Click( object sender, EventArgs e )
        {
            if ( !(ImgSrc is Image) ) return;

            var grayscaleMode = GrayscaleMode.None;
            Enum.TryParse( cbGrayMode.SelectedValue.ToString(), out grayscaleMode );
            var cm = GetMatrix();
            imgPreview.Image = Gray( ImgSrc, grayscaleMode, cm );

            #region Select Mode to Apply
            //switch ( grayscaleMode )
            //{
            //    case GrayscaleMode.BT709:
            //    case GrayscaleMode.BT601:
            //    case GrayscaleMode.RMY:
            //    case GrayscaleMode.Y:
            //    case GrayscaleMode.Sepia_1:
            //    case GrayscaleMode.Sepia_2:
            //    case GrayscaleMode.Sepia_3:
            //    case GrayscaleMode.Grayscale_1:
            //    case GrayscaleMode.Grayscale_2:
            //    case GrayscaleMode.Grayscale_3:
            //    case GrayscaleMode.BlackWhite:
            //    case GrayscaleMode.ComicHigh:
            //    case GrayscaleMode.ComicLow:
            //    case GrayscaleMode.HiSat:
            //    case GrayscaleMode.LoSat:
            //    case GrayscaleMode.LomoGraph_1:
            //    case GrayscaleMode.LomoGraph_2:
            //    case GrayscaleMode.Invert:
            //    case GrayscaleMode.Polaroid_1:
            //    case GrayscaleMode.Polaroid_2:
            //    case GrayscaleMode.Kodachrome:
            //    case GrayscaleMode.Custom:
            //        imgPreview.Image = Gray( ImgSrc, grayscaleMode );
            //        break;
            //    case GrayscaleMode.Achromatomaly:
            //    case GrayscaleMode.Achromatopsia:
            //    case GrayscaleMode.Deuteranomaly:
            //    case GrayscaleMode.Deuteranopia:
            //    case GrayscaleMode.Protanomaly:
            //    case GrayscaleMode.Protanopia:
            //    case GrayscaleMode.Tritanomaly:
            //    case GrayscaleMode.Tritanopia:
            //        imgPreview.Image = Gray( ImgSrc, grayscaleMode );
            //        break;
            //    case GrayscaleMode.TestMatrix:
            //        var cm = GetMatrix();
            //        imgPreview.Image = Gray( ImgSrc, grayscaleMode, cm );
            //        break;
            //    default:
            //        break;
            //}
            #endregion

        }

        private void btnOpen_Click( object sender, EventArgs e )
        {
            if ( dlgOpen.ShowDialog() == DialogResult.OK )
            {
                if ( exts_image.Contains( Path.GetExtension( dlgOpen.FileName ).ToLower() ) )
                {
                    LoadImage( dlgOpen.FileNames );
                }
                else if ( exts_cm.Contains( Path.GetExtension( dlgOpen.FileName ).ToLower() ) )
                {
                    cbGrayMode.Text = "TestMatrix";
                    LoadMatrix( dlgOpen.FileNames );
                }
            }
        }

        private void btnSave_Click( object sender, EventArgs e )
        {
            if ( dlgSave.ShowDialog() == DialogResult.OK )
            {
                if ( exts_image.Contains( Path.GetExtension( dlgSave.FileName ).ToLower() ) )
                {
                    SaveImage( dlgSave.FileName );
                }
                else if ( exts_cm.Contains( Path.GetExtension( dlgSave.FileName ).ToLower() ) )
                {
                    SaveMatrix( dlgSave.FileName, GetMatrix() );
                }
            }
        }

        private void btnOriginal_MouseDown( object sender, MouseEventArgs e )
        {
            ImgDst = imgPreview.Image;
            imgPreview.Image = ImgSrc;
        }

        private void btnOriginal_MouseUp( object sender, MouseEventArgs e )
        {
            imgPreview.Image = ImgDst is Image ? ImgDst : ImgSrc;
        }

        private void edMatrix_ValueChanged( object sender, EventArgs e )
        {
            if(chkLiveCalc.Checked)
            {
                btnTest.PerformClick();
            }
        }
    }
}
