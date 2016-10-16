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
using System.IO;

[assembly: Addin]
[assembly: AddinDependency( "AddinHost", "1.0" )]

namespace InternalFilters
{
    [Extension]
    public class ImageEditor:IAddin
    {
        private FileVersionInfo fv = null;
        private EditorForm fm = null;

        protected internal Form ParentForm = null;

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
            get
            {
                return ( "Editor" );
                //if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                //return ( fv.InternalName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = AddinUtils.T("Editor");
        public string DisplayName
        {
            get{ return ( AddinUtils._( this, _displayName ) ); }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = AddinUtils.T("Image Editor");
        public string Description
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                if ( string.IsNullOrEmpty( _description ) ) _description = fv.FileDescription;
                return ( AddinUtils._( this, _description ) );
            }
            set { _description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public AddinType Type
        {
            get
            {
                return AddinType.App;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image LargeIcon
        {
            get
            {
                return ( Properties.Resources.Editor_32x );
                //throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image SmallIcon
        {
            get
            {
                return ( Properties.Resources.Editor_16x );
                //throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image ImageData
        {
            get
            {
                if ( fm == null ) { return EditorForm.GetImage(); }
                else return ( fm.ImageData );
            }
            set
            {
                if ( fm == null ) { EditorForm.SetImage( value ); }
                else fm.ImageData = value;
            }
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

        /// <summary>
        /// 
        /// </summary>
        public void Show()
        {
            //MessageBox.Show( "Calling Show() method", "Title", MessageBoxButtons.OK );
            Show( null );
        }
        /// <summary>
        /// 
        /// </summary>
        public void Show( Form parent = null )
        {
            //EditorForm fm = new EditorForm(Host);
            if(fm == null)
            {
                fm = new EditorForm( Host, this );
                AddinUtils.Translate( this, fm );
                fm.Text = DisplayName;
                fm.MdiParent = parent;
                fm.WindowState = FormWindowState.Maximized;
                fm.Show();
            }
            else
            {
                fm.Activate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _supportMultiFile = false;
        public bool SupportMultiFile
        {
            get { return ( _supportMultiFile ); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void Open( string filename )
        {
            if(File.Exists( filename ) )
            {
                ImageData = new Bitmap( filename );
                if ( Host.CurrentApp != this )
                {
                    Host.CurrentApp = this;
                    Host.CurrentApp.Show( ParentForm );
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenames"></param>
        public void Open( string[] filenames )
        {
            if ( filenames.Length > 0 )
            {
                if ( File.Exists( filenames[0] ) )
                {
                    ImageData = new Bitmap( filenames[0] );
                    if ( Host.CurrentApp != this )
                    {
                        Host.CurrentApp = this;
                        Host.CurrentApp.Show( ParentForm );
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Image Apply( Image image )
        {
            MessageBox.Show( "Calling Apply() method", "Title", MessageBoxButtons.OK );
            return ( image );
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Command( AddinCommand cmd, out ValueType result, params object[] cmdArgs )
        {
            result = null;
            switch(cmd)
            {
                case AddinCommand.Open:
                    if ( cmdArgs.Length > 0 && cmdArgs[0] is string )
                        Open( cmdArgs[0] as string );
                    else if ( cmdArgs.Length > 0 && cmdArgs[0] is string[] )
                        Open( cmdArgs[0] as string[] );
                    break;
                case AddinCommand.ZoomIn:
                case AddinCommand.ZoomOut:
                case AddinCommand.ZoomRegion:
                case AddinCommand.ZoomFit:
                case AddinCommand.Zoom100:
                case AddinCommand.ZoomLevel:
                    if ( fm is EditorForm ) result = fm.Zoom( cmd );
                    break;
                case AddinCommand.GetImageSize:
                    if ( fm is EditorForm && fm.ImageData is Image)
                    {
                        result = new Size( fm.ImageData.Width, fm.ImageData.Height );
                    }
                    break;
            }
            return ( true );
        }

    }
}
