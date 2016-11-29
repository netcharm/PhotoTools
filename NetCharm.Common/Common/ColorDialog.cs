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

        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public Color[] CustomColors
        {
            get
            {
                return ( colorGrid.CustomColors.ToArray() );
            }
            set
            {
                colorGrid.CustomColors.AddRange( value );
            }
        }

        public bool ShowApply
        {
            get { return ( btnApply.Visible ); }
            set { btnApply.Visible = value; }
        }

        public ColorDialog()
        {
            InitializeComponent();
            colorGrid.CustomColors.Clear();
            //this.DesignMode
        }

        public ColorDialog( Color color )
        {
            InitializeComponent();
            colorGrid.CustomColors.Clear();

            Color = color;
        }

        private void ColorDialog_Load( object sender, EventArgs e )
        {
            this.Translate();

            colorPanel.BackColor = colorManager.Color;

            btnOk.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;

            if( cmPalette.Items.Count<=0 )
            {
                foreach ( var palette in Enum.GetValues( colorGrid.Palette.GetType() ) )
                {
                    var mi = new ToolStripMenuItem( palette.ToString() );
                    //mi.Click += cmPalette_Click;
                    mi.CheckOnClick = true;
                    cmPalette.Items.Add( mi );
                    if ( mi.Text == colorGrid.Palette.ToString() )
                        mi.Checked = true;
                }
            }
        }

        private void cmPalette_ItemClicked( object sender, ToolStripItemClickedEventArgs e )
        {
            if ( e.ClickedItem is ToolStripMenuItem )
            {
                colorGrid.Palette = (Cyotek.Windows.Forms.ColorPalette) Enum.Parse( typeof( Cyotek.Windows.Forms.ColorPalette ), e.ClickedItem.Text );
                foreach ( var item in cmPalette.Items )
                {
                    if ( item != e.ClickedItem ) ( item as ToolStripMenuItem ).Checked = false;
                }
            }
        }

        private void colorEditorManager_ColorChanged( object sender, EventArgs e )
        {
            colorPanel.BackColor = colorManager.Color;
        }

        private void btnPalette_Click( object sender, EventArgs e )
        {
            int x = btnPalette.Width - btnPalette.Padding.Right - cmPalette.Width;
            int y = btnPalette.Height;
            cmPalette.Show( btnPalette, new Point( x, y ) );
        }

        private void btnApply_Click( object sender, EventArgs e )
        {
            this.Apply?.Invoke( this, e );
        }

    }
}
