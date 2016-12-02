using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExtensionMethods;
using NetCharm.Common.Controls;
using NetCharm.Image.Addins;

namespace InternalFilters.Actions
{
    using System.IO;
    using NetCharm.Common;
    using NetCharm.Image;
    using ParamList = Dictionary<string, ParamItem>;

    public partial class PinObjectForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private Image thumb = null;
        private Image thumbBackup = null;
        private Image picObject = null;
        private Image picObjectBackup = null;
        private Image picText = null;

        private NetCharm.Common.ColorDialog dlgColor = null;

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
            set { mode = (PinObjectMode) Convert.ToInt32( value.Value ); }
        }

        private PinOption option = new PinOption();
        internal ParamItem ParamOption
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "PinOption";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = option.GetType();
                pi.Value = option;
                return ( pi );
            }
            set
            {
                //option = this.addin.FromJSON<PinOption>( value.Value.ToString() );
                if ( value.Value is PinOption )
                    option = (PinOption) value.Value;
                else //if ( value.Value is JObject )
                {
                    var kv = option.FromJSON( value.Value );
                    option = kv == null ? option : kv;
                }
            }
        }

        private bool objectOnly = false;
        internal ParamItem ParamObjectOnly
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "PinObjectOnly";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = objectOnly.GetType();
                pi.Value = objectOnly;
                return ( pi );
            }
            set { objectOnly = (bool) value.Value; }
        }

        private List<ListViewItem> effects = new List<ListViewItem>();
        private List<ParamList> effectParams = new List<ParamList>();

        internal void LoadPicture(string picFile)
        {
            if(File.Exists(picFile))
            {
                picObject = AddinUtils.LoadImage( picFile );
                option.ImageCache = picObject as Bitmap;
                option.PictureFile = picFile;
                Preview();
            }
        }

        internal void GetOptions()
        {
            option.Blend = (float) Convert.ToDouble( slideBlend.Value );
            option.Opacity = (float) Convert.ToDouble( slideOpacity.Value );

            option.Pos = csSelect.CornetRegion;

            option.Enabled = chkEnabled.Checked;
            option.Tile = chkTile.Checked;

            option.Margin.X = (float) Convert.ToDouble( slideMarginX.Value );
            option.Margin.Y = (float) Convert.ToDouble( slideMarginY.Value );

            option.Offset.X = (float) Convert.ToDouble( slideOffsetX.Value );
            option.Offset.Y = (float) Convert.ToDouble( slideOffsetY.Value );

            option.Filters.Clear();
            for ( var i = 0; i < lvFilters.Items.Count; i++ )
            {
                var filter = (IAddin)lvFilters.Items[i].Tag;
                option.Filters[filter.Name] = option.FilterParams[filter].Clone();
            }

            option.Text = edText.Text;

            switch ( tabObject.SelectedIndex )
            {
                case 0:
                    mode = PinObjectMode.Picture;
                    break;
                case 1:
                    mode = PinObjectMode.Text;
                    break;
                case 2:
                    mode = PinObjectMode.Tag;
                    break;
            }
            option.Mode = mode;
        }

        internal void Preview()
        {
            if ( this.Tag is bool && (bool) this.Tag )
            {
                GetOptions();
                bool tile = option.Tile;

                #region Draw Picture / Text Preview
                option.Tile = false;
                objectOnly = true;
                switch ( mode )
                {
                    case PinObjectMode.Picture:
                        if ( picObject is Image )
                        {
                            if ( picObject.Width > imgPicture.Width || picObject.Height > imgPicture.Height )
                                imgPicture.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
                            else
                                imgPicture.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Normal;
                            option.ImageCache = picObject as Bitmap;
                            imgPicture.Image = addin.Apply( picObject );
                        }
                        break;
                    case PinObjectMode.Text:
                        if ( !string.IsNullOrEmpty( option.Text ) )
                        {
                            picText = addin.Apply( option.Text.ToBitmap(
                                option.TextFont,
                                option.TextFace,
                                option.TextSize,
                                option.TextColor.ToColor()
                            ) as Image );

                            //picText = addin.Apply( AddinUtils.TextToBitmap32( option.Text, option.TextFont, option.TextFontStyle, option.TextColor.ToColor() ) );
                            if ( picText.Width > imgText.Width || picText.Height > imgText.Height )
                                imgText.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
                            else
                                imgText.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Normal;
                            imgText.Image = picText;
                            toolTip.SetToolTip( imgText, $"{imgText.Image.Width}x{imgText.Image.Height}" );
                        }
                        break;
                }
                option.Tile = tile;
                objectOnly = false;
                #endregion

                #region Draw Image Preview
                if ( addin.ImageData is Image )
                {
                    objectOnly = false;
                    //imgPreview.Image = AddinUtils.CreateThumb( addin.Apply( addin.ImageData ), imgPreview.ClientSize );
                    imgPreview.Image = addin.Apply( addin.ImageData );
                }
                #endregion
            }
        }

        public PinObjectForm()
        {
            InitializeComponent();
        }

        public PinObjectForm( IAddin addin )
        {
            InitializeComponent();

            this.addin = addin;
            this.Text = this.addin.DisplayName;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            toolTip.ToolTipTitle = this.addin.DisplayName;
            AddinUtils.Translate( this.addin, this, toolTip );
        }

        #region DrapDrop Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _DragEnter( object sender, DragEventArgs e )
        {
            string[] flist = (string[])e.Data.GetData( DataFormats.FileDrop, true );
            if ( flist.Length > 0 && File.Exists( flist[0] ) && 
                string.Equals(Path.GetExtension(flist[0]), ".png", StringComparison.CurrentCultureIgnoreCase))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _DragDrop( object sender, DragEventArgs e )
        {
            string[] flist = (string[])e.Data.GetData( DataFormats.FileDrop, true );
            if ( flist.Length > 0 && File.Exists( flist[0] ) )
            {
                picObject = AddinUtils.LoadImage( flist[0] );
                option.ImageCache = picObject as Bitmap;
                Preview();
            }
        }
        #endregion DragDrop Events

        private void PinObjectForm_Load( object sender, EventArgs e )
        {
            this.Tag = false;

            if(option is PinOption)
            {
                LoadPicture( option.PictureFile );

                addin.Filters.Clear();
                effects.Clear();
                ilLarge.Images.Clear();
                ilSmall.Images.Clear();
                lvFilters.BeginUpdate();
                foreach ( var ap in option.Filters )
                {
                    var an = ap.Key;
                    if ( addin.Host.Addins.ContainsKey( an ) )
                    {
                        var filter = addin.Host.Addins[an];
                        var param = ap.Value;
                        option.FilterParams[filter] = param;

                        addin.Filters.Add( filter );

                        ilLarge.Images.Add( filter.LargeIcon );
                        ilSmall.Images.Add( filter.SmallIcon );
                        effects.Add( new ListViewItem( filter.DisplayName ) );
                        effects.Last().Selected = false;
                        effects.Last().Tag = filter;
                        effects.Last().ImageIndex = ilLarge.Images.Count;
                        effects.Last().Checked = true;
                    }
                }
                lvFilters.VirtualListSize = effects.Count;
                lvFilters.EndUpdate();

                edText.Text = option.Text;
            }

            slideOffsetX.Enabled = chkTile.Checked;
            slideOffsetY.Enabled = chkTile.Checked;

            thumb = addin.ImageData;
            imgPreview.Image = thumb;            
            imgPreview.ZoomToFit();
            imageActions.Source = thumb;

            csSelect.CornetRegion = CornerRegionType.BottomCenter;

            imageActions.ZoomLevels = imgPicture.ZoomLevels;

            this.Tag = true;
            Preview();
        }

        private void lvFilters_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            //lvFilters.VirtualListSize = effects.Count;
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

                    switch(mode)
                    {
                        case PinObjectMode.Picture:
                            filter.ImageData = picObject;
                            break;
                        case PinObjectMode.Text:
                            filter.ImageData = picText;
                            break;
                        case PinObjectMode.Tag:
                            break;
                    }
                    filter.Params = option.FilterParams[filter];
                    if ( filter.Show( this, true ) == DialogResult.OK )
                    {
                        option.FilterParams[filter] = filter.Params.Clone();
                        Preview();
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
                    if( ( lvi.Tag as IAddin ) is IAddin)
                    {
                        ( lvi.Tag as IAddin ).Enabled = lvi.Checked;
                        Preview();
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
                        Preview();
                    }
                    lv.Invalidate( lvi.Bounds );
                }
            }
        }

        private void btnEffectAdd_Click( object sender, EventArgs e )
        {
            lvFilters.BeginUpdate();
            foreach ( IAddin filter in AddinUtils.ShowAddinsDialog())
            {
                addin.Filters.Add( filter );
                ilLarge.Images.Add( filter.LargeIcon );
                ilSmall.Images.Add( filter.SmallIcon );
                effects.Add( new ListViewItem( filter.DisplayName ) );
                effects.Last().Selected = false;
                effects.Last().Tag = filter;
                effects.Last().ImageIndex = ilLarge.Images.Count;
                effects.Last().Checked = true;

                option.FilterParams[filter] = filter.Params;
            }
            lvFilters.VirtualListSize = effects.Count;
            lvFilters.EndUpdate();
            Preview();
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
                addin.Filters.Remove( filter );
                ilLarge.Images.RemoveAt( item.ImageIndex );
                ilSmall.Images.RemoveAt( item.ImageIndex );
                effects.Remove( item );
                option.FilterParams.Remove( filter );
            }
            lvFilters.SelectedIndices.Clear();
            lvFilters.VirtualListSize = effects.Count;
            lvFilters.EndUpdate();
            Preview();
        }

        private void btnEffectUp_Click( object sender, EventArgs e )
        {
            lvFilters.BeginUpdate();
            foreach (int i in lvFilters.SelectedIndices )
            {
                if ( i <= 0 ) continue;
                var fi = effects[i];
                effects[i] = effects[i-1];
                effects[i - 1] = fi;
                lvFilters.Items[i - 1].Selected = true;
                lvFilters.Items[i].Selected = false;
            }
            lvFilters.EndUpdate();
            //lvFilters.Update();
            Preview();
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
                if ( i >= effects.Count-1 ) continue;
                var fi = effects[i];
                effects[i] = effects[i + 1];
                effects[i + 1] = fi;
                lvFilters.Items[i + 1].Selected = true;
                lvFilters.Items[i].Selected = false;
            }
            lvFilters.EndUpdate();
            //lvFilters.Update();
            Preview();
        }

        private void btnPosRandom_Click( object sender, EventArgs e )
        {
            csSelect.CornetRegion = CornerRegionType.None;

            option.Pos = csSelect.CornetRegion;
            option.RandomPos = true;

            Preview();
        }

        private void csSelect_CornetRegionClick( object sender, EventArgs e )
        {
            option.Pos = csSelect.CornetRegion;
            option.RandomPos = false;

            Preview();
        }

        private void chkTile_Click( object sender, EventArgs e )
        {
            slideOffsetX.Enabled = chkTile.Checked;
            slideOffsetY.Enabled = chkTile.Checked;
            Preview();
        }

        private void slideValue_ValueChanged( object sender, EventArgs e )
        {
            Preview();
        }

        private void btnOpenPic_Click( object sender, EventArgs e )
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "PNG File(*.png)|*.png|All File(*.*)|*.*";
            dlgOpen.Multiselect = false;
            if ( dlgOpen.ShowDialog() == DialogResult.OK )
            {
                LoadPicture( dlgOpen.FileName );
            }
        }

        private void btnOriginalPic_MouseDown( object sender, MouseEventArgs e )
        {
            picObjectBackup = imgPicture.Image;
            imgPicture.Image = picObject;
        }

        private void btnOriginalPic_MouseUp( object sender, MouseEventArgs e )
        {
            imgPicture.Image = picObjectBackup;
        }

        private void btnOpenFont_Click( object sender, EventArgs e )
        {
            var optionBackup = option.Clone();

            dlgFontEx.UseFont = false;
            dlgFontEx.FamilyName = option.TextFont;
            dlgFontEx.TypefaceName = option.TextFace;
            dlgFontEx.Size = option.TextSize;
            dlgFontEx.Color = option.TextColor.ToColor();
            if ( dlgFontEx.ShowDialog() == DialogResult.OK )
            {
                option.TextFont = dlgFontEx.FamilyName;
                option.TextFace = dlgFontEx.TypefaceName;
                option.TextSize = dlgFontEx.Size;
                option.TextColor = dlgFontEx.Color.ToHtml();

                Preview();
            }
            else
            {
                option = optionBackup;
                Preview();
            }
        }

        private void dlgFont_Apply( object sender, EventArgs e )
        {
            option.TextFont = dlgFontEx.FamilyName;
            option.TextFace = dlgFontEx.TypefaceName;
            option.TextSize = dlgFontEx.Size;
            option.TextColor = dlgFontEx.Color.ToHtml();
            Preview();
        }

        private void btnColorPicker_Click( object sender, EventArgs e )
        {
            string c = option.TextColor;
            Color color = option.TextColor.ToColor();

            dlgColor = new NetCharm.Common.ColorDialog();
            dlgColor.Apply += new System.EventHandler( dlgColor_Apply );
            dlgColor.Color = color;
            if ( dlgColor.ShowDialog() == DialogResult.OK )
            {
                option.TextColor = dlgColor.Color.ToHtml();
                Preview();
            }
            else
            {
                option.TextColor = c;
                Preview();
            }
        }

        private void dlgColor_Apply( object sender, EventArgs e )
        {
            string c = option.TextColor;

            option.TextColor = dlgColor.Color.ToHtml();
            Preview();
            option.TextColor = c;
        }

        private void imgPicture_DoubleClick( object sender, EventArgs e )
        {
            btnOpenPic.PerformClick();
        }

        private void edText_TextChanged( object sender, EventArgs e )
        {
            Preview();
        }

        private void tabObject_SelectedIndexChanged( object sender, EventArgs e )
        {
            Preview();
        }

        private void imageActions_ViewOriginalDown( object sender, EventArgs e )
        {
            thumbBackup = imgPreview.Image;
            imgPreview.Image = thumb;
        }

        private void imageActions_ViewOriginalUp( object sender, EventArgs e )
        {
            if ( thumbBackup is Image )
                imgPreview.Image = thumbBackup;
        }

        private void imageActions_ZoomChanged( object sender, EventArgs e )
        {
            if ( imageActions.Zoom <= 0 )
            {
                imgPreview.ZoomToFit();
                imageActions.Zoom = imgPreview.Zoom;
            }
            else
                imgPreview.Zoom = imageActions.Zoom;
        }

        private void imageActions_ZoomIn( object sender, EventArgs e )
        {
            imgPreview.ZoomIn();
            imageActions.Zoom = imgPreview.Zoom;
        }

        private void imageActions_ZoomOut( object sender, EventArgs e )
        {
            imgPreview.ZoomOut();
            imageActions.Zoom = imgPreview.Zoom;
        }
    }
}
