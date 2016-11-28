using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image;

namespace NetCharm.Common.Controls
{
    public partial class CornerSide : UserControl
    {
        private CornerRegionType _region = CornerRegionType.None;
        /// <summary>
        /// 
        /// </summary>
        public CornerRegionType CornetRegion
        {
            get { return ( _region ); }
            set { SetRegion( value ); }
        }

        public event EventHandler CornetRegionClick;

        /// <summary>
        /// 
        /// </summary>
        public CornerSide()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CornerRegionType GetRegion()
        {
            if ( btnCSTL.Checked ) _region = CornerRegionType.TopLeft;
            else if ( btnCSTC.Checked ) _region = CornerRegionType.TopCenter;
            else if ( btnCSTR.Checked ) _region = CornerRegionType.TopRight;
            else if ( btnCSML.Checked ) _region = CornerRegionType.MiddleLeft;
            else if ( btnCSMC.Checked ) _region = CornerRegionType.MiddleCenter;
            else if ( btnCSMR.Checked ) _region = CornerRegionType.MiddleRight;
            else if ( btnCSBL.Checked ) _region = CornerRegionType.BottomLeft;
            else if ( btnCSBC.Checked ) _region = CornerRegionType.BottomCenter;
            else if ( btnCSBR.Checked ) _region = CornerRegionType.BottomRight;
            else _region = CornerRegionType.None;

            return ( _region );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        private void SetRegion( CornerRegionType region )
        {
            _region = region;
            switch (region)
            {
                case CornerRegionType.TopLeft:
                    btnCSTL.PerformClick();
                    break;
                case CornerRegionType.TopCenter:
                    btnCSTC.PerformClick();
                    break;
                case CornerRegionType.TopRight:
                    btnCSTR.PerformClick();
                    break;
                case CornerRegionType.MiddleLeft:
                    btnCSML.PerformClick();
                    break;
                case CornerRegionType.MiddleCenter:
                    btnCSMC.PerformClick();
                    break;
                case CornerRegionType.MiddleRight:
                    btnCSMR.PerformClick();
                    break;
                case CornerRegionType.BottomLeft:
                    btnCSBL.PerformClick();
                    break;
                case CornerRegionType.BottomCenter:
                    btnCSBC.PerformClick();
                    break;
                case CornerRegionType.BottomRight:
                    btnCSBR.PerformClick();
                    break;
                default:
                    btnCSTL.Checked = false;
                    btnCSTC.Checked = false;
                    btnCSTR.Checked = false;
                    btnCSML.Checked = false;
                    btnCSMC.Checked = false;
                    btnCSMR.Checked = false;
                    btnCSBL.Checked = false;
                    btnCSBC.Checked = false;
                    btnCSBR.Checked = false;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            this.CornetRegionClick?.Invoke( this, e );
        }
    }
}
