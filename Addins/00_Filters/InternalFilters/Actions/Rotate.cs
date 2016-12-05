using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Accord.Imaging.Filters;
using Mono.Addins;
using NetCharm.Image.Addins;

namespace InternalFilters.Actions
{
    [Extension]
    class Rotate : BaseAddinEffect
    {
        private RotateForm fm = null;

        /// <summary>
        /// 
        /// </summary>
        public override AddinType Type
        {
            get { return ( AddinType.Action ); }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _name = "Rotate";
        public override string Name
        {
            get { return ( _name ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = T("Rotate");
        public override string DisplayName
        {
            get { return ( _( _displayName ) ); }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override string CategoryName
        {
            get { return ( "Edit" ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayGroupName = T("Edit");
        public override string DisplayCategoryName
        {
            get { return _( _displayGroupName ); }
            set { _displayGroupName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = T("Rotate Image");
        public override string Description
        {
            get { return ( _( _description ) ); }
            set { _description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Image LargeIcon
        {
            get { return ( Properties.Resources.Rotate_32x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public override Image SmallIcon
        {
            get { return ( Properties.Resources.Rotate_16x ); }
        }

        private void InitParams()
        {
            Dictionary<string, object> kv = new Dictionary<string, object>();
            kv.Add( "Mode", RotateFlipType.RotateNoneFlipNone );
            kv.Add( "Angle", 0f );
            kv.Add( "KeepSize", false );

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
        /// <param name="parent"></param>
        public override DialogResult Show( Form parent = null, bool setup = false )
        {
            if ( fm == null )
            {
                fm = new RotateForm( this );
                fm.host = Host;
                fm.Text = DisplayName;
                fm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                fm.MaximizeBox = false;
                fm.MinimizeBox = false;
                fm.ShowIcon = false;
                fm.ShowInTaskbar = false;
                fm.StartPosition = FormStartPosition.CenterParent;

                if ( Params.Count == 0 ) InitParams();

                if ( Params.ContainsKey( "Mode" ) )
                    fm.SetMode( "Mode", Params["Mode"] );
                if ( Params.ContainsKey( "Angle" ) )
                    fm.SetAngle( "Angle", Params["Angle"] );
                if ( Params.ContainsKey( "KeepSize" ) )
                    fm.SetKeepSize( "KeepSize", Params["KeepSize"] );

                Translate( fm );
            }
            var result = fm.ShowDialog();
            if ( result == DialogResult.OK )
            {
                _success = true;

                Params["Mode"] = fm.GetMode( "Mode" );
                Params["Angle"] = fm.GetAngle( "Angle" );
                Params["KeepSize"] = fm.GetKeepSize( "KeepSize" );

                if ( !setup )
                {
                    ImgDst = Apply( ImgSrc );
                }
            }
            else
                _success = false;
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
            if ( image is Image )
            {
                if ( Params.Count == 0 ) InitParams();

                RotateFlipType flip = (RotateFlipType)Params["Mode"].Value;
                double angle = Convert.ToDouble(Params["Angle"].Value);
                bool keep = Convert.ToBoolean(Params["KeepSize"].Value);

                Image dst = RotateForm.RotateImage( image, flip, angle, keep );
                AddinUtils.CloneExif( image, dst );
                return ( dst );
            }
            return ( image );
        }
    }
}
