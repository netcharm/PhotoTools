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
    [ToolboxBitmap( typeof( ColorDialog ) )]
    [ToolboxItem( true )]
    public partial class ColorDialogEx : Component
    {
        private NetCharm.Common.ColorDialog dialog = new NetCharm.Common.ColorDialog();

        public event EventHandler Apply;

        private bool _apply = true;
        public bool ShowApply
        {
            get { return ( _apply ); }
            set { _apply = value; }
        }

        private Color _color = default(Color);
        public Color Color
        {
            get
            {
                _color = dialog.Color;
                return ( _color );
            }
            set { _color = value; }
        }

        private Color[] _colors = new Color[] { };
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public Color[] CustomColors
        {
            get
            {
                _colors = dialog.CustomColors;
                return ( _colors );
            }
            set
            {
                _colors = value;
                dialog.CustomColors = value;
            }
        }

        public ColorDialogEx()
        {
            InitializeComponent();
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
            dialog.Color = _color;
            dialog.ShowApply = _apply;
        }

        public ColorDialogEx(Color color)
        {
            InitializeComponent();
            Color = color;
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
            dialog.Color = _color;
            dialog.ShowApply = _apply;
        }

        public DialogResult ShowDialog()
        {
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
            dialog.Color = _color;
            dialog.ShowApply = _apply;
            return ( dialog.ShowDialog() );
        }

        public DialogResult ShowDialog( Color color )
        {
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
            _color = color;
            return ( ShowDialog() );
        }
    }
}
