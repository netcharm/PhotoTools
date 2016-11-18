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
    public enum BlurMode
    {
        Normal = 0,
        Gaussian,
        Box
    }

    [Extension]
    class Blur : BaseAddinEffect
    {
        BlurForm fm = null;

        #region Properties override
        public override AddinType Type
        {
            get { return ( AddinType.Effect ); }
        }

        private string _name = "Blur";
        public override string Name
        {
            get { return _name; }
        }

        private string _displayname = T("Blur");
        public override string DisplayName
        {
            get { return _( _displayname ); }
            set { _displayname = value; }
        }

        public override string CategoryName
        {
            get { return ( "Clearness" ); }
        }

        private string _displayGroupName = T("Clearness");
        public override string DisplayCategoryName
        {
            get { return _( _displayGroupName ); }
            set { _displayGroupName = value; }
        }

        private string _description = T("Blur Image");
        public override string Description
        {
            get { return _( _description ); }
            set { _description = value; }
        }

        public override Image LargeIcon
        {
            get { return Properties.Resources.Blur_32x; }
        }

        public override Image SmallIcon
        {
            get { return Properties.Resources.Blur_16x; }
        }

        #endregion

        #region Method override
        /// <summary>
        /// 
        /// </summary>
        private void InitParams()
        {
            Dictionary<string, object> kv = new Dictionary<string, object>();
            kv.Add( "BlurMode", BlurMode.Normal );
            kv.Add( "GaussianSigma", (double) 1.4 );
            kv.Add( "GaussianSize", 7 );
            kv.Add( "GaussianThreshold", 0 );
            kv.Add( "BoxSize", 3 );

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
                var cfm = (form as BlurForm);

                Params["BlurMode"] = cfm.ParamMode;
                Params["GaussianSigma"] = cfm.ParmaGaussianSigma;
                Params["GaussianSize"] = cfm.ParmaGaussianSize;
                Params["GaussianThreshold"] = cfm.ParmaGaussianThreshold;
                Params["BoxSize"] = cfm.ParmaBoxSize;
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
                var cfm = (form as BlurForm);

                cfm.ParamMode = Params["BlurMode"];
                cfm.ParmaGaussianSigma = Params["GaussianSigma"];
                cfm.ParmaGaussianSize = Params["GaussianSize"];
                cfm.ParmaGaussianThreshold = Params["GaussianThreshold"];
                cfm.ParmaBoxSize = Params["BoxSize"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public override void Show( Form parent = null, bool setup = false )
        {
            _success = false;
            if ( fm == null )
            {
                fm = new BlurForm( this );
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
            if ( fm.ShowDialog() == DialogResult.OK )
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
            BlurMode blurMode = (BlurMode) Params["BlurMode"].Value;
            double gaussianSigma = (double) Params["GaussianSigma"].Value;
            int gaussianSize = (int) Params["GaussianSize"].Value;
            int gaussianThreshold = (int) Params["GaussianThreshold"].Value;
            int boxSize = (int) Params["BoxSize"].Value;

            Accord.Imaging.Filters.IFilter filter = null;
            switch ( blurMode )
            {
                case BlurMode.Normal:
                    filter = new Accord.Imaging.Filters.Blur();
                    dst = ( filter as Accord.Imaging.Filters.Blur ).Apply( dst );
                    break;
                case BlurMode.Gaussian:
                    filter = new Accord.Imaging.Filters.GaussianBlur();
                    ( filter as Accord.Imaging.Filters.GaussianBlur ).Sigma = gaussianSigma;
                    ( filter as Accord.Imaging.Filters.GaussianBlur ).Size = gaussianSize;
                    ( filter as Accord.Imaging.Filters.GaussianBlur ).Threshold = gaussianThreshold;
                    dst = filter.Apply( dst );
                    break;
                case BlurMode.Box:
                    filter = new Accord.Imaging.Filters.FastBoxBlur( (byte) boxSize, (byte) boxSize );
                    dst = AddinUtils.ProcessImage( filter, dst, false );
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
            //return base.Command( cmd, out result, args );
            result = null;
            switch ( cmd )
            {
                case AddinCommand.GetImageSelection:
                    if ( fm is BlurForm )
                    {
                        //result = fm.GetImageSelection();
                    }
                    break;
                case AddinCommand.SetImageSelection:
                    if ( fm is BlurForm )
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
