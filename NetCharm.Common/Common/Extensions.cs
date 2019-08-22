using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using NetCharm.Common;
using NGettext.WinForm;
using Media = System.Windows.Media;

namespace ExtensionMethods
{
    /// <summary>
    /// Source code copy from
    /// http://www.cnblogs.com/bomo/archive/2013/02/26/2934055.html
    /// </summary>
    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap( Bitmap source )
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                int PixelCount = Width * Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize( source.PixelFormat );

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if ( Depth != 8 && Depth != 24 && Depth != 32 )
                {
                    throw new ArgumentException( "Only 8, 24 and 32 bpp images are supported." );
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits( rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat );

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                System.Runtime.InteropServices.Marshal.Copy( Iptr, Pixels, 0, Pixels.Length );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                System.Runtime.InteropServices.Marshal.Copy( Pixels, 0, Iptr, Pixels.Length );

                // Unlock bitmap data
                source.UnlockBits( bitmapData );
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel( int x, int y )
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if ( i > Pixels.Length - cCount )
                throw new IndexOutOfRangeException();

            if ( Depth == 32 ) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb( a, r, g, b );
            }
            if ( Depth == 24 ) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb( r, g, b );
            }
            if ( Depth == 8 )
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb( c, c, c );
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel( int x, int y, Color color )
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if ( Depth == 32 ) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if ( Depth == 24 ) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if ( Depth == 8 )
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TextAlignH
    {
        //
        // 摘要:
        //     对象或文本与控件元素的左侧对齐。
        Left = 0,
        //
        // 摘要:
        //     对象或文本与控件元素的右侧对齐。
        Right = 1,
        //
        // 摘要:
        //     对象或文本与控件元素的中心对齐。
        Center = 2
    }

    /// <summary>
    /// 
    /// </summary>
    static public partial class NetCharmExtensions
    {
        #region GDI32 API function
        [System.Runtime.InteropServices.DllImport( "gdi32.dll" )]
        static public extern bool DeleteObject( IntPtr hObject );

        #endregion

        #region CultrueInfo Routines & Pre-Defined
        private static string locale_en = "en-us";
        private static string locale_ui = CultureInfo.InstalledUICulture.Name.ToLower();

        private static CultureInfo locale_eninfo = CultureInfo.GetCultureInfo("en-US");
        private static CultureInfo locale_uiinfo = CultureInfo.InstalledUICulture;

        private static XmlLanguage locale_enkey = XmlLanguage.GetLanguage( locale_en );
        private static XmlLanguage locale_uikey = XmlLanguage.GetLanguage( locale_ui );

        static public string[] SystemLocales( this CultureInfo culture )
        {
            List<string> langs = new List<string>();
            foreach ( CultureInfo ci in CultureInfo.GetCultures( CultureTypes.AllCultures ) )
            {
                string specName = "(none)";
                try { specName = CultureInfo.CreateSpecificCulture( ci.Name ).Name; }
                catch { }
                //langs.Add( String.Format( "{0,-12}{1,-12}{2}", ci.Name, specName, ci.EnglishName ) );
                langs.Add( string.Format( "{0}", ci.Name ) );
            }

            langs.Sort();  // sort by name

            return langs.ToArray();
        }

        #endregion

        #region Form I18N Extension
        static public bool InDesignMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

        /// <summary>
        /// Fake function for gettext collection msgid
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static public string T( this string t )
        {
            return ( t );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addin"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        static public string _( this string t )
        {
            if ( InDesignMode ) return ( t );
            string result = t;


            if ( !string.IsNullOrEmpty( t ) )
            {
                //string path = Assembly.GetExecutingAssembly().Location;
                string path = Assembly.GetCallingAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( path ) ), "locale" );
                I18N i10n = new I18N( domain, addinRoot );
                result = I18N._( i10n.Catalog, t );
            }
            else
                return ( t );

            if ( string.Equals( result, t, StringComparison.CurrentCulture ) )
            {
                string path = Assembly.GetExecutingAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( path ) ), "locale" );
                I18N i10n = new I18N( domain, addinRoot );
                result = I18N._( i10n.Catalog, t );
            }

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
        static public void Translate( this Form form, ToolTip tooltip = null, object[] extra = null )
        {
            if ( InDesignMode ) return;
            if ( form is Form )
            {
                //string path = Assembly.GetExecutingAssembly().Location;
                string path = Assembly.GetCallingAssembly().Location;
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
        static public string _( this Form form, string text )
        {
            return ( _( text ) );
        }
        #endregion

        #region Color Extension Methods
        public enum HtmlColorOrder
        {
            NAME = 0,
            RGB = 1,
            BGR = 2,
            RGBA = 3,
            BGRA = 4,
            ARGB = 5,
            ABGR = 6,
            HSL = 7,
            HSLA = 8,
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>        
        static public string ToHtml( this Color color )
        {
            string html = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
            return ( html );
        }

        static public string ToHtml( this Color color, bool sharp = true )
        {
            string html = sharp ? "#":string.Empty;
            html = string.Format( $"{html}{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B );
            return ( html );
        }

        static public string ToHtml( this Color color, bool sharp = true, HtmlColorOrder order = default( HtmlColorOrder ) )
        {
            string html = sharp ? "#":string.Empty;
            switch ( order )
            {
                case HtmlColorOrder.NAME:
                    if ( color.IsNamedColor )
                        html = string.Format( $"{color.Name}" );
                    else
                        html = color.ToHtml( sharp, HtmlColorOrder.RGB );
                    break;
                case HtmlColorOrder.RGB:
                    html = string.Format( $"{html}" + "{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B );
                    break;
                case HtmlColorOrder.BGR:
                    html = string.Format( $"{html}" + "{0:X2}{1:X2}{2:X2}", color.B, color.G, color.R );
                    break;
                case HtmlColorOrder.RGBA:
                    html = string.Format( $"{html}" + "{0:X2}{1:X2}{2:X2}{3:X2}", color.R, color.G, color.B, color.A );
                    break;
                case HtmlColorOrder.BGRA:
                    html = string.Format( $"{html}" + "{0:X2}{1:X2}{2:X2}{3:X2}", color.B, color.G, color.R, color.A );
                    break;
                case HtmlColorOrder.ARGB:
                    html = string.Format( $"{html}" + "{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B );
                    break;
                case HtmlColorOrder.ABGR:
                    html = string.Format( $"{html}" + "{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.B, color.G, color.R );
                    break;
            }
            return ( html );
        }

        static public string ToCSS( this Color color, bool value = true, HtmlColorOrder order = default( HtmlColorOrder ) )
        {
            string html = string.Empty;
            double factor = 255f;
            switch ( order )
            {
                case HtmlColorOrder.NAME:
                    if ( color.IsNamedColor )
                        html = string.Format( $"{color.Name}" );
                    else
                        html = color.ToCSS( value, HtmlColorOrder.RGBA );
                    break;
                case HtmlColorOrder.RGB:
                    if(value)
                        html = string.Format( "rgb( {0}, {1}, {2} )", color.R, color.G, color.B );
                    else
                        html = string.Format( "rgb( {0:0%}, {1:0%}, {2:0%} )", color.R / factor, color.G / factor, color.B / factor );
                    break;
                case HtmlColorOrder.RGBA:
                    if ( value )
                        html = string.Format( "rgba( {0}, {1}, {2}, {3:F2} )", color.R, color.G, color.B, color.A / factor );
                    else
                        html = string.Format( "rgba( {0:0%}, {1:0%}, {2:0%}, {3:F2} )", color.R / factor, color.G / factor, color.B / factor, color.A / factor );
                    break;
                case HtmlColorOrder.HSL:
                    html = string.Format( "hsl( {0:F0}, {1:0%}, {2:0%} )", color.GetHue(), color.GetSaturation(), color.GetBrightness() );
                    break;
                case HtmlColorOrder.HSLA:
                    html = string.Format( "hsla( {0:F0}, {1:0%}, {2:0%}, {3:F2} )", color.GetHue(), color.GetSaturation(), color.GetBrightness(), color.A / factor );
                    break;
            }
            return ( html );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        static public Color FromHtml( this string html )
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
        static public Color ToColor( this string html )
        {
            return ( FromHtml( html ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        static public Media.Color ToMediaColor( this Color color )
        {
            return ( Media.Color.FromArgb( color.A, color.R, color.G, color.B ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        static public Color ToDrawingColor( this Media.Color color )
        {
            return ( Color.FromArgb( color.A, color.R, color.G, color.B ) );
        }
        #endregion

        #region Font & Typeface Routines
        /// <summary>
        /// 
        /// </summary>
        static private Dictionary<string, Media.Typeface> TypefaceList = new Dictionary<string, Media.Typeface>();

        /// <summary>
        /// 
        /// </summary>
        static private Dictionary<string, Dictionary<string, Media.Typeface>> FamilyList = new Dictionary<string, Dictionary<string, Media.Typeface>>();

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        static public Dictionary<string, Dictionary<string, Media.Typeface>> GetFamilies()
        {
            #region Fetch All Typefaces
            if ( FamilyList.Count > 0 ) return ( FamilyList );

            ICollection<Media.FontFamily> families = null;
            //ICollection<Media.Typeface> typeface = null;

            families = Media.Fonts.SystemFontFamilies;
            try
            {
                foreach( var f in Media.Fonts.SystemTypefaces )
                {
                    var f_key = f.FontFamily.FamilyNames.ContainsKey( locale_enkey ) ? f.FontFamily.FamilyNames[locale_enkey] : f.FontFamily.FamilyNames.First().Value;
                    var locale_key = f.FontFamily.FamilyNames.ContainsKey( locale_enkey ) ? locale_enkey : f.FontFamily.FamilyNames.Keys.First();

                    try
                    {
                        var facelist = new Dictionary<string, Media.Typeface>();
                        if( FamilyList.ContainsKey( f_key ) )
                            facelist = FamilyList[f_key];

                        if( f.FaceNames.ContainsKey( locale_key ) )
                            facelist[f.FaceNames[locale_key].Replace( f_key, "" ).Trim()] = f;

                        if( f.FaceNames.ContainsKey( locale_uikey ) )
                            facelist[f.FaceNames[locale_uikey].Replace( f_key, "" ).Trim()] = f;

                        FamilyList[f_key] = facelist;
                        if( f.FontFamily.FamilyNames.ContainsKey( locale_uikey ) )
                        {
                            var familyname = f.FontFamily.FamilyNames[locale_uikey];
                            FamilyList[familyname] = facelist;
                        }
                    }
                    catch( Exception )
                    {
                        FamilyList[f_key] = null;
                    }
                }

            }
            catch( Exception ) { }
            #endregion

            #region Save Typeface list to Text file
            StringBuilder sb = new StringBuilder();
            foreach ( var f in FamilyList )
            {
                sb.AppendLine( f.Key.ToString() );
                foreach ( var ff in f.Value )
                {
                    //sb.AppendLine( $"  {ff.Key}*{ff.Value.ToString()}" );
                    sb.AppendLine( $"  {ff.Key}" );
                }
            }
            sb.AppendLine( "--------------------------------------------------" );
            sb.AppendLine( $"Total {FamilyList.Count} Families" );
            System.IO.File.WriteAllText( "typefaces.txt", sb.ToString() );
            #endregion

            return ( FamilyList );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="face"></param>
        /// <param name="underline"></param>
        /// <param name="strikeout"></param>
        /// <returns></returns>
        static public FontStyle GetFontStyle( this Media.Typeface face, bool underline = false, bool strikeout = false )
        {
            FontStyle result = FontStyle.Regular;
            var regular = false;
            if ( face is Media.Typeface )
            {
                //if ( face.Style == System.Windows.FontStyles.Italic )
                //    result |= FontStyle.Italic;
                //if ( face.Weight == System.Windows.FontWeights.Bold )
                //    result |= FontStyle.Bold;

                foreach ( var s in face.FaceNames[locale_enkey].Split() )
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
        /// <param name="font"></param>
        /// <returns></returns>
        static public Media.Typeface ToTypeface( this Font font )
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
        static public Font ToFont( this Media.Typeface fontface, float size, bool underline = false, bool strikeout = false )
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

        #region Add Face locale Dict
        /// <summary>
        /// Add Face locale Dict
        /// </summary>
        private static Dictionary<string, string> FontStyleList = new Dictionary<string, string>()
        {
            { T( "Thin" ), "Thin" },
            { T( "Light" ), "Light" },
            { T( "Regular" ), "Regular" },
            { T( "Medium" ), "Medium" },
            { T( "Bold" ), "Bold" },
            { T( "Black" ), "Black" },
            { T( "Oblique" ), "Oblique" },
            { T( "Italic" ), "Italic" },

            { T( "SemiBold" ), "SemiBold" },
            { T( "SemiCondensed" ), "SemiCondensed" },
            { T( "Condensed" ), "Condensed" },
            { T( "Expanded" ), "Expanded" },
            { T( "Extended" ), "Extended" },
            { T( "Heavy" ), "Heavy" },
            { T( "ExtraBlack" ), "Extra Black" },
            { T( "Extra Black" ), "Extra Black" },
            { T( "UltraLight" ), "Ultra Light" },
            { T( "Ultra Light" ), "Ultra Light" },
            { T( "Ultra" ), "Ultra" },
            { T( "Extra" ), "Extra" },
            { T( "Roman" ), "Roman" },
            { T( "Normal" ), "Regular" },
            { T( "regular" ), "Regular" },

            { T( "250" ), "Thin" },
            { T( "350" ), "Regular" },
            { T( "275" ), "Ultra Light" },
            { T( "750" ), "Heavy" },

            { T( "W3" ), "Light" },
            { T( "W6" ), "SemiBold" }
        };
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facenames"></param>
        /// <returns></returns>
        static public Dictionary<string, string> FaceNameList( this Dictionary<string, string> facenames )
        {
            return ( FontStyleList );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facename"></param>
        /// <param name="underline"></param>
        /// <param name="strikeout"></param>
        /// <returns></returns>
        static public FontStyle ToFontStyle( this string facename, bool underline = false, bool strikeout = false )
        {
            FontStyle result = FontStyle.Regular;

            var regular = false;
            foreach ( var style in facename.Split() )
            {
                var s = style;
                if ( FontStyleList.ContainsKey( style ) )
                    s = FontStyleList[style];
                if ( string.Equals( s, "Bold", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    result |= FontStyle.Bold;
                }
                else if ( string.Equals( s, "Italic", StringComparison.CurrentCultureIgnoreCase ) ||
                    string.Equals( s, "Oblique", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    result |= FontStyle.Italic;
                }
                else if ( string.Equals( style.Trim(), "Underline", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    result |= FontStyle.Underline;
                }
                else if ( string.Equals( style.Trim(), "Strikeout", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    result |= FontStyle.Strikeout;
                }
                else if ( string.Equals( s, "Regular", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    regular = true;
                }
            }
            if ( !regular )
                result &= ~FontStyle.Regular;

            if ( underline )
                result |= FontStyle.Underline;
            if ( strikeout )
                result |= FontStyle.Strikeout;

            return ( result );
        }
        #endregion

        #region Bitmap Extension Methods
        #region PixelFormat catalogs
        /// <summary>
        /// 
        /// </summary>
        static public PixelFormat[] AlphaFormat = new PixelFormat[]
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
        static public PixelFormat[] Format1bpp = new PixelFormat[]
        {
            PixelFormat.Format1bppIndexed
        };
        static public PixelFormat[] Format4bpp = new PixelFormat[]
        {
            PixelFormat.Format4bppIndexed
        };
        static public PixelFormat[] Format8bpp = new PixelFormat[]
        {
            PixelFormat.Format8bppIndexed
        };
        static public PixelFormat[] Format15bpp = new PixelFormat[]
        {
        };
        static public PixelFormat[] Format16bpp = new PixelFormat[]
        {
            PixelFormat.Format16bppArgb1555,
            PixelFormat.Format16bppGrayScale,
            PixelFormat.Format16bppRgb555,
            PixelFormat.Format16bppRgb565
        };
        static public PixelFormat[] Format24bpp = new PixelFormat[]
        {
            PixelFormat.Format24bppRgb
        };
        static public PixelFormat[] Format32bpp = new PixelFormat[]
        {
            PixelFormat.Format32bppArgb,
            PixelFormat.Format32bppPArgb,
            PixelFormat.Format32bppRgb
        };
        static public PixelFormat[] Format48bpp = new PixelFormat[]
        {
            PixelFormat.Format48bppRgb
        };
        static public PixelFormat[] Format64bpp = new PixelFormat[]
        {
            PixelFormat.Format64bppArgb,
            PixelFormat.Format64bppPArgb
        };
        #endregion

        #region EXIF file
        static public ImageFormat[] ExifFormat = new ImageFormat[]
        {
            ImageFormat.Exif,
            ImageFormat.Jpeg,
            ImageFormat.Tiff,
            ImageFormat.MemoryBmp
        };
        #endregion

        #region Test Image Color Info
        static public bool IsAlpha( this Image image )
        {
            return ( AlphaFormat.Contains( image.PixelFormat ) );
        }

        static public bool Is1bit( this Image image )
        {
            return ( Format1bpp.Contains( image.PixelFormat ) );
        }

        static public bool Is4bits( this Image image )
        {
            return ( Format4bpp.Contains( image.PixelFormat ) );
        }

        static public bool Is8bits( this Image image )
        {
            return ( Format8bpp.Contains( image.PixelFormat ) );
        }

        static public bool Is15bits( this Image image )
        {
            return ( Format15bpp.Contains( image.PixelFormat ) );
        }

        static public bool Is16bits( this Image image )
        {
            return ( Format16bpp.Contains( image.PixelFormat ) );
        }

        static public bool Is24bits( this Image image )
        {
            return ( Format24bpp.Contains( image.PixelFormat ) );
        }

        static public bool Is32bits( this Image image )
        {
            return ( Format32bpp.Contains( image.PixelFormat ) );
        }

        static public bool Is48bits( this Image image )
        {
            return ( Format48bpp.Contains( image.PixelFormat ) );
        }

        static public bool Is64bits( this Image image )
        {
            return ( Format64bpp.Contains( image.PixelFormat ) );
        }
        #endregion

        #region Image EXIF info
        static public bool IsExif( this Image image )
        {
            return ( ExifFormat.Contains( image.RawFormat ) );
        }

        #endregion

        #region Image Convert Routines
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        static public Icon ToIcon( this Image image )
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
        static public Icon ToIcon( this Bitmap image )
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
        /// Convert Media BitmapSource to Bitmap32
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        static public Bitmap ToBitmap( this Media.Imaging.BitmapSource source, bool copyMethod = false )
        {
            if ( copyMethod )
            {
                Bitmap bmp = new Bitmap( source.PixelWidth, source.PixelHeight,
                    PixelFormat.Format32bppArgb);
                BitmapData data = bmp.LockBits( new Rectangle(Point.Empty, bmp.Size),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);
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
        static public Media.Imaging.BitmapSource ToBitmapSource( this Bitmap source )
        {
            var hBmp = source.GetHbitmap();
            var result = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                          hBmp,
                          IntPtr.Zero,
                          System.Windows.Int32Rect.Empty,
                          Media.Imaging.BitmapSizeOptions.FromEmptyOptions() );
            DeleteObject( hBmp );
            return ( result );
        }
        #endregion

        #region Text to Bitmap Routines
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="fgColor"></param>
        /// <returns></returns>
        static public Bitmap ToBitmap( this string text, Font font, Color fgColor )
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
        static public Bitmap ToBitmap( this string text, Font font, Color fgColor, Color bgColor )
        {
            #region Text Style Setting
            StringFormat tFormat = new StringFormat(StringFormatFlags.DisplayFormatControl | StringFormatFlags.MeasureTrailingSpaces);
            #endregion

            #region Measure String Size
            SizeF sizeF = new Size();
            using ( var g = Graphics.FromImage( new Bitmap( 10, 10, PixelFormat.Format32bppArgb ) ) )
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
            sizeF.Width *= 1.05f;
            #endregion

            #region Make Text Picture
            Size size = sizeF.ToSize();
            size.Height *= 2;
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
                                 new PointF( 0, size.Height / 4.0f ),
                                 tFormat );
                tPath.CloseFigure();
                //g.DrawPath( new Pen( color, 1f ), tPath );
                g.FillPath( new SolidBrush( fgColor ), tPath );
            }
            #endregion

            #region Crop Transparent Area
            var dstRect = textImg.ContentBound();
            var dst =  new Bitmap(dstRect.Width, dstRect.Height, textImg.PixelFormat);
            using ( var g = Graphics.FromImage( dst ) )
            {
                g.DrawImage( textImg, 0, 0, dstRect, GraphicsUnit.Pixel );
            }
            #endregion

            return ( dst );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontface"></param>
        /// <param name="fontsize"></param>
        /// <param name="fgColor"></param>
        /// <returns></returns>
        static public Bitmap ToBitmap( this string text, Media.Typeface fontface, float fontsize, Color fgColor )
        {
            var locale = CultureInfo.CurrentUICulture.IetfLanguageTag;
            var decoration = new System.Windows.TextDecorationCollection();
            return ( ToBitmap( text, fontface, decoration, fontsize, locale, TextAlignH.Left, fgColor, Color.Transparent ) );
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
        static public Bitmap ToBitmap( this string text, Media.Typeface fontface, System.Windows.TextDecorationCollection decoration, float fontsize, string locale, TextAlignH align, Color fgColor, Color bgColor )
        {
            if ( !( fontface is Media.Typeface ) ) return ( null );

            #region Calc fontface screen size
            float dpiX = 96f;
            float dpiY = 96f;
            using ( Graphics g = Graphics.FromHwnd( IntPtr.Zero ) )
            {
                dpiX = g.DpiX;
                dpiY = g.DpiY;
            }

            float emSize = fontsize * dpiY / 72f;
            //float emSize = 12 * dpiY / 72f;
            #endregion

            #region Set FontStyle
            var fontStyle = System.Windows.FontStyles.Normal;
            var fontWeight = System.Windows.FontWeights.Normal;
            var textDecorations = new System.Windows.TextDecorationCollection();
            foreach ( var d in decoration )
            {
                textDecorations.Add( d );
            }

            fontStyle = fontface.Style;
            fontWeight = fontface.Weight;
            #endregion

            #region Get CultureInfo
            //var culture = CultureInfo.InstalledUICulture;
            var culture = CultureInfo.CurrentUICulture;
            if ( string.IsNullOrEmpty( locale ) )
            {
                //if ( !fontface.FaceNames.ContainsKey( locale_uikey ) )
                if ( !fontface.FontFamily.FamilyNames.ContainsKey( locale_uikey ) )
                    culture = new CultureInfo( "en-us" );
            }
            else
            {
                try
                {
                    culture = new CultureInfo( locale );
                }
                catch ( CultureNotFoundException )
                {
                }
            }
            var flow = culture.TextInfo.IsRightToLeft ? System.Windows.FlowDirection.RightToLeft : System.Windows.FlowDirection.LeftToRight;
            #endregion

            #region Create FormattedText
            Media.Brush foreground = new  Media.SolidColorBrush(fgColor.ToMediaColor());
            Media.FormattedText formattedText = new Media.FormattedText( text,
                //CultureInfo.InvariantCulture,
                //CultureInfo.CurrentUICulture,
                culture,
                flow,
                fontface, emSize, foreground);
            //new Media.NumberSubstitution(Media.NumberCultureSource.Text,
            //    culture,
            //    Media.NumberSubstitutionMethod.AsCulture)
            //);
            formattedText.Trimming = System.Windows.TextTrimming.None;
            #endregion

            #region Set FormattedText Max Size
            //formattedText.MaxTextWidth = rectangle.Width / ( dpiX / 96.0 );
            //formattedText.MaxTextHeight = rectangle.Height / ( dpiY / 96.0 );
            //formattedText.MaxTextHeight = ( emSize + 4 ) / ( dpiY / 96.0 );
            formattedText.MaxTextWidth = 1280;
            formattedText.MaxTextHeight = 720;
            #endregion

            #region Set FormattedText Alignment
            if ( align == TextAlignH.Left )
                formattedText.TextAlignment = System.Windows.TextAlignment.Left;
            else if ( align == TextAlignH.Center )
                formattedText.TextAlignment = System.Windows.TextAlignment.Center;
            else if ( align == TextAlignH.Right )
                formattedText.TextAlignment = System.Windows.TextAlignment.Right;
            #endregion

            #region Set FormattedText Style
            formattedText.SetFontStyle( fontStyle );
            formattedText.SetFontWeight( fontWeight );
            formattedText.SetTextDecorations( textDecorations );
            #endregion

            #region Draw the FormattedText on a Drawing Visual
            Media.DrawingVisual drawingVisual = new Media.DrawingVisual();
            using ( var drawingContext = drawingVisual.RenderOpen() )
            {
                //drawingContext.PushEffect( new Media.Effects.BlurBitmapEffect(), null );
                drawingContext.DrawText( formattedText, new System.Windows.Point( 0, 0 ) );
                drawingContext.Close();
            }
            #endregion

            #region Render the DrawingVisual into a RenderTargetBitmap 
            double offset = 1.5f;
            if ( fontface.Style == System.Windows.FontStyles.Italic || fontface.Style == System.Windows.FontStyles.Oblique ) offset = 2.0f;
            var pixelWidth = Convert.ToInt32( Math.Ceiling(formattedText.Width * (dpiX / 96.0)) * offset);
            var pixelHeight = Convert.ToInt32( Math.Ceiling(formattedText.Height * (dpiY / 96.0))* offset);
            if ( pixelWidth == 0 || pixelHeight == 0 )
                return ( new Bitmap( 1, 1 ) );

            var rtb = new Media.Imaging.RenderTargetBitmap(
              pixelWidth, pixelHeight,
              dpiX, dpiY,
              Media.PixelFormats.Pbgra32 );
            rtb.Render( drawingVisual );
            #endregion

            #region Create a System.Drawing.Bitmap 
            //var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppPArgb );
            var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppArgb );
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

            #region Crop Transparent Area
            var srcRect = bitmap.ContentBound();
            var dst =  new Bitmap(srcRect.Width, srcRect.Height, bitmap.PixelFormat);
            using ( var g = Graphics.FromImage( dst ) )
            {
                g.DrawImage( bitmap, 0, 0, srcRect, GraphicsUnit.Pixel );
            }
            #endregion
            return ( dst );
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontface"></param>
        /// <param name="fontsize"></param>
        /// <param name="locale"></param>
        /// <param name="align"></param>
        /// <param name="fgColor"></param>
        /// <param name="bgColor"></param>
        /// <param name="underline"></param>
        /// <param name="strikeout"></param>
        /// <returns></returns>
        static public Bitmap ToBitmap( this string text, Media.Typeface fontface, float fontsize, string locale, TextAlignH align, Color fgColor, Color bgColor, bool underline = false, bool strikeout = false )
        {
            var decoration = new System.Windows.TextDecorationCollection();
            if ( underline )
                decoration.Add( System.Windows.TextDecorations.Underline );
            if ( strikeout )
                decoration.Add( System.Windows.TextDecorations.Strikethrough );
            return ( ToBitmap( text, fontface, decoration, fontsize, locale, align, fgColor, bgColor ) );

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
        static public Bitmap ToBitmap( this string text, string fontfamily, string fontstyle, float fontsize, Color fgColor )
        {
            var locale = CultureInfo.CurrentUICulture.IetfLanguageTag;
            return ( ToBitmap( text, fontfamily, fontstyle, fontsize, locale, fgColor, Color.Transparent ) );
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
        static public Bitmap ToBitmap( this string text, string fontfamily, string fontstyle, float fontsize, string locale, Color fgColor, Color bgColor )
        {
            #region Set font style
            var styleFont = fontstyle.ToFontStyle();
            #endregion

            #region Get Text Align
            var align = TextAlignH.Left;
            foreach ( var s in fontstyle.Split() )
            {
                if ( string.Equals( s.Trim(), "Center", StringComparison.CurrentCultureIgnoreCase ) )
                    align = TextAlignH.Center;
                else if ( string.Equals( s.Trim(), "Right", StringComparison.CurrentCultureIgnoreCase ) )
                    align = TextAlignH.Right;
            }
            #endregion

            var emSize = fontsize * 96/72f;

            #region Draw Text to Bitmap
            #region Detecting Is TTF Font of Not
            Font font = null; // new Font(fontfamily, emSize);
            try
            {
                font = new Font( fontfamily, emSize, styleFont );
            }
            catch ( Exception )
            {
                //try
                //{
                //    font = new Font( fontfamily, emSize, FontStyle.Italic );
                //}
                //catch ( Exception )
                //{
                //    font = new Font( fontfamily, emSize, FontStyle.Bold );
                //}
            }
            #endregion
            if ( font is Font && string.Equals( font.Name, fontfamily, StringComparison.CurrentCultureIgnoreCase ) )
            {
                return ( ToBitmap( text, font, fgColor, bgColor ) );
            }
            else
            {
                #region Get Font family info
                var family = new Media.FontFamily(fontfamily);
                var familytypeface = family.FamilyTypefaces[0];

                var style = styleFont.HasFlag( FontStyle.Italic ) ? System.Windows.FontStyles.Italic : System.Windows.FontStyles.Normal;
                var weight = styleFont.HasFlag( FontStyle.Bold ) ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;
                var underline = false;
                var strikeout = false;
                var textDecorations = new System.Windows.TextDecorationCollection();
                if ( styleFont.HasFlag( FontStyle.Strikeout ) )
                {
                    textDecorations.Add( System.Windows.TextDecorations.Strikethrough );
                    strikeout = true;
                }
                if ( styleFont.HasFlag( FontStyle.Underline ) )
                {
                    textDecorations.Add( System.Windows.TextDecorations.Underline );
                    underline = true;
                }

                Media.FontFamily fallback = new Media.FontFamily(SystemFonts.DefaultFont.FontFamily.Name);
                #endregion

                #region Adjust fontstyle string
                var fontstyles = new List<string>();
                foreach ( var s in fontstyle.Split() )
                {
                    var st = s.Trim();
                    if ( string.Equals( st, "Underline", StringComparison.CurrentCultureIgnoreCase ) ||
                       string.Equals( st, "Strikeout", StringComparison.CurrentCultureIgnoreCase ) )
                        continue;
                    else if ( string.Equals( st, "250", StringComparison.CurrentCultureIgnoreCase ) )
                        fontstyles.Add( "Thin" );
                    else if ( string.Equals( st, "350", StringComparison.CurrentCultureIgnoreCase ) )
                        fontstyles.Add( "Regular" );
                    else if ( string.Equals( st, "SemiBold", StringComparison.CurrentCultureIgnoreCase ) )
                        fontstyles.Add( "W6" );
                    else
                        fontstyles.Add( st );
                }
                fontstyle = string.Join( " ", fontstyles );
                #endregion

                #region Get typeface from custom FamilyList or create default
                Media.Typeface face = null;
                if ( FamilyList.Count > 0 )
                {
                    if ( FamilyList.ContainsKey( fontfamily ) )
                        if ( FamilyList[fontfamily].ContainsKey( fontstyle ) )
                            face = FamilyList[fontfamily][fontstyle];
                }

                if ( face == null )
                {
                    #region Create default typeface with family name
                    var locale_key = family.FamilyNames.ContainsKey(locale_enkey) ? locale_enkey : family.FamilyNames.First().Key;
                    var key = $"{fontfamily}*{fontstyle}";
                    var key0 = $"{family.FamilyNames[locale_key]}*{fontstyle}";
                    if ( TypefaceList.ContainsKey( key ) )
                        face = TypefaceList[key];
                    else if ( TypefaceList.ContainsKey( key0 ) )
                        face = TypefaceList[key0];
                    else
                    {
                        face = new Media.Typeface( family, style, weight, familytypeface.Stretch, fallback );
                        TypefaceList[key0] = face;
                        TypefaceList[key] = face;
                    }
                    #endregion
                }
                #endregion

                return ( text.ToBitmap( face, emSize, locale, align, fgColor, bgColor, underline, strikeout ) );
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        static public GraphicsPath ToGraphicsPath( this string text, Font font, Color color )
        {
            #region Text Style Setting
            StringFormat tFormat = new StringFormat(StringFormatFlags.DisplayFormatControl | StringFormatFlags.MeasureTrailingSpaces);
            #endregion

            #region Measure String Size
            SizeF sizeF = new Size();
            using ( var g = Graphics.FromImage( new Bitmap( 10, 10, PixelFormat.Format32bppArgb ) ) )
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
        /// 
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        static public Bitmap Base64ToImage( this string base64 )
        {
            Bitmap img = null;
            if ( !string.IsNullOrEmpty( base64 ) )
            {
                try
                {
                    byte[] arr = Convert.FromBase64String(base64);
                    using ( MemoryStream ms = new MemoryStream( arr ) )
                    {
                        img = new Bitmap( ms );

                    }
                }
                catch ( Exception )
                {

                }
            }
            return ( img );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        static public string ImageToBase64( Image img )
        {
            string base64 = string.Empty;
            if ( img is Image )
            {
                try
                {
                    using ( MemoryStream ms = new MemoryStream() )
                    {
                        img.Save( ms, ImageFormat.Png );

                        byte[] arr = ms.ToArray();
                        base64 = Convert.ToBase64String( arr, Base64FormattingOptions.InsertLineBreaks );
                    }
                }
                catch ( Exception )
                {

                }
            }
            return ( base64 );
        }
        #endregion

        #region Image Basic Routine
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageSize"></param>
        /// <param name="patternSize"></param>
        /// <returns></returns>
        static public Bitmap MakePatternImage( Size imageSize, int patternSize = 8, Color? fgColor = null, Color? bgColor = null )
        {
            Bitmap pat = new Bitmap(patternSize*2, patternSize*2, PixelFormat.Format32bppArgb);
            using ( var g = Graphics.FromImage( pat ) )
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                SolidBrush bb = new SolidBrush( fgColor ?? Color.Silver);
                SolidBrush wb = new SolidBrush( bgColor ?? Color.White);

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
        /// <param name="src"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        static public Rectangle ContentBound( this Bitmap src, ContentMaskMode mode = ContentMaskMode.Alpha, bool fastmode = true )
        {
            Rectangle result = new Rectangle(0, 0, src.Width, src.Height);

            LockBitmap lockbmp = new LockBitmap(src);
            //锁定Bitmap，通过Pixel访问颜色
            lockbmp.LockBits();
            #region Get Ref Color value
            Color cRef = lockbmp.GetPixel(0, 0);
            switch ( mode )
            {
                case ContentMaskMode.Alpha:
                    cRef = Color.Transparent;
                    break;
                case ContentMaskMode.TopLeft:
                    cRef = lockbmp.GetPixel( 0, 0 );
                    break;
                case ContentMaskMode.BottomRight:
                    cRef = lockbmp.GetPixel( lockbmp.Width - 1, lockbmp.Height - 1 );
                    break;
            }
            #endregion

            int xMin = 0;
            int xMax = lockbmp.Width-1;
            int yMin = 0;
            int yMax = lockbmp.Height-1;

            if ( fastmode )
            {
                #region Check pixels value with Ref Value
                var w = lockbmp.Width-1;
                var h = lockbmp.Height-1;
                var hh = (int)Math.Ceiling(lockbmp.Height / 2.0f)+1;
                var wh = (int)Math.Ceiling(lockbmp.Width / 2.0f)+1;

                #region Get Bound Top & Bottom
                for ( var y = 0; y < h; y++ )
                {
                    var yc = h - y;
                    for ( var x = 0; x < w + 1; x++ )
                    {
                        Color ct = lockbmp.GetPixel( x, y );
                        Color cb = lockbmp.GetPixel( x, yc );
                        switch ( mode )
                        {
                            case ContentMaskMode.Alpha:
                                if ( ct.A != cRef.A )
                                {
                                    if ( yMin == 0 ) yMin = y;
                                }
                                if ( cb.A != cRef.A )
                                {
                                    if ( yMax == h ) yMax = yc;
                                }
                                break;
                            default:
                                if ( ct.A != cRef.A || ct.R != cRef.R || ct.G != cRef.G || ct.B != cRef.B )
                                {
                                    if ( yMin == 0 ) yMin = y;
                                }
                                if ( cb.A != cRef.A || cb.R != cRef.R || cb.G != cRef.G || cb.B != cRef.B )
                                {
                                    if ( yMax == h ) yMax = yc;
                                }
                                break;
                        }
                        if ( yMin != 0 && yMax != h ) break;
                    }
                    if ( yc <= yMin ) break;
                    if ( yMin != 0 && yMax != h ) break;
                }
                #endregion

                #region Get Bound Left & Right
                for ( var x = 0; x < w; x++ )
                {
                    var xc = w - x;
                    for ( var y = yMin + 1; y < yMax; y++ )
                    {
                        Color cl = lockbmp.GetPixel( x, y );
                        Color cr = lockbmp.GetPixel( xc, y );
                        switch ( mode )
                        {
                            case ContentMaskMode.Alpha:
                                if ( cl.A != cRef.A )
                                {
                                    if ( xMin == 0 ) xMin = x;
                                }
                                if ( cr.A != cRef.A )
                                {
                                    if ( xMax == w ) xMax = xc;
                                }
                                break;
                            default:
                                if ( cl.A != cRef.A || cl.R != cRef.R || cl.G != cRef.G || cl.B != cRef.B )
                                {
                                    if ( xMin == 0 ) xMin = x;
                                }
                                if ( cr.A != cRef.A || cr.R != cRef.R || cr.G != cRef.G || cr.B != cRef.B )
                                {
                                    if ( xMax == w ) xMax = xc;
                                }
                                break;
                        }
                        if ( xMin != 0 && xMax != w ) break;
                    }
                    if ( xc <= xMin ) break;
                    if ( xMin != 0 && xMax != w ) break;
                }
                #endregion

                #endregion
            }
            else
            {
                #region Check pixels value with Ref Value
                bool content = false;
                xMin = lockbmp.Width - 1;
                xMax = 0;
                yMin = lockbmp.Height - 1;
                yMax = 0;
                Color c = lockbmp.GetPixel( 0, 0 );
                for ( var y = 0; y < lockbmp.Height; y++ )
                {
                    content = false;
                    for ( var x = 0; x < lockbmp.Width; x++ )
                    {
                        c = lockbmp.GetPixel( x, y );
                        switch ( mode )
                        {
                            case ContentMaskMode.Alpha:
                                if ( !content && c.A != cRef.A )
                                {
                                    if ( x < xMin ) xMin = x - 1;
                                    if ( y < yMin ) yMin = y - 1;
                                    content = true;
                                }
                                else if ( content && c.A != cRef.A )
                                {
                                    if ( x > xMax ) xMax = x + 1;
                                    if ( y > yMax ) yMax = y + 1;
                                }
                                break;
                            case ContentMaskMode.TopLeft:
                            case ContentMaskMode.BottomRight:
                                if ( !content && ( c.R != cRef.R || c.G != cRef.G || c.B != cRef.B ) )
                                {
                                    if ( x < xMin ) xMin = x - 1;
                                    if ( y < yMin ) yMin = y - 1;
                                    content = true;
                                }
                                else if ( content && ( c.R != cRef.R || c.G != cRef.G || c.B != cRef.B ) )
                                {
                                    if ( x > xMax ) xMax = x + 1;
                                    if ( y > yMax ) yMax = y + 1;
                                }
                                break;
                        }
                    }
                }
                #endregion
            }

            //从内存解锁Bitmap
            lockbmp.UnlockBits();

            #region adjust bound size
            xMin = xMin < 0 ? 0 : xMin;
            xMax = xMax >= src.Width ? src.Width - 1 : xMax;
            yMin = yMin < 0 ? 0 : yMin;
            yMax = yMax >= src.Height ? src.Height - 1 : yMax;
            result.X = xMin;
            result.Y = yMin;
            result.Width = ( xMax - xMin + 1 ) < 0 ? src.Width : xMax - xMin + 1;
            result.Height = ( yMax - yMin + 1 ) < 0 ? src.Height : yMax - yMin + 1;
            #endregion

            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="mode"></param>
        /// <param name="fastmode"></param>
        /// <returns></returns>
        static public Rectangle ContentBound( this Image src, ContentMaskMode mode = ContentMaskMode.Alpha, bool fastmode = true )
        {
            return ( ContentBound( src as Bitmap, mode, fastmode ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="pos"></param>
        /// <param name="ca"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        static public bool GetPosOfRegion( this RectangleF region, PointF pos, out CornerRegionType ca, out Cursor cursor )
        {
            bool result = false;
            ca = CornerRegionType.None;
            cursor = Cursors.Default;

            float ratio = 8f;
            PointF pN = pos;

            if ( region.Width > 0 && region.Height > 0 )
            {
                CornerRegion cr = new CornerRegion(region, ratio);
                ca = cr.GetRegion( pN );
                switch ( ca )
                {
                    case CornerRegionType.TopLeft:
                        cursor = Cursors.SizeNWSE;
                        result = true;
                        break;
                    case CornerRegionType.TopCenter:
                        cursor = Cursors.SizeNS;
                        result = true;
                        break;
                    case CornerRegionType.TopRight:
                        cursor = Cursors.SizeNESW;
                        result = true;
                        break;
                    case CornerRegionType.MiddleLeft:
                        cursor = Cursors.SizeWE;
                        result = true;
                        break;
                    case CornerRegionType.MiddleCenter:
                        cursor = Cursors.SizeAll;
                        result = true;
                        break;
                    case CornerRegionType.MiddleRight:
                        cursor = Cursors.SizeWE;
                        result = true;
                        break;
                    case CornerRegionType.BottomLeft:
                        cursor = Cursors.SizeNESW;
                        result = true;
                        break;
                    case CornerRegionType.BottomCenter:
                        cursor = Cursors.SizeNS;
                        result = true;
                        break;
                    case CornerRegionType.BottomRight:
                        cursor = Cursors.SizeNWSE;
                        result = true;
                        break;
                    default:
                        cursor = Cursors.Default;
                        result = false;
                        break;
                }
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="src"></param>
        static public void CloneExif( this Image dst, Image src )
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
        static public void AutoRotate( this Image image )
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
        static public Image Invert( this Image image, bool InPlace = false )
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

                //g.SmoothingMode = SmoothingMode.HighQuality;
                //g.PixelOffsetMode = PixelOffsetMode.Half;
                //g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextContrast = 2;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                g.DrawImage( image,
                             new Rectangle( 0, 0, image.Width, image.Height ),
                             0, 0, image.Width, image.Height,
                             GraphicsUnit.Pixel,
                             a );
            }
            return ( dst );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="srcRect"></param>
        /// <returns></returns>
        static public Image Crop( this Image src, Rectangle srcRect )
        {
            var result = new Bitmap( srcRect.Width, srcRect.Height, src.PixelFormat );
            using ( var g = Graphics.FromImage( result ) )
            {
                g.DrawImage( src, 0, 0, srcRect, GraphicsUnit.Pixel );
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="newSize"></param>
        /// <returns></returns>
        static public Image Resize( this Image src, Size newSize )
        {
            var result = new Bitmap( newSize.Width, newSize.Height, src.PixelFormat );
            using ( var g = Graphics.FromImage( result ) )
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextContrast = 2;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                var srcRect = new Rectangle(0, 0, src.Width, src.Height);
                var dstRect = new Rectangle(0, 0, newSize.Width, newSize.Height);
                g.DrawImage( src, dstRect, srcRect, GraphicsUnit.Pixel );
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        static public Image Resize( this Image src, int width, int height )
        {
            return ( Resize( src, new Size( width, height ) ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        static public Image Resize( this Image src, float factor )
        {
            int w = (int)Math.Round(src.Width * factor);
            int h = (int)Math.Round(src.Height * factor);
            return ( Resize( src, new Size( w, h ) ) );
        }

        #endregion

        #region Image Effect
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="opacity"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        static public Bitmap Shadow( this Bitmap src, Color color, int width, double opacity = 0.6f, double angle = 315 )
        {
            Bitmap result = new Bitmap(src);
            #region Get DPI 
            float dpiX = 96f;
            float dpiY = 96f;

            using ( Graphics g = Graphics.FromHwnd( IntPtr.Zero ) )
            {
                dpiX = g.DpiX;
                dpiY = g.DpiY;
            }
            #endregion

            int offset = 4*width;

            #region Create Effect
            var effect = new Media.Effects.DropShadowEffect();
            effect.BlurRadius = width;
            effect.Color = color.ToMediaColor();
            effect.Direction = angle;
            effect.Opacity = opacity;
            effect.ShadowDepth = width;

            //var effect = new Media.Effects.BlurEffect();
            //effect.Radius = 50;
            #endregion

            #region Draw source bitmap to DrawingVisual
            Media.DrawingVisual drawingVisual = new Media.DrawingVisual();
            drawingVisual.Effect = effect;
            using ( var drawingContext = drawingVisual.RenderOpen() )
            {
                //drawingContext.PushEffect( new Media.Effects.BlurBitmapEffect(), null );
                System.Windows.Rect dRect = new System.Windows.Rect(2*width, 2*width, src.Width, src.Height);
                drawingContext.DrawImage( src.ToBitmapSource(), dRect );
                drawingContext.Close();
            }
            #endregion

            #region Render the DrawingVisual into a RenderTargetBitmap 
            var rtb = new Media.Imaging.RenderTargetBitmap(
              src.Width + offset, src.Height + offset,
              dpiX, dpiY,
              Media.PixelFormats.Pbgra32 );
            rtb.Render( drawingVisual );
            #endregion

            #region Create a System.Drawing.Bitmap 
            var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppArgb );
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

            #region Crop Transparent Area
            var srcRect = bitmap.ContentBound();
            result = bitmap.Crop( srcRect ) as Bitmap;

            //result =  new Bitmap(srcRect.Width, srcRect.Height, bitmap.PixelFormat);
            //using ( var g = Graphics.FromImage( result ) )
            //{
            //    g.DrawImage( bitmap, 0, 0, srcRect, GraphicsUnit.Pixel );
            //}
            #endregion

            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        static public Bitmap Blur( this Bitmap src, double radius = 20 )
        {
            Bitmap result = new Bitmap(src);
            #region Get DPI 
            float dpiX = 96f;
            float dpiY = 96f;

            using ( Graphics g = Graphics.FromHwnd( IntPtr.Zero ) )
            {
                dpiX = g.DpiX;
                dpiY = g.DpiY;
            }
            #endregion

            int width = (int)Math.Ceiling(radius);
            int offset = 4*width;

            #region Create Effect
            var effect = new Media.Effects.BlurEffect();
            effect.Radius = radius;
            #endregion

            #region Draw source bitmap to DrawingVisual
            Media.DrawingVisual drawingVisual = new Media.DrawingVisual();
            drawingVisual.Effect = effect;
            using ( var drawingContext = drawingVisual.RenderOpen() )
            {
                //drawingContext.PushEffect( new Media.Effects.BlurBitmapEffect(), null );
                System.Windows.Rect dRect = new System.Windows.Rect(2*width, 2*width, src.Width, src.Height);
                drawingContext.DrawImage( src.ToBitmapSource(), dRect );
                drawingContext.Close();
            }
            #endregion

            #region Render the DrawingVisual into a RenderTargetBitmap 
            var rtb = new Media.Imaging.RenderTargetBitmap(
              src.Width + offset, src.Height + offset,
              dpiX, dpiY,
              Media.PixelFormats.Pbgra32 );
            rtb.Render( drawingVisual );
            #endregion

            #region Create a System.Drawing.Bitmap 
            var bitmap = new Bitmap( rtb.PixelWidth, rtb.PixelHeight, PixelFormat.Format32bppArgb );
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

            #region Crop Transparent Area
            result = bitmap.Crop( bitmap.ContentBound() ) as Bitmap;
            //var srcRect = bitmap.ContentBound();
            //result = new Bitmap( srcRect.Width, srcRect.Height, bitmap.PixelFormat );
            //using ( var g = Graphics.FromImage( result ) )
            //{
            //    g.DrawImage( bitmap, 0, 0, srcRect, GraphicsUnit.Pixel );
            //}
            #endregion

            return ( result );
        }
        #endregion

        #endregion

    }
}

