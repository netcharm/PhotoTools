﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace InternalFilters.Actions
{
    public partial class PinObjectForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private Image thumb = null;
        private Image thumbBackup = null;

        private PinObjectMode mode = PinObjectMode.Picture;
        internal ParamItem ParamMode
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "PinObjectMode";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = mode.GetType();
                pi.Value = mode;
                return ( pi );
            }
            set { mode = (PinObjectMode) value.Value; }
        }

        private List<ListViewItem> effects = new List<ListViewItem>();

        public PinObjectForm()
        {
            InitializeComponent();
        }

        public PinObjectForm( IAddin filter )
        {
            InitializeComponent();

            this.addin = filter;
            this.Text = addin.DisplayName;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            toolTip.ToolTipTitle = addin.DisplayName;
            AddinUtils.Translate( addin, this, toolTip );
        }

        private void StampObjectForm_Load( object sender, EventArgs e )
        {
            thumb = AddinUtils.CreateThumb( addin.ImageData, imgPreview.Size );
            imgPreview.Image = thumb;
        }

        private void lvFilters_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            try
            {
                if ( e.ItemIndex >= 0 && e.ItemIndex < effects.Count && effects.Count > 0 )
                {
                    e.Item = effects[e.ItemIndex];
                    e.Item.BackColor = ( e.ItemIndex % 2 == 1 ) ? Color.AliceBlue : e.Item.BackColor;
                }
                else
                {
                    e.Item = new ListViewItem( new string[] { "None" } );
                    e.Item.BackColor = Color.MediumAquamarine;
                }
            }
            catch ( Exception ex )
            {
                e.Item = new ListViewItem( new string[] { "Error" } );
                e.Item.BackColor = Color.LightPink;
            }
        }

        private void btnOriginal_Click( object sender, EventArgs e )
        {
            if ( btnOriginal.Checked )
            {
                thumbBackup = imgPreview.Image;
                imgPreview.Image = thumb;
            }
            else
            {
                if ( thumbBackup is Image )
                    imgPreview.Image = thumbBackup;
            }
        }

        private void btnEffectAdd_Click( object sender, EventArgs e )
        {
            foreach(IAddin filter in AddinUtils.ShowAddinsDialog())
            {
                addin.Filters.Add( filter );
                ilLarge.Images.Add( filter.LargeIcon );
                ilSmall.Images.Add( filter.SmallIcon );
                effects.Add( new ListViewItem( filter.DisplayName ) );
                effects.Last().Tag = filter;
                effects.Last().ImageIndex = ilLarge.Images.Count;
            }
            lvFilters.VirtualListSize = effects.Count;
        }

        private void btnEffectRemove_Click( object sender, EventArgs e )
        {
            foreach ( int i in lvFilters.SelectedIndices )
            {
                ListViewItem item = effects[i];

                IAddin filter = item.Tag as IAddin;
                if ( filter is IAddin )
                {
                    if ( addin.Filters.IndexOf( filter ) >= 0 )
                        addin.Filters.Remove( filter );
                    ilLarge.Images.RemoveAt( item.ImageIndex );
                    ilSmall.Images.RemoveAt( item.ImageIndex );
                    effects.Remove( item );
                }
            }
            lvFilters.VirtualListSize = effects.Count;
        }

        private void btnEffectUp_Click( object sender, EventArgs e )
        {
            foreach(int i in lvFilters.SelectedIndices )
            {
                if ( i <= 0 ) continue;
                var fi = effects[i];
                effects[i] = effects[i-1];
                effects[i - 1] = fi;
            }
            lvFilters.Invalidate();
        }

        private void btnEffectDown_Click( object sender, EventArgs e )
        {
            foreach ( int i in lvFilters.SelectedIndices )
            {
                if ( i >= effects.Count-1 ) continue;
                var fi = effects[i];
                effects[i] = effects[i + 1];
                effects[i + 1] = fi;
            }
            lvFilters.Invalidate();
        }

    }
}