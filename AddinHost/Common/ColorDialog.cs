using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExtensionMethods;

namespace NetCharm.Common
{
    public partial class ColorDialog : Form
    {
        public event EventHandler Apply;

        public Color Color
        {
            get { return ( colorManager.Color ); }
            set { colorManager.Color = value; }
        }

        public bool ShowApply
        {
            get { return ( btnApply.Visible ); }
            set { btnApply.Visible = value; }
        }

        public ColorDialog()
        {
            InitializeComponent();
            //AddinUtils.Translate( null, this );
            this.Translate();
        }

        private void ColorDialog_Load( object sender, EventArgs e )
        {
            colorPanel.BackColor = colorManager.Color;

            btnOk.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void colorEditorManager1_ColorChanged( object sender, EventArgs e )
        {
            colorPanel.BackColor = colorManager.Color;
        }

        private void btnApply_Click( object sender, EventArgs e )
        {
            this.Apply?.Invoke( this, e );
        }
    }
}
