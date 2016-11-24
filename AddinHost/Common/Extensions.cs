using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Markup;
using NetCharm.Image;
using NGettext.WinForm;

using Media = System.Windows.Media;

namespace ExtensionMethods
{
    public static class NetCharmExtensionMethods
    {
        #region CultrueInfo Pre-Defined
        private static string locale_en = "en-us";
        private static string locale_ui = CultureInfo.InstalledUICulture.Name.ToLower();

        private static XmlLanguage locale_enkey = XmlLanguage.GetLanguage( locale_en );
        private static XmlLanguage locale_uikey = XmlLanguage.GetLanguage( locale_ui );
        #endregion

        #region Form I18N Extension
        /// <summary>
        /// Fake function for gettext collection msgid
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string T( this string t )
        {
            return ( t );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addin"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string _( this string t )
        {
            string result = t;

            if ( !string.IsNullOrEmpty( t ) )
            {
                string path = Assembly.GetExecutingAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( path ) ), "locale" );
                I18N i10n = new I18N( domain, addinRoot );
                result = I18N._( i10n.Catalog, t );
            }
            else
                return ( t );

            if ( string.Equals( result, t, StringComparison.CurrentCulture ) )
            {
                string path = Assembly.GetEntryAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( path ) ), "locale" );
                I18N i10n = new I18N( domain, addinRoot );
                result = I18N._( i10n.Catalog, t );
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public static void Translate( this Form form, ToolTip tooltip = null, object[] extra = null )
        {
            if ( form is Form )
            {
                string path = Assembly.GetExecutingAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( path ) ), "locale" );
                I18N i10n = new I18N( domain, addinRoot, form, tooltip, extra );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string _( this Form form, string text )
        {
            return ( _( text ) );
        }
        #endregion

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
        public static Color ToColor( this string html )
        {
            return ( FromHtml( html ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Media.Color ToMediaColor( this Color color )
        {
            return ( Media.Color.FromArgb( color.A, color.R, color.G, color.B ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToDrawingColor( this Media.Color color )
        {
            return ( Color.FromArgb( color.A, color.R, color.G, color.B ) );
        }

        #endregion

        #region Bitmap Extension Methods

        #region PixelFormat catalogs
        /// <summary>
        /// 
        /// </summary>
        public static PixelFormat[] AlphaFormat = new PixelFormat[]
        {
            PixelFormat.Canonical,
            PixelFormat.Alpha,
            PixelFormat.PAlpha,
            PixelFormat.Format16bppArgb1555,
            PixelFormat.Format32bppArgb,
            PixelFormat.Format32bppPArgb,
            PixelFormat.Format64bppArgb,
            PixelFormat.Format64bppPArgb
        };
        public static PixelFormat[] Format1bpp = new PixelFormat[]
        {
            PixelFormat.Format1bppIndexed
        };
        public static PixelFormat[] Format4bpp = new PixelFormat[]
        {
            PixelFormat.Format4bppIndexed
        };
        public static PixelFormat[] Format8bpp = new PixelFormat[]
        {
            PixelFormat.Format8bppIndexed
        };
        public static PixelFormat[] Format15bpp = new PixelFormat[]
        {
        };
        public static PixelFormat[] Format16bpp = new PixelFormat[]
        {
            PixelFormat.Format16bppArgb1555,
            PixelFormat.Format16bppGrayScale,
            PixelFormat.Format16bppRgb555,
            PixelFormat.Format16bppRgb565
        };
        public static PixelFormat[] Format24bpp = new PixelFormat[]
        {
            PixelFormat.Format24bppRgb
        };
        public static PixelFormat[] Format32bpp = new PixelFormat[]
        {
            PixelFormat.Format32bppArgb,
            PixelFormat.Format32bppPArgb,
            PixelFormat.Format32bppRgb
        };
        public static PixelFormat[] Format48bpp = new PixelFormat[]
        {
            PixelFormat.Format48bppRgb
        };
        public static PixelFormat[] Format64bpp = new PixelFormat[]
        {
            PixelFormat.Format64bppArgb,
            PixelFormat.Format64bppPArgb
        };
        #endregion

        #region EXIF file
        public static ImageFormat[] ExifFormat = new ImageFormat[]
        {
            ImageFormat.Exif,
            ImageFormat.Jpeg,
            ImageFormat.Tiff,
            ImageFormat.MemoryBmp
        };
        #endregion

        #region Test Image Color Info
        public static bool IsAlpha( this Image image )
        {
            return ( AlphaFormat.Contains( image.PixelFormat ) );
        }

        public static bool Is1bit( this Image image )
        {
            return ( Format1bpp.Contains( image.PixelFormat ) );
        }

        public static bool Is4bits( this Image image )
        {
            return ( Format4bpp.Contains( image.PixelFormat ) );
        }

        public static bool Is8bits( this Image image )
        {
            return ( Format8bpp.Contains( image.PixelFormat ) );
        }

        public static bool Is15bits( this Image image )
        {
            return ( Format15bpp.Contains( image.PixelFormat ) );
        }

        public static bool Is16bits( this Image image )
        {
            return ( Format16bpp.Contains( image.PixelFormat ) );
        }

        public static bool Is24bits( this Image image )
        {
            return ( Format24bpp.Contains( image.PixelFormat ) );
        }

        public static bool Is32bits( this Image image )
        {
            return ( Format32bpp.Contains( image.PixelFormat ) );
        }

        public static bool Is48bits( this Image image )
        {
            return ( Format48bpp.Contains( image.PixelFormat ) );
        }

        public static bool Is64bits( this Image image )
        {
            return ( Format64bpp.Contains( image.PixelFormat ) );
        }
        #endregion

        #region Image EXIF info
        public static bool IsExif( this Image image )
        {
            return ( ExifFormat.Contains( image.RawFormat ) );
        }

        #endregion

        #region Image Convert Routines
        static private Dictionary<string, Media.Typeface> TypefaceList = new Dictionary<string, Media.Typeface>();
        
        public static FontStyle GetFontStyle(this Media.Typeface face, bool underline=false, bool strikeout=false)
        {
            FontStyle result = FontStyle.Regular;
            var regular = false;
            if(face is Media.Typeface )
            {
                //if ( face.Style == System.Windows.FontStyles.Italic )
                //    result |= FontStyle.Italic;
                //if ( face.Weight == System.Windows.FontWeights.Bold )
                //    result |= FontStyle.Bold;
                
                foreach(var s in face.FaceNames[locale_enkey].Split())
                {
                    if ( string.Equals( s, "Bold" ) )
                        result |= FontStyle.Bold;
                    else if ( string.Equals( s, "Italic" ) )
                        result |= FontStyle.Italic;
                    else if ( string.Equals( s, "Regular" ) )
                        regular = true;
                }
                if ( !regular ) result &= ~FontStyle.Regular;
            }
            if ( underline ) result |= FontStyle.Underline;
            if ( strikeout ) result |= FontStyle.Strikeout;

            return ( result );
        }

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
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="fgColor"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap( this string text, Font font, Color fgColor )
        {
            return ( ToBitmap( text, font, fgColor, Color.Transparent ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="fgColor"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap( this string text, Font font, Color fgColor, Color bgColor )
        {
            #region Text Style Setting
            StringFormat tFormat = new StringFormat(StringFormatFlags.DisplayFormatControl | StringFormatFlags.MeasureTrailingSpaces);
            #endregion

            #region Measure String Size
            SizeF sizeF = new Size();
            using ( var g = Graphics.FromImage( new Bitmap( 10, 10, PixelFormat.Format32bppPArgb ) ) )
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextContrast = 2;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                sizeF = g.MeasureString( text, font, new PointF( 0, 0 ), tFormat );
            }
            #endregion

            #region Make Text Picture
            Size size = sizeF.ToSize();
            Bitmap textImg = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            using ( var g = Graphics.FromImage( textImg ) )
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextContrast = 2;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                // Here only for test. The output image is pool outline.
                //g.DrawString( text, font, new SolidBrush( color ), 0, 0, tFormat );

                g.FillRectangle( new SolidBrush( bgColor ), new RectangleF( 0, 0, size.Width, size.Height ) );

                // calc right emsize for given fontsize in current canvas dpi
                float emSize = g.DpiY * font.Size / 72f;
                var tPath = new GraphicsPath();
                tPath.StartFigure();
                tPath.AddString( text, font.FontFamily, (int) font.Style, emSize,
                                 new PointF( 0, 0 ),
                                 tFormat );
                tPath.CloseFigure();
                //g.DrawPath( new Pen( color, 1f ), tPath );
                g.FillPath( new SolidBrush( fgColor ), tPath );
            }
            #endregion

            return ( textImg );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontface"></param>
        /// <param name="fontsize"></param>
        /// <param name="fgColor"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap( this string text, Media.Typeface fontface, float fontsize, Color fgColor )
        {
            return ( ToBitmap( text, fontface, fontsize, fgColor, Color.Transparent ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontface"></param>
        /// <param name="fontsize"></param>
        /// <param name="fgColor"></param>
        /// <param name="bgColor"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap( this string text, Media.Typeface fontface, float fontsize, Color fgColor, Color bgColor, bool underline = false, bool strikeout = false )
        {
            if ( !( fontface is Media.Typeface ) ) return ( null );

            #region Calc fontface screen size
            float dpiX = 96f;
            float dpiY = 96f;
            //using ( Graphics g = Graphics.FromHwnd( IntPtr.Zero ) )
            //{
            //    dpiX = g.DpiX;
            //    dpiY = g.DpiY;
            //}

            float emSize = fontsize * dpiY / 72f;
            //float emSize = 12 * dpiY / 72f;
            #endregion

            #region Create FormattedText
            var culture = CultureInfo.InstalledUICulture;
            //if ( !fontface.FaceNames.ContainsKey( locale_uikey ) )
            if ( !fontface.FontFamily.FamilyNames.ContainsKey( locale_uikey ) )
                culture = new CultureInfo( "en-us" );
            var flow = culture.TextInfo.IsRightToLeft ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight;

            Media.Brush foreground = new  Media.SolidColorBrush(fgColor.ToMediaColor());

            Media.FormattedText formattedText = new Media.FormattedText( text,
                //CultureInfo.InvariantCulture,
                CultureInfo.CurrentUICulture,
                flow,
                fontface, emSize, foreground);
            //new Media.NumberSubstitution(Media.NumberCultureSource.Text,
            //    culture,
            //    Media.NumberSubstitutionMethod.AsCulture)
            //);
            formattedText.Trimming = System.Windows.TextTrimming.None;
            //formattedText.TextAlignment = System.Windows.TextAlignment.Right;
            formattedText.TextAlignment = System.Windows.TextAlignment.Left;
            #endregion

            #region Set FontStyle
            var fontStyle = System.Windows.FontStyles.Normal;
            var fontWeight = System.Windows.FontWeights.Normal;
            var textDecorations = new System.Windows.TextDecorationCollection();
            if ( underline )
                textDecorations.Add( System.Windows.TextDecorations.Underline );
            if ( strikeout )
                textDecorations.Add( System.Windows.TextDecorations.Strikethrough );

            fontStyle = fontface.Style;
            fontWeight = fontface.Weight;
            #endregion

            #region FormattedText Style
            formattedText.SetFontStyle( fontStyle );
            formattedText.SetFontWeight( fontWeight );
            formattedText.SetTextDecorations( textDecorations );
            #endregion

            #region Draw the FormattedText on a Drawing Visual
            //formattedText.MaxTextWidth = rectangle.Width / ( dpiX / 96.0 );
            //formattedText.MaxTextHeight = rectangle.Height / ( dpiY / 96.0 );
            formattedText.MaxTextWidth = 1280;
            formattedText.MaxTextHeight = 700;
            //formattedText.MaxTextHeight = ( emSize + 4 ) / ( dpiY / 96.0 );

            Media.DrawingVisual drawingVisual = new Media.DrawingVisual();
            using ( var drawingContext = drawingVisual.RenderOpen() )
            {
                drawingContext.DrawText( formattedText, new System.Windows.Point( 0, 0 ) );
                drawingContext.Close();
            }
            #endregion

            #region Render the DrawingVisual into a RenderTargetBitmap 
            var pixelWidth = Convert.ToInt32( Math.Ceiling(formattedText.Width * (dpiX / 96.0)));
            var pixelHeight = Convert.ToInt32( Math.Ceiling(formattedText.Height * (dpiY / 96.0)));
            if ( pixelWidth == 0 || pixelHeight == 0 )
                return ( new Bitmap( 1, 1 ) );

            var rtb = new Media.Imaging.RenderTargetBitmap(
              pixelWidth, pixelHeight,
              dpiX, dpiY,
              Media.PixelFormats.Pbgra32 );
            rtb.Render( drawingVisual );
            #endregion

            #region Create a System.Drawing.Bitmap 
            var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppPArgb );
            // Draw background color
            using ( var g = Graphics.FromImage( bitmap ) )
            {
                g.FillRectangle( new SolidBrush( bgColor ), new Rectangle( 0, 0, bitmap.Width, bitmap.Height ) );
            }
            #endregion

            #region Copy the RenderTargetBitmap pixels into the bitmap's pixel buffer
            var pdata = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly,
                bitmap.PixelFormat);
            rtb.CopyPixels( System.Windows.Int32Rect.Empty,
                pdata.Scan0, pdata.Stride * pdata.Height, pdata.Stride );
            bitmap.UnlockBits( pdata );
            #endregion
            return ( bitmap );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontfamily"></param>
        /// <param name="fontstyle"></param>
        /// <param name="fontsize"></param>
        /// <param name="fgColor"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap( this string text, string fontfamily, string fontstyle, float fontsize, Color fgColor )
        {
            return ( ToBitmap( text, fontfamily, fontstyle, fontsize, fgColor, Color.Transparent ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontfamily"></param>
        /// <param name="fontstyle"></param>
        /// <param name="fontsize"></param>
        /// <param name="fgColor"></param>
        /// <param name="bgColor"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap( this string text, string fontfamily, string fontstyle, float fontsize, Color fgColor, Color bgColor )
        {
            var styleFont = FontStyle.Regular;
            //var underline = false;
            //var strikeout = false;

            #region Set font style
            string[] styles = fontstyle.Split();
            foreach ( var style in styles )
            {
                if ( string.Equals( style.Trim(), "Italic", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    styleFont |= FontStyle.Italic;
                }
                else if ( string.Equals( style.Trim(), "Oblique", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    styleFont |= FontStyle.Italic;
                }
                else if ( string.Equals( style.Trim(), "Bold", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    styleFont |= FontStyle.Bold;
                }
                else if ( string.Equals( style.Trim(), "Underline", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    styleFont |= FontStyle.Underline;
                    //underline = true;
                }
                else if ( string.Equals( style.Trim(), "Strikeout", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    styleFont |= FontStyle.Strikeout;
                    //strikeout = true;
                }
            }
            #endregion

            var emSize = fontsize * 96/72f;

            #region Draw Text to Bitmap
            Font font = new Font(fontfamily, emSize, styleFont);
            if ( string.Equals( font.Name, fontfamily, StringComparison.CurrentCultureIgnoreCase ) )
            {
                return ( ToBitmap( text, font, fgColor, bgColor ) );
            }
            else
            {
                var family = new Media.FontFamily(fontfamily);
                var familytypeface = family.FamilyTypefaces[0];

                var style = styleFont.HasFlag( FontStyle.Italic ) ? System.Windows.FontStyles.Italic : System.Windows.FontStyles.Normal;
                var weight = styleFont.HasFlag( FontStyle.Bold ) ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;
                var textDecorations = new System.Windows.TextDecorationCollection();
                if ( styleFont.HasFlag( FontStyle.Strikeout ) )
                    textDecorations.Add( System.Windows.TextDecorations.Strikethrough );
                if ( styleFont.HasFlag( FontStyle.Underline ) )
                    textDecorations.Add( System.Windows.TextDecorations.Underline );

                Media.FontFamily fallback = new Media.FontFamily(SystemFonts.DefaultFont.FontFamily.Name);

                Media.Typeface face = null;

                var key = $"{fontfamily}-{fontstyle}";
                if( TypefaceList.Count<=0)
                {
                    TypefaceList = Media.Fonts.SystemTypefaces.AsParallel().ToDictionary( o => $"{o.FontFamily.FamilyNames[locale_enkey]}-{o.FaceNames[locale_enkey]}", o => o );
                }
                else if(TypefaceList.ContainsKey(key))
                {
                    face = TypefaceList[key];
                }
                else
                {
                    var faces = Media.Fonts.SystemTypefaces.AsParallel().Where( o => { return($"{o.FontFamily}-{o.FaceNames[locale_enkey]}" == key); } );
                    if ( faces.Count() > 0 )
                        face = faces.First();
                    else
                        face = new Media.Typeface( family, style, weight, familytypeface.Stretch, fallback );

                    TypefaceList[key] = face;
                }

                return ( ToBitmap( text, face, emSize, fgColor, bgColor ) );
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public static Media.Typeface ToTypeface( this Font font )
        {
            var family = new Media.FontFamily(font.FontFamily.Name);

            var style = font.Style.HasFlag( FontStyle.Italic ) ? System.Windows.FontStyles.Italic : System.Windows.FontStyles.Normal;
            var weight = font.Style.HasFlag( FontStyle.Bold ) ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;
            var textDecorations = new System.Windows.TextDecorationCollection();
            if ( font.Style.HasFlag( FontStyle.Strikeout ) )
                textDecorations.Add( System.Windows.TextDecorations.Strikethrough );
            if ( font.Style.HasFlag( FontStyle.Underline ) )
                textDecorations.Add( System.Windows.TextDecorations.Underline );

            Media.FontFamily fallback = new Media.FontFamily(SystemFonts.DefaultFont.FontFamily.Name);
            Media.Typeface result = new Media.Typeface(family, style, weight, System.Windows.FontStretches.Medium, fallback);
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontface"></param>
        /// <param name="size"></param>
        /// <param name="underline"></param>
        /// <param name="strikeout"></param>
        /// <returns></returns>
        public static Font ToFont( this Media.Typeface fontface, float size, bool underline = false, bool strikeout = false )
        {
            var family = fontface.FontFamily.FamilyNames.ContainsKey(locale_uikey) ?
                         fontface.FontFamily.FamilyNames[locale_uikey] :
                         fontface.FontFamily.FamilyNames[locale_enkey];

            var style = FontStyle.Regular;
            if ( fontface.Style != System.Windows.FontStyles.Normal )
                style |= FontStyle.Italic;
            if ( fontface.Weight != System.Windows.FontWeights.Normal )
                style |= FontStyle.Bold;
            if ( underline )
                style |= FontStyle.Underline;
            if ( strikeout )
                style |= FontStyle.Strikeout;

            var emSize = size * 72/96f;

            Font result = new Font(family, emSize, style);
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static GraphicsPath ToGraphicsPath( this string text, Font font, Color color )
        {
            #region Text Style Setting
            StringFormat tFormat = new StringFormat(StringFormatFlags.DisplayFormatControl | StringFormatFlags.MeasureTrailingSpaces);
            #endregion

            #region Measure String Size
            SizeF sizeF = new Size();
            using ( var g = Graphics.FromImage( new Bitmap( 10, 10, PixelFormat.Format32bppPArgb ) ) )
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextContrast = 2;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                sizeF = g.MeasureString( text, font, new PointF( 0, 0 ), tFormat );
            }
            #endregion

            #region Make Text String Path
            var tPath = new GraphicsPath();
            Size size = sizeF.ToSize();
            Bitmap textImg = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            using ( var g = Graphics.FromImage( textImg ) )
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextContrast = 2;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                float emSize = g.DpiY * font.Size / 72f;
                tPath.StartFigure();
                tPath.AddString( text, font.FontFamily, (int) font.Style, emSize,
                                 new PointF( 0, 0 ),
                                 tFormat );
                tPath.CloseFigure();
            }
            #endregion
            return ( tPath );
        }

        /// <summary>
        /// Convert Media BitmapSource to Bitmap32
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap( this Media.Imaging.BitmapSource source, bool copyMethod = false )
        {
            if ( copyMethod )
            {
                Bitmap bmp = new Bitmap(
                source.PixelWidth,
                source.PixelHeight,
                PixelFormat.Format32bppPArgb);
                BitmapData data = bmp.LockBits(
                new Rectangle(Point.Empty, bmp.Size),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppPArgb);
                source.CopyPixels(
                  System.Windows.Int32Rect.Empty,
                  data.Scan0,
                  data.Height * data.Stride,
                  data.Stride );
                bmp.UnlockBits( data );
                return ( bmp );

            }
            else
            {
                Bitmap bitmap;
                using ( var outStream = new MemoryStream() )
                {
                    Media.Imaging.BitmapEncoder enc = new Media.Imaging.BmpBitmapEncoder();
                    enc.Frames.Add( Media.Imaging.BitmapFrame.Create( source ) );
                    enc.Save( outStream );
                    bitmap = new Bitmap( outStream );
                }
                return bitmap;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Media.Imaging.BitmapSource ToBitmapSource( this Bitmap source )
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                          source.GetHbitmap(),
                          IntPtr.Zero,
                          System.Windows.Int32Rect.Empty,
                          Media.Imaging.BitmapSizeOptions.FromEmptyOptions() );
        }

        #endregion

        #region Image Opration Routine
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="src"></param>
        public static void CloneExif( this Image dst, Image src )
        {
            if ( src is Image && dst is Image )
            {
                if ( ExifFormat.Contains( src.RawFormat ) && ExifFormat.Contains( dst.RawFormat ) )
                {
                    foreach ( var item in src.PropertyItems )
                    {
                        dst.SetPropertyItem( item );
                    }
                }
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="InPlace"></param>
        /// <returns></returns>
        public static Image Invert( this Image image, bool InPlace = false )
        {
            Bitmap dst = null;
            if ( InPlace )
            {
                dst = image as Bitmap;
            }
            else
            {
                dst = new Bitmap( image.Width, image.Height, image.PixelFormat );
                dst.CloneExif( image );
            }
            using ( var g = Graphics.FromImage( dst ) )
            {
                ImageAttributes a = new ImageAttributes();
                ColorMatrix c = new ColorMatrix( new[]{                 // Invert Matrix
                    new float[] { -1.00f,      0,      0, 0, 0},        // red scaling factor
                    new float[] {      0, -1.00f,      0, 0, 0},        // green scaling factor
                    new float[] {      0,      0, -1.00f, 0, 0},        // blue scaling factor
                    new float[] {      0,      0,      0, 1, 0},        // alpha scaling factor
                    new float[] { 1.000f, 1.000f, 1.000f, 0, 1}         // three translations
                } );
                a.SetColorMatrix( c, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );

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

        #endregion

    }
}

