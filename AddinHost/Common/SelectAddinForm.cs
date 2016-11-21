using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExtensionMethods;

namespace NetCharm.Image.Addins.Common
{
    internal enum GroupMode
    {
        None = 0,
        Type = 1,
        Category = 2
    }

    public partial class SelectAddinForm : Form
    {
        private AddinHost Host = null;

        private List<ListViewItem> AddinItems = new List<ListViewItem>();
        private GroupMode categoryMode = GroupMode.None;

        internal List<IAddin> GetSelectedAddins()
        {
            var result = new List<IAddin>();
            foreach ( ListViewItem item in lvAddins.SelectedItems )
            {
                result.Add( item.Tag as IAddin );
            }
            return ( result );
        }

        private ListViewGroup GetGroup( string GroupName, string DisplayGroupName )
        {
            ListViewGroup result = null;
            foreach ( ListViewGroup group in lvAddins.Groups )
            {
                if ( string.Equals( group.Name, GroupName, StringComparison.CurrentCultureIgnoreCase ) )
                {
                    result = group;
                    break;
                }
            }
            if ( !( result is ListViewGroup ) )
            {
                result = new ListViewGroup( GroupName, DisplayGroupName );
                lvAddins.Groups.Add( result );
            }

            return ( result );
        }

        private ListViewGroup GetGroup( IAddin filter, GroupMode category = GroupMode.None )
        {
            ListViewGroup result = null;
            if ( category == GroupMode.Type )
            {
                var grpName = filter.Type.ToString();
                result = GetGroup( grpName, AddinUtils._( Host, grpName ) );
            }
            else if ( category == GroupMode.Category )
                result = GetGroup( filter.CategoryName, filter.DisplayCategoryName );
            else
                result = GetGroup( "None", AddinUtils._( Host, AddinUtils.T( "None" ) ) );
            return ( result );
        }

        private void ListAddins( GroupMode groupMode = GroupMode.Type )
        {
            #region Add addin to Listview & Category by addin Group
            lvAddins.BeginUpdate();
            if ( lvAddins.Items.Count <= 0 )
            {
                foreach ( var item in AddinItems )
                {
                    item.Group = GetGroup( item.Tag as IAddin, categoryMode );
                    lvAddins.Items.Add( item );
                }
            }
            else
            {
                foreach ( ListViewItem item in lvAddins.Items )
                {
                    item.Group = GetGroup( item.Tag as IAddin, categoryMode );
                }
            }
            lvAddins.EndUpdate();
            #endregion
        }

        public SelectAddinForm()
        {
            InitializeComponent();
            //AddinUtils.Translate( null, this );
            this.Translate();
        }

        public SelectAddinForm(AddinHost host)
        {
            Host = host;
            InitializeComponent();
            //AddinUtils.Translate( null, this );
            this.Translate();
        }

        private void SelectAddinForm_Load( object sender, EventArgs e )
        {
            lvAddins.Clear();
            lvAddins.Groups.Clear();

            ilLarge.Images.Clear();
            ilSmall.Images.Clear();

            var addins = new Dictionary<string, IAddin>[] { Host.Actions, Host.Effects };

            var addinList = addins.SelectMany(dict => dict)
                         .ToDictionary(pair => pair.Key, pair => pair.Value);

            AddinItems.Clear();
            foreach ( var addin in addinList )
            {
                if ( string.Equals( Host.CurrentFilter.Name, addin.Value.Name, StringComparison.CurrentCultureIgnoreCase ) ) continue;

                ilLarge.Images.Add( addin.Value.LargeIcon );
                ilSmall.Images.Add( addin.Value.SmallIcon );

                var lvi = new ListViewItem();
                lvi.Text = addin.Value.DisplayName;
                lvi.Tag = addin.Value;
                lvi.ImageIndex = ilLarge.Images.Count - 1;
                AddinItems.Add( lvi );
            }
            lvAddins.VirtualListSize = AddinItems.Count;

            btnGroupType.PerformClick();
        }

        private void lvAddins_DoubleClick( object sender, EventArgs e )
        {
            if(lvAddins.SelectedIndices.Count>0)
            {
                btnOk.PerformClick();
            }
        }

        private void lvAddins_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            if ( AddinItems.Count > 0 && e.ItemIndex >= 0 && e.ItemIndex < AddinItems.Count )
            {
                e.Item = AddinItems[e.ItemIndex];
                var addinItem = e.Item.Tag as IAddin;
                e.Item.Group = GetGroup( addinItem, categoryMode );
            }
        }

        private void btnGroup_Click( object sender, EventArgs e )
        {
            if(sender == btnGroupNone )
            {
                categoryMode = GroupMode.None;
            }
            else if ( sender == btnGroupType )
            {
                categoryMode = GroupMode.Type;
            }
            else if ( sender == btnGroupCategory )
            {
                categoryMode = GroupMode.Category;
            }
            ListAddins( categoryMode );
        }

    }
}
