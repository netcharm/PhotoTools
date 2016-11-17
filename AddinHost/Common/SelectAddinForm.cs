using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetCharm.Image.Addins.Common
{
    public partial class SelectAddinForm : Form
    {
        private AddinHost Host = null;

        public SelectAddinForm()
        {
            InitializeComponent();
        }

        public SelectAddinForm(AddinHost host)
        {
            Host = host;
            InitializeComponent();
        }

        internal List<IAddin> GetSelectedAddins()
        {
            var result = new List<IAddin>();
            foreach ( ListViewItem item in lvAddins.SelectedItems)
            {
                result.Add( item.Tag as IAddin );
            }
            return ( result );
        }

        private void SelectAddinForm_Load( object sender, EventArgs e )
        {
            if ( Host is AddinHost )
            {
                lvAddins.Clear();
                lvAddins.Groups.Clear();

                ilLarge.Images.Clear();
                ilSmall.Images.Clear();

                var grpAction = new ListViewGroup("Actions", AddinUtils._( Host, "Actions" ));
                lvAddins.Groups.Add( grpAction );
                foreach ( var addin in Host.Actions )
                {
                    if ( string.Equals( Host.CurrentFilter.Name, addin.Value.Name, StringComparison.CurrentCultureIgnoreCase ) ) continue;

                    ilLarge.Images.Add( addin.Value.LargeIcon );
                    ilSmall.Images.Add( addin.Value.SmallIcon );

                    var lvi = new ListViewItem(grpAction);
                    lvi.Text = addin.Value.DisplayName;
                    lvi.Tag = addin.Value;
                    lvi.ImageIndex = ilLarge.Images.Count - 1;
                    lvAddins.Items.Add( lvi );
                }

                var grpEffect = new ListViewGroup("Effects", AddinUtils._( Host, "Effects" ));
                lvAddins.Groups.Add( grpEffect );
                foreach ( var addin in Host.Effects )
                {
                    if ( string.Equals( Host.CurrentFilter.Name, addin.Value.Name, StringComparison.CurrentCultureIgnoreCase ) ) continue;

                    ilLarge.Images.Add( addin.Value.LargeIcon );
                    ilSmall.Images.Add( addin.Value.SmallIcon );

                    var lvi = new ListViewItem(grpEffect);
                    lvi.Text = addin.Value.DisplayName;
                    lvi.Tag = addin.Value;
                    lvi.ImageIndex = ilLarge.Images.Count - 1;
                    lvAddins.Items.Add( lvi );
                }
                //Host.Actions
            }
        }
    }
}
