using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using NetCharm.Image.Addins;

namespace ExtensionMethods
{
    static public class AddinExtensions
    {
        #region IAddin extensions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image CreateThumb( this IAddin addin, Size size )
        {
            var source = addin.ImageData;
            if ( source is System.Drawing.Image )
            {
                double thumbSize = Math.Min(size.Width, size.Height);
                double factor = Math.Max(source.Width, source.Height) / thumbSize;
                int w = (int)Math.Round( source.Width / factor );
                int h = (int)Math.Round( source.Height / factor );

                Bitmap dst = source.Resize(new Size(w, h)) as Bitmap;
                return ( dst );
            }
            else
                return ( null );
        }

        #endregion

        #region JSON Config Save / Load routines
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addin"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        static public T LoadJSON<T>( this IAddin addin, string jsonFile )
        {
            T config = default(T);
            if ( addin is IAddin )
            {
                string addinRoot = Path.GetDirectoryName( Path.GetFullPath( addin.Location ) );
                string configFile = Path.Combine(addinRoot, jsonFile);
                if ( File.Exists( configFile ) )
                {
                    string json = File.ReadAllText( configFile );
                    JavaScriptSerializer serializer  = new JavaScriptSerializer();
                    config = (T) serializer.Deserialize( json, typeof( T ) );
                }
            }
            else
            {
                string path = Assembly.GetEntryAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.GetDirectoryName( Path.GetFullPath( path ) );
                string configFile = Path.Combine(addinRoot, jsonFile);
                if ( File.Exists( configFile ) )
                {
                    string json = File.ReadAllText( configFile );
                    JavaScriptSerializer serializer  = new JavaScriptSerializer();
                    config = (T) serializer.Deserialize( json, typeof( T ) );
                }

            }
            return ( config );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addin"></param>
        /// <param name="jsonFile"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        static public bool SaveJSON<T>( this IAddin addin, string jsonFile, T config )
        {
            bool result = false;
            if ( addin is IAddin )
            {
                string addinRoot = Path.GetDirectoryName( Path.GetFullPath( addin.Location ) );
                string configFile = Path.Combine(addinRoot, jsonFile);

                JavaScriptSerializer serializer  = new JavaScriptSerializer();
                var json = serializer.Serialize(config);
                File.WriteAllText( configFile, json );
            }
            else
            {
                string path = Assembly.GetEntryAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.GetDirectoryName( Path.GetFullPath( path ) );
                string configFile = Path.Combine(addinRoot, jsonFile);

                JavaScriptSerializer serializer  = new JavaScriptSerializer();
                var json = serializer.Serialize(config);
                File.WriteAllText( configFile, json );
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addin"></param>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        static public T FromJSON<T>( this IAddin addin, string jsonContent )
        {
            T config = default(T);
            if ( addin is IAddin )
            {
                JavaScriptSerializer serializer  = new JavaScriptSerializer();
                config = (T) serializer.Deserialize( jsonContent, typeof( T ) );
            }
            return ( config );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addin"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        static public string ToJSON<T>( this IAddin addin, T config )
        {
            string result = string.Empty;
            if ( addin is IAddin )
            {
                JavaScriptSerializer serializer  = new JavaScriptSerializer();
                result = serializer.Serialize( config );
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        static public bool LoadJSON<T>( this T config, string jsonFile )
        {
            bool result = false;
            try
            {
                if ( File.Exists( jsonFile ) )
                {
                    string json = File.ReadAllText( jsonFile );
                    JavaScriptSerializer serializer  = new JavaScriptSerializer();
                    config = (T) serializer.Deserialize( json, typeof( T ) );
                    result = true;
                }
            }
            catch(Exception)
            {
            }
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        static public bool SaveJSON<T>( this T config, string jsonFile)
        {
            bool result = false;
            try
            {
                JavaScriptSerializer serializer  = new JavaScriptSerializer();
                var json = serializer.Serialize(config);
                File.WriteAllText( jsonFile, json );
                result = true;
            }
            catch(Exception)
            {
            }
            return ( result );
        }

        #endregion
    }
}
