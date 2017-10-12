using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Utilities.Validation;

namespace Utilities.Extensions
{
    public static class ObjectExtension
    {
        public static async Task<bool> AreAllEqualAsync<T>(this object item, params T[] values)
        {
            var match = false;
            if (values == null || values.Length == 0)
                return true;
            await Task.Factory.StartNew(() =>
            {
                var toCompare = JsonConvert.SerializeObject(item);
                match = values.All(v => JsonConvert.SerializeObject(v).Equals(toCompare));
            });
            return match;

        }
        public static async Task<T> CloneAsync<T>(this object item)
        {
            using (var stream = new MemoryStream())
            {
                var dcs = new DataContractSerializer(typeof(T));
                dcs.WriteObject(stream, item);
                stream.Position = 0;
                return await Task.FromResult((T)dcs.ReadObject(stream));
            }
        }
        [Obsolete("TrimAllPropertiesAndWithSpecificCharacterAsync", false)]
        public static async Task<T> TrimAllPropertiesAsync<T>(this object obj) where T : class
        {
            Check.NotNull(obj, "object null");
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
            return await Task.FromResult((T)obj);
        }

        public static object GetPropValue(this object item, string propName)
        {
            return item.GetType().GetProperty(propName).GetValue(item, null);
        }

        public static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }

        public static void SetPropValue(this object item, string propName, object value)
        {
            PropertyInfo propertyInfoQuestion = item.GetType().GetProperty(propName);
            if (propertyInfoQuestion != null)
            {
                propertyInfoQuestion.SetValue(item, Convert.ChangeType(value, propertyInfoQuestion.PropertyType), null);
            }

        }
        public static async Task<T> TrimAllPropertiesAndWithSpecificCharacterAsync<T>(this object obj, string characterToClean = "") where T : class
        {
            Check.NotNull(obj, "object null");
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    var currentValue = (string)property.GetValue(obj, null);
                    if (currentValue != null)
                    {
                        property.SetValue(obj,
                            !String.IsNullOrEmpty(characterToClean)
                                ? currentValue.Trim().Replace(characterToClean, "")
                                : currentValue.Trim(), null);
                    }
                }
                else if (property.PropertyType == typeof(Char))
                {
                    obj = !String.IsNullOrEmpty(characterToClean)
                                ? ((string)obj).Trim().Replace(characterToClean, "")
                                : ((string)obj).Trim();
                }
                else
                {
                    var prop = property.GetValue(obj);
                    if (prop == null) continue;
                    if (prop is IEnumerable)
                    {
                        var propIEnumerable = prop as IEnumerable;
                        if (propIEnumerable != null)
                        {
                            var fixedList = new List<string>();
                            foreach (var listitem in propIEnumerable)
                            {
                                var propertyNested = listitem.GetType();
                                var name = propertyNested.Name;
                                if (name != "String")
                                {
                                    await TrimAllPropertiesAndWithSpecificCharacterAsync<object>(listitem, characterToClean);
                                }
                                else
                                {

                                    var temp = (string)listitem;
                                   var fixedTemp = !String.IsNullOrEmpty(characterToClean)? temp.Trim().Replace(characterToClean, ""): temp.Trim();
                                    fixedList.Add(temp);
                                }
                            }
                            if (fixedList.Any())
                            {
                                property.SetValue(obj, fixedList);
                            }
                        }

                    }
                    else
                    {
                        var name = property.Name;
                        var value = obj.GetPropValue(name);
                        var type = value.GetType();
                        if (type == typeof(DateTime) || type == typeof(DateTime?))
                        {
                            continue;
                        }
                        if (!type.IsPrimitive)
                        {
                            await TrimAllPropertiesAndWithSpecificCharacterAsync<object>(value, characterToClean);
                        }

                    }
                }

            }

            return await Task.FromResult((T)obj);
        }
    }


}