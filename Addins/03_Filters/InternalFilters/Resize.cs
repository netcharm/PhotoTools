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
            get { return AddinType.Filter; }
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

                if ( Params.ContainsKey( "Width" ) )
                    fm.SetWidth( Params["Width"] );
                else if( ImgSrc != null)
                    fm.SetWidth( ImgSrc.Width );

                if ( Params.ContainsKey( "Height" ) )
                    fm.SetHeight( Params["Height"] );
                else if ( ImgSrc != null )
                    fm.SetHeight( ImgSrc.Height );
            }
            if ( fm.ShowDialog() == DialogResult.OK )
            {
                if ( Params.ContainsKey( "Width" ) )
                    Params["Width"] = fm.GetWidth();
                else
                    Params.Add( "Width", fm.GetWidth() );

                if ( Params.ContainsKey( "Height" ) )
                    Params["Height"] = fm.GetHeight();
                else
                    Params.Add( "Height", fm.GetHeight() );

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
            if(image != null)
            {
                ResizeBicubic filter = new ResizeBicubic((int)Params["Width"].Value, (int)Params["Height"].Value);
                return ( AddinUtils.ProcessImage( filter, image ) );
            }
            return ( image );
        }

    }
}
