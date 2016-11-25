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
            get { return ( dialog.ShowApply ); }
            set { dialog.ShowApply = value; }
        }

        public Color Color
        {
            get { return ( dialog.Color ); }
            set { dialog.Color = value; }
        }

        public ColorDialogEx()
        {
            InitializeComponent();
            if( this.Apply != null )
            {
                dialog.Apply += new System.EventHandler( this.Apply );
            }
        }

        public DialogResult ShowDialog()
        {
            dialog.Color = Color;
            return ( dialog.ShowDialog() );
        }

        public DialogResult ShowDialog( Color color )
        {
            Color = color;
            return ( ShowDialog() );
        }

        //private void btnApply_Click( object sender, EventArgs e )
        //{
        //    this.Apply?.Invoke( this, e );
        //}
    }
}
