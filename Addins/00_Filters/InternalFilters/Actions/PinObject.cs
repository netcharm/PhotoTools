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

        #region Position
        public bool RandomPos = false;
        public CornerRegionType Pos = CornerRegionType.None;
        #endregion

        #region Transform
        //public float Rotate = 0f;
        //public float Scale = 1f;
        #endregion

        #region Effects
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

        public override string GroupName
        {
            get { return ( "Decoration" ); }
        }

        private string _displayGroupName = T("Decoration");
        public override string DisplayGroupName
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
            //return base.Command( cmd, out result, args );
            result = null;
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
                    foreach(var item in args[0] as Dictionary<string, ParamItem>)
                    {

                    }
                    SetParams( fm, null );
                    break;
                default:
                    break;
            }
            return ( true );
        }
        #endregion
    }
}
