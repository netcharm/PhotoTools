using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Resources;
using Mono.Addins;
using NetCharm.Image.Addins;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

[assembly: Addin]
[assembly: AddinDependency( "AddinHost", "1.0" )]
namespace AddinTest
{
    [Extension]
    public class AddinTest : IAddin
    {
        private FileVersionInfo fv = null;
        private Image img = null;

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
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.InternalName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = "App Test";
        public string DisplayName
        {
            get { return ( _displayName ); }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName
        {
            get { return ( "Test" ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayGroupName = AddinUtils.T("Test");
        public string DisplayCategoryName
        {
            get { return ( AddinUtils._( this, _displayGroupName ) ); }
            set { _displayGroupName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = "";
        public string Description
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                if ( string.IsNullOrEmpty( _description ) ) _description = fv.FileDescription;
                return ( _description );
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
                return ( null );
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
                return ( null );
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

        /// <summary>
        /// 
        /// </summary>
        private bool _success = true;
        public bool Success
        {
            get { return ( _success ); }
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
        public void Show(Form parent=null)
        {
            Form fm = new Form();
            fm.Text = "Test Form";
            fm.MdiParent = parent;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
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
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenames"></param>
        public void Open( string[] filenames )
        {
            //
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
        /// <param name="cmdArgs"></param>
        /// <returns></returns>
        public bool Command( AddinCommand cmd, out object result, params object[] cmdArgs )
        {
            result = null;
            return ( true );
        }

    }
}
