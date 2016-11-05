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
        public override string GroupName
        {
            get { return ( "Edit" ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayGroupName = T("Edit");
        public override string DisplayGroupName
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public override void Show( Form parent = null )
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

                Translate( fm );
            }
            if ( fm.ShowDialog() == DialogResult.OK )
            {
                _success = true;
                if ( Params.ContainsKey( "Mode" ) )
                    Params["Mode"] = fm.GetMode( "Mode" );
                else
                    Params.Add( "Mode", fm.GetMode( "Mode" ) );

                if ( Params.ContainsKey( "Angle" ) )
                    Params["Angle"] = fm.GetAngle( "Angle" );
                else
                    Params.Add( "Angle", fm.GetAngle( "Angle" ) );

                if ( Params.ContainsKey( "KeepSize" ) )
                    Params["KeepSize"] = fm.GetKeepSize( "KeepSize" );
                else
                    Params.Add( "KeepSize", fm.GetKeepSize( "KeepSize" ) );

                ImgDst = Apply( ImgSrc );
            }
            else
                _success = false;
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
            if ( image != null )
            {
                RotateFlipType flip = (RotateFlipType)Params["Mode"].Value;
                double angle = (double)Params["Angle"].Value;
                bool keep = (bool)Params["KeepSize"].Value;

                Image dst = RotateForm.RotateImage( image, flip, angle, keep );
                AddinUtils.CloneExif( image, dst );
                return ( dst );
            }
            return ( image );
        }
    }
}
