using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Mono.Addins;
using NetCharm.Image.Addins;
using System.ComponentModel;
using System.Diagnostics;
using Accord.Imaging.Filters;
using System.IO;
using NGettext;
using NGettext.WinForm;
//using Accord.Imaging;
using System.Drawing.Imaging;

namespace InternalFilters.Actions
{
    [Extension]
    class Resize : IAddin, IFilter, IFilterInformation
    {
        private ICatalog catalog = null;

        private FileVersionInfo fv = null;
        private ResizeForm fm = null;

        private System.Drawing.Image ImgSrc = null;
        private System.Drawing.Image ImgDst = null;

        /// <summary>
        /// 
        /// </summary>
        private AddinHost _host = null;
        public AddinHost Host
        {
            get { return _host; }
            set { _host = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Location
        {
            get
            {
                return ( GetType().Module.FullyQualifiedName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid GUID
        {
            get { return ( GetType().GUID ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Author
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.CompanyName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Copyright
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.LegalCopyright );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Version
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.FileVersion );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Domain
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( Path.GetFileNameWithoutExtension( fv.InternalName ) );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return ( "Resize" ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = T("Resize");
        public string DisplayName
        {
            get { return ( _( _displayName ) ); }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GroupName
        {
            get { return ( "Edit" ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayGroupName = T("Edit");
        public string DisplayGroupName
        {
            get { return ( _( _displayGroupName ) ); }
            set { _displayGroupName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = T("Resize Image");
        public string Description
        {
            get { return ( _( _description ) ); }
            set { _description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public AddinType Type
        {
            get { return AddinType.Action; }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Drawing.Image LargeIcon
        {
            get { return ( Properties.Resources.Resize_32x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Drawing.Image SmallIcon
        {
            get { return ( Properties.Resources.Resize_16x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Drawing.Image ImageData
        {
            get
            {
                if ( ImgDst is System.Drawing.Image ) return ( ImgDst );
                else return ( ImgSrc );
            }
            set { ImgSrc = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _enabled = true;
        public bool Enabled { get { return ( _enabled ); } set { _enabled = value; } }

        /// <summary>
        /// 
        /// </summary>
        private bool _visible = true;
        public bool Visible { get { return ( _visible ); } protected internal set { _visible = value; } }

        /// <summary>
        /// 
        /// </summary>
        public List<IAddin> Filters
        {
            get { return ( null ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, ParamItem> _params = new Dictionary<string, ParamItem>();
        public Dictionary<string, ParamItem> Params
        {
            get { return ( _params ); }
        }

        private static string T(string t)
        {
            return ( t );
        }

        private ICatalog GetCatalog()
        {
            string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( Location ) ), "locale" );
            I18N i10n = new I18N( Domain, addinRoot );
            catalog = i10n.Catalog;
            return ( catalog );
        }

        private string _( string t )
        {
            if ( catalog == null )
                catalog = GetCatalog();

            if(catalog == null)
                return ( I18N._( t ) );
            else
                return ( I18N._( catalog, t ) );
        }

        public void Translate(Form form)
        {
            string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( Location ) ), "locale" );

            I18N i10n = new I18N( Domain, addinRoot, form );
            catalog = i10n.Catalog;

            DisplayName = _( DisplayName );
            Description = _( Description );
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _supportMultiFile = false;
        public virtual bool SupportMultiFile
        {
            get { return ( _supportMultiFile ); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public virtual void Open( string filename )
        {
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenames"></param>
        public virtual void Open( string[] filenames )
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="img"></param>
        private void SetParams( ResizeForm form, System.Drawing.Image img = null )
        {
            if ( img is System.Drawing.Image )
            {
                form.ParamWidth = new ParamItem()
                {
                    Name = "Width",
                    DisplayName = AddinUtils._( this, "Width" ),
                    Type = ImgSrc.Width.GetType(),
                    Value = ImgSrc.Width
                };
                form.ParamHeight = new ParamItem()
                {
                    Name = "Height",
                    DisplayName = AddinUtils._( this, "Height" ),
                    Type = ImgSrc.Height.GetType(),
                    Value = ImgSrc.Height
                };
            }
            else
            {
                if ( Params.ContainsKey( "Width" ) )
                    form.ParamWidth = Params["Width"];

                if ( Params.ContainsKey( "Height" ) )
                    form.ParamHeight = Params["Height"];
            }
            if ( Params.ContainsKey( "Aspect" ) )
                form.ParamAspect = Params["Aspect"];

            if ( Params.ContainsKey( "Method" ) )
                form.ParamMethod = Params["Method"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        private void GetParams( ResizeForm form )
        {
            Params["Width"] = form.ParamWidth;
            Params["Height"] = form.ParamHeight;
            Params["Aspect"] = form.ParamAspect;
            Params["Method"] = form.ParamMethod;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Show()
        {
            MessageBox.Show( "Calling Show() method", "Title", MessageBoxButtons.OK );
        }
        /// <summary>
        /// 
        /// </summary>
        public void Show( Form parent = null, bool setup = false )
        {
            if ( fm == null )
            {
                fm = new ResizeForm( this );
                fm.host = Host;
                fm.Text = DisplayName;
                fm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                fm.MaximizeBox = false;
                fm.MinimizeBox = false;
                fm.ShowIcon = false;
                fm.ShowInTaskbar = false;
                fm.StartPosition = FormStartPosition.CenterParent;

                Translate( fm );

                if(setup)
                {
                    if(Params.ContainsKey("Width") && Params.ContainsKey( "Height" ) )
                        SetParams( fm, null );
                    else
                        SetParams( fm, ImgSrc );
                }
                else if ( ImgSrc != null )
                {
                    SetParams( fm, ImgSrc );
                }
            }
            if ( fm.ShowDialog() == DialogResult.OK )
            {
                _success = true;
                GetParams( fm );
                if ( !setup )
                {
                    ImgDst = Apply( ImgSrc );
                }
            }
            if ( fm != null )
            {
                fm.Dispose();
                fm = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public System.Drawing.Image Apply( System.Drawing.Image image )
        {
            if ( image != null )
            {
                var w = Params.ContainsKey( "Width" ) && Params["Width"].Value is int ?  (int)Params["Width"].Value : image.Width;
                var h = Params.ContainsKey( "Height" ) && Params["Height"].Value is int ?  (int)Params["Height"].Value : image.Height;

                var aspect = Params.ContainsKey( "Aspect" ) && Params["Aspect"].Value is bool ? (bool) Params["Aspect"].Value : true;
                if ( aspect )
                {
                    double factor_o = image.Width / (float)image.Height;
                    w = w >= h ? w : (int) Math.Round( h / factor_o );
                    h = w >= h ? (int) Math.Round( w * factor_o ) : h;
                }

                var method = Params.ContainsKey( "Method" ) && Params["Method"].Value is int ? (int) Params["Method"].Value : 0;
                System.Drawing.Image dst = image;
                if ( method == 0 )
                {
                    ResizeBicubic filter = new ResizeBicubic(w, h);
                    dst = AddinUtils.ProcessImage( filter, image );
                }
                else if ( method == 1 )
                {
                    ResizeBilinear filter = new ResizeBilinear(w, h);
                    dst = AddinUtils.ProcessImage( filter, image );
                }
                else if ( method == 2 )
                {
                    ResizeNearestNeighbor filter = new ResizeNearestNeighbor(w, h);
                    dst = AddinUtils.ProcessImage( filter, image );
                }
                return ( dst );
            }
            return ( image );
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _success = false;
        public bool Success
        {
            get { return ( _success ); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <param name="cmdArgs"></param>
        /// <returns></returns>
        public bool Command( AddinCommand cmd, out object result, params object[] args)
        {
            result = null;
            return ( true );
        }

        #region IFilter Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap Apply( Bitmap image )
        {
            return ( Apply( image as System.Drawing.Image ) as Bitmap );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public Bitmap Apply( BitmapData imageData )
        {
            return ( Apply( Accord.Imaging.UnmanagedImage.FromManagedImage( imageData ) ).ToManagedImage() );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Accord.Imaging.UnmanagedImage Apply( Accord.Imaging.UnmanagedImage image )
        {
            return ( Accord.Imaging.UnmanagedImage.FromManagedImage( Apply( image.ToManagedImage() ) ) );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="destinationImage"></param>
        public void Apply( Accord.Imaging.UnmanagedImage sourceImage, Accord.Imaging.UnmanagedImage destinationImage )
        {
            destinationImage = Apply( sourceImage );
        }
        #endregion

        #region IFilterInformation Implementation
        private Dictionary<PixelFormat, PixelFormat> _formatTranslations = new Dictionary<PixelFormat, PixelFormat>();
        public Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get
            {
                if ( !( _formatTranslations is Dictionary<PixelFormat, PixelFormat> ) )
                {
                    _formatTranslations = new Dictionary<PixelFormat, PixelFormat>();
                }
                if ( _formatTranslations.Count == 0 )
                {
                    _formatTranslations.Add( PixelFormat.Format8bppIndexed, PixelFormat.Format8bppIndexed );
                    _formatTranslations.Add( PixelFormat.Format24bppRgb, PixelFormat.Format24bppRgb );
                    _formatTranslations.Add( PixelFormat.Format32bppArgb, PixelFormat.Format32bppArgb );
                }
                return ( _formatTranslations );
            }
        }

        #endregion

    }
}
