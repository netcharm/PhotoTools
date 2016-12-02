using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Web.Script.Serialization;
using NetCharm.Image.Addins;
using Newtonsoft.Json;

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
        static public Image CreateThumb( this IAddin addin, Size size )
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramlist"></param>
        /// <param name="addin"></param>
        static public void SetParams(this Dictionary<string, ParamItem> paramlist, IAddin addin)
        {
            foreach ( var kv in paramlist )
            {
                addin.Params[kv.Key] = kv.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramlist"></param>
        /// <returns></returns>
        static public Dictionary<string, ParamItem> Clone( this Dictionary<string, ParamItem> paramlist )
        {
            var result = new Dictionary<string, ParamItem>();
            foreach ( var kv in paramlist )
            {
                result[kv.Key] = new ParamItem();
                result[kv.Key].Name = kv.Value.Name;
                result[kv.Key].DisplayName = kv.Value.DisplayName;
                result[kv.Key].Name = kv.Value.Name;

                if ( kv.Value.Value is long )
                    kv.Value.Value = Convert.ToInt32( kv.Value.Value );

                result[kv.Key].Value = kv.Value.Value;
                if ( result[kv.Key].Value != null )
                    result[kv.Key].Type = result[kv.Key].Value.GetType();
                else
                    result[kv.Key].Type = typeof( object );
            }
            return ( result );
        }
        #endregion

        #region JSON Config Save / Load routines
        //internal class IntConvert : JsonConverter
        //{
        //    private readonly Type[] _types;

        //    public override bool CanConvert( Type objectType )
        //    {
        //        return _types.Any( t => t == objectType );
        //    }

        //    public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        static private JsonSerializerSettings jsonSetting = new JsonSerializerSettings()
        {
            //Converters.Add(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Auto,            
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            NullValueHandling = NullValueHandling.Ignore
        };

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
                    config = JsonConvert.DeserializeObject<T>( json, jsonSetting );
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
                    config = JsonConvert.DeserializeObject<T>( json, jsonSetting );
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
                var json = JsonConvert.SerializeObject( config, Formatting.Indented, jsonSetting );
                File.WriteAllText( configFile, json );
            }
            else
            {
                string path = Assembly.GetEntryAssembly().Location;
                string domain = Path.GetFileNameWithoutExtension(path);
                string addinRoot = Path.GetDirectoryName( Path.GetFullPath( path ) );
                string configFile = Path.Combine(addinRoot, jsonFile);
                var json = JsonConvert.SerializeObject( config, Formatting.Indented, jsonSetting );
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
                config = JsonConvert.DeserializeObject<T>( jsonContent, jsonSetting );
            }
            return ( config );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        static public T FromJSON<T>(this T obj, string jsonContent)
        {
            T config = default(T);
            try
            {
                config = JsonConvert.DeserializeObject<T>( jsonContent, jsonSetting );
            }
            catch(Exception)
            {

            }
            return ( config );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        static public T FromJSON<T>( this T obj, object jsonContent )
        {
            T config = default(T);
            try
            {
                config = JsonConvert.DeserializeObject<T>( jsonContent.ToString(), jsonSetting );
            }
            catch ( Exception )
            {

            }
            return ( config == null ? default(T) : config );
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
                result = JsonConvert.SerializeObject( config, Formatting.Indented, jsonSetting );
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
        static public T LoadJSON<T>( this T config, string jsonFile )
        {
            T result = config;
            try
            {
                if ( File.Exists( jsonFile ) )
                {
                    string json = File.ReadAllText( jsonFile );
                    config = JsonConvert.DeserializeObject<T>( json, jsonSetting );
                    result = config;
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
                var json = JsonConvert.SerializeObject( config, Formatting.Indented, jsonSetting  );
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
