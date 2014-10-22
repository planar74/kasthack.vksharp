﻿#define COMMAS
#define CHECKBUILDER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VKSharp.Core.Interfaces;
using VKSharp.Data.Request;

namespace VKSharp.Helpers
{
    public static class ParserHelper {
        private static readonly HttpClient Client = new HttpClient();
        /// <summary>
        /// Dictionary with generated parsers
        /// key: typeof(TEntity) value: Func&lt;string,TEntity&gt;
        /// Most of them are built in runtime
        /// </summary>
        private static readonly Dictionary<Type, object> Parsers = new Dictionary<Type, object> {
                    {typeof(string), new Func<string,string>(s=>s.Trim('\r','\n','\t', ' ')) },
                    {typeof(bool), new Func<string,bool>(s=>{int r; return int.TryParse( s, out r) && (r==1);})},
                    {typeof(bool?), new Func<string,bool?>(s=>{int r; return int.TryParse( s, out r)?(bool?)(r==1):null;})},
                };
        /// <summary>
        /// Delegate for static TEntity.TryParse(string,out TEntity) method
        /// </summary>
        /// <typeparam name="T">Type for TryParse</typeparam>
        /// <param name="input">Input string</param>
        /// <param name="value">Variable targetInnerType fill</param>
        /// <returns>Was parsing successful</returns>
        private delegate bool GenericTryParse<T>(string input, out T value);

        #region Types & MethodInfos
        /// <summary>
        /// Current type
        /// </summary>
        private static readonly Type HelperType = typeof( ParserHelper );
        /// <summary>
        /// TEntity? type
        /// </summary>
        private static readonly Type NullableType = typeof(Nullable<>);
        /// <summary>
        /// MethodInfo for ParserHelper.BuildNullableTryParse&lt;TEntity&gt;
        /// </summary>
        private static readonly Lazy<MethodInfo> GetNullableTryParseBuilderLazy = new Lazy<MethodInfo>(
            () => HelperType.GetMethod("BuildNullableTryParse", BindingFlags.Static | BindingFlags.NonPublic));
        private static readonly Lazy<MethodInfo> GetParsersBuilderLazy = new Lazy<MethodInfo>(
            () => HelperType.GetMethod("GetParsers", BindingFlags.Static | BindingFlags.NonPublic));
        private static readonly Lazy<MethodInfo> ParseEnumFromIntStringLazy = new Lazy<MethodInfo>(
            () => HelperType.GetMethod("ParseEnum", BindingFlags.Static | BindingFlags.NonPublic));
        /// <summary>
        /// Wrapper for GetNullableTryParseBuilderLazy
        /// </summary>
        private static MethodInfo GetNullableTryParseBuilder { get { return GetNullableTryParseBuilderLazy.Value; } }
        /// <summary>
        /// Wrapper for GetParsersBuilderLazy
        /// </summary>
        private static MethodInfo GetParsersBuilder { get { return GetParsersBuilderLazy.Value; } }
        private static MethodInfo ParseEnumMethod { get { return ParseEnumFromIntStringLazy.Value; } }
        #endregion
        /// <summary>
        /// Parses the enum from int string/Enum string.
        /// </summary>
        /// <returns><c>true</c>, if enum was parsed, <c>false</c> otherwise.</returns>
        /// <param name="s">Input string</param>
        /// <param name="value">Output value</param>
        private static bool ParseEnum<TEnum, TBase>(string s, out TEnum value) where TEnum:struct{
            long i;
            var ret = long.TryParse( s, out i );
            //trim to prevent oveflow exception
            i &= ( -1L ^ ( 1L << 63 ) ) >> ( 63 - ( Marshal.SizeOf<TBase>() * 8 ) );
            if ( ret )
                value =  (TEnum) Convert.ChangeType( i, typeof(TBase) );
            else ret = Enum.TryParse( s, true, out value );
            return ret;
        }

        /// <summary>
        /// Return TEntity.TryParse method
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <returns>TEntity.TryParse delegate</returns>
        private static GenericTryParse<T> GetTryParse<T>() {
            var targetType = typeof(T);
            MethodInfo tryParseMethod=null;
#if CHECKBUILDER
            try {
#endif
                if (targetType.IsEnum)
                    tryParseMethod = ParseEnumMethod.MakeGenericMethod(targetType, targetType.GetEnumUnderlyingType());
                else
                    tryParseMethod = targetType.GetMethod(
                        "TryParse",
                        BindingFlags.Static | BindingFlags.Public,
                        Type.DefaultBinder,
                        new []{typeof(string),targetType.MakeByRefType()},
                        null
                    );
#if CHECKBUILDER
            }
            catch {
                throw;
            }
#endif
            if ( tryParseMethod == null ) return null;
            return (GenericTryParse<T>)tryParseMethod.CreateDelegate(typeof(GenericTryParse<T>));
        }
        /// <summary>
        /// Build TryParse method for TEntity
        /// </summary>
        /// <typeparam name="T">Type for parsing</typeparam>
        /// <returns>Delegate which invokes TEntity.TryParse and returns parsed value/default(TEntity) if failed. Returns null if method was not found</returns>
        private static Func<string, T> BuildTryParse<T>() {
            var tryparse = GetTryParse<T>();
            if ( tryparse == null ) return null;
            return parseValue => { T retvalue; return tryparse(parseValue, out retvalue) ? retvalue : default(T); };
        }
        //Don't rename. Invoked via reflection only.
        /// <summary>
        /// Build TryParse method for TEntity?
        /// </summary>
        /// <typeparam name="T">Type for parsing</typeparam>
        /// <returns>Delegate which invokes TEntity.TryParse and returns parsed value/null if failed. Returns null if method was not found</returns>
        private static Func<string, T?> BuildNullableTryParse<T>() where T : struct {
            var tryparse = GetTryParse<T>();
            if (tryparse == null) return null;
            return parseValue => { T retvalue; return tryparse(parseValue, out retvalue) ? (T?)retvalue : null; };
        }
        /// <summary>
        /// Returns delegate which parses string and updates specified property
        /// </summary>
        /// <typeparam name="TEntity">Type of entity targetInnerType update</typeparam>
        /// <typeparam name="TProperty">Type of property targetInnerType update</typeparam>
        /// <param name="property">Target property info</param>
        /// <param name="propertyType">typeof(TProperty) if generated earlier</param>
        /// <returns></returns>
        private static Action<TEntity, string> GetPropertyUpdaterDelegate<TEntity, TProperty>(PropertyInfo property, Type propertyType = null)
        {
            Func<string, TProperty> parser;
            object o;
            propertyType = propertyType ?? typeof(TProperty);
            //Build parserInfo if we don't have it yet
            if (!Parsers.TryGetValue(propertyType, out o)) {
                //Tentity is TEntity? -> build nullable parserInfo
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == NullableType) {
                    var targetInnerType = propertyType.GetGenericArguments()[0];                     // TEntity? -> TEntity
                    var gntpInfo = GetNullableTryParseBuilder.MakeGenericMethod(targetInnerType);    // =MethodInfo(BuildNullableTryParse<TEntity>)
                    var gntpType = typeof(Func<Func<string, TProperty>>);                            // BuildNullableTryParse<TEntity> delegate type
                    var buildFunc = (Func<Func<string, TProperty>>)gntpInfo.CreateDelegate(gntpType);// BuildNullableTryParse<TEntity> as delegate
                    parser = buildFunc();                                                            // generate parser
                }
                else parser = BuildTryParse<TProperty>();
                if (parser == null) return null;         //Failed to build parser -> null
                Parsers.Add(propertyType, parser);       //add to cache
            }
            else parser = (Func<string, TProperty>)o;   //take from cache
            var updater = (Action<TEntity, TProperty>)  //get property setter & make delegate
                Delegate.CreateDelegate(typeof(Action<TEntity, TProperty>), null, property.GetSetMethod());
            return (entity, value) => updater(entity, parser(value));
        }
        //Don't rename. Invoked via reflection only.
        /// <summary>
        /// Add 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ret"></param>
        /// <param name="props"></param>
        private static void GetParsers<TEntity, TProperty>(IDictionary<string, Action<TEntity, string>> ret,
            IReadOnlyDictionary<Type, PropertyInfo[]> props) {
            var propertyType = typeof(TProperty);
            if (!props.ContainsKey(propertyType)) return; //no properties of this type -> return
            foreach (var pi in props[propertyType]) {//build updater delegates for each property of this type
                var updater = GetPropertyUpdaterDelegate<TEntity, TProperty>(pi, propertyType);
                if (updater != null) ret.Add(ToSnake(pi.Name), updater);
                //key: property name converted to snake_case, value: delegate which updates property
            }
        }
        /// <summary>
        /// Execute VKRequest & get response string
        /// </summary>
        /// <param name="request">Request targetInnerType execute</param>
        /// <param name="format">Response format(json|xml)</param>
        /// <returns>Response</returns>
        public static async Task<string> ExecRawAsync<T>(VKRequest<T> request, string format)
            where T : IVKEntity<T> {

                var ps = request.Parameters.ToList();
                ps.Add(new KeyValuePair<string, string>("v", "5.21"));
                ps.Add(new KeyValuePair<string, string>("https", "1"));
                var path = "/method/" + request.MethodName + "." + format;
                if (request.Token != null)
                    ps.Add(new KeyValuePair<string, string>("access_token", request.Token.Token));
                return await (await Client.PostAsync(
                    new Uri(BuiltInData.Instance.VKDomain + path),
                    new FormUrlEncodedContent(ps)
                )).Content.ReadAsStringAsync();
        }
        /// <summary>
        /// Get parsers for TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type to parse</typeparam>
        /// <returns>Dictionary where keys are names of properties and values are updater delegates</returns>
        public static Dictionary<string, Action<TEntity, string>> GetStringParsers<TEntity>()
        {
            var ret = new Dictionary<string, Action<TEntity, string>>();
            var entityType = typeof(TEntity);
            var props = entityType
                .GetProperties()
                .GroupBy(a => a.PropertyType)
                .ToDictionary(a => a.Key, a => a.ToArray());
            var method = GetParsersBuilder;
            foreach (var type in props.Keys.ToArray())
#if CHECKBUILDER
                try {
#endif
                    //=GetParsers<TEntity, type>(ret, props)
                    method.MakeGenericMethod( entityType, type ).Invoke( null, new object[] { ret, props } );
#if CHECKBUILDER
                }
                catch ( Exception ) {
                    
                }
#endif
            //GetParsers<TEntity, byte>( ret, props );
            return ret;
        }
        /// <summary>
        /// Converts CamelCase targetInnerType snake_case
        /// </summary>
        /// <param name="name">Name for converting</param>
        /// <returns>Converted string</returns>
        public static string ToSnake(this string name)
        {
            var t = new StringBuilder();
            t.Append(Char.ToLower(name[0]));
            for (var index = 1; index < name.Length; index++)
            {
                var c = name[index];
                //add '_' before numbers and captials 
                if (Char.IsUpper(c) || (Char.IsNumber(c) && !Char.IsNumber(name[index - 1])))
                {
                    t.Append('_');
                    t.Append(Char.ToLower(c));
                }
                else t.Append(c);
            }
            return t.ToString();
        }
    }
}
