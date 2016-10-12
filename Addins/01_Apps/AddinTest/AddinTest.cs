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
        public string DisplayName
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.ProductName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.FileDescription );
            }
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
        public Image LargeImage
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
        public Image SmallImage
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
        /// <param name="image"></param>
        /// <returns></returns>
        public Image Apply( Image image )
        {
            MessageBox.Show( "Calling Apply() method", "Title", MessageBoxButtons.OK );
            return ( image );
            //throw new NotImplementedException();
        }
    }
}
