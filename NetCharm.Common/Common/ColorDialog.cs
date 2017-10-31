using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using ExtensionMethods;

namespace NetCharm.Common
{
    public partial class ColorDialog : Form
    {
        public event EventHandler Apply;
        private Dictionary<string, ColorCollection> CustomPalette = new Dictionary<string, ColorCollection>();

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
                    mi.Tag = false;
                    //mi.Click += cmPalette_Click;
                    mi.CheckOnClick = true;
                    
                    cmPalette.Items.Add( mi );
                    if ( mi.Text == colorGrid.Palette.ToString() )
                        mi.Checked = true;
                }
                if ( cmPalette.Items.Count > 0 )
                {
                    var mi_sep = new ToolStripSeparator();
                    mi_sep.Tag = false;
                    cmPalette.Items.Add( mi_sep );
                }
                var mi_Load = new ToolStripMenuItem( this._("Load Palette") );
                mi_Load.Tag = false;
                cmPalette.Items.Add( mi_Load );
                var mi_Save = new ToolStripMenuItem( this._("Save Palette") );
                mi_Save.Tag = false;
                cmPalette.Items.Add( mi_Save );
                var mi_sep1 = new ToolStripSeparator();
                mi_sep1.Tag = false;
                mi_sep1.Visible = false;
                cmPalette.Items.Add( mi_sep1 );
            }
        }

        private void cmPalette_ItemClicked( object sender, ToolStripItemClickedEventArgs e )
        {
            if ( e.ClickedItem is ToolStripMenuItem )
            {
                if ( e.ClickedItem.Text.StartsWith( this._( "Load Palette" ) ) )
                {
                    cmPalette.Close();
                    #region Load Palette
                    using ( FileDialog dialog = new OpenFileDialog
                    {
                        Filter = PaletteSerializer.DefaultOpenFilter,
                        DefaultExt = "pal",
                        Title = this._( "Open Palette File" )
                    } )
                    {
                        if ( dialog.ShowDialog( this ) == DialogResult.OK )
                        {
                            try
                            {
                                IPaletteSerializer serializer;

                                serializer = PaletteSerializer.GetSerializer( dialog.FileName );
                                if ( serializer != null )
                                {
                                    ColorCollection palette;

                                    if ( !serializer.CanRead )
                                    {
                                        throw new InvalidOperationException( this._( "Serializer does not support reading palettes." ) );
                                    }

                                    using ( FileStream file = File.OpenRead( dialog.FileName ) )
                                    {
                                        palette = serializer.Deserialize( file );
                                    }

                                    if ( palette != null )
                                    {
                                        // we can only display 96 colors in the color grid due to it's size, so if there's more, bin them
                                        while ( palette.Count > 96 )
                                        {
                                            palette.RemoveAt( palette.Count - 1 );
                                        }

                                        // or if we have less, fill in the blanks
                                        while ( palette.Count < 96 )
                                        {
                                            palette.Add( Color.White );
                                        }

                                        colorGrid.Colors = palette;

                                        //var item = cmPalette.Items[cmPalette.Items.Count-1];
                                        //if ( item.GetType() == typeof( ToolStripSeparator ) ) item.Visible = true;

                                        var pal_name = Path.GetFileNameWithoutExtension( dialog.FileName );
                                        bool exists = false;
                                        foreach ( var item in cmPalette.Items )
                                        {
                                            //if ( item.GetType() == typeof( ToolStripSeparator ) ) continue;
                                            if ( item.GetType() == typeof( ToolStripSeparator ) )
                                                ( item as ToolStripSeparator ).Visible = true;
                                            else if ( item.GetType() == typeof( ToolStripMenuItem ) )
                                            {
                                                if ( ( item as ToolStripMenuItem ).Text.Equals( pal_name, StringComparison.CurrentCultureIgnoreCase ) )
                                                {
                                                    exists = true;
                                                    ( item as ToolStripMenuItem ).Checked = true;
                                                }
                                                else
                                                {
                                                    ( item as ToolStripMenuItem ).Checked = false;
                                                }
                                            }
                                        }
                                        if ( !exists )
                                        {
                                            var mi_pal = new ToolStripMenuItem( pal_name );
                                            mi_pal.Tag = true;
                                            mi_pal.CheckOnClick = true;
                                            mi_pal.Checked = true;
                                            cmPalette.Items.Add( mi_pal );
                                            CustomPalette.Add( pal_name, palette );
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show( this._( "Sorry, unable to open palette, the file format is not supported or is not recognized." ), this._( "Load Palette" ), MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                                }
                            }
                            catch ( Exception ex )
                            {
                                MessageBox.Show( string.Format( this._( "Sorry, unable to open palette. {0}" ), ex.GetBaseException().Message ), this._( "Load Palette" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
                            }
                        }
                    }
                    #endregion
                }
                else if ( e.ClickedItem.Text.StartsWith( this._( "Save Palette" ) ) )
                {
                    cmPalette.Close();
                    #region Save Palette
                    using ( FileDialog dialog = new SaveFileDialog
                    {
                        Filter = PaletteSerializer.DefaultSaveFilter,
                        DefaultExt = "pal",
                        Title = this._( "Save Palette File As" )
                    } )
                    {
                        if ( dialog.ShowDialog( this ) == DialogResult.OK )
                        {
                            IPaletteSerializer serializer;

                            serializer = PaletteSerializer.AllSerializers.Where( s => s.CanWrite ).ElementAt( dialog.FilterIndex - 1 );
                            if ( serializer != null )
                            {
                                if ( !serializer.CanWrite )
                                {
                                    throw new InvalidOperationException( this._( "Serializer does not support writing palettes." ) );
                                }

                                try
                                {
                                    using ( FileStream file = File.OpenWrite( dialog.FileName ) )
                                    {
                                        serializer.Serialize( file, colorGrid.Colors );
                                    }
                                }
                                catch ( Exception ex )
                                {
                                    MessageBox.Show( string.Format( this._( "Sorry, unable to save palette. {0}" ), ex.GetBaseException().Message ), this._( "Save Palette" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
                                }
                            }
                            else
                            {
                                MessageBox.Show( this._( "Sorry, unable to save palette, the file format is not supported or is not recognized." ), this._( "Save Palette" ), MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                            }
                        }
                    }
                    #endregion
                }
                else if ( e.ClickedItem.Text.StartsWith( "-" ) )
                {
                    return;
                }
                else
                {
                    foreach ( var item in cmPalette.Items )
                    {
                        if ( item.GetType() == typeof( ToolStripSeparator ) ) continue;
                        if ( item != e.ClickedItem ) ( item as ToolStripMenuItem ).Checked = false;
                    }
                    if ( (bool) e.ClickedItem.Tag )
                    {
                        var pal_name = e.ClickedItem.Text;
                        if ( CustomPalette.ContainsKey( pal_name ) )
                        {
                            colorGrid.Colors = CustomPalette[pal_name];
                        }
                    }
                    else
                        colorGrid.Palette = (ColorPalette) Enum.Parse( typeof( ColorPalette ), e.ClickedItem.Text );
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

        private void tsmiColorValueCopy_Click( object sender, EventArgs e )
        {
            try
            {
                string color = string.Empty;
                bool sharp = tsmiColorValueHexSharp.Checked;
                bool value = !tsmiColorValueCssPercent.Checked;
                var tsmi = sender as ToolStripMenuItem;
                if(string.Equals(tsmi.Name, "tsmiColorValueHexRGB", StringComparison.CurrentCultureIgnoreCase))
                {
                    color = colorManager.Color.ToHtml( sharp, NetCharmExtensions.HtmlColorOrder.RGB );
                }
                else if( string.Equals( tsmi.Name, "tsmiColorValueHexBGR", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToHtml( sharp, NetCharmExtensions.HtmlColorOrder.BGR );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueHexRGBA", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToHtml( sharp, NetCharmExtensions.HtmlColorOrder.RGBA );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueHexBGRA", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToHtml( sharp, NetCharmExtensions.HtmlColorOrder.BGRA );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueHexARGB", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToHtml( sharp, NetCharmExtensions.HtmlColorOrder.ARGB );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueHexABGR", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToHtml( sharp, NetCharmExtensions.HtmlColorOrder.ABGR );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueCssRGB", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToCSS( value, NetCharmExtensions.HtmlColorOrder.RGB );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueCssRGBA", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToCSS( value, NetCharmExtensions.HtmlColorOrder.RGBA );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueCssHSL", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToCSS( sharp, NetCharmExtensions.HtmlColorOrder.HSL );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueCssHSLA", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToCSS( sharp, NetCharmExtensions.HtmlColorOrder.HSLA );
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueColorRGB", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = $"COLOR{colorManager.Color.ToHtml( false, NetCharmExtensions.HtmlColorOrder.RGB )}";
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueColorBGR", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = $"COLOR{colorManager.Color.ToHtml( false, NetCharmExtensions.HtmlColorOrder.BGR )}";
                }
                else if ( string.Equals( tsmi.Name, "tsmiColorValueName", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    color = colorManager.Color.ToHtml( sharp, NetCharmExtensions.HtmlColorOrder.NAME );
                }
                Clipboard.SetText( color );
            }
            catch (Exception) { }
        }
    }
}
