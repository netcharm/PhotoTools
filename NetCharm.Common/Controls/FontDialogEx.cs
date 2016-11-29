using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Markup;
using ExtensionMethods;
using Media = System.Windows.Media;

namespace NetCharm.Common.Controls
{
    [ToolboxBitmap( typeof( FontDialog ) )]
    [ToolboxItem( true )]
    public partial class FontDialogEx : Component
    {
        #region CultrueInfo Pre-Defined
        private static string locale_en = "en-us";
        private static string locale_ui = CultureInfo.InstalledUICulture.Name.ToLower();

        private static XmlLanguage locale_enkey = XmlLanguage.GetLanguage( locale_en );
        private static XmlLanguage locale_uikey = XmlLanguage.GetLanguage( locale_ui );
        #endregion

        private NetCharm.Common.FontDialog dialog = new NetCharm.Common.FontDialog();

        public event EventHandler Apply;

        private bool _apply = true;
        public bool ShowApply
        {
            get { return ( _apply ); }
            set { _apply = value; }
        }

        private Font _font = SystemFonts.DefaultFont;
        public Font Font
        {
            get { return ( _font ); }
            set
            {
                _font = value;
                if(_font is Font)
                {
                    _size = _font.Size;
                    _family = new Media.FontFamily( _font.FontFamily.Name );
                }
            }
        }

        private bool _usefont = true;
        public bool UseFont
        {
            get { return ( _usefont ); }
            set { _usefont = value; }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never)]
        [Browsable( false )]
        public bool IsTTF
        {
            get { return ( dialog.IsTTF ); }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never)]
        [Browsable( false )]
        private Media.FontFamily _family = default(Media.FontFamily);
        public Media.FontFamily FontFamily
        {
            get {
                var ffs = Media.Fonts.SystemFontFamilies.Where(
                    o => { return ( o.FamilyNames.Values.Contains( FamilyName )); });
                if ( ffs.Count() > 0 )
                {
                    _family = ffs.First();
                }
                else
                    _family = new Media.FontFamily( FamilyName );
                return ( _family ); }
            //set { _family = value; }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never )]
        [Browsable( false )]
        public string FamilyName
        {
            get
            {
                return ( dialog.FontFamily );
                //return ( _family.FamilyNames[locale_uikey] ); 
            }
            //set { _family = new Media.FontFamily( value ); }
            set { dialog.FontFamily = value; }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never)]
        [Browsable( false )]
        private Media.Typeface _typeface = default(Media.Typeface);
        public Media.Typeface Typeface
        {
            get {
                var font = new Font( dialog.FontFamily, dialog.FontSize, dialog.FontFace.ToFontStyle() );
                _typeface = font.ToTypeface();
                return ( _typeface ); }
            set { _typeface = value; }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never )]
        [Browsable( false )]
        public string TypefaceName
        {
            get { return ( dialog.FontFace ); }
            set { dialog.FontFace = value; }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never)]
        [Browsable( false )]
        private double _size = 10f;
        public float Size
        {
            get
            {
                //_size = dialog.Font.Size;
                _size = dialog.FontSize;
                return ( (float) _size );
            }
            set
            {
                _size = value;
                dialog.FontSize = value;
            }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never)]
        [Browsable( false )]
        public Color Color
        {
            get { return (dialog.FontColor); }
            set { dialog.FontColor = value; }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never)]
        [Browsable( false )]
        private bool _underline = false;
        public bool Underline
        {
            get
            {
                _underline = dialog.FontStyle.HasFlag( FontStyle.Underline );
                return ( _underline );
            }
            set { _underline = value; }
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable( EditorBrowsableState.Never)]
        [Browsable( false )]
        private bool _strikeout = false;
        public bool Strikeout
        {
            get
            {
                _strikeout = dialog.FontStyle.HasFlag( FontStyle.Strikeout );
                return ( _strikeout );
            }
            set { _strikeout = value; }
        }


        public FontDialogEx()
        {
            InitializeComponent();

            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
        }

        public DialogResult ShowDialog()
        {
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
            dialog.UseFont = UseFont;
            dialog.ShowApply = _apply;
            dialog.SelectedFont = _font;
            dialog.FontSize = (float)_size;
            return ( dialog.ShowDialog() );
        }

    }
}
