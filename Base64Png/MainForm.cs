using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Base64Png
{
    public partial class MainForm : Form
    {
        string title = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageSize"></param>
        /// <param name="patternSize"></param>
        /// <returns></returns>
        internal Image MakePatternImage( Size imageSize, int patternSize = 8 )
        {
            Bitmap pat = new Bitmap(patternSize*2, patternSize*2, PixelFormat.Format32bppArgb);
            using ( var g = Graphics.FromImage( pat ) )
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                SolidBrush bb = new SolidBrush(Color.Silver);
                SolidBrush wb = new SolidBrush(Color.White);

                g.FillRectangle( bb, 0, 0, patternSize, patternSize );
                g.FillRectangle( bb, patternSize, patternSize, patternSize, patternSize );

                g.FillRectangle( wb, 0, patternSize, patternSize, patternSize );
                g.FillRectangle( wb, patternSize, 0, patternSize, patternSize );
            }

            Bitmap bg = new Bitmap(imageSize.Width, imageSize.Height, PixelFormat.Format32bppArgb);
            using ( var g = Graphics.FromImage( bg ) )
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                TextureBrush pb = new TextureBrush(pat);
                g.FillRectangle( pb, 0, 0, imageSize.Width, imageSize.Height );
            }
            return ( bg );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        internal Image Base64ToImage(string base64)
        {
            Image img = null;
            if ( !string.IsNullOrEmpty( base64 ) )
            {
                try
                {
                    string bs = Regex.Replace(base64, @"data:image/.*?;base64,", "", RegexOptions.IgnoreCase);
                    byte[] arr = Convert.FromBase64String(bs.Trim());
                    using ( MemoryStream ms = new MemoryStream( arr ) )
                    {
                        img = Image.FromStream( ms );

                    }
                }
                catch(Exception)
                {
                        
                }
            }
            return ( img );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="png"></param>
        /// <returns></returns>
        internal string ImageToBase64(Image img, bool prefix = false )
        {
            string base64 = string.Empty;
            if ( img is Image )
            {
                try
                {
                    using ( MemoryStream ms = new MemoryStream() )
                    {
                        //img.Save( ms, ImageFormat.Png );
                        img.Save( ms, img.RawFormat);

                        byte[] arr = ms.ToArray();
                        base64 = Convert.ToBase64String( arr, Base64FormattingOptions.InsertLineBreaks );
                        if ( prefix )
                        {
                            base64 = $"data:{img.RawFormat.GetMimeType()};base64,{base64}";
                        }
                    }
                }
                catch ( Exception )
                {

                }
            }
            return ( base64 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flist"></param>
        /// <returns></returns>
        internal string LoadXML(string[] flist)
        {
            string base64 = string.Empty;
            if ( flist.Length > 0 && File.Exists( flist[0] ) )
            {
                XmlDocument doc = new XmlDocument();
                doc.Load( flist[0] );

                var names = doc.GetElementsByTagName( "name" );
                if ( names.Count > 0 )
                    this.Text = $"{title} - {names[0].Attributes["value"].Value}";
                else
                {
                    var items = doc.GetElementsByTagName( "item" );
                    if ( items.Count > 0 ) this.Text = $"{title} - {items[0].Attributes["text"].Value}";
                }

                var thumbs = doc.GetElementsByTagName( "thumbnail" );
                if ( thumbs.Count > 0 )
                {
                    base64 = thumbs[0].InnerText.Trim();
                }
            }
            return ( base64 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flist"></param>
        /// <returns></returns>
        internal Image LoadImage( string[] flist )
        {
            Image img = null;
            if ( flist.Length > 0 && File.Exists(flist[0]))
            {
                using ( FileStream fs = File.OpenRead( flist[0] ) )
                {
                    img = Image.FromStream( fs, true, true );
                }
            }
            return ( img );
        }

        /// <summary>
        /// 
        /// </summary>
        private void AdjustSizeMode()
        {
            if ( picPreview.Image is Image)
            {
                if ( picPreview.Image.Width > picPreview.ClientSize.Width || picPreview.Image.Height > picPreview.ClientSize.Height )
                    picPreview.SizeMode = PictureBoxSizeMode.Zoom;
                else
                    picPreview.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            title = this.Text;

            #region extracting icon from application to this form window
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            #endregion

            picPreview.BackgroundImage = MakePatternImage( picPreview.Size );
        }

        #region DrapDrop Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter( object sender, DragEventArgs e )
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop( object sender, DragEventArgs e )
        {
            //string[] txts = { "System.String", "UnicodeText", "Text", "OemText", "Html", "RTF" };
            //string[] bmps = { "Bitmap", "Dib", "DeviceIndependentBitmap", "DragImageBits" };
            //string[] files = { "FileDrop", "FileName", "FileNameW", "Shell IDList Array" };

            var formats = e.Data.GetFormats();
            if ( formats.Contains( "FileContents" ) && formats.Contains( "text/html" ) )
            {
                var obj = e.Data.GetData("text/html", true);
                string html = string.Empty;
                if ( obj is string )
                {
                    html = (string) obj;
                }
                else if ( obj is MemoryStream )
                {
                    MemoryStream ms = (MemoryStream)obj;
                    byte[] buffer = new byte[ms.Length];
                    ms.Read( buffer, 0, (int) ms.Length );
                    if ( buffer[1] == (byte) 0 )  // Detecting unicode
                    {
                        html = Encoding.Unicode.GetString( buffer );
                    }
                    else
                    {
                        html = Encoding.ASCII.GetString( buffer );
                    }
                }
                // Using a regex to parse HTML, but JUST FOR THIS EXAMPLE :-)
                //var match = new Regex(@"<img[^/]src=""([^""]*)""").Match(html);
                var match = new Regex(@"(<img[^/])(.*?src=)(""([^""]*)"")").Match(html);
                if ( match.Success )
                {
                    Uri uri = new Uri(match.Groups[match.Groups.Count-1].Value);
                    picPreview.ImageLocation = uri.AbsoluteUri;
                    picPreview.WaitOnLoad = false;
                    picPreview.LoadAsync();
                    //picPreview.Load();
                    //btnEncode.PerformClick();
                }
            }
            else if ( formats.Contains( "Text" ) || formats.Contains( "UnicodeText" ) || formats.Contains( "System.String" ) )
            {
                edBase64.Text = (string) e.Data.GetData( "UnicodeText", true );
                btnDecode.PerformClick();
            }
            else if ( formats.Contains( "FileDrop" ) )
            {
                string[] flist = (string[])e.Data.GetData( DataFormats.FileDrop, true );

                string[] images = { ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".bmp", ".gif" };

                if ( flist.Length > 0 )
                {
                    if ( string.Equals( Path.GetExtension( flist[0] ), ".xml", StringComparison.CurrentCultureIgnoreCase ) )
                    {
                        edBase64.Text = LoadXML( flist );
                        btnDecode.PerformClick();
                    }
                    else if ( images.Contains( Path.GetExtension( flist[0] ).ToLower() ) )
                    {
                        //picPreview.Image = LoadImage( flist );
                        picPreview.ImageLocation = flist[0];
                        picPreview.WaitOnLoad = false;
                        picPreview.LoadAsync();
                    }
                }
            }
        }
        #endregion DragDrop Events

        private void btnDecode_Click( object sender, EventArgs e )
        {
            picPreview.WaitOnLoad = false;
            picPreview.ImageLocation = string.Empty;
            picPreview.Image = Base64ToImage( edBase64.Text.Trim() );
            AdjustSizeMode();
        }

        private void btnEncode_Click( object sender, EventArgs e )
        {
            edBase64.Text = ImageToBase64( picPreview.Image, chkEncPrefix.Checked );
        }

        private void btnPaste_Click( object sender, EventArgs e )
        {
            if ( Clipboard.ContainsImage() )
            {
                picPreview.Image = Clipboard.GetImage();
                AdjustSizeMode();
                btnEncode.PerformClick();
            }
            else if ( Clipboard.ContainsText() )
            {
                edBase64.Text = Clipboard.GetText();
                btnDecode.PerformClick();
            }
        }

        private void btnCopy_Click( object sender, EventArgs e )
        {
            Clipboard.SetText( edBase64.Text );
        }

        private void edBase64_KeyUp( object sender, KeyEventArgs e )
        {
            if ( e.Control && e.KeyCode == Keys.A )
            {
                edBase64.SelectAll();
            }
        }

        private void picPreview_DoubleClick( object sender, EventArgs e )
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Image File (*.jpg;*.png;*.tif;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.bmp;*.gif";
            if(dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picPreview.Image = LoadImage( dlgOpen.FileNames );
            }
        }

        private void picPreview_LoadCompleted( object sender, AsyncCompletedEventArgs e )
        {
            AdjustSizeMode();
            btnEncode.PerformClick();
        }

    }

    static class Exts
    {
        public static string GetMimeType( this ImageFormat imageFormat )
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First( codec => codec.FormatID == imageFormat.Guid ).MimeType;
        }
    }

}
