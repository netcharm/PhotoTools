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
    public partial class ColorDialogEx : Component
    {
        private NetCharm.Common.ColorDialog dialog = new NetCharm.Common.ColorDialog();

        public event EventHandler Apply;

        public bool ShowApply
        {
            get
            {
                return ( dialog is ColorDialog ? dialog.ShowApply : true );
            }
            set { dialog.ShowApply = value; }
        }

        public Color Color
        {
            get { return ( dialog is ColorDialog ? dialog.Color : default(Color) ); }
            set { dialog.Color = value; }
        }

        public ColorDialogEx()
        {
            InitializeComponent();
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
        }

        public ColorDialogEx(Color color)
        {
            InitializeComponent();
            Color = color;
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
        }

        public DialogResult ShowDialog()
        {
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }

            dialog.Color = Color;
            return ( dialog.ShowDialog() );
        }

        public DialogResult ShowDialog( Color color )
        {
            if ( this.Apply is EventHandler )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }

            Color = color;
            return ( ShowDialog() );
        }
    }
}
