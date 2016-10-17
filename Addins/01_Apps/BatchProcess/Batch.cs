using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Addins;
using NetCharm.Image.Addins;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

[assembly: Addin]
[assembly: AddinDependency( "AddinHost", "1.0" )]
namespace BatchProcess
{
    [Extension]
    public class Batch : IAddin
    {
        private FileVersionInfo fv = null;
        private BatchProcessForm fm = null;
        private Image img = null;

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
                return ( "Batch" );
                //if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                //return ( fv.InternalName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = AddinUtils.T("Batch");
        public string DisplayName
        {
            get { return ( AddinUtils._( this, _displayName) ); }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = AddinUtils.T("Batch Image Process");
        public string Description
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                if ( string.IsNullOrEmpty( _description ) ) _description = fv.FileDescription;
                return ( AddinUtils._( this, _description) );
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
                return ( Properties.Resources.Batch_32x );
                //return ( null );
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
                return ( Properties.Resources.Batch_16x );
                //return ( null );
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
                return ( img );
            }
            set
            {
                img = value;
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
            Show( ParentForm );
        }
        /// <summary>
        /// 
        /// </summary>
        public void Show( Form parent = null )
        {
            ParentForm = parent;
            if ( fm == null )
            {
                //fm = new BatchProcessForm( Host );
                fm = new BatchProcessForm( this );
                fm.Text = fv.ProductName;
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
        private bool _supportMultiFile = true;
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
            if (fm is BatchProcessForm )
            {
                fm.AddFiles( filename );
                if ( Host.CurrentApp != this)
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
            if ( fm is BatchProcessForm )
            {
                fm.AddFiles( filenames );
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
        public bool Command( AddinCommand cmd, out object result, params object[] cmdArgs )
        {
            result = null;
            switch ( cmd )
            {
                case AddinCommand.Open:
                    if ( cmdArgs.Length > 0 && cmdArgs[0] is string )
                        Open( cmdArgs[0] as string );
                    else if ( cmdArgs.Length > 0 && cmdArgs[0] is string[] )
                        Open( cmdArgs[0] as string[] );
                    break;
                case AddinCommand.ZoomIn:
                    break;
                case AddinCommand.ZoomOut:
                    break;
                case AddinCommand.ZoomFit:
                    break;
                case AddinCommand.Zoom100:
                    break;
            }
            return ( true );
        }
    }
}
