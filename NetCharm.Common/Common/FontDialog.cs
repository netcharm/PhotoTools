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
                lbSize.Text = value.Size.ToString();
                _fontsize = value.Size;
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

        private bool _usefont = true;
        public bool UseFont
        {
            get { return ( _usefont ); }
            set { _usefont = value; }
        }

        private string _familyName = "";
        public string FontFamily
        {
            get { return ( _familyName ); }
            set { _familyName = value; }
        }

        private string _typefaceName = "";
        public string FontFace
        {
            get
            {
                if ( edStyle.Tag == null ) return ( "Regular" );
                //var styles = edStyle.Text.Split();
                var styles = ((string)edStyle.Tag).Split();
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
            set
            {
                _fontsize = (double) value;
                edSize.Text = value.ToString();
                lbSize.Text = value.ToString();
            }
        }

        private string _locale = "en-us";
        public string LocaleName
        {
            get
            {
                try
                {
                    List<string> locales = (List<string>)cbCharset.Tag;
                    var idx = cbCharset.SelectedIndex;
                    idx = idx >= cbCharset.Items.Count ? cbCharset.Items.Count - 1 : idx;
                    idx = idx < 0 ? 0 : idx;
                    _locale = locales[idx];
                }
                catch(Exception)
                { }
                return ( _locale );
            }
            set {
                _locale = value;
                cbCharset.Text = CultureInfo.GetCultureInfo( _locale ).DisplayName;
            }
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

        internal bool IsLoading { get; private set; }

        private string locale_en = "en-us";
        private string locale_ui = CultureInfo.InstalledUICulture.Name.ToLower();
        private CultureInfo locale_eninfo = CultureInfo.CurrentCulture;
        private CultureInfo locale_uiinfo = CultureInfo.InstalledUICulture;
        private XmlLanguage locale_enkey = null;
        private XmlLanguage locale_uikey = null;

        private List<ListViewItem> families = new List<ListViewItem>();
        private Dictionary<string, Bitmap> familiySamples = new Dictionary<string, Bitmap>();
        private Dictionary<string, Bitmap> styleSamples = new Dictionary<string, Bitmap>();

        private float[] FontSizeList_En = new float[]
        {
            8f, 9f, 10f, 11f, 12f, 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 36f, 48f, 72f
        };

        private Dictionary< string, string> FontStyleList = new Dictionary<string, string>();
        private Dictionary<string, float> FontSizeList = new Dictionary<string, float>();

        private Media.Typeface curFamily = Media.Fonts.SystemTypefaces.FirstOrDefault();
        private string curFamilyName = SystemFonts.DefaultFont.FontFamily.Name;
        private string curFaceName = "";

        private Dictionary<string, string> LocaleTextList = new Dictionary<string, string>();

        private void Preview()
        {
            if ( IsLoading ) return;
            if ( curFamily == null ) return;
            
            List<string> style = new List<string>();
            style.AddRange( FontFace.Split() );
            if ( chkEffectUnderline.Checked )
                style.Add( "Underline" );
            if ( chkEffectStrikeout.Checked )
                style.Add( "Strikeout" );

            string text = "AaBbYyZz";
            if( LocaleTextList.ContainsKey(LocaleName) )
                text = LocaleTextList[LocaleName];
            picPreview.Image = text.ToBitmap( curFamilyName, string.Join( " ", style ), FontSize, LocaleName, FontColor, Color.Transparent );

            //string text_ui = this._("Culture Text");
            //if ( curFamily.FontFamily.FamilyNames.ContainsKey( locale_uikey ) )
            //{
            //    picPreview.Image = text_ui.ToBitmap( curFamilyName, string.Join(" ", style), FontSize, LocaleName, FontColor, Color.Transparent );
            //}
            //else
            //{
            //    picPreview.Image = text.ToBitmap( curFamilyName, string.Join( " ", style ), FontSize, LocaleName, FontColor, Color.Transparent );
            //}
        }

        private FontStyle GetStyle(string facename, bool underline=false, bool strikeout=false)
        {
            FontStyle result = FontStyle.Regular;

            var regular = false;
            foreach(var style in facename.Split())
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

        private void lvDrawItem( DrawListViewItemEventArgs e, Bitmap src, Color fgColor, Color bgColor )
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

                float aspect = (float)src.Width / src.Height;
                var dstSize = new SizeF((e.Bounds.Height-6)*aspect, e.Bounds.Height - 6);
                if ( src.Height <= dstSize.Height )
                {
                    dstSize.Width = src.Width;
                    dstSize.Height = src.Height;
                }
                RectangleF fgRect = new RectangleF(e.Bounds.X+4, e.Bounds.Y+3, dstSize.Width, dstSize.Height);
                RectangleF bgRect = new RectangleF(e.Bounds.X+3, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                if ( e.State.HasFlag( ListViewItemStates.Focused ) )
                {
                    g.FillRectangle( new SolidBrush( SystemColors.Highlight ), bgRect );
                    //g.DrawImage( sample, fgRect );
                    g.DrawImage( src.Invert(), fgRect );
                    //e.DrawFocusRectangle();
                }
                else if ( e.Item.Selected )
                {
                    g.FillRectangle( new SolidBrush( SystemColors.Highlight ), bgRect );
                    g.DrawImage( src.Invert(), fgRect );
                }
                else
                {
                    g.FillRectangle( new SolidBrush( bgColor ), bgRect );
                    g.DrawImage( src, fgRect );
                }
            }
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

        public FontDialog()
        {
            InitializeComponent();
            this.Translate();
            IsLoading = true;

            locale_enkey = XmlLanguage.GetLanguage( locale_en );
            locale_uikey = XmlLanguage.GetLanguage( locale_ui );

            #region Init Face locale Dict
            FontStyleList.Clear();
            FontStyleList[this._( "Thin" )] = "Thin";
            FontStyleList[this._( "Light" )] = "Light";
            FontStyleList[this._( "Regular" )] = "Regular";
            FontStyleList[this._( "Medium" )] = "Medium";
            FontStyleList[this._( "Bold" )] = "Bold";
            FontStyleList[this._( "Black" )] = "Black";
            FontStyleList[this._( "Oblique" )] = "Oblique";
            FontStyleList[this._( "Italic" )] = "Italic";

            FontStyleList[this._( "SemiBold" )] = "SemiBold";
            FontStyleList[this._( "SemiCondensed" )] = "SemiCondensed";
            FontStyleList[this._( "Condensed" )] = "Condensed";
            FontStyleList[this._( "Expanded" )] = "Expanded";
            FontStyleList[this._( "Extended" )] = "Extended";
            FontStyleList[this._( "Heavy" )] = "Heavy";
            FontStyleList[this._( "ExtraBlack" )] = "Extra Black";
            FontStyleList[this._( "Ultra Light" )] = "Ultra Light";
            FontStyleList[this._( "Ultra" )] = "Ultra";
            FontStyleList[this._( "Roman" )] = "Roman";
            FontStyleList[this._( "Normal" )] = "Regular";
            FontStyleList[this._( "regular" )] = "Regular";

            FontStyleList[this._( "250" )] = "Thin";
            FontStyleList[this._( "350" )] = "Regular";
            FontStyleList[this._( "275" )] = "Ultra Light";
            FontStyleList[this._( "750" )] = "Heavy";

            FontStyleList[this._( "W3" )] = "Light";
            FontStyleList[this._( "W6" )] = "SemiBold";
            //FontStyleList["常规体"] = "Regular";
            #endregion

            #region Init font sizes
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

            #region Init Locale Text for sample
            foreach(var locale in CultureInfo.CurrentUICulture.SystemLocales())
            {
                if ( string.IsNullOrEmpty( locale ) ) continue;
                LocaleTextList[locale.ToLower()] = "AaBbYyZz";
            }
            LocaleTextList["zh"] = "简体中文 Text";
            LocaleTextList["zh-cn"] = "简体中文 Text";
            LocaleTextList["zh-chs"] = "简体中文 Text";
            LocaleTextList["zh-hans"] = "简体中文 Text";
            LocaleTextList["zh-sg"] = "简体中文 Text";
            LocaleTextList["zh-mo"] = "简体中文 Text";

            LocaleTextList["zh-hk"] = "繁體中文 Text";
            LocaleTextList["zh-tw"] = "繁體中文 Text";
            LocaleTextList["zh-cht"] = "繁體中文 Text";
            LocaleTextList["zh-hant"] = "繁體中文 Text";

            LocaleTextList["ja"] = "日本語 亜あぁアァ Text";
            LocaleTextList["ja-jp"] = "日本語 亜あぁアァ Text";

            #endregion
        }

        private void FontDialog_Load( object sender, EventArgs e )
        {
            IsLoading = true;

            #region Fetch All Typefaces
            var FamilyList = ExtensionMethods.NetCharmExtensions.GetFamilies();
            //var FamilyList = ExtensionMethods.NetCharmExtensions.FamilyList;
            //foreach ( var f in Media.Fonts.SystemTypefaces )
            //{
            //    var f_key = f.FontFamily.FamilyNames.ContainsKey(locale_enkey)? f.FontFamily.FamilyNames[locale_enkey] : f.FontFamily.FamilyNames.First().Value;

            //    try
            //    {
            //        Media.GlyphTypeface gtf = null;
            //        f.TryGetGlyphTypeface( out gtf );

            //        var facelist = new Dictionary<string, Media.Typeface>();
            //        if(f.FaceNames.ContainsKey(locale_enkey))
            //            facelist[f.FaceNames[locale_enkey]] = f;
            //        else
            //            facelist[f.FaceNames.First().Value] = f;

            //        if ( f.FaceNames.ContainsKey( locale_uikey ) )
            //            facelist[f.FaceNames[locale_uikey]] = f;

            //        if ( gtf != null )
            //        {
            //            //gtf.Win32FaceNames
            //            if ( gtf.Win32FaceNames.ContainsKey( locale_eninfo ) )
            //                facelist[gtf.Win32FaceNames[locale_eninfo]] = f;
            //            else
            //                facelist[gtf.Win32FaceNames.First().Value] = f;

            //            if ( gtf.Win32FaceNames.ContainsKey( locale_uiinfo ) )
            //                facelist[gtf.Win32FaceNames[locale_uiinfo]] = f;
            //        }

            //        FamilyList[f_key] = facelist;
            //        if ( f.FontFamily.FamilyNames.ContainsKey( locale_enkey ) )
            //            FamilyList[f.FontFamily.FamilyNames[locale_enkey]] = facelist;
            //        else
            //            FamilyList[f.FontFamily.FamilyNames.First().Value] = facelist;

            //        if ( f.FontFamily.FamilyNames.ContainsKey( locale_uikey ) )
            //            FamilyList[f.FontFamily.FamilyNames[locale_uikey]] = facelist;

            //        if ( gtf != null )
            //        {
            //            //gtf.Win32FamilyNames
            //            foreach ( var wfn in gtf.Win32FamilyNames )
            //            {
            //                if ( !FamilyList.ContainsKey( wfn.Value ) )
            //                    FamilyList[wfn.Value] = facelist;
            //            }
            //        }
            //    }
            //    catch ( Exception )
            //    {
            //        FamilyList[f_key] = null;
            //    }
            //}

            //StringBuilder sb = new StringBuilder();
            //foreach(var f in FamilyList )
            //{
            //    sb.AppendLine( f.Key.ToString() );
            //    foreach(var ff in f.Value)
            //    {
            //        sb.AppendLine( $"  {ff.Key}*{ff.Value.ToString()}");
            //    }
            //}
            //System.IO.File.WriteAllText( "typefaces.txt", sb.ToString() );
            #endregion

            #region Make Font List
            families.Clear();
            foreach ( var ff in Media.Fonts.SystemFontFamilies )
            {
                var locale_key = ff.FamilyNames.Keys.First();
                if ( ff.FamilyNames.ContainsKey( locale_enkey ) )
                    locale_key = locale_enkey;
                var familyName = ff.FamilyNames.ContainsKey( locale_uikey ) ? ff.FamilyNames[locale_uikey] : ff.FamilyNames[locale_key];

                var item = new ListViewItem(familyName);
                item.Text = familyName;
                //item.Tag = ff;
                item.Tag = FamilyList[familyName];
                families.Add( item );
            }
            families.Sort( CompareItemText );
            #endregion

            #region Fetch All Typeface List
            //TypefaceList = (Dictionary < string, Media.Typeface > )Media.Fonts.SystemTypefaces.Select( face => new { Key = $"{face.FontFamily.FamilyNames[locale_enkey]}*{face.FaceNames[locale_enkey]}", Value = face } );
            #endregion

            #region Init Font Color
            colorGrid.Color = FontColor;
            #endregion

            #region Init Font Size List
            lbSize.Text = FontSize.ToString();
            #endregion

            #region Init Font Style List
            lvStyle.Items.Clear();
            if ( lvStyle.Columns.Count == 0 )
            {
                lvStyle.Columns.Add( "Font Style" );
                lvStyle.Columns[0].Width = lvStyle.ClientSize.Width;
                //lvStyle.Columns[0].AutoResize( ColumnHeaderAutoResizeStyle.HeaderSize );
            }
            lvStyle.View = View.Details;
            #endregion

            #region Init Font Family List
            lvFamily.Clear();
            if ( lvFamily.Columns.Count==0)
            {
                lvFamily.Columns.Add( "Font Family" );
                lvFamily.Columns[0].Width = lvFamily.ClientSize.Width;
                //lvStyle.Columns[0].AutoResize( ColumnHeaderAutoResizeStyle.ColumnContent );
            }
            lvFamily.View = View.Details;
            lvFamily.VirtualListSize = families.Count;
            lvFamily.VirtualMode = true;
            #endregion

            #region Selected default font 
            int idx = 0;
            if ( string.IsNullOrEmpty( _familyName ) ) _familyName = Font.FontFamily.Name;
            string ffn = UseFont ? Font.Name : _familyName;
            var lvis = families.AsParallel().Where(o => { return ( string.Equals( o.Text, ffn, StringComparison.CurrentCultureIgnoreCase )); } );
            if ( lvis.Count() > 0 )
            {
                ListViewItem lvi = lvis.First();
                idx = families.IndexOf( lvi );
                lvFamily.Select();
                lvFamily.EnsureVisible( idx );
                lvFamily.FocusedItem = lvFamily.FindItemWithText( ffn, false, idx );
                lvFamily.FocusedItem.Selected = true;
            }
            #endregion

            #region Add family supported styles
            var ffc = (Dictionary<string, Media.Typeface>)families[idx].Tag;

            //curFamily = ffc;
//            var ffcn = ffc.FamilyNames.ContainsKey(locale_uikey) ? ffc.FamilyNames[locale_uikey] : ffc.FamilyNames[locale_enkey];
            curFamilyName = lvFamily.FocusedItem is ListViewItem ? lvFamily.FocusedItem.Text : families[idx].Text;
            //curFamilyName = lvFamily.FocusedItem.Text;

            styleSamples.Clear();
            lvStyle.Items.Clear();
            foreach ( var f in ffc)//.Reverse() )
            {
                var facename = f.Key.Replace( "250", "Thin" ).Replace( "350", "Regular" );
                var item = new ListViewItem(f.Key);
                item.Text = string.Join( " ", facename.Split().Select( o => o._() ) );
                item.Tag = facename;
                lvStyle.Items.Add( item );
            }

            if ( lvStyle.Items.Count > 0 )
            {
                if(!UseFont && !string.IsNullOrEmpty(_typefaceName ))
                {
                    var fcns = _typefaceName.Split();
                    var fs = _typefaceName.ToFontStyle();

                    if ( fs.HasFlag( FontStyle.Underline ) )
                        chkEffectUnderline.Checked = true;

                    if ( fs.HasFlag( FontStyle.Strikeout ) )
                        chkEffectStrikeout.Checked = true;

                    List<string> faces = new List<string>();
                    foreach ( var s in fcns)
                    {
                        if(!string.Equals(s, "underline", StringComparison.CurrentCultureIgnoreCase) &&
                            !string.Equals( s, "strikeout", StringComparison.CurrentCultureIgnoreCase) )
                        {
                            faces.Add( s._() );
                        }
                    }
                    curFaceName = string.Join( " ", faces );
                    var lvs = lvStyle.FindItemWithText( curFaceName );
                    if(lvs is ListViewItem)
                    {
                        lvs.Selected = true;
                        edStyle.Text = lvs.Text;
                    }
                    else
                    {
                        lvStyle.Items[0].Selected = true;
                        edStyle.Text = lvStyle.Items[0].Text;
                        curFaceName = (string) lvStyle.Items[0].Tag;
                    }
                }
                else
                {
                    lvStyle.Items[0].Selected = true;
                    edStyle.Text = lvStyle.Items[0].Text;
                    curFaceName = (string) lvStyle.Items[0].Tag;
                }
            }
            lvStyle.Update();
            #endregion

            IsLoading = false;
            Preview();
        }

        private void btnApply_Click( object sender, EventArgs e )
        {
            this.Apply?.Invoke( this, e );
        }

        private void lvFamily_DrawItem( object sender, DrawListViewItemEventArgs e )
        {
            try
            {

                if ( e.ItemIndex >= 0 && e.ItemIndex < lvFamily.VirtualListSize )
                {
                    var familyItem = families[e.ItemIndex];

                    var familyName = familyItem.Text;
                    //var family = (Media.FontFamily)familyItem.Tag;
                    var family = (Dictionary<string, Media.Typeface>)familyItem.Tag;

                    Bitmap sample = null;
                    Color fgColor = e.Item.ForeColor;
                    Color bgColor = e.Item.BackColor;
                    if ( familiySamples.ContainsKey( familyName ) )
                    {
                        sample = familiySamples[familyName];
                    }
                    else
                    {
                        //var face = family.Last().Key;
                        //var face = family.Reverse().First().Key;
                        var face = family.First().Key;
                        sample = e.Item.Text.ToBitmap( familyName, face, 12, fgColor );
                        //sample = e.Item.Text.ToBitmap( family.Source, face, 12, fgColor );

                        familiySamples[familyName] = sample;
                    }
                    lvDrawItem( e, sample, fgColor, bgColor );
                }
            }
            catch ( Exception )
            {

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
                //lvFamily.FocusedItem.EnsureVisible();

                int idx = lvFamily.FocusedItem.Index;

                //var ff = (Media.FontFamily)families[idx].Tag;
                var ff = (Dictionary<string, Media.Typeface>)families[idx].Tag;
                curFamily = ff.First().Value;
                //curFamily = ff;
                //curFamilyName = ff.FamilyNames.ContainsKey( locale_uikey ) ? ff.FamilyNames[locale_uikey] : ff.FamilyNames[locale_enkey];
                //curFamilyName = ff.FamilyNames[locale_enkey];
                curFamilyName = lvFamily.FocusedItem.Text;

                lvStyle.BeginUpdate();
                #region Add family supported styles
                styleSamples.Clear();
                lvStyle.Items.Clear();
                //foreach ( var typeface in ff.FamilyTypefaces )
                foreach ( var f in ff)//.Reverse() )
                {
                    var facename = f.Key.Replace( "250", "Thin" ).Replace( "350", "Regular" ).Replace("W3", "Light").Replace("W6","SemiBold");
                    var item = new ListViewItem(f.Key);
                    item.Text = string.Join( " ", facename.Split().Select( o => o._() ) );
                    item.Tag = facename;
                    lvStyle.Items.Add( item );
                }
                if ( lvStyle.Items.Count > 0 )
                {
                    lvStyle.Items[0].Selected = true;
                    edStyle.Text = lvStyle.Items[0].Text;
                    edStyle.Tag = lvStyle.Items[0].Tag;
                    curFaceName = (string) lvStyle.Items[0].Tag;
                }
                #endregion
                lvStyle.EndUpdate();

                #region Add family sizes

                #endregion

                #region Add family charsets
                List<string> charsets = new List<string>();
                List<string> charsets_ui = new List<string>();
                foreach (var kv in ff.Values.First().FontFamily.FamilyNames )
                {
                    //charsets.Add( kv.Key.IetfLanguageTag );
                    //var charset = CultureInfo.GetCultureInfoByIetfLanguageTag( kv.Key.IetfLanguageTag ).DisplayName;
                    var charset = CultureInfo.GetCultureInfo( kv.Key.IetfLanguageTag ).DisplayName;
                    charsets_ui.Add( charset );
                    charsets.Add( kv.Key.IetfLanguageTag );
                }
                cbCharset.Items.Clear();
                cbCharset.Tag = charsets;
                cbCharset.Items.AddRange( charsets_ui.Distinct().ToArray() );
                cbCharset.Text = CultureInfo.CurrentUICulture.DisplayName;
                cbCharset.SelectedIndex = cbCharset.SelectedIndex < 0 ? 0 : cbCharset.SelectedIndex;
                _locale = charsets[cbCharset.SelectedIndex];
                #endregion

                if (!lvFamily.FocusedItem.Text.StartsWith(edFamily.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    edFamily.Text = lvFamily.FocusedItem.Text;
                }

                _familyName = curFamilyName;
                Preview();
            }
        }

        private void lvFamily_SearchForVirtualItem( object sender, SearchForVirtualItemEventArgs e )
        {
            //e.IncludeSubItemsInSearch = true;
            var results = families.Where( o => o.Text.StartsWith(e.Text, StringComparison.CurrentCultureIgnoreCase));
            if ( results.Count() > 0 )
            {
                e.Index = results.First().Index;
           }
        }

        private void lvFamily_KeyPress( object sender, KeyPressEventArgs e )
        {
            if ( e.KeyChar == (char) Keys.Back )
                edFamily.Text = edFamily.Text.Substring( 0, edFamily.Text.Length - 1 );
            else
                edFamily.Text += e.KeyChar;
        }

        private void lvStyle_DrawItem( object sender, DrawListViewItemEventArgs e )
        {
            try
            {
                if ( e.ItemIndex >= 0 && e.ItemIndex < lvStyle.Items.Count )
                {
                    Color fgColor = e.Item.ForeColor;
                    Color bgColor = e.Item.BackColor;

                    var facename = (string)e.Item.Tag;
                    Bitmap sample = null;
                    if ( styleSamples.ContainsKey( facename ) )
                        sample = styleSamples[facename];
                    else
                    {
                        sample = e.Item.Text.ToBitmap( curFamilyName, facename, 14.0f, fgColor );
                        styleSamples[facename] = sample;
                    }
                    //Bitmap sample = "[Unspported Style]".ToBitmap( new Font("Arial", 14), fgColor );
                    lvDrawItem( e, sample, fgColor, bgColor );
                }
            }
            catch ( Exception )
            {

            }
        }

        private void lvStyle_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( lvStyle.FocusedItem is ListViewItem)
            {
                curFaceName = (string)lvStyle.FocusedItem.Tag;
                edStyle.Text = lvStyle.FocusedItem.Text;
                edStyle.Tag = lvStyle.FocusedItem.Tag;
                //FontStyle = curFaceName.
                Preview();
            }
        }

        private void lbSize_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( lbSize.SelectedItem != null)
                edSize.Text = (string) lbSize.SelectedItem;
            if( FontSizeList.ContainsKey( edSize.Text ) )
                FontSize = FontSizeList[edSize.Text];
            Preview();
        }

        private void colorGrid_ColorChanged( object sender, EventArgs e )
        {
            _fontcolor = colorGrid.Color;
            Preview();
        }

        private void chkEffect_CheckedChanged( object sender, EventArgs e )
        {
            if ( chkEffectUnderline.Checked ) _fontstyle &= ~FontStyle.Underline;
            if ( chkEffectStrikeout.Checked ) _fontstyle &= ~FontStyle.Strikeout;

            Preview();
        }

        private void edFamily_TextChanged( object sender, EventArgs e )
        {
            var lvis = families.AsParallel().Where(o => { return ( o.Text.StartsWith(edFamily.Text, StringComparison.CurrentCultureIgnoreCase )); } );
            if ( lvis.Count() > 0 )
            {
                ListViewItem lvi = lvis.First();
                int idx = families.IndexOf( lvi );
                lvFamily.Select();
                lvFamily.EnsureVisible( idx );
                lvFamily.FocusedItem = lvFamily.FindItemWithText( edFamily.Text, false, idx );
                lvFamily.FocusedItem.Selected = true;
            }
        }

        private void edSize_TextChanged( object sender, EventArgs e )
        {
            edSize.Text = edSize.Text.Trim();
            if (FontSizeList.ContainsKey(edSize.Text.Trim()))
            {
                FontSize = FontSizeList[edSize.Text.Trim()];
            }
        }

        private void cbCharset_SelectedIndexChanged( object sender, EventArgs e )
        {
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
