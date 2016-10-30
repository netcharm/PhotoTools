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

[assembly: Addin]
[assembly: AddinDependency( "AddinHost", "1.0" )]

namespace InternalFilters
{
    [Extension]
    class Resize : IAddin
    {
        private ICatalog catalog = null;

        private FileVersionInfo fv = null;
        private ResizeForm fm = null;

        private Image ImgSrc = null;
        private Image ImgDst = null;

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
            get { return AddinType.Effect; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image LargeIcon
        {
            get { return ( Properties.Resources.Resize_32x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image SmallIcon
        {
            get { return ( Properties.Resources.Resize_16x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image ImageData
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
        private ComponentResourceManager resources = new ComponentResourceManager();
        public ComponentResourceManager Resources
        {
            get { return resources; }
            set { resources = value; }
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
        private void SetParams( ResizeForm form, Image img = null )
        {
            if ( img is Image )
            {
                fm.ParamWidth = new ParamItem()
                {
                    Name = "Width",
                    DisplayName = AddinUtils._( this, "Width" ),
                    Type = ImgSrc.Width.GetType(),
                    Value = ImgSrc.Width
                };
                fm.ParamHeight = new ParamItem()
                {
                    Name = "Height",
                    DisplayName = AddinUtils._( this, "Height" ),
                    Type = ImgSrc.Width.GetType(),
                    Value = ImgSrc.Width
                };
            }
            else
            {
                if ( Params.ContainsKey( "Width" ) )
                    fm.ParamWidth = Params["Width"];

                if ( Params.ContainsKey( "Height" ) )
                    fm.ParamHeight = Params["Height"];
            }
            if ( Params.ContainsKey( "Aspect" ) )
                fm.ParamAspect = Params["Aspect"];

            if ( Params.ContainsKey( "Method" ) )
                fm.ParamMethod = Params["Method"];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        private void GetParams( ResizeForm form )
        {
            if ( Params.ContainsKey( "Width" ) )
                Params["Width"] = fm.ParamWidth;
            else
                Params.Add( "Width", fm.ParamWidth );

            if ( Params.ContainsKey( "Height" ) )
                Params["Height"] = fm.ParamHeight;
            else
                Params.Add( "Height", fm.ParamHeight );

            if ( Params.ContainsKey( "Aspect" ) )
                Params["Aspect"] = fm.ParamAspect;
            else
                Params.Add( "Aspect", fm.ParamAspect );

            if ( Params.ContainsKey( "Method" ) )
                Params["Method"] = fm.ParamMethod;
            else
                Params.Add( "Method", fm.ParamMethod );
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
        public void Show( Form parent = null )
        {
            if ( fm == null )
            {
                fm = new ResizeForm( this );
                Translate( fm );
                fm.Text = DisplayName;

                if ( ImgSrc != null )
                {
                    SetParams( fm, ImgSrc );
                }
            }
            if ( fm.ShowDialog() == DialogResult.OK )
            {
                GetParams( fm );
                ImgDst = Apply( ImgSrc );
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
        public Image Apply( Image image )
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
                Image dst = image;
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
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <param name="cmdArgs"></param>
        /// <returns></returns>
        public bool Command( AddinCommand cmd, out object result, params object[] args)
        {
            result = null;
            return ( true );
        }

    }
}
