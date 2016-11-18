using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mono.Addins;
using NetCharm.Image.Addins;


namespace InternalFilters.Actions
{
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using ParamList = Dictionary<string, ParamItem>;

    public enum PinObjectMode
    {
        Text,
        Picture,
        Tag
    }

    public class PinOption
    {
        public bool Enabled = true;
        public bool Tile = false;
        public float Blend = 100f;

        public Bitmap Picture = null;
        public string Text = string.Empty;
        public string Tag = string.Empty;

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
        public Dictionary<IAddin, ParamList> FilterParams = new Dictionary<IAddin, ParamList>();
        public float Opaque = 100f;
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
            kv.Add( "PinObjectList", typeof( List<object> ) );

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
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public override void Show( Form parent = null, bool setup = false )
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
            if ( fm.ShowDialog() == DialogResult.OK )
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public override Image Apply( Image image )
        {
            Bitmap dst = AddinUtils.CloneImage(image) as Bitmap;

            GetParams( fm );
            PinObjectMode PinObjectMode = (PinObjectMode) Params["PinObjectMode"].Value;
            //
            // Todo filter apply
            //
            switch ( PinObjectMode )
            {
                case PinObjectMode.Picture:
                    Bitmap src = AddinUtils.LoadImage("布老虎.png") as Bitmap;
                    PinOption option = (PinOption) Params["PinOption"].Value;
                    option.Picture = src;
                    dst = DrawPicture( dst, option );
                    break;
                case PinObjectMode.Text:
                    break;
                case PinObjectMode.Tag:
                    break;
            }

            AddinUtils.CloneExif( image, dst );
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
                        var subitems = new List<AddinSubItem>();
                        subitems.Add( new AddinSubItem( this, 
                            "Text", this._( "Text" ), 
                            "", this._( "" ), 
                            Properties.Resources.Text_16x, Properties.Resources.Text_32x ) );
                        subitems.Add( new AddinSubItem( this, 
                            "Picture", this._( "Picture" ), 
                            "", this._( "" ), 
                            Properties.Resources.Picture_16x, Properties.Resources.Picture_16x ) );
                        subitems.Add( new AddinSubItem( this, 
                            "Tag", this._( "Smart Tag" ), 
                            "", this._( "" ), 
                            Properties.Resources.Tag_16x, Properties.Resources.Tag_32x ) );
                        result = subitems;
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
        protected internal Bitmap DrawPicture( Bitmap dst, PinOption option )
        {
            Bitmap result = dst;
            if ( option.Picture is Image )
            {
                Bitmap src = new Bitmap(option.Picture);

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
                            pos.X = offset.X;
                            pos.Y = offset.Y;
                            break;
                        case CornerRegionType.TopCenter:
                            pos.X = offset.X + (dst.Width - src.Width) / 2f;
                            pos.Y = offset.Y;
                            break;
                        case CornerRegionType.TopRight:
                            pos.X = -offset.X + ( dst.Width - src.Width );
                            pos.Y = offset.Y;
                            break;

                        case CornerRegionType.MiddleLeft:
                            pos.X = offset.X;
                            pos.Y = offset.Y + (dst.Height - src.Height) / 2f;
                            break;
                        case CornerRegionType.MiddleCenter:
                            pos.X = offset.X + ( dst.Width - src.Width ) / 2f;
                            pos.Y = offset.Y + ( dst.Height - src.Height ) / 2f;
                            break;
                        case CornerRegionType.MiddleRight:
                            pos.X = -offset.X + ( dst.Width - src.Width );
                            pos.Y = offset.Y + ( dst.Height - src.Height ) / 2f;
                            break;

                        case CornerRegionType.BottomLeft:
                            pos.X = offset.X;
                            pos.Y = -offset.Y + ( dst.Height - src.Height );
                            break;
                        case CornerRegionType.BottomCenter:
                            pos.X = offset.X + ( dst.Width - src.Width ) / 2f;
                            pos.Y = -offset.Y + ( dst.Height - src.Height );
                            break;
                        case CornerRegionType.BottomRight:
                            pos.X = -offset.X + ( dst.Width - src.Width );
                            pos.Y = -offset.Y + ( dst.Height - src.Height );
                            break;

                        default:
                            pos.X = option.Location.X + offset.X;
                            pos.Y = option.Location.Y + offset.Y;
                            break;
                    }
                    pos.X = pos.X >= 0 ? pos.X : 0;
                    pos.Y = pos.Y >= 0 ? pos.Y : 0;
                    pos.X = pos.X > dst.Width - option.Picture.Width ? dst.Width - option.Picture.Width : pos.X;
                    pos.Y = pos.Y > dst.Height - option.Picture.Height ? dst.Height - option.Picture.Height : pos.Y;
                    #endregion
                }
                #endregion

                #region Make Tile Image
                if ( option.Tile)
                {
                    RectangleF rect = new RectangleF(0, 0, src.Width, src.Height);
                    rect.Inflate( margin.X / 2f, margin.Y / 2f );
                    //rect = RectangleF.Inflate(rect, margin.X / 2f, margin.Y / 2f );

                    Bitmap tileElement = new Bitmap((int)Math.Round(rect.Width), (int)Math.Round(rect.Height)*2, PixelFormat.Format32bppArgb);
                    using ( var g = Graphics.FromImage( tileElement ) )
                    {
                        PointF p00 = new PointF(margin.X / 2f, margin.Y / 2f);
                        PointF p10 = new PointF(offset.X - rect.Width, margin.Y / 2f);
                        PointF p11 = new PointF(margin.X / 2f + offset.X, margin.Y * 1.5f + src.Height);

                        g.DrawImage( src, p00 );
                        g.DrawImage( src, p10 );
                        g.DrawImage( src, p11 );
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
                    AddinUtils.SetParams( filter, option.FilterParams[filter] );                    
                    src = filter.Apply( src as Image ) as Bitmap;
                }
                #endregion

                #region Draw Picture to Image
                using ( Graphics g = Graphics.FromImage( result ) )
                {
                    ColorMatrix c = new ColorMatrix() { Matrix33 = option.Opaque / 100f };
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
                return ( result );
            }
            return ( result );
        }



        #endregion
    }
}
