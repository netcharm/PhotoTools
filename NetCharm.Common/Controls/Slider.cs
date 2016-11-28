using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetCharm.Common.Controls
{
    [ToolboxBitmap( typeof( TrackBar ) )]
    public partial class Slider : TrackBar
    {
        public Slider()
        {
            InitializeComponent();
            SetStyle( ControlStyles.UserPaint, true );
        }

        //protected override void OnPaint( PaintEventArgs pe )
        //{
        //    //base.OnPaint( pe );
        //    //pe.Graphics.FillRectangle( Brushes.AliceBlue, pe.ClipRectangle );
        //}

        protected override void OnPaintBackground( PaintEventArgs pe )
        {
            base.OnPaint( pe );
            pe.Graphics.FillRectangle( Brushes.AliceBlue, pe.ClipRectangle );
        }

    }
}
