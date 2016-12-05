using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mono.Addins;
using ExtensionMethods;
using NetCharm.Common;
using NetCharm.Image.Addins;


namespace InternalFilters.Actions
{
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Web.Script.Serialization;

    using ParamList = Dictionary<string, ParamItem>;

    [Serializable]
    public enum PinObjectMode
    {
        None,
        Text,
        Picture,
        Tag
    }

    [Serializable]
    public class PinOption
    {
        public bool Enabled = true;
        public bool Tile = false;
        public float Blend = 100f;
        public PinObjectMode Mode = PinObjectMode.None;

        [NonSerialized]
        [ScriptIgnore]
        internal Bitmap ImageCache = null;

        /// <summary>
        /// Pin Picture Options
        /// </summary>
        public string PictureFile = null;

        /// <summary>
        /// Pin Simple Text Options
        /// </summary>
        public string Text = string.Empty;
        public string TextColor = ColorTranslator.ToHtml( Color.Transparent );
        public string TextFont = string.Empty;
        public string TextFace = string.Empty;
        public float TextSize = 12f;
        public FontStyle TextFontStyle = FontStyle.Regular;

        /// <summary>
        /// Pin Tag Options
        /// </summary>
        public string TagFile = string.Empty;

        #region Position
        public bool RandomPos = false;
        public CornerRegionType Pos = CornerRegionType.None;
        public PointF Location = new PointF();
        public PointF Offset = new PointF();
        public PointF Margin = new PointF();
        #endregion

        #region Transform
        //public float Rotate = 0f;
        //public float Scale = 1f;
        #endregion

        #region Effects
        [NonSerialized]
        [ScriptIgnore]
        public Dictionary<IAddin, ParamList> FilterParams = new Dictionary<IAddin, ParamList>();
        public Dictionary<string, ParamList> Filters = new Dictionary<string, ParamList>();
        public float Opacity = 100f;
        //public float GradientWidth = 0f;
        //public Color GradientColor1 = Color.DarkGray;
        //public Color GradientColor2 = Color.DarkGray;
        //public Color GradientColor3 = Color.DarkGray;
        //public float GradientOpaque = 100f;
        //public float ShadowWidth = 0f;
        //public Color ShadowColor = Color.DarkGray;
        //public float ShadowOpaque = 100f;
        //public float GlowWidth = 0f;
        //public Color GlowColor = Color.WhiteSmoke;
        //public float GlowOpaque = 100f;
        //public float OutlineWidth = 0f;
        //public Color OutlineColor = Color.WhiteSmoke;
        //public float OutlineOpaque = 100f;
        #endregion

        public PinOption Clone()
        {
            var result = new PinOption();

            result.Enabled = this.Enabled;
            result.Mode = this.Mode;

            result.Tile = this.Tile;
            result.Blend = this.Blend;
            result.Opacity = this.Opacity;

            result.Location = this.Location;
            result.Offset = this.Offset;
            result.Pos = this.Pos;
            result.RandomPos = this.RandomPos;

            result.FilterParams = this.FilterParams;

            result.Text = this.Text;
            result.TextColor = this.TextColor;
            result.TextFace = this.TextFace;
            result.TextFont = this.TextFont;
            result.TextFontStyle = this.TextFontStyle;
            result.TextSize = this.TextSize;

            result.PictureFile = this.PictureFile;

            result.TagFile = this.TagFile;

            return ( result );
        }
    }

    [Extension]
    partial class PinObject : BaseAddinEffect
    {
        PinObjectForm fm = null;

        #region Properties override
        public override AddinType Type
        {
            get { return ( AddinType.Action ); }
        }

        private string _name = "Pin";
        public override string Name
        {
            get { return _name; }
        }

        private string _displayname = T("Pin");
        public override string DisplayName
        {
            get { return _( _displayname ); }
            set { _displayname = value; }
        }

        public override string CategoryName
        {
            get { return ( "Decoration" ); }
        }

        private string _displayGroupName = T("Decoration");
        public override string DisplayCategoryName
        {
            get { return _( _displayGroupName ); }
            set { _displayGroupName = value; }
        }

        private string _description = T("Decoration Image");
        public override string Description
        {
            get { return _( _description ); }
            set { _description = value; }
        }

        public override Image LargeIcon
        {
            get { return Properties.Resources.Pin_32x; }
        }

        public override Image SmallIcon
        {
            get { return Properties.Resources.Pin_16x; }
        }

        private List<IAddin> _filters = new List<IAddin>();
        public override List<IAddin> Filters
        {
            get { return ( _filters ); }
            //set { _filters = value; }
        }
        #endregion

        #region Method override
        /// <summary>
        /// 
        /// </summary>
        private void InitParams()
        {
            Dictionary<string, object> kv = new Dictionary<string, object>();
            kv.Add( "PinObjectMode", PinObjectMode.Picture );
            kv.Add( "PinOption", new PinOption() );
            kv.Add( "PinObjectOnly", false );

            Params.Clear();
            foreach ( var item in kv )
            {
                Params.Add( item.Key, new ParamItem() );
                Params[item.Key].Name = item.Key;
                Params[item.Key].DisplayName = AddinUtils._( this, item.Key );
                Params[item.Key].Type = item.Value.GetType();
                Params[item.Key].Value = item.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected override void GetParams( Form form )
        {
            if ( Params.Count == 0 ) InitParams();

            if ( form is Form && !form.IsDisposed )
            {
                var cfm = (form as PinObjectForm);
                Params["PinObjectMode"] = cfm.ParamMode;
                Params["PinOption"] = cfm.ParamOption;
                Params["PinObjectOnly"] = cfm.ParamObjectOnly;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="img"></param>
        protected override void SetParams( Form form, System.Drawing.Image img = null )
        {
            if ( Params.Count == 0 ) InitParams();

            if ( form is Form && !form.IsDisposed )
            {
                var cfm = (form as PinObjectForm);
                cfm.ParamMode = Params["PinObjectMode"];
                cfm.ParamOption = Params["PinOption"];
                cfm.ParamObjectOnly = Params["PinObjectOnly"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public override DialogResult Show( Form parent = null, bool setup = false )
        {
            _success = false;
            //return;
            if ( fm == null )
            {
                fm = new PinObjectForm( this );
                fm.host = Host;
                //Translate( fm );
                SetParams( fm, ImgSrc );
            }
            var result = fm.ShowDialog();
            if ( result == DialogResult.OK )
            {
                _success = true;
                GetParams( fm );
                if(!setup)
                {
                    ImgDst = Apply( ImgSrc );
                }
            }
            if ( fm != null )
            {
                fm.Dispose();
                fm = null;
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public override Image Apply( Image image )
        {
            GetParams( fm );

            if ( !( image is Image ) ) return ( image );

            var st = DateTime.Now.Ticks;

            Bitmap dst = AddinUtils.CloneImage(image) as Bitmap;

            PinObjectMode PinObjectMode = (PinObjectMode) Params["PinObjectMode"].Value;
            bool objectOnly = (bool) Params["PinObjectOnly"].Value;
            PinOption option = (PinOption) Params["PinOption"].Value;

            switch ( PinObjectMode )
            {
                case PinObjectMode.Picture:
                    dst = DrawPicture( dst, option, objectOnly );
                    break;
                case PinObjectMode.Text:
                    dst = DrawText( dst, option, objectOnly );
                    break;
                case PinObjectMode.Tag:
                    break;
            }

            AddinUtils.CloneExif( image, dst );
            float tc = new TimeSpan( DateTime.Now.Ticks - st ).Seconds + new TimeSpan( DateTime.Now.Ticks - st ).Milliseconds / 1000f;
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.ApplyTiming, tc ) );
            return ( dst );
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
            result = null;
            //return base.Command( cmd, out result, args );
            base.Command( cmd, out result, args );
            switch ( cmd )
            {
                case AddinCommand.GetImageSelection:
                    #region Get Selection Region
                    if ( fm is PinObjectForm )
                    {
                        //result = fm.GetImageSelection();
                    }
                    #endregion
                    break;
                case AddinCommand.SetImageSelection:
                    #region Set Selection Region
                    if ( fm is PinObjectForm )
                    {
                        if ( args.Length > 0 )
                        {
                            if ( args[0] is Rectangle )
                            {
                                //fm.SetImageSelection( (Rectangle) args[0] );
                            }
                            else if ( args[0] is RectangleF )
                            {
                                //fm.SetImageSelection( (RectangleF) args[0] );
                            }
                        }
                    }
                    #endregion
                    break;
                case AddinCommand.SubItems:
                    if(args.Length==0)
                    {
                        #region Return subitems info
                        var subitems = new List<AddinSubItem>();
                        subitems.Add( new AddinSubItem( this, 
                            "Text", this._( "Text" ), 
                            "", this._( "" ), 
                            this._("Simple Text"),
                            Properties.Resources.Text_16x, Properties.Resources.Text_32x ) );
                        subitems.Add( new AddinSubItem( this, 
                            "Picture", this._( "Picture" ), 
                            "", this._( "" ),
                            this._( "Picture" ),
                            Properties.Resources.Picture_16x, Properties.Resources.Picture_16x ) );
                        subitems.Add( new AddinSubItem( this, 
                            "Tag", this._( "Smart Tag" ), 
                            "", this._( "" ),
                            this._( "Rich-Contents Text" ),
                            Properties.Resources.Tag_16x, Properties.Resources.Tag_32x ) );
                        result = subitems;
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show( $"Subitem \"{args[0] as string}\" Clicked" );
                    }
                    break;
                case AddinCommand.GetParams:
                    GetParams( fm );
                    result = Params;
                    break;
                case AddinCommand.SetParams:
                    SetParams( fm, null );
                    break;
                default:
                    break;
            }
            return ( true );
        }
        #endregion

        #region Draw Object routines
        protected internal Bitmap DrawPicture( Bitmap dst, PinOption option, bool objectOnly=false )
        {
            Bitmap result = new Bitmap(dst);
            if ( option.ImageCache is Image )
            {
                Bitmap src = new Bitmap(option.ImageCache);

                #region Calc Margin & Offset
                PointF margin = new PointF(option.Margin.X/100f*dst.Width, option.Margin.Y/100f*dst.Height);
                PointF offset = new PointF(option.Offset.X/100f*src.Width, option.Offset.Y/100f*src.Height);
                #endregion

                #region Calc Location
                PointF pos = new PointF(0,0);
                if ( option.RandomPos )
                {
                    Random rnd = new Random();
                    pos.X = (float) rnd.NextDouble() * ( dst.Width - src.Width );
                    pos.Y = (float) rnd.NextDouble() * ( dst.Height - src.Height );
                }
                else
                {
                    #region Calc Corner / Side Position
                    switch ( option.Pos )
                    {
                        case CornerRegionType.TopLeft:
                            pos.X = margin.X;
                            pos.Y = margin.Y;
                            break;
                        case CornerRegionType.TopCenter:
                            pos.X = margin.X + (dst.Width - src.Width) / 2f;
                            pos.Y = margin.Y;
                            break;
                        case CornerRegionType.TopRight:
                            pos.X = -margin.X + ( dst.Width - src.Width );
                            pos.Y = margin.Y;
                            break;

                        case CornerRegionType.MiddleLeft:
                            pos.X = margin.X;
                            pos.Y = margin.Y + (dst.Height - src.Height) / 2f;
                            break;
                        case CornerRegionType.MiddleCenter:
                            pos.X = margin.X + ( dst.Width - src.Width ) / 2f;
                            pos.Y = margin.Y + ( dst.Height - src.Height ) / 2f;
                            break;
                        case CornerRegionType.MiddleRight:
                            pos.X = -margin.X + ( dst.Width - src.Width );
                            pos.Y = margin.Y + ( dst.Height - src.Height ) / 2f;
                            break;

                        case CornerRegionType.BottomLeft:
                            pos.X = margin.X;
                            pos.Y = -margin.Y + ( dst.Height - src.Height );
                            break;
                        case CornerRegionType.BottomCenter:
                            pos.X = margin.X + ( dst.Width - src.Width ) / 2f;
                            pos.Y = -margin.Y + ( dst.Height - src.Height );
                            break;
                        case CornerRegionType.BottomRight:
                            pos.X = -margin.X + ( dst.Width - src.Width );
                            pos.Y = -margin.Y + ( dst.Height - src.Height );
                            break;

                        default:
                            pos.X = option.Location.X + margin.X;
                            pos.Y = option.Location.Y + margin.Y;
                            break;
                    }
                    //pos.X = pos.X >= 0 ? pos.X : 0;
                    //pos.Y = pos.Y >= 0 ? pos.Y : 0;
                    //pos.X = pos.X > dst.Width - option.ImageCache.Width ? dst.Width - option.ImageCache.Width : pos.X;
                    //pos.Y = pos.Y > dst.Height - option.ImageCache.Height ? dst.Height - option.ImageCache.Height : pos.Y;
                    #endregion
                }
                #endregion

                #region Make Tile Image
                if ( option.Tile)
                {
                    RectangleF rect = new RectangleF(0, 0, src.Width, src.Height);
                    rect.Inflate( margin.X / 2f, margin.Y / 2f );
                    //rect = RectangleF.Inflate(rect, margin.X / 2f, margin.Y / 2f );

                    Bitmap tileElement = new Bitmap((int)Math.Round(rect.Width*2), (int)Math.Round(rect.Height)*2, PixelFormat.Format32bppArgb);
                    using ( var g = Graphics.FromImage( tileElement ) )
                    {
                        PointF p00 = new PointF(margin.X / 2f, margin.Y / 2f);
                        PointF p01 = new PointF(p00.X + rect.Width, p00.Y);
                        PointF p10 = new PointF(offset.X - rect.Width, p00.Y+rect.Height);
                        PointF p11 = new PointF(p10.X + rect.Width, p10.Y);
                        PointF p12 = new PointF(p11.X + rect.Width, p10.Y);

                        g.DrawImage( src, p00 );
                        g.DrawImage( src, p01 );
                        g.DrawImage( src, p10 );
                        g.DrawImage( src, p11 );
                        g.DrawImage( src, p12 );
                    }

                    int nw = (int)Math.Ceiling(Math.Sqrt(Math.Pow(dst.Width,2) + Math.Pow(dst.Height,2)));
                    Bitmap tile = new Bitmap(nw, nw, PixelFormat.Format32bppArgb);
                    using ( var g = Graphics.FromImage( tile ) )
                    {
                        Brush tb = new TextureBrush(tileElement);
                        g.FillRectangle( tb, 0, 0, nw, nw );
                    }
                    src = tile;
                }
                #endregion

                #region Apply Filters
                foreach ( IAddin filter in Filters)
                {
                    if(filter.Enabled)
                    {
                        if( option.FilterParams.ContainsKey(filter) )
                        {
                            filter.Params = option.FilterParams[filter];
                        }
                        src = filter.Apply( src as Image ) as Bitmap;
                    }
                }
                #endregion
                if ( objectOnly ) return ( src );

                #region Draw Picture to Image
                using ( Graphics g = Graphics.FromImage( result ) )
                {
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextContrast = 2;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    ColorMatrix c = new ColorMatrix() { Matrix33 = option.Opacity / 100f };
                    ImageAttributes a = new ImageAttributes();
                    a.SetColorMatrix( c, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );

                    //Matrix m = new Matrix();
                    //m.RotateAt( ARotate + factorR, TGPPointF.Create( s.X / 2, s.Y / 2 ) );
                    //g.MultiplyTransform( m );
                    if(option.Tile)
                    {
                        pos.X = ( dst.Width - src.Width ) / 2f;
                        pos.Y = ( dst.Height - src.Height ) / 2f;
                    }

                    g.DrawImage( src,
                        new Rectangle( (int) Math.Round( pos.X ), (int) Math.Round( pos.Y ), src.Width, src.Height ),
                        0, 0, src.Width, src.Height,
                        GraphicsUnit.Pixel,
                        a );
                }
                #endregion

                #if DEBUG

                #endif
                return ( result );
            }
            return ( result );
        }

        protected internal Bitmap DrawText( Bitmap dst, PinOption option, bool objectOnly = false )
        {
            Bitmap result = new Bitmap(dst);
            if ( !string.IsNullOrEmpty( option.Text ) )
            {
                var align = string.Empty;
                //if ( option.Pos == CornerRegionType.TopLeft || option.Pos == CornerRegionType.MiddleLeft || option.Pos == CornerRegionType.BottomLeft )
                //    align = "Left";
                //else if ( option.Pos == CornerRegionType.TopCenter || option.Pos == CornerRegionType.MiddleCenter || option.Pos == CornerRegionType.BottomCenter )
                //    align = "Center";
                //else if ( option.Pos == CornerRegionType.TopRight || option.Pos == CornerRegionType.MiddleRight || option.Pos == CornerRegionType.BottomRight )
                //    align = "Right";
                option.ImageCache = option.Text.ToBitmap( option.TextFont, (option.TextFace + $" {align}").Trim(), option.TextSize, option.TextColor.ToColor() );
                result = DrawPicture( dst, option, objectOnly );
            }
            return ( result );
        }

        #endregion
    }
}
