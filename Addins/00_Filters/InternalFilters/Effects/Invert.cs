using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mono.Addins;
using NetCharm.Image.Addins;


namespace InternalFilters.Effects
{
    [Serializable]
    public enum InvertMode
    {
        Normal = 0
    }

    [Extension]
    class Invert : BaseAddinEffect
    {
        InvertForm fm = null;

        #region Properties override
        public override AddinType Type
        {
            get { return ( AddinType.Effect ); }
        }

        private string _name = "Invert";
        public override string Name
        {
            get { return _name; }
        }

        private string _displayname = T("Invert");
        public override string DisplayName
        {
            get { return _( _displayname ); }
            set { _displayname = value; }
        }

        public override string CategoryName
        {
            get { return ( "Color" ); }
        }

        private string _displayGroupName = T("Color");
        public override string DisplayCategoryName
        {
            get { return _( _displayGroupName ); }
            set { _displayGroupName = value; }
        }

        private string _description = T("Invert Image Color");
        public override string Description
        {
            get { return _( _description ); }
            set { _description = value; }
        }

        public override Image LargeIcon
        {
            get { return Properties.Resources.Invert_32x; }
        }

        public override Image SmallIcon
        {
            get { return Properties.Resources.Invert_16x; }
        }

        #endregion

        #region Method override
        /// <summary>
        /// 
        /// </summary>
        private void InitParams()
        {
            Dictionary<string, object> kv = new Dictionary<string, object>();
            kv.Add( "InvertMode", InvertMode.Normal );

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
                var cfm = (form as InvertForm);
                Params["InvertMode"] = cfm.ParamMode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="img"></param>
        protected override void SetParams( Form form, Image img = null )
        {
            if ( Params.Count == 0 ) InitParams();

            if ( form is Form && !form.IsDisposed )
            {
                var cfm = (form as InvertForm);
                cfm.ParamMode = Params["InvertMode"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public override DialogResult Show( Form parent = null, bool setup = false )
        {
            _success = false;
            if ( fm == null )
            {
                fm = new InvertForm( this );
                fm.host = Host;
                fm.Text = DisplayName;
                fm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                fm.MaximizeBox = false;
                fm.MinimizeBox = false;
                fm.ShowIcon = false;
                fm.ShowInTaskbar = false;
                fm.StartPosition = FormStartPosition.CenterParent;

                Translate( fm );
                SetParams( fm, ImgSrc );
                //Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.GetImageSelection, 0 ) );
            }
            var result = fm.ShowDialog();
            if ( result == DialogResult.OK )
            {
                _success = true;
                GetParams( fm );
                if ( !setup )
                {
                    ImgDst = Apply( ImgSrc );
                    //Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.SetImageSelection, new RectangleF( 0, 0, 0, 0 ) ) );
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

            Bitmap dst = AddinUtils.CloneImage(image) as Bitmap;

            InvertMode InvertMode = (InvertMode) Params["InvertMode"].Value;

            Accord.Imaging.Filters.Invert filter = new Accord.Imaging.Filters.Invert();
            dst = AddinUtils.ProcessImage( filter, dst, false ) as Bitmap;

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
                    if ( fm is InvertForm )
                    {
                        //result = fm.GetImageSelection();
                    }
                    break;
                case AddinCommand.SetImageSelection:
                    if ( fm is InvertForm )
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
                    break;
                default:
                    break;
            }
            return ( true );
        }
        #endregion
    }
}
