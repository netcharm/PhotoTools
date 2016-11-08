using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mono.Addins;
using NetCharm.Image.Addins;


namespace InternalFilters.Effects
{
    public enum HslFilterMode
    {
        Normal = 0
    }

    [Extension]
    class HslFilter : BaseAddinEffect
    {
        HslFilterForm fm = null;

        #region Properties override
        public override AddinType Type
        {
            get { return ( AddinType.Effect ); }
        }

        private string _name = "HslFilter";
        public override string Name
        {
            get { return _name; }
        }

        private string _displayname = T("HslFilter");
        public override string DisplayName
        {
            get { return _( _displayname ); }
            set { _displayname = value; }
        }

        public override string GroupName
        {
            get { return ( "Color" ); }
        }

        private string _displayGroupName = T("Color");
        public override string DisplayGroupName
        {
            get { return _( _displayGroupName ); }
            set { _displayGroupName = value; }
        }

        private string _description = T("Using HSL Value Filter Image");
        public override string Description
        {
            get { return _( _description ); }
            set { _description = value; }
        }

        //public override Image LargeIcon
        //{
        //    get { return Properties.Resources.HslFilter_32x; }
        //}

        //public override Image SmallIcon
        //{
        //    get { return Properties.Resources.HslFilter_16x; }
        //}

        #endregion

        #region Method override
        /// <summary>
        /// 
        /// </summary>
        private void InitParams()
        {
            Dictionary<string, object> kv = new Dictionary<string, object>()
            {
                { "HslFilterMode", HslFilterMode.Normal },
                { "HslHue", 180 },
                { "HslSaturation", 1.0f },
                { "HslLuminance", 0.5f },
                { "HslTolerance", 5.0f },
                { "GrayscaleMode", GrayscaleMode.None }
            };

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
                var cfm = (form as HslFilterForm);
                Params["HslFilterMode"] = cfm.ParamMode;
                Params["HslHue"] = cfm.ParamHue;
                Params["HslSaturation"] = cfm.ParamSaturation;
                Params["HslLuminance"] = cfm.ParamLuminance;
                Params["HslTolerance"] = cfm.ParamTolerance;
                Params["GrayscaleMode"] = cfm.ParamGrayscaleMode;
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
                var cfm = (form as HslFilterForm);
                cfm.ParamMode = Params["HslFilterMode"];
                cfm.ParamHue = Params["HslHue"];
                cfm.ParamSaturation = Params["HslSaturation"];
                cfm.ParamLuminance = Params["HslLuminance"];
                cfm.ParamTolerance = Params["HslTolerance"];
                cfm.ParamGrayscaleMode = Params["GrayscaleMode"];
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
                fm = new HslFilterForm( this );
                fm.host = Host;
                //Translate( fm );
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
            HslFilterMode HslFilterMode = (HslFilterMode) Params["HslFilterMode"].Value;
            GrayscaleMode grayscaleMode = (GrayscaleMode)Params["GrayscaleMode"].Value;
            int hueValue = (int)Params["HslHue"].Value;
            float satValue = (float)Params["HslSaturation"].Value;
            float lumValue = (float)Params["HslLuminance"].Value;
            float tolValue = (float)Params["HslTolerance"].Value;
            float factor_n = 1 - tolValue / 100;
            float factor_p = 1 + tolValue / 100;
            //
            // your filter codes begin
            //
            Accord.Imaging.Filters.HSLFiltering filter = new Accord.Imaging.Filters.HSLFiltering();
            filter.Hue = new Accord.IntRange( (int) Math.Round( hueValue * factor_n ), (int) Math.Round( hueValue * factor_p ) );
            filter.Saturation = new Accord.Range( satValue * factor_n, satValue * factor_p );
            filter.Luminance = new Accord.Range( lumValue * factor_n, lumValue * factor_p );
            //filter.FillColor = new Accord.Imaging.HSL( hueValue, satValue, lumValue );
            //filter.FillOutsideRange = false;
            filter.FillColor = new Accord.Imaging.HSL( hueValue, 0.0f, lumValue );
            filter.FillOutsideRange = true;

            //
            // your filter codes end
            //
            //dst = AddinUtils.ProcessImage( filter, dst, false );
            dst = filter.Apply( dst );
            dst = Accord.Imaging.Image.Clone( dst as Bitmap, PixelFormat.Format32bppArgb );
            Accord.Imaging.UnmanagedImage uimg = Accord.Imaging.UnmanagedImage.FromManagedImage(dst);
            Color fc = filter.FillColor.ToRGB().Color;
            for ( int y = 0; y < uimg.Height; y++ )
            {
                for ( int x = 0; x < uimg.Width; x++ )
                {
                    if ( uimg.GetPixel( x, y ) == fc ) uimg.SetPixel( x, y, Color.Transparent );
                }
            }
            dst = uimg.ToManagedImage();

            IAddin gfilter = Host.Effects["Grayscale"];
            Dictionary<string, ParamItem> oldParams = gfilter.Params;
            object data = null;
            gfilter.Command( AddinCommand.InitParams, out data,
                new Dictionary<string, object>()
                {
                    { "GrayscaleMode", grayscaleMode }
                } 
            );

            var gimg = gfilter.Apply( image );

            gfilter.Command( AddinCommand.SetParams, out data, oldParams );
            
            using ( var g = Graphics.FromImage( gimg ) )
            {
                g.DrawImage( dst, 0, 0, dst.Width, dst.Height );
            }
            dst = gimg as Bitmap;

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
                    if ( fm is HslFilterForm )
                    {
                        //result = fm.GetImageSelection();
                    }
                    break;
                case AddinCommand.SetImageSelection:
                    if ( fm is HslFilterForm )
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
