using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using Mono.Addins;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using NGettext.WinForm;
using NGettext;
using System.Diagnostics;
using System.Reflection;
using Accord.Imaging.Filters;

namespace NetCharm.Image.Addins
{
    /// <summary>
    /// 
    /// </summary>
    public class ParamItem : object
    {
        /// <summary>
        /// 
        /// </summary>
        private string _name = null;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = null;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Type _type = null;
        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private object _value = null;
        public Object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [TypeExtensionPoint]
    public interface IAddin
    {
        AddinHost Host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Guid GUID { get; }
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Location { get; }
        /// <summary>
        /// 
        /// </summary>
        string Domain { get; }
        /// <summary>
        /// 
        /// </summary>
        AddinType Type { get; }
        /// <summary>
        /// 
        /// </summary>
        System.Drawing.Image LargeIcon { get; }
        /// <summary>
        /// 
        /// </summary>
        System.Drawing.Image SmallIcon { get; }
        /// <summary>
        /// 
        /// </summary>
        string Author { get; }
        /// <summary>
        /// 
        /// </summary>
        string Copyright { get; }
        /// <summary>
        /// 
        /// </summary>
        string Version { get; }
        /// <summary>
        /// 
        /// </summary>
        System.Drawing.Image ImageData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        ComponentResourceManager Resources { get; }
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, ParamItem> Params { get; }
        /// <summary>
        /// 
        /// </summary>
        void Show();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        void Show(Form parent);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        System.Drawing.Image Apply( System.Drawing.Image image );

        /// <summary>
        /// 
        /// </summary>
        bool SupportMultiFile { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        void Open( string filename );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenames"></param>
        void Open( string[] filenames );

        List<KeyValuePair<AddinCommand, object>> CommandProperties { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <param name="cmdArgs"></param>
        /// <returns></returns>
        bool Command( AddinCommand cmd, out ValueType result, params object[] cmdArgs );
    }

    /// <summary>
    /// 
    /// </summary>
    public class AddinBase : IAddin
    {
        private static ICatalog catalog = null;

        private FileVersionInfo fv = null;
        //private Form fm = null;

        protected internal System.Drawing.Image ImgSrc = null;
        protected internal System.Drawing.Image ImgDst = null;

        protected internal Form Parent = null;

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
        public virtual Guid GUID
        {
            get { return ( GetType().GUID ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Author
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
        public virtual string Copyright
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
        public virtual string Version
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
        private string _name = "AddinSample";
        public virtual string Name
        {
            get { return ( _name ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = "AddinSample";
        public virtual string DisplayName
        {
            get { return ( _( _displayName ) ); }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = "Addin Sample";
        public virtual string Description
        {
            get { return ( _( _description ) ); }
            set { _description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual AddinType Type
        {
            get { return AddinType.Filter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Drawing.Image LargeIcon
        {
            get { return ( Properties.Resources.AddIn_32x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Drawing.Image SmallIcon
        {
            get { return ( Properties.Resources.AddIn_16x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Drawing.Image ImageData
        {
            get
            {
                if ( ImgDst is System.Drawing.Image) return(ImgDst);
                else return (ImgSrc);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ICatalog GetCatalog()
        {
            string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( Location ) ), "locale" );
            I18N i10n = new I18N( Domain, addinRoot );
            catalog = i10n.Catalog;
            return ( catalog );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected internal static string T( string t )
        {
            if ( catalog is ICatalog ) return ( catalog.GetString(t) );
            return ( t );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected internal string _( string t )
        {
            if ( catalog == null )
                catalog = GetCatalog();

            if ( catalog == null )
                return ( I18N._( t ) );
            else
                return ( I18N._( catalog, t ) );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected internal void Translate( Form form, ToolTip tooltip=null, object[] extra=null )
        {
            string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( Location ) ), "locale" );

            I18N i10n = new I18N( Domain, addinRoot, form, tooltip, extra );
            catalog = i10n.Catalog;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Show()
        {
            MessageBox.Show( "Calling Show() method", "Title", MessageBoxButtons.OK );
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Show( Form parent = null )
        {
            MessageBox.Show( "Calling Show(parentForm) method", "Title", MessageBoxButtons.OK );
            //if ( fm == null )
            //{
            //    fm = new Form( this );
            //    Translate( fm );
            //    fm.Text = DisplayName;

            //    if ( Params.ContainsKey( "Width" ) )
            //        fm.SetWidth( Params["Width"] );
            //    else if ( ImgSrc != null )
            //        fm.SetWidth( ImgSrc.Width );

            //    if ( Params.ContainsKey( "Height" ) )
            //        fm.SetHeight( Params["Height"] );
            //    else if ( ImgSrc != null )
            //        fm.SetHeight( ImgSrc.Height );
            //}
            //if ( fm.ShowDialog() == DialogResult.OK )
            //{
            //    if ( Params.ContainsKey( "Width" ) )
            //        Params["Width"] = fm.GetWidth();
            //    else
            //        Params.Add( "Width", fm.GetWidth() );

            //    if ( Params.ContainsKey( "Height" ) )
            //        Params["Height"] = fm.GetHeight();
            //    else
            //        Params.Add( "Height", fm.GetHeight() );

            //    ImgDst = Apply( ImgSrc );
            //}
            //else ImgDst = ImgSrc;
            //if ( fm != null )
            //{
            //    fm.Dispose();
            //    fm = null;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public virtual System.Drawing.Image Apply( System.Drawing.Image image )
        {
            return ( image );
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
        private List<KeyValuePair<AddinCommand, object>> _cmdProperties = new List<KeyValuePair<AddinCommand, object>>();
        public List<KeyValuePair<AddinCommand, object>> CommandProperties
        {
            get { return ( _cmdProperties ); }
            set { _cmdProperties = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <param name="cmdArgs"></param>
        /// <returns></returns>
        public virtual bool Command( AddinCommand cmd, out ValueType result, params object[] cmdArgs )
        {
            result = null;
            return ( true );
        }
    }
}
