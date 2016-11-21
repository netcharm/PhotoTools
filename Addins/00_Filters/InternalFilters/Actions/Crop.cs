using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mono.Addins;
using NetCharm.Image;
using NetCharm.Image.Addins;

namespace InternalFilters.Actions
{
    [Extension]
    class Crop : BaseAddinEffect
    {
        private CropForm fm = null;

        #region Properties override
        public override AddinType Type
        {
            get { return ( AddinType.Action ); }
        }

        private string _name = "Crop";
        public override string Name
        {
            get { return _name; }
        }

        private string _displayname = T("Crop");
        public override string DisplayName
        {
            get { return _( _displayname ); }
            set { _displayname = value; }
        }

        public override string CategoryName
        {
            get { return ( "Edit"); }
        }

        private string _displayGroupName = T("Edit");
        public override string DisplayCategoryName
        {
            get { return _( _displayGroupName ); }
            set { _displayGroupName = value; }
        }

        private string _description = T("Crop Image");
        public override string Description
        {
            get { return _( _description ); }
            set { _description = value; }
        }

        public override Image LargeIcon
        {
            get{return Properties.Resources.Crop_32x;}
        }

        public override Image SmallIcon
        {
            get { return Properties.Resources.Crop_16x; }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="img"></param>
        protected override void SetParams( Form form, System.Drawing.Image img = null )
        {
            if ( Params.ContainsKey( "CropMode" ) )
                ( form as CropForm ).ParamCropMode = Params["CropMode"];
            if ( Params.ContainsKey( "CropSide" ) )
                ( form as CropForm ).ParamCropSide = Params["CropSide"];
            if ( Params.ContainsKey( "CropAspect" ) )
                ( form as CropForm ).ParamCropAspect = Params["CropAspect"];
            if ( Params.ContainsKey( "CropRegion" ) )
                ( form as CropForm ).ParamCropRegion = Params["CropRegion"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected override void GetParams( Form form )
        {
            if ( Params.ContainsKey( "CropMode" ) )
                Params["CropMode"] = ( form as CropForm ).ParamCropMode;
            else
                Params.Add( "CropMode", ( form as CropForm ).ParamCropMode );

            if ( Params.ContainsKey( "CropSide" ) )
                Params["CropSide"] = ( form as CropForm ).ParamCropSide;
            else
                Params.Add( "CropSide", ( form as CropForm ).ParamCropSide );

            if ( Params.ContainsKey( "CropAspect" ) )
                Params["CropAspect"] = ( form as CropForm ).ParamCropAspect;
            else
                Params.Add( "CropAspect", ( form as CropForm ).ParamCropAspect );

            if ( Params.ContainsKey( "CropRegion" ) )
                Params["CropRegion"] = ( form as CropForm ).ParamCropRegion;
            else
                Params.Add( "CropRegion", ( form as CropForm ).ParamCropRegion );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public override void Show( Form parent = null, bool setup = false )
        {
            ImgDst = ImgSrc;

            if ( fm == null )
            {
                fm = new CropForm( this );
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
                Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.GetImageSelection, 0 ) );
            }
            if ( fm.ShowDialog() == DialogResult.OK )
            {
                _success = true;
                GetParams( fm );
                if ( !setup )
                {
                    ImgDst = Apply( ImgSrc );
                    Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.SetImageSelection, new RectangleF( 0, 0, 0, 0 ) ) );
                }
            }
            else
                _success = false;
            if ( fm != null )
            {
                fm.Dispose();
                fm = null;
            }
        }

        public override Image Apply( Image image )
        {
            Rectangle region = new Rectangle(0, 0, ImgSrc.Width, ImgSrc.Height);

            if ( Params.ContainsKey( "CropRegion" ) )
                region = (Rectangle) Params["CropRegion"].Value;

            if ( Params.ContainsKey( "CropSide" ) )
            {
                var side = (SideType)Params["CropSide"].Value;
                region = Rectangle.Round( AddinUtils.AdjustRegion( region, ImgSrc, side ) );
            }

            if ( region.Width > 0 && region.Height > 0 )
            {
                Accord.Imaging.Filters.Crop filter = new Accord.Imaging.Filters.Crop(region);
                Bitmap dst = filter.Apply( AddinUtils.CloneImage(ImgSrc) as Bitmap );
                AddinUtils.CloneExif( ImgSrc, dst );
                return (dst) ;
            }
            else
                return ( ImgSrc );
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
            switch ( cmd )
            {
                case AddinCommand.GetImageSelection:
                    if ( fm is CropForm )
                    {
                        result = fm.GetImageSelection();
                    }
                    break;
                case AddinCommand.SetImageSelection:
                    if ( fm is CropForm )
                    {
                        if ( args.Length > 0 )
                        {
                            if ( args[0] is Rectangle )
                                fm.SetImageSelection( (Rectangle) args[0] );
                            else if ( args[0] is RectangleF )
                                fm.SetImageSelection( (RectangleF) args[0] );
                        }
                    }
                    break;
                case AddinCommand.ApplyTiming:
                    break;
                default:
                    break;
            }
            return ( true );
        }
    }
}
