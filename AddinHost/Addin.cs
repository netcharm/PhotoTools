using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using Mono.Addins;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

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
        private ValueType _type = null;
        public ValueType Type
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
    public class AddinImp : object
    {
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
        private Guid _guid = new Guid();
        public Guid GUID
        {
            get { return ( _guid ); }
            //set { name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _name = null;
        public string Name
        {
            get { return ( _name ); }
            //set { name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayname = null;
        public string DisplayName
        {
            get { return ( _displayname ); }
            //set { displayname = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = null;
        public string Description
        {
            get { return ( _description ); }
            //set { description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _path = null;
        public string Path
        {
            get
            {
                if ( _path == null ) _path = System.IO.Path.GetFileNameWithoutExtension( GetType().Module.ToString() );
                return ( _path );
            }
            //set { path = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private AddinType _type = AddinType.Unknown;
        public AddinType Type
        {
            get { return ( _type ); }
            //set { type = value; }
        }
        //private Image img = Properties.Resources.AddInterface_32x;
        //private Icon _icon = new Icon( typeof(Image), "AddInterface_32x" );
        /// <summary>
        /// 
        /// </summary>
        private Icon _icon = null;
        public Icon Icon
        {
            get
            {
                if ( _icon == null ) Icon.FromHandle( ( Properties.Resources.AddIn_32x as Bitmap ).GetHicon() );
                return ( _icon );
            }
            //set { icon = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private System.Drawing.Image _image = null;
        public System.Drawing.Image LargeImage
        {
            get
            {
                if ( _image == null ) _image = Properties.Resources.AddIn_32x as Bitmap;
                return ( _image );
            }
            //set { _image = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private System.Drawing.Image _smallimage = null;
        public System.Drawing.Image SmallImage
        {
            get
            {
                if ( _smallimage == null ) _smallimage = Properties.Resources.AddIn_16x as Bitmap;
                return ( _smallimage );
            }
            //set { _smallimage = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _author = null;
        public string Author
        {
            get { return _author; }
            //set { _author = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _copyright = null;
        public string Copyright
        {
            get { return _copyright; }
            //set { _copyright = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _version = null;
        public string Version
        {
            get { return _version; }
            //set { _version = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, ParamItem> _params = new Dictionary<string, ParamItem>();
        public Dictionary<string, ParamItem> Params
        {
            get { return _params; }
            //set { }
        }

        //public virtual override Image Apply( Image image );
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool Show()
        {
            return ( true );
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
        string DisplayName { get; }
        /// <summary>
        /// 
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 
        /// </summary>
        string Location { get; }
        /// <summary>
        /// 
        /// </summary>
        AddinType Type { get; }
        /// <summary>
        /// 
        /// </summary>
        System.Drawing.Image LargeImage { get; }
        /// <summary>
        /// 
        /// </summary>
        System.Drawing.Image SmallImage { get; }
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
    }

}
