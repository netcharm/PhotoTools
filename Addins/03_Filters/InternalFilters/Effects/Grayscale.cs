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

    }
}
