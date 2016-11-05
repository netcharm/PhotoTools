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
    [Extension]
    class Grayscale : BaseAddinEffect
    {
        GrayscaleForm fm = null;

        #region Properties override
        public override AddinType Type
        {
            get { return ( AddinType.Effect ); }
        }

        private string _name = "Grayscale";
        public override string Name
        {
            get { return _name; }
        }

        private string _displayname = T("Grayscale");
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

        private string _description = T("Convert Image to Grayscale");
        public override string Description
        {
            get { return _( _description ); }
            set { _description = value; }
        }

        public override Image LargeIcon
        {
            get { return Properties.Resources.Grayscale_32x; }
        }

        public override Image SmallIcon
        {
            get { return Properties.Resources.Grayscale_16x; }
        }

        #endregion

        #region Method override
        protected override void GetParams( Form form )
        {
            //base.GetParams( form );
        }

        protected override void SetParams( Form form, Image img = null )
        {
            //base.SetParams( form, img );
        }

        public override void Show( Form parent = null )
        {
            _success = false;
            if ( fm == null )
            {
                fm = new GrayscaleForm( this );
                fm.host = Host;
                fm.Text = DisplayName;
                fm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
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
                ImgDst = Apply( ImgSrc );
                Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.SetImageSelection, new RectangleF( 0, 0, 0, 0 ) ) );
            }
            if ( fm != null )
            {
                fm.Dispose();
                fm = null;
            }
        }

        public override Image Apply( Image image )
        {
            return base.Apply( image );
        }

        public override bool Command( AddinCommand cmd, out object result, params object[] args )
        {
            return base.Command( cmd, out result, args );
        }
        #endregion
    }
}
