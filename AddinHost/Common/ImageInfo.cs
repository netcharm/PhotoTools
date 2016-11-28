using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NetCharm.Image
{
 
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MetaInfo
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

        public CircularStack( int limit )
        {
            _limit = limit;
            _items = new List<T>( limit );
        }

        public void Push( T item )
        {
            _items.Add( item );
            if ( _items.Count >= _limit )
            {
                _items.RemoveAt( 0 );
            }
        }

        public T Pop()
        {
            if ( _items is List<T> )
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


}
