using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NetCharm.Image.Addins
{
    //using ParamList = Dictionary<string, ParamItem>;

    //public class ParamList : Dictionary<string, ParamItem> { }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AddinSubItem
    {
        private IAddin addin = null;

        private string _name=string.Empty;
        public virtual string Name
        {
            get { return ( _name ); }
            protected internal set { _name = value; }
        }

        private string _displayName=string.Empty;
        public virtual string DisplayName
        {
            get { return ( AddinUtils._( addin, _displayName ) ); }
            protected internal set { _displayName = value; }
        }

        private string _groupName=string.Empty;
        public virtual string GroupName
        {
            get { return ( _groupName ); }
            protected internal set { _groupName = value; }
        }

        private string _displayGroupName=string.Empty;
        public virtual string DisplayGroupName
        {
            get { return ( AddinUtils._( addin, _displayName ) ); }
            protected internal set { _displayName = value; }
        }

        private string _tooltipText=string.Empty;
        public virtual string TooltipText
        {
            get { return ( AddinUtils._( addin, _tooltipText ) ); }
            protected internal set { _tooltipText = value; }
        }

        private System.Drawing.Image _smallIcon = null;
        public virtual System.Drawing.Image SmallIcon
        {
            get
            {
                if ( _smallIcon is System.Drawing.Image )
                {
                    return ( _smallIcon );
                }
                else if ( addin is IAddin )
                {
                    if ( addin.Type == AddinType.App )
                        return ( Properties.Resources.Application_16x );
                    else if ( addin.Type == AddinType.Action )
                        return ( Properties.Resources.Action_16x );
                    else if ( addin.Type == AddinType.Effect )
                        return ( Properties.Resources.Effect_16x );
                    else
                        return ( Properties.Resources.AddIn_16x );
                }
                return ( null );
            }
            protected internal set { _smallIcon = value; }
        }

        private System.Drawing.Image _largeIcon = null;
        public virtual System.Drawing.Image LargeIcon
        {
            get
            {
                if ( _largeIcon is System.Drawing.Image )
                {
                    return ( _largeIcon );
                }
                else if ( addin is IAddin )
                {
                    if ( addin.Type == AddinType.App )
                        return ( Properties.Resources.Application_32x );
                    else if ( addin.Type == AddinType.Action )
                        return ( Properties.Resources.Action_32x );
                    else if ( addin.Type == AddinType.Effect )
                        return ( Properties.Resources.Effect_32x );
                    else
                        return ( Properties.Resources.AddIn_32x );
                }
                return ( null );
            }
            protected internal set { _largeIcon = value; }
        }

        public AddinSubItem( IAddin addin, string name, string displayname, string group, string displaygroup, string tooltip, System.Drawing.Image smallicon, System.Drawing.Image largeicon )
        {
            this.addin = addin;
            _name = name;
            _displayName = displayname;
            _groupName = group;
            _displayGroupName = displaygroup;
            _tooltipText = tooltip;
            _smallIcon = smallicon;
            _largeIcon = largeicon;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ImageInfo
    {
        public List<PropertyItem> EXIF;
        public Dictionary<string, string> IPTC;
        public System.Windows.Media.Imaging.BitmapMetadata Meta;
    }

    [Serializable]
    public class SaveOption
    {
        public bool KeepExif = true;
        public bool Overwrite = true;
        public int Quality = 90;
        public string RenameMask=string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class CircularStack<T>
    {
        private List<T> _items = new List<T>();
        private int _limit = 0;

        public int Count
        {
            get { return ( _items is List<T> ? _items.Count : -1 ); }
        }

        public CircularStack()
        {
            _items = new List<T>();
        }

        public CircularStack(int limit)
        {
            _limit = limit;
            _items = new List<T>(limit);
        }

        public void Push(T item)
        {
            _items.Add( item );
            if ( _items.Count >= _limit )
            {
                _items.RemoveAt( 0 );
            }
        }

        public T Pop()
        {
            if( _items is List<T>)
            {
                if ( Count > 0 )
                {
                    var item = _items.Last();
                    _items.Remove( _items.Last() );
                    return ( item );
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public T Peek()
        {
            return ( _items.Last() );
        }

        public void Clear()
        {
            _items.Clear();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CommandPropertiesChangeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public CommandPropertiesChangeEventArgs()
        {
            _cmd = AddinCommand.Unknown;
            _value = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="property"></param>
        public CommandPropertiesChangeEventArgs( AddinCommand command, ValueType property )
        {
            _cmd = command;
            _value = property;
        }

        public CommandPropertiesChangeEventArgs( AddinCommand command, object property )
        {
            _cmd = command;
            _value = property;
        }

        /// <summary>
        /// 
        /// </summary>
        private AddinCommand _cmd = AddinCommand.Unknown;
        public AddinCommand Command
        {
            get { return ( _cmd ); }
            private set { }
        }

        /// <summary>
        /// 
        /// </summary>
        private object _value = null;
        public object Property
        {
            get { return ( _value ); }
            private set { }
        } // readonly
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum AddinType
    {
        Unknown = 0,
        App = 1,
        Action = 2,
        Editor = 3,
        Effect = 4,
        FormatIn = 5,
        FormatOut = 6,
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public enum AddinCommand
    {
        Unknown = 0,

        About = 10,
        Commands = 20,
        Log = 30,
        SubItems = 40,

        Open = 100,
        Save = 101,
        SaveAs = 102,
        Close = 103,

        Copy = 201,
        Cut = 202,
        Paste = 203,
        Clear = 204,
        Undo = 210,
        Redo = 211,

        Zoom = 301,
        ZoomIn = 302,
        ZoomOut = 303,
        Zoom100 = 304,
        ZoomFit = 305,
        ZoomRegion = 306,
        ZoomLevel = 310,

        GetImage = 1001,
        SetImage = 1002,
        GetThumb = 1003,
        SetThumb = 1004,
        GetImageName = 1101,
        GetImageSize = 1102,
        GetImageInfo = 1103,
        GetImageColors = 1104,
        GetImageSelection = 1201,
        SetImageSelection = 1202,

        GetHost = 2001,
        SetHost = 2002,
        GetAddin = 2003,
        SetAddin = 2004,

        InitParams = 2101,
        GetParams = 2111,
        SetParams = 2112,

        Show = 3001,
        Apply = 3002,
        ApplyAll = 3003,
        ApplyTiming = 3100,

        HasPreview = 9001,
        SupportMultiFiles = 9002,


        Reset = 10001,
    }
}
