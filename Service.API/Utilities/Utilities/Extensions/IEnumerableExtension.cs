using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Utilities.Validation;

namespace Utilities.Extensions
{

    public static class EnumerableExtension
    {


        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();
            var l = source.ToLookup(firstKeySelector);

            foreach (var item in l)
            {
                var dict = new Dictionary<TSecondKey, TValue>();
                retVal.Add(item.Key, dict);
                var subdict = item.ToLookup(secondKeySelector);
                foreach (var subitem in subdict)
                {
                    dict.Add(subitem.Key, aggregate(subitem));
                }
            }

            return retVal;
        }
        public async static Task<IEnumerable<T>> AsEnumerableDataTable<T>(this DataTable table) where T : new()
        {
            //check for table availability
            if (table == null)
                throw new NullReferenceException("DataTable");

            //grab property length
            int propertiesLength = typeof(T).GetProperties().Length;

            //if no properties stop
            if (propertiesLength == 0)
                throw new NullReferenceException("Properties");

            //create list to hold object T values
            var objList = new List<T>();

            //iterate thru rows of the datatable
            foreach (DataRow row in table.Rows)
            {
                //create a new instance of our object T
                var obj = new T();

                //grab properties of object T
                PropertyInfo[] objProperties = obj.GetType().GetProperties();

                //iterate thru and populate property values
                for (int i = 0; i < propertiesLength; i++)
                {
                    //grab current property
                    PropertyInfo property = objProperties[i];

                    //check datatable to see if datacolumn exists
                    if (table.Columns.Contains(property.Name))
                    {
                        //get row cell value
                        object objValue = row[property.Name];

                        //check for nullable property type and handle
                        var propertyType = property.PropertyType;
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            propertyType = propertyType.GetGenericArguments()[0];

                        //set property value
                        objProperties[i].SetValue(obj, Convert.ChangeType(objValue, propertyType, CultureInfo.CurrentCulture), null);
                    }
                }

                //add to obj list
                objList.Add(obj);
            }
            return await Task.FromResult(objList);
        }
        public static async Task<IEnumerable<T>> TrimAllPropertiesAsync<T>(this IEnumerable<T> enumerable) where T : class
        {
            var tem = new List<T>();
            var trimAllProperties = enumerable as IList<T> ?? enumerable.ToList();
            foreach (var obj in trimAllProperties)
            {
                var stringProperties = obj.GetType().GetProperties()
                         .Where(p => p.PropertyType == typeof(string));

                foreach (var stringProperty in stringProperties)
                {
                    var currentValue = (string)stringProperty.GetValue(obj, null);
                    if (currentValue != null)
                    {
                        stringProperty.SetValue(obj, currentValue.Trim(), null);
                    }
                }
                tem.Add(obj);
            }
            return await Task.FromResult(tem.Any() ? tem : trimAllProperties);
        }
        public static async Task<T> FindInArrayAsync<T>(this IEnumerable<object> enumerable) where T : class
        {
            var entity = enumerable.FirstOrDefault(x => (x as T) != null) as T;
            return await Task.FromResult(entity);
        }
        public static async Task<bool> AreAllEqualAsync<T>(this IEnumerable<T> enumerable)
        {
            var values = enumerable as IList<T> ?? enumerable.ToList();

            if (enumerable == null || !values.Any())
                return await Task.FromResult(true);

            var toCompare = JsonConvert.SerializeObject(values.ElementAt(0));
            var result = values.All(v => JsonConvert.SerializeObject(v).Equals(toCompare));

            return await Task.FromResult(result);
        }

        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
        {
            var propInfo = ObjectExtension.GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {
            var propInfo = ObjectExtension.GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> query, string name)
        {
            var propInfo = ObjectExtension.GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string name)
        {
            var propInfo = ObjectExtension.GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }

        public static bool TryGetValue<T>(this IDictionary<string, object> collection, string key, out T value)
        {

            Check.NotEmpty(key, "key");

            object valueObj;
            if (collection.TryGetValue(key, out valueObj))
            {

                if (valueObj is T)
                {

                    value = (T)valueObj;
                    return true;
                }
            }

            value = default(T);
            return false;
        }

        public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(this IDictionary<string, TValue> dictionary, string prefix)
        {
            Check.NotEmpty(prefix, "prefix");
            TValue exactMatchValue;
            if (dictionary.TryGetValue(prefix, out exactMatchValue))
            {

                yield return new KeyValuePair<string, TValue>(prefix, exactMatchValue);
            }

            foreach (var entry in dictionary)
            {

                string key = entry.Key;

                if (key.Length <= prefix.Length)
                {
                    continue;
                }

                if (!key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Everything is prefixed by the empty string
                if (prefix.Length == 0)
                {
                    yield return entry;
                }
                else
                {
                    char charAfterPrefix = key[prefix.Length];
                    switch (charAfterPrefix)
                    {
                        case '[':
                        case '.':
                            yield return entry;
                            break;
                    }
                }
            }
        }
    }
}