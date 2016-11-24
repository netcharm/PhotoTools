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
    public partial class FontDialogEx : Form
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
                var font = new Font(curFamilyName, FontSize, FontStyle);
                return ( _font );
            }
            set
            {
                _font = value;
                var item = lvFamily.FindItemWithText( _font.FontFamily.Name );
                lvFamily.SelectedIndices.Clear();
                if ( item is ListViewItem)
                {
                    item.Selected = true;
                }
                else if( lvFamily.Items.Count>0)
                {
                    lvFamily.SelectedIndices.Add( 0 );
                }

            }
        }

        private bool _isTTF = true;
        public bool IsTTF
        {
            get
            {
                if ( string.IsNullOrEmpty( curFamilyName ) )
                    _isTTF = false;
                else
                {
                    var font = new Font(curFamilyName, 12);
                    _isTTF = string.Equals( font.Name, curFamilyName, StringComparison.CurrentCultureIgnoreCase ) ? true : false;
                }
                return ( _isTTF );
            }
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
            get
            {
                var styles = edStyle.Text.Split();
                _typefaceName = string.Empty;
                var typefacename = new List<string>();
                foreach(var style in styles)
                {
                    if ( FontStyleList.ContainsKey( style ) )
                        typefacename.Add( FontStyleList[style] );
                    else
                        typefacename.Add( style );
                }
                _typefaceName = string.Join( " ", typefacename );
                return ( _typefaceName );
            }
            set { _typefaceName = value; }
        }

        private FontStyle _fontstyle = FontStyle.Regular;
        public FontStyle FontStyle
        {
            get
            {
                var styles = edStyle.Text.Split();
                _fontstyle = FontStyle.Regular;
                bool regular = false;
                foreach (var style in styles)
                {
                    var name = style;
                    if ( FontStyleList.ContainsKey( style ) )
                        name = FontStyleList[style];
                    if ( string.Equals( name, "Bold" ) )
                        _fontstyle |= FontStyle.Bold;
                    else if ( string.Equals( name, "Italic" ) )
                        _fontstyle |= FontStyle.Italic;
                    else if ( string.Equals( name, "Regular" ) )
                        regular = true;
                }
                if(!regular) _fontstyle &= ~FontStyle.Regular;

                if ( chkEffectUnderline.Checked ) _fontstyle |= FontStyle.Underline;
                if ( chkEffectStrikeout.Checked ) _fontstyle |= FontStyle.Strikeout;

                return ( _fontstyle );
            }
            set
            {
                chkEffectUnderline.Checked = value.HasFlag( FontStyle.Underline ) ? true : false;
                chkEffectStrikeout.Checked = value.HasFlag( FontStyle.Strikeout ) ? true : false;

                _fontstyle = value;
            }
        }

        private double _fontsize = 12;
        public float FontSize
        {
            get
            {                
                _fontsize = FontSizeList.ContainsKey( edSize.Text) ? FontSizeList[edSize.Text] : 12f;
                return ( (float) _fontsize );
            }
            set { _fontsize = (double) value; }
        }

        private Color _fontcolor = Color.Black;
        public Color FontColor
        {
            get
            {
                _fontcolor = colorGrid.Color;
                return ( _fontcolor );
            }
            set
            {
                _fontcolor = value;
                colorGrid.Color = value;
            }
        }

        public bool IsLoading { get; private set; }

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

        private Dictionary< string, string> FontStyleList = new Dictionary<string, string>();
        private Dictionary<string, float> FontSizeList = new Dictionary<string, float>();
        private Dictionary<string, Media.Typeface> TypefaceList = new Dictionary<string, Media.Typeface>();
        private Dictionary<string, TypefaceInfo> TypefaceInfoList = new Dictionary<string, TypefaceInfo>();

        private Media.FontFamily curFamily = Media.Fonts.SystemFontFamilies.FirstOrDefault();
        private string curFamilyName = SystemFonts.DefaultFont.FontFamily.Name;
        private Media.Typeface curFace = null;
        private string curFaceName = "";
        private float curSize = 12;

        private void Preview()
        {
            if ( IsLoading ) return;
            if ( curFamily == null ) return;
            if ( curFace == null ) return;

            List<string> style = new List<string>();
            style.AddRange( TypeFaceName.Split() );
            if ( chkEffectUnderline.Checked )
                style.Add( "Underline" );
            if ( chkEffectStrikeout.Checked )
                style.Add( "Strikeout" );

            //foreach(var f in Media.Fonts.SystemFontFamilies)
            //{
            //    f.FamilyTypefaces[0]
            //}
            //var key = $"{curFamilyName}-{string.Join(" ", style)}";
            //if( TypefaceInfoList.ContainsKey(key) )
            //{
            //    var value = TypefaceInfoList[key].FaceNameLong;
            //    var face = Media.Fonts.SystemTypefaces.Where( o => { return($"{o.FontFamily}-{o.FaceNames[locale_enkey]}" == value); } );
            //}
            //else
            //{
            //    var face = Media.Fonts.SystemTypefaces.Where( o => { return($"{o.FontFamily}-{o.FaceNames[locale_enkey]}" == key); } );
            //}

            string text = "AaBbYyZz";
            string text_ui = this._("Microsoft Text Test");
            if ( curFamily.FamilyNames.ContainsKey( locale_uikey ) )
            {
                picPreview.Image = text_ui.ToBitmap( curFamilyName, string.Join(" ", style), FontSize, FontColor, Color.Transparent );
            }
            else
            {
                picPreview.Image = text.ToBitmap( curFamilyName, string.Join( " ", style ), FontSize, FontColor, Color.Transparent );
            }
        }

        public FontDialogEx()
        {
            InitializeComponent();
            this.Translate();

            locale_enkey = XmlLanguage.GetLanguage( locale_en );
            locale_uikey = XmlLanguage.GetLanguage( locale_ui );

            #region Add Face locale Dict
            FontStyleList.Clear();
            FontStyleList[this._( "250" )] = "Thin";
            FontStyleList[this._( "350" )] = "Regular";
            FontStyleList[this._( "Light" )] = "Light";
            FontStyleList[this._( "Regular" )] = "Regular";
            FontStyleList[this._( "Medium" )] = "Medium";
            FontStyleList[this._( "Bold" )] = "Bold";
            FontStyleList[this._( "Black" )] = "Black";
            FontStyleList[this._( "Oblique" )] = "Oblique";
            FontStyleList[this._( "Italic" )] = "Italic";
            #endregion

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
            IsLoading = true;

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

            #region Fetch All Typeface List
            //TypefaceList.Clear();
            //foreach(var tf in Media.Fonts.SystemTypefaces)
            //{
            //    var family = tf.FontFamily.FamilyNames[locale_enkey];
            //    var face = tf.FaceNames[locale_enkey];
            //    TypefaceList[$"{family}-{face}"] = tf;
            //}
            //TypefaceList = Media.Fonts.SystemTypefaces.AsParallel().ToDictionary( o => $"{o.FontFamily.FamilyNames[locale_enkey]}-{o.FaceNames[locale_enkey]}", o => o );

            //TypefaceList = (Dictionary < string, Media.Typeface > )Media.Fonts.SystemTypefaces.Select( face => new { Key = $"{face.FontFamily.FamilyNames[locale_enkey]}-{face.FaceNames[locale_enkey]}", Value = face } );
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

            #region Init Font Color
            colorGrid.Color = FontColor;
            #endregion

            #region Init Font Size List
            //lbSize.Items.Clear();
            lbSize.SelectedIndex = 0;
            #endregion
            IsLoading = false;
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
                        }
                        if ( string.Equals( font.Name, familyName, StringComparison.CurrentCultureIgnoreCase ) )
                        {
                            sample = familyName.ToBitmap( font, fgColor );
                        }
                        else
                        {
                            Media.Typeface face = new Media.Typeface( (Media.FontFamily) family.Tag,
                                        System.Windows.FontStyles.Normal,
                                        System.Windows.FontWeights.Normal,
                                        System.Windows.FontStretches.Normal );
                            sample = familyName.ToBitmap( face, 16, fgColor );
                        }
                        //Media.Typeface face = new Media.Typeface( (Media.FontFamily) family.Tag,
                        //            System.Windows.FontStyles.Normal,
                        //            System.Windows.FontWeights.Normal,
                        //            System.Windows.FontStretches.Normal );
                        //sample = familyName.ToBitmap( face, 16, fgColor );
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
                    else if ( e.Item.Selected )
                    {
                        g.FillRectangle( new SolidBrush( Color.BlueViolet ), bgRect );
                        g.DrawImage( sample.Invert(), fgRect );
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

                lvStyle.Items.Clear();
                foreach ( var typeface in ff.FamilyTypefaces )
                {
                    foreach ( var kv in typeface.AdjustedFaceNames )
                    {
                        var facename = kv.Value.Replace( "250", "Thin" ).Replace( "350", "Regular" );
                        var item = new ListViewItem(kv.Value);
                        item.Text = facename._();
                        item.Tag = facename;
                        //item.Tag = new Media.Typeface( curFamily, typeface.Style, typeface.Weight, typeface.Stretch );
                        lvStyle.Items.Add( item );
                    }
                }
                if ( lvStyle.Items.Count > 0 )
                {
                    lvStyle.Items[0].Selected = true;
                    edStyle.Text = lvStyle.Items[0].Text;
                    //curFace = (Media.Typeface) lvStyle.Items[0].Tag;
                    //curFaceName = ff.FamilyTypefaces[0].AdjustedFaceNames[locale_enkey];
                    curFaceName = (string) lvStyle.Items[0].Tag;
                }
                //else
                //    curFace = null;
                #endregion

                #region Add family sizes

                #endregion

                #region Add family charsets
                cbCharset.Items.Clear();

                #endregion
                Preview();
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

                    //Bitmap sample = "[Unspported Font]".ToBitmap( new Font("Arial", 14), fgColor );
                    Bitmap sample = e.Item.Text.ToBitmap( curFamilyName, (string)e.Item.Tag, 14, fgColor);

                    //var face = (Media.Typeface)e.Item.Tag;

                    //#region First try using GDI Plus
                    //var locale = XmlLanguage.GetLanguage("en-us");
                    //if ( face.FontFamily.FamilyNames.ContainsKey( locale_uikey ) )
                    //    locale = locale_uikey;
                    //var fontname = face.FontFamily.FamilyNames[locale];
                    //var fontstyle = FontStyle.Regular;
                    //var fontstyle_ = face.Style;
                    //var facename = face.FaceNames[locale_enkey];
                    //if ( face.Weight == System.Windows.FontWeights.Bold )
                    //    fontstyle |= FontStyle.Bold;
                    //if ( face.Style == System.Windows.FontStyles.Italic || face.Style == System.Windows.FontStyles.Oblique )
                    //    fontstyle |= FontStyle.Italic;
                    //Font font = new Font(fontname, 16, fontstyle);
                    //#endregion

                    //if (string.Equals(font.Name, fontname, StringComparison.CurrentCultureIgnoreCase ))
                    //{
                    //    sample = e.Item.Text.ToBitmap( font, fgColor );
                    //}
                    //else
                    //{
                    //    sample = e.Item.Text.ToBitmap( face, 16, fgColor );
                    //    if ( sample.Width == 1 || sample.Height == 1 )
                    //    {
                    //        sample = e.Item.Text.ToBitmap( font, fgColor );
                    //    }
                    //}

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
                    else if ( e.Item.Selected )
                    {
                        g.FillRectangle( new SolidBrush( Color.BlueViolet ), bgRect );
                        g.DrawImage( sample.Invert(), fgRect );
                    }
                    else
                    {
                        g.FillRectangle( new SolidBrush( e.Item.BackColor ), bgRect );
                        g.DrawImage( sample, fgRect );
                    }
                }
            }
        }

        private void lvStyle_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( lvStyle.FocusedItem is ListViewItem)
            {
                curFaceName = (string)lvStyle.FocusedItem.Tag;

                //curFace = face;
                edStyle.Text = this._( curFaceName );

                //if ( curFace.Style == System.Windows.FontStyles.Italic )
                //    _fontstyle |= FontStyle.Italic;
                //if ( curFace.Weight == System.Windows.FontWeights.Bold )
                //    _fontstyle |= FontStyle.Bold;

                Preview();
            }
        }

        private void lbSize_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( lbSize.SelectedItem != null)
                edSize.Text = (string) lbSize.SelectedItem;
            FontSize = FontSizeList[edSize.Text];
            Preview();
        }

        private void colorGrid_ColorChanged( object sender, EventArgs e )
        {
            _fontcolor = colorGrid.Color;
            Preview();
        }

        private void chkEffectUnderline_CheckedChanged( object sender, EventArgs e )
        {
            if ( chkEffectUnderline.Checked ) _fontstyle &= ~FontStyle.Underline;
            if ( chkEffectStrikeout.Checked ) _fontstyle &= ~FontStyle.Strikeout;

            Preview();
        }
    }

    internal class TypefaceInfo
    {
        internal string FamilyName;
        internal string FaceName;
        internal string AdjustFaceName;
        internal string FaceNameLong;
    }

}
