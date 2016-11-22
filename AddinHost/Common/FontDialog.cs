using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Markup;
using ExtensionMethods;

using Media = System.Windows.Media;

namespace NetCharm.Common
{
    public partial class FontDialog : Form
    {
        public event EventHandler Apply;

        public bool ShowApply
        {
            get { return ( btnApply.Visible ); }
            set { btnApply.Visible = value; }
        }

        private Font _font = SystemFonts.DefaultFont;
        public override Font Font
        {
            get
            {
                //_font = 
                return ( _font );
            }
            set
            {
                _font = value;
            }
        }

        private bool _isTTF = true;
        public bool IsTTF
        {
            get { return ( _isTTF ); }
        }

        private string _familyName = "";
        public string FamilyName
        {
            get { return ( _familyName ); }
            set { _familyName = value; }
        }

        private string _typefaceName = "";
        public string TypeFaceName
        {
            get { return ( _typefaceName ); }
            set { _typefaceName = value; }
        }

        //private FontStyle _style = FontStyle.Regular;
        //public FontStyle Style
        //{
        //    get { return ( _style ); }
        //    set { _style = value; }
        //}

        //private double _size = 10;
        //public float Size
        //{
        //    get { return ( (float) _size ); }
        //    set { _size = (double) value; }
        //}

        private Color _color = default(Color);
        public Color Color
        {
            get
            {
                _color = colorGrid.Color;
                return ( _color );
            }
            set
            {
                _color = value;
                colorGrid.Color = value;
            }
        }

        private string locale_en = "en-us";
        private string locale_ui = CultureInfo.InstalledUICulture.Name.ToLower();
        private CultureInfo locale_eninfo = CultureInfo.CurrentCulture;
        private CultureInfo locale_uiinfo = CultureInfo.InstalledUICulture;
        private XmlLanguage locale_enkey = null;
        private XmlLanguage locale_uikey = null;

        private List<ListViewItem> families = new List<ListViewItem>();
        private Dictionary<string, Bitmap> familiySamples = new Dictionary<string, Bitmap>();

        private float[] FontSizeList_En = new float[]
        {
            8f, 9f, 10f, 11f, 12f, 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 36f, 48f, 72f
        };

        private Dictionary<string, float> FontSizeList = new Dictionary<string, float>();

        private Media.FontFamily curFamily = Media.Fonts.SystemFontFamilies.FirstOrDefault();
        private string curFamilyName = SystemFonts.DefaultFont.FontFamily.Name;
        private string curFaceName = "";
        private float curSize = 12;

        public FontDialog()
        {
            InitializeComponent();
            this.Translate();

            locale_enkey = XmlLanguage.GetLanguage( locale_en );
            locale_uikey = XmlLanguage.GetLanguage( locale_ui );

            #region Add font sizes
            if ( locale_uiinfo.TwoLetterISOLanguageName.ToLower().StartsWith( "zh" ) )
            {
                FontSizeList["初号"] = 42f;
                FontSizeList["小初"] = 36f;
                FontSizeList["一号"] = 26f;
                FontSizeList["小一"] = 24f;
                FontSizeList["二号"] = 22f;
                FontSizeList["小二"] = 18f;
                FontSizeList["三号"] = 16f;
                FontSizeList["小三"] = 15f;
                FontSizeList["四号"] = 14f;
                FontSizeList["小四"] = 12f;
                FontSizeList["五号"] = 10.5f;
                FontSizeList["小五"] = 9f;
                FontSizeList["六号"] = 7.5f;
                FontSizeList["小六"] = 6.5f;
                FontSizeList["七号"] = 5.5f;
                FontSizeList["八号"] = 5;
            }
            foreach(var size in FontSizeList_En)
            {
                FontSizeList[size.ToString()] = size;
            }
            lbSize.DataSource = FontSizeList.Keys.ToList();
            #endregion

        }

        private static int CompareItemText( ListViewItem x, ListViewItem y )
        {
            if ( x == null )
            {
                if ( y == null )
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if ( y == null )
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    return ( x.Text.CompareTo( y.Text ) );
                }
            }
        }

        private void FontDialog_Load( object sender, EventArgs e )
        {
            #region Fetch Font List
            families.Clear();
            foreach ( var ff in Media.Fonts.SystemFontFamilies )
            {
                var familyName = ff.FamilyNames.ContainsKey( locale_uikey ) ? ff.FamilyNames[locale_uikey] : ff.FamilyNames[locale_enkey];

                var item = new ListViewItem(familyName);
                item.Text = familyName;
                item.Tag = ff;
                families.Add( item );
            }
            families.Sort( CompareItemText );
            #endregion

            #region Init Font Family List
            lvFamily.Clear();
            if ( lvFamily.Columns.Count==0)
            {
                lvFamily.Columns.Add( "Font Family" );
                lvFamily.Columns[0].Width = lvFamily.ClientSize.Width;
            }
            lvFamily.View = View.Details;
            lvFamily.VirtualListSize = families.Count;
            lvFamily.VirtualMode = true;
            #endregion

            #region Init Font Style List
            lvStyle.Items.Clear();
            if ( lvStyle.Columns.Count == 0 )
            {
                lvStyle.Columns.Add( "Font Style" );
                lvStyle.Columns[0].Width = lvStyle.ClientSize.Width;
            }
            lvStyle.View = View.Details;
            #endregion

            #region Init Font Size List
            //lbSize.Items.Clear();
            #endregion
        }

        private void lvFamily_DrawItem( object sender, DrawListViewItemEventArgs e )
        {
            if(e.ItemIndex>=0 && e.ItemIndex<lvFamily.VirtualListSize)
            {
                var family = families[e.ItemIndex];
                var familyName = family.Text;

                using ( var g = e.Graphics )
                {
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextContrast = 2;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    Bitmap sample = null;
                    Color fgColor = Color.Black;
                    Color bgColor = Color.White;
                    if ( familiySamples.ContainsKey( familyName ) )
                    {
                        sample = familiySamples[familyName];
                    }
                    else
                    {
                        fgColor = Color.Black;
                        bgColor = Color.AntiqueWhite;

                        Font font = SystemFonts.DefaultFont;
                        try
                        {
                            //fgColor = Color.Black;
                            //bgColor = Color.AntiqueWhite;

                            float emSize = g.DpiY * 16 / 72f;
                            font = new Font( familyName, emSize );

                            //family.BackColor = bgColor;
                        }
                        catch ( Exception )
                        {
                            //fgColor = Color.LightGray;
                            //bgColor = Color.LightSalmon;

                            //family.BackColor = bgColor;
                        }
                        //if(string.Equals(font.Name, familyName, StringComparison.CurrentCultureIgnoreCase))
                        //{
                        //    sample = familyName.ToBitmap( font, fgColor );
                        //}
                        //else
                        //{
                        //    Media.Typeface face = new Media.Typeface( (Media.FontFamily) family.Tag,
                        //                System.Windows.FontStyles.Normal,
                        //                System.Windows.FontWeights.Normal,
                        //                System.Windows.FontStretches.Normal );
                        //    sample = familyName.ToBitmap( face, 16, fgColor );
                        //}
                        Media.Typeface face = new Media.Typeface( (Media.FontFamily) family.Tag,
                                    System.Windows.FontStyles.Normal,
                                    System.Windows.FontWeights.Normal,
                                    System.Windows.FontStretches.Normal );
                        sample = familyName.ToBitmap( face, 16, fgColor );
                        if( sample.Width == 1 || sample.Height == 1)
                            sample = familyName.ToBitmap( font, fgColor );
                        familiySamples[familyName] = sample;
                    }

                    RectangleF fgRect = new RectangleF(e.Bounds.X+2, e.Bounds.Y+2, sample.Width-4, e.Bounds.Height-4);
                    if ( sample.Height > fgRect.Height )
                    {
                        float factor = (float)sample.Width / sample.Height;
                        fgRect.Width = fgRect.Height * factor;
                    }

                    RectangleF bgRect = new RectangleF(e.Bounds.X+3, e.Bounds.Y+1, e.Bounds.Width-3, e.Bounds.Height-2);

                    if ( e.State.HasFlag( ListViewItemStates.Focused ) )
                    {
                        g.FillRectangle( new SolidBrush( Color.BlueViolet ), bgRect );
                        //g.DrawImage( sample, fgRect );
                        g.DrawImage( sample.Invert(), fgRect );
                        e.DrawFocusRectangle();
                    }
                    else
                    {
                        g.FillRectangle( new SolidBrush( family.BackColor ), bgRect );
                        g.DrawImage( sample, fgRect );
                    }
                }
            }
        }

        private void lvFamily_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            if ( e.ItemIndex >= 0 && e.ItemIndex < lvFamily.VirtualListSize )
            {
                System.Drawing.FontFamily family = System.Drawing.FontFamily.Families[e.ItemIndex];                   
                e.Item = families[e.ItemIndex];
            }
        }

        private void lvFamily_SelectedIndexChanged( object sender, EventArgs e )
        {
            if(lvFamily.FocusedItem is ListViewItem)
            {
                #region Add family supported styles

                int idx = lvFamily.FocusedItem.Index;

                var ff = (Media.FontFamily)families[idx].Tag;

                curFamily = ff;
                curFamilyName = ff.FamilyNames.ContainsKey( locale_uikey ) ? ff.FamilyNames[locale_uikey] : ff.FamilyNames[locale_enkey];

                //var typefaces = ff.GetTypefaces();
                lvStyle.Items.Clear();
                foreach ( var typeface in ff.FamilyTypefaces )
                {
                    foreach ( var kv in typeface.AdjustedFaceNames )
                    {
                        var item = new ListViewItem(kv.Value);
                        item.Text = kv.Value;
                        item.Tag = new Media.Typeface( curFamily, typeface.Style, typeface.Weight, typeface.Stretch );
                        lvStyle.Items.Add( item );
                    }
                }

                //System.Drawing.FontFamily family = System.Drawing.FontFamily.Families[idx];

                //lbStyle.Items.Clear();

                //if (family.IsStyleAvailable(FontStyle.Underline))
                //    chkEffectUnderline.Enabled = true;
                //else
                //    chkEffectUnderline.Enabled = false;

                //if ( family.IsStyleAvailable( FontStyle.Strikeout ) )
                //    chkEffectStrikeout.Enabled = true;
                //else
                //    chkEffectStrikeout.Enabled = false;

                //if ( family.IsStyleAvailable( FontStyle.Regular ) )
                //    lbStyle.Items.Add( this._("Regular") );
                //if ( family.IsStyleAvailable( FontStyle.Italic ) )
                //    lbStyle.Items.Add( this._( "Italic" ) );
                //if ( family.IsStyleAvailable( FontStyle.Bold ) )
                //    lbStyle.Items.Add( this._( "Bold" ) );

                #endregion

                #region Add family sizes

                #endregion

                #region Add family charsets
                cbCharset.Items.Clear();


                #endregion

            }
        }

        private void lvStyle_DrawItem( object sender, DrawListViewItemEventArgs e )
        {
            if ( e.ItemIndex >= 0 && e.ItemIndex < lvFamily.VirtualListSize )
            {
                using ( var g = e.Graphics )
                {
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextContrast = 2;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    float dpiX = g.DpiX;
                    float dpiY = g.DpiY;

                    Color fgColor = Color.Black;
                    Color bgColor = Color.AntiqueWhite;

                    var face = (Media.Typeface)e.Item.Tag;
                    Bitmap sample = e.Item.Text.ToBitmap( face, 16, fgColor );
                    if ( sample.Width == 1 || sample.Height == 1 )
                    {
                        //Font font = new Font(face.FaceNames[XmlLanguage.GetLanguage("en-us")], 16);
                        var locale = XmlLanguage.GetLanguage("en-us");
                        var fontname = face.FontFamily.FamilyNames[locale];
                        var fontstyle = FontStyle.Regular;
                        var fontstyle_ = face.Style;
                        var facename = face.FaceNames[locale];
                        if(face.Weight == System.Windows.FontWeights.Bold)
                            fontstyle |= FontStyle.Bold;
                        if( face.Style == System.Windows.FontStyles.Italic || face.Style == System.Windows.FontStyles.Oblique )
                            fontstyle |= FontStyle.Italic;
                        //if ( face.Style == System.Windows.TextDecorations.Strikethrough )
                        //    fontstyle |= FontStyle.Italic;

                        //if(string.Equals(facename, "Regular", StringComparison.CurrentCultureIgnoreCase))
                        //{
                        //    fontstyle |= FontStyle.Regular;
                        //}
                        //if ( string.Equals( facename, "Italic", StringComparison.CurrentCultureIgnoreCase ) )
                        //{
                        //    fontstyle |= FontStyle.Italic;
                        //}
                        //if ( string.Equals( facename, "Bold", StringComparison.CurrentCultureIgnoreCase ) )
                        //{
                        //    fontstyle |= FontStyle.Bold;
                        //}
                        //if ( string.Equals( facename, "Strikeout", StringComparison.CurrentCultureIgnoreCase ) )
                        //{
                        //    fontstyle |= FontStyle.Strikeout;
                        //}
                        //if ( string.Equals( facename, "Underline", StringComparison.CurrentCultureIgnoreCase ) )
                        //{
                        //    fontstyle |= FontStyle.Underline;
                        //}

                        Font font = new Font(fontname, 16, fontstyle);
                        sample = e.Item.Text.ToBitmap( font, fgColor );
                    }

                    RectangleF fgRect = new RectangleF(e.Bounds.X+2, e.Bounds.Y+2, sample.Width-4, e.Bounds.Height-4);
                    if ( sample.Height > fgRect.Height )
                    {
                        float factor = (float)sample.Width / sample.Height;
                        fgRect.Width = fgRect.Height * factor;
                    }

                    RectangleF bgRect = new RectangleF(e.Bounds.X+3, e.Bounds.Y+1, e.Bounds.Width-3, e.Bounds.Height-2);

                    if ( e.State.HasFlag( ListViewItemStates.Focused ) )
                    {
                        g.FillRectangle( new SolidBrush( Color.BlueViolet ), bgRect );
                        //g.DrawImage( sample, fgRect );
                        g.DrawImage( sample.Invert(), fgRect );
                        e.DrawFocusRectangle();
                    }
                    else
                    {
                        g.FillRectangle( new SolidBrush( bgColor ), bgRect );
                        g.DrawImage( sample, fgRect );
                    }
                }
            }

            //float emSize = dpiY * 16 / 72f;
            //FontStyle style = FontStyle.Regular;

            ////curFamily.FamilyMaps

            //_font = new Font( curFamilyName, emSize, style );

            ////option 1
            //Media.FontFamily mfont = new Media.FontFamily(_font.Name);
            ////option 2 does the same thing
            //Media.FontFamilyConverter conv = new Media.FontFamilyConverter();
            //Media.FontFamily mfont1 = conv.ConvertFromString(_font.Name) as Media.FontFamily;
            //conv.ConvertToInvariantString( curFamily );
            ////option 3
            //Media.FontFamily mfont2 = Media.Fonts.SystemFontFamilies.Where(x => x.Source == _font.Name).FirstOrDefault();

        }

        private void lvStyle_SelectedIndexChanged( object sender, EventArgs e )
        {
            var face = (Media.Typeface)lvStyle.FocusedItem.Tag;
            if ( face.FaceNames.ContainsKey( locale_uikey ) )
                curFaceName = face.FaceNames[locale_uikey];
            else
                curFaceName = face.FaceNames[locale_enkey];

            edStyle.Text = this._( curFaceName );
        }

        private void lbSize_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( lbSize.SelectedItem != null)
                edSize.Text = (string) lbSize.SelectedItem;
        }
    }
}
