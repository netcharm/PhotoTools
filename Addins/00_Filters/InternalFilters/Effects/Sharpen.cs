using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GDIPlusX.GDIPlus11.Effects;
using Mono.Addins;
using NetCharm.Image.Addins;


namespace InternalFilters.Effects
{
    [Serializable]
    public enum SharpenMode
    {
        Normal = 0,
        Gaussian,
        GDI
    }

    [Extension]
    class Sharpen : BaseAddinEffect
    {
        SharpenForm fm = null;

        #region Properties override
        public override AddinType Type
        {
            get { return ( AddinType.Effect ); }
        }

        private string _name = "Sharpen";
        public override string Name
        {
            get { return _name; }
        }

        private string _displayname = T("Sharpen");
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

        private string _description = T("Sharpen Image");
        public override string Description
        {
            get { return _( _description ); }
            set { _description = value; }
        }

        public override Image LargeIcon
        {
            get { return Properties.Resources.Sharpen_32x; }
        }

        public override Image SmallIcon
        {
            get { return Properties.Resources.Sharpen_16x; }
        }

        #endregion

        #region Method override
        /// <summary>
        /// 
        /// </summary>
        private void InitParams()
        {
            Dictionary<string, object> kv = new Dictionary<string, object>();
            kv.Add( "SharpenMode", SharpenMode.Normal );
            kv.Add( "GaussianSigma", (double) 1.4 );
            kv.Add( "GaussianSize", 5 );
            kv.Add( "GaussianThreshold", 0 );
            kv.Add( "GdiRatio", 1.5f );
            kv.Add( "GdiAmount", 50f );

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
                var cfm = (form as SharpenForm);

                Params["SharpenMode"] = cfm.ParamMode;
                Params["GaussianSigma"] = cfm.ParamGaussianSigma;
                Params["GaussianSize"] = cfm.ParamGaussianSize;
                Params["GaussianThreshold"] = cfm.ParamGaussianThreshold;
                Params["GdiRatio"] = cfm.ParamGdiRatio;
                Params["GdiAmount"] = cfm.ParamGdiAmount;
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
                var cfm = (form as SharpenForm);

                cfm.ParamMode = Params["SharpenMode"];
                cfm.ParamGaussianSigma = Params["GaussianSigma"];
                cfm.ParamGaussianSize = Params["GaussianSize"];
                cfm.ParamGaussianThreshold = Params["GaussianThreshold"];
                cfm.ParamGdiRatio = Params["GdiRatio"];
                cfm.ParamGdiAmount = Params["GdiAmount"];
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
                fm = new SharpenForm( this );
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
            Bitmap dst = AddinUtils.CloneImage(image) as Bitmap;

            GetParams( fm );
            SharpenMode sharpenMode = (SharpenMode) Params["SharpenMode"].Value;
            double gaussianSigma = (double) Params["GaussianSigma"].Value;
            int gaussianSize = (int) Params["GaussianSize"].Value;
            int gaussianThreshold = (int) Params["GaussianThreshold"].Value;
            float gdiRatio = (float) Params["GdiRatio"].Value;
            float gdiAmount = (float) Params["GdiAmount"].Value;

            Accord.Imaging.Filters.IFilter filter = null;
            switch( sharpenMode )
            {
                case SharpenMode.Normal:
                    filter = new Accord.Imaging.Filters.Sharpen();
                    dst = (filter as Accord.Imaging.Filters.Sharpen).Apply( dst );
                    break;
                case SharpenMode.Gaussian:
                    filter = new Accord.Imaging.Filters.GaussianSharpen();
                    ( filter as Accord.Imaging.Filters.GaussianSharpen ).Sigma = gaussianSigma;
                    ( filter as Accord.Imaging.Filters.GaussianSharpen ).Size = gaussianSize;
                    ( filter as Accord.Imaging.Filters.GaussianSharpen ).Threshold = gaussianThreshold;
                    dst = filter.Apply( dst );
                    break;
                case SharpenMode.GDI:
                    var effect = new SharpenEffect(gdiRatio, gdiAmount);
                    dst.ApplyEffect( effect, new Rectangle( 0, 0, dst.Width, dst.Height ) );
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
                    if ( fm is SharpenForm )
                    {
                        //result = fm.GetImageSelection();
                    }
                    break;
                case AddinCommand.SetImageSelection:
                    if ( fm is SharpenForm )
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
