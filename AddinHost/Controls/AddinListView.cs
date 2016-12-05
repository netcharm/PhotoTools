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
    //using System.Reflection;
    using ExtensionMethods;
    using ParamList = Dictionary<string, ParamItem>;

    public partial class AddinListView : UserControl
    {
        private Size toolbarSize = new Size(32, 32);

        public bool ShowToolbar
        {
            get { return (flowEffectTool.Visible); }
            set
            {
                flowEffectTool.Visible = value;
                this.OnResize( EventArgs.Empty );
                PerformLayout();

                //typeof( Control ).GetMethod( "OnResize",
                //    BindingFlags.Instance | BindingFlags.NonPublic )
                //    .Invoke( lvFilters, new object[] { EventArgs.Empty } );
            }
        }

        private System.Drawing.Image _image = null;
        public System.Drawing.Image Image
        {
            get { return ( _image ); }
            set { _image = value; }
        }

        private List<ListViewItem> effects = new List<ListViewItem>();
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [Browsable(false)]
        public List<IAddin> Filters
        {
            get
            {
                var result = effects.Select(o => o.Tag as IAddin ).ToList();
                return ( result );
            }
            set
            {
                effectParams.Clear();
                ilLarge.Images.Clear();
                ilSmall.Images.Clear();
                effects.Clear();
                foreach ( var filter in value )
                {
                    ilLarge.Images.Add( filter.LargeIcon );
                    ilSmall.Images.Add( filter.SmallIcon );
                    effects.Add( new ListViewItem( filter.DisplayName ) );
                    effects.Last().Selected = false;
                    effects.Last().Tag = filter;
                    effects.Last().ImageIndex = ilLarge.Images.Count;
                    effects.Last().Checked = true;
                    effectParams[filter] = filter.Params.Clone();
                }
            }
        }

        //private Dictionary<IAddin, ParamList> effectParams = new Dictionary<IAddin, ParamList>();
        private Dictionary<IAddin, Dictionary<string, ParamItem>> effectParams = new Dictionary<IAddin, Dictionary<string, ParamItem>>();
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [Browsable(false)]
        public Dictionary<string, Dictionary<string, ParamItem>> FilterParams
        {
            get
            {
                var result = new Dictionary<string, Dictionary<string, ParamItem>>();
                foreach(var kv in effectParams)
                {
                    result[kv.Key.Name] = kv.Value.Clone();
                }
                return ( result );
                //return ( (Dictionary<string, Dictionary<string, ParamItem>>) effectParams.Select(
                //    o => new { Key = o.Key.Name, Value = o.Value.Clone() } )
                //);
            }
            set
            {
                effectParams.Clear();
                var Host = AddinHost.GetHost();
                if ( Host is AddinHost )
                {
                    foreach ( var kv in value )
                    {
                        if ( Host.Addins.ContainsKey( kv.Key ) )
                        {
                            var filter = Host.Addins[kv.Key];
                            effectParams[filter] = kv.Value.Clone();
                        }
                    }
                }
            }
        }

        public FlowDirection Flow
        {
            get { return ( flowEffects.FlowDirection ); }
            set
            {
                if ( value == FlowDirection.TopDown || value == FlowDirection.BottomUp )
                {
                    flowEffects.FlowDirection = FlowDirection.TopDown;
                    flowEffectTool.FlowDirection = FlowDirection.LeftToRight;
                    this.MinimumSize = new Size( 160, 210 );
                }
                else
                {
                    flowEffects.FlowDirection = FlowDirection.LeftToRight;
                    flowEffectTool.FlowDirection = FlowDirection.TopDown;
                    this.MinimumSize = new Size( 210, 160 );
                }
                flowEffects.PerformLayout();
            }
        }

        public AddinListView()
        {
            InitializeComponent();
        }

        private void lvFilters_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            try
            {
                if ( effects.Count > 0 && e.ItemIndex >= 0 && e.ItemIndex < effects.Count )
                {
                    e.Item = effects[e.ItemIndex];
                    if ( lvFilters.View == View.Details )
                    {
                        e.Item.BackColor = ( e.ItemIndex % 2 == 1 ) ? Color.AliceBlue : e.Item.BackColor;
                    }
                }
                else
                {
                    e.Item = new ListViewItem( new string[] { "None" } );
                    e.Item.BackColor = Color.MediumAquamarine;
                }
            }
            catch ( Exception )
            {
                e.Item = new ListViewItem( new string[] { "Error" } );
                e.Item.BackColor = Color.LightPink;
            }
        }

        private void lvFilters_DoubleClick( object sender, EventArgs e )
        {
            ListView lv = (ListView)sender;
            if ( lvFilters.FocusedItem is ListViewItem )
            {
                var filter = lvFilters.FocusedItem.Tag as IAddin;
                if ( filter is IAddin )
                {
                    lvFilters.FocusedItem.Checked = filter.Enabled;

                    if( effectParams.ContainsKey( filter ) )
                        filter.Params = effectParams[filter];
                    if ( _image is System.Drawing.Image ) filter.ImageData = _image;
                    if ( filter.Show( this.FindForm(), true ) == DialogResult.OK )
                    {
                        effectParams[filter] = filter.Params.Clone();
                    }
                }
            }
        }

        private void lvFilters_MouseClick( object sender, MouseEventArgs e )
        {
            ListView lv = (ListView)sender;
            ListViewItem lvi = lv.GetItemAt(e.X, e.Y);
            if ( lvi is ListViewItem )
            {
                if ( e.X < ( lvi.Bounds.Left + 16 ) )
                {
                    lvi.Checked = !lvi.Checked;
                    if ( ( lvi.Tag as IAddin ) is IAddin )
                    {
                        ( lvi.Tag as IAddin ).Enabled = lvi.Checked;
                    }
                    lv.Invalidate( lvi.Bounds );
                }
            }
        }

        private void lvFilters_KeyPress( object sender, KeyPressEventArgs e )
        {
            if ( e.KeyChar == (char) Keys.Space )
            {
                ListView lv = (ListView)sender;
                ListViewItem lvi = lv.FocusedItem;
                if ( lvi is ListViewItem )
                {
                    lvi.Checked = !lvi.Checked;
                    if ( ( lvi.Tag as IAddin ) is IAddin )
                    {
                        ( lvi.Tag as IAddin ).Enabled = lvi.Checked;
                    }
                    lv.Invalidate( lvi.Bounds );
                }
            }
        }

        private void btnEffectAdd_Click( object sender, EventArgs e )
        {
            lvFilters.BeginUpdate();
            foreach ( IAddin filter in AddinUtils.ShowAddinsDialog() )
            {
                ilLarge.Images.Add( filter.LargeIcon );
                ilSmall.Images.Add( filter.SmallIcon );
                effects.Add( new ListViewItem( filter.DisplayName ) );
                effects.Last().Selected = false;
                effects.Last().Tag = filter;
                effects.Last().ImageIndex = ilLarge.Images.Count;
                effects.Last().Checked = true;
            }
            lvFilters.VirtualListSize = effects.Count;
            lvFilters.EndUpdate();
        }

        private void btnEffectRemove_Click( object sender, EventArgs e )
        {
            List<int> selected = new List<int>();
            foreach ( int i in lvFilters.SelectedIndices )
            {
                selected.Add( i );
            }
            selected.Reverse();
            lvFilters.BeginUpdate();
            foreach ( int i in selected )
            {
                ListViewItem item = effects[i];

                IAddin filter = item.Tag as IAddin;

                //ilLarge.Images.RemoveAt( item.ImageIndex );
                //ilSmall.Images.RemoveAt( item.ImageIndex );
                effects.Remove( item );
            }
            lvFilters.SelectedIndices.Clear();
            lvFilters.VirtualListSize = effects.Count;
            lvFilters.EndUpdate();
        }

        private void btnEffectUp_Click( object sender, EventArgs e )
        {
            lvFilters.BeginUpdate();
            foreach ( int i in lvFilters.SelectedIndices )
            {
                if ( i <= 0 ) continue;
                var fi = effects[i];
                effects[i] = effects[i - 1];
                effects[i - 1] = fi;
                lvFilters.Items[i - 1].Selected = true;
                lvFilters.Items[i].Selected = false;
            }
            lvFilters.EndUpdate();
        }

        private void btnEffectDown_Click( object sender, EventArgs e )
        {
            List<int> selected = new List<int>();
            foreach ( int i in lvFilters.SelectedIndices )
            {
                selected.Add( i );
            }
            selected.Reverse();
            lvFilters.BeginUpdate();
            foreach ( int i in selected )
            {
                if ( i >= effects.Count - 1 ) continue;
                var fi = effects[i];
                effects[i] = effects[i + 1];
                effects[i + 1] = fi;
                lvFilters.Items[i + 1].Selected = true;
                lvFilters.Items[i].Selected = false;
            }
            lvFilters.EndUpdate();
        }

        private void AddinListView_Resize( object sender, EventArgs e )
        {
            if ( flowEffects.FlowDirection == FlowDirection.TopDown || flowEffects.FlowDirection == FlowDirection.BottomUp )
            {
                flowEffectTool.Width = this.ClientSize.Width - flowEffectTool.Margin.Left - flowEffectTool.Margin.Right;
                flowEffectTool.Height = toolbarSize.Height;
                lvFilters.Width = this.ClientSize.Width - lvFilters.Margin.Left - lvFilters.Margin.Right;
                if(ShowToolbar)
                {
                    lvFilters.Height = this.ClientSize.Height - 
                        lvFilters.Margin.Top - lvFilters.Margin.Bottom -
                        flowEffectTool.Height -
                        flowEffectTool.Margin.Top - flowEffectTool.Margin.Bottom;
                }
                else
                {
                    lvFilters.Height = this.ClientSize.Height -
                        lvFilters.Margin.Top - lvFilters.Margin.Bottom;
                }
                this.MinimumSize = new Size( 160, 210 );
            }
            else
            {
                flowEffectTool.Width = toolbarSize.Width;
                flowEffectTool.Height = this.ClientSize.Height - flowEffectTool.Margin.Top - flowEffectTool.Margin.Bottom;
                if ( ShowToolbar )
                {
                    lvFilters.Width = this.ClientSize.Width - 
                                  lvFilters.Margin.Left - lvFilters.Margin.Right -
                                  flowEffectTool.Width -
                                  flowEffectTool.Margin.Left - flowEffectTool.Margin.Right;
                }
                else
                {
                    lvFilters.Width = this.ClientSize.Width -
                                  lvFilters.Margin.Left - lvFilters.Margin.Right;
                }
                lvFilters.Height = this.ClientSize.Height - lvFilters.Margin.Top - lvFilters.Margin.Bottom;
                this.MinimumSize = new Size( 210, 160 );
            }
        }
    }
}
