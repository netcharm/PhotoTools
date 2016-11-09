using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetCharm.Image.Addins.Controls
{
    public partial class CornerSide : UserControl
    {
        private CornerRegionType _region = CornerRegionType.None;
        public CornerRegionType CornetRegion
        {
            get { return ( _region ); }
        }

        public CornerSide()
        {
            InitializeComponent();
        }

        private void btnCS_Click( object sender, EventArgs e )
        {
            if ( sender == btnCSTL )
            {
                _region = CornerRegionType.TopLeft;
            }
            else if ( sender == btnCSTC )
            {
                _region = CornerRegionType.TopCenter;
            }
            else if ( sender == btnCSTR )
            {
                _region = CornerRegionType.TopRight;
            }
            else if ( sender == btnCSML )
            {
                _region = CornerRegionType.MiddleLeft;
            }
            else if ( sender == btnCSMC )
            {
                _region = CornerRegionType.MiddleCenter;
            }
            else if ( sender == btnCSMR )
            {
                _region = CornerRegionType.MiddleRight;
            }
            else if ( sender == btnCSBL )
            {
                _region = CornerRegionType.BottomLeft;
            }
            else if ( sender == btnCSBC )
            {
                _region = CornerRegionType.BottomCenter;
            }
            else if ( sender == btnCSBR )
            {
                _region = CornerRegionType.BottomRight;
            }
            else
            {
                _region = CornerRegionType.None;
            }
        }
    }
}
