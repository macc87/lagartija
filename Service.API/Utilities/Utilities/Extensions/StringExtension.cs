using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utilities.Extensions.Core;
using Utilities.Validation;

namespace Utilities.Extensions
{
    public static class StringExtension
    {
        private const string PassPhrase = "P57uur@s!!!%%verys3cr3t##^^%%ertuhfdgnDDdgARppqF!qwehfjds;;sms"; // can be any string
        private const string InitVector = "@1B!!3D4e5F6g7H8"; // must be 16 bytes

        private static readonly StringBuilder Buffer = new StringBuilder();

        public static TimeSpan GetTimeSpan(this string interval)
        {
            //  interval='60s' = 60 Seconds
            //  Posible units -> t-Ticks l-Milliseconds s-Seconds m-Minutes h-Hours d-Days
            //  Default unit is seconds (unit not specified or not in supplied list)
            string timeString;
            var timeUnit = ValidateInterval(interval, out timeString);
            switch (timeUnit)
            {
                case "t":
                    return TimeSpan.FromTicks(Convert.ToInt64(timeString)); // Default time in seconds
                case "l":
                    return TimeSpan.FromMilliseconds(Convert.ToInt64(timeString)); // Default time in seconds
                case "m":
                    return TimeSpan.FromMinutes(Convert.ToInt64(timeString)); // Default time in seconds
                case "h":
                    return TimeSpan.FromHours(Convert.ToInt64(timeString)); // Default time in seconds
                case "d":
                    return TimeSpan.FromDays(Convert.ToInt64(timeString)); // Default time in seconds
                default:
                    return TimeSpan.FromSeconds(Convert.ToInt64(timeString)); // Default time in seconds
            }
        }

        public static DateTime GetDateTimeSpan(this string interval)
        {
            //  interval='60s' = 60 Seconds
            //  Posible units -> t-Ticks l-Milliseconds s-Seconds m-Minutes h-Hours d-Days y -Years n -Months
            //  Default unit is seconds (unit not specified or not in supplied list)
            string timeString;
            var timeUnit = ValidateInterval(interval, out timeString);

            switch (timeUnit)
            {
                case "t":
                    return DateTime.UtcNow.AddTicks(Convert.ToInt64(timeString));
                case "l":
                    return DateTime.UtcNow.AddMilliseconds(Convert.ToDouble(timeString));
                case "m":
                    return DateTime.UtcNow.AddMinutes(Convert.ToDouble(timeString));
                case "h":
                    return DateTime.UtcNow.AddHours(Convert.ToDouble(timeString));
                case "d":
                    return DateTime.UtcNow.AddDays(Convert.ToDouble(timeString));
                case "n":
                    return DateTime.UtcNow.AddMonths(Convert.ToInt32(timeString));
                case "y":
                    return DateTime.UtcNow.AddYears(Convert.ToInt32(timeString));
                default:
                    return DateTime.UtcNow.AddSeconds(Convert.ToDouble(timeString));
            }
        }

        public static String RemoveWhiteSpaces(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new Exception("The string provided is null or empty.");
            }
            return Regex.Replace(str, @"\s+", " ").Trim();
        }

        public static DateTime? ToNullableDate(this String dateString)
        {
            if (String.IsNullOrEmpty((dateString ?? "").Trim()))
                return null;

            DateTime resultDate;
            if (DateTime.TryParse(dateString, out resultDate))
                return resultDate;

            return null;
        }

        public static String RemoveSpecialCharacters(this string str)
        {
            Regex r = new Regex("[^A-Za-z0-9 ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            var result = r.Replace(str, string.Empty);
            return result;
        }
        private static string ValidateInterval(string interval, out string timeString)
        {
            if (String.IsNullOrEmpty(interval))
            {
                interval = "60s";
            }
            var timeUnit = interval.Substring(interval.Length - 1, 1);

            timeString = interval;

            if (!(timeUnit.All(Char.IsDigit)))
                timeString = interval.Substring(0, interval.Length - 1);
            return timeUnit;
        }

        public static async Task<T> DeserializeXmlAsync<T>(this string xmlString) where T : class
        {
            T returnValue = default(T);

            var action = new Action(() =>
            {
                var serial = new XmlSerializer(typeof(T));
                var reader = new StringReader(xmlString);
                object result = serial.Deserialize(reader);

                if (result != null && result is T)
                {
                    returnValue = ((T)result);
                }

                reader.Close();
            });

            returnValue = await Task.Run(action).ContinueWith(task => returnValue);

            return returnValue;
        }

        public static T DeserializeXml<T>( this string xmlString ) where T : class
        {
            T returnValue = default( T );

            var serial = new XmlSerializer( typeof( T ) );
            var reader = new StringReader( xmlString );
            var result = serial.Deserialize( reader );
            if ( result is T )
            {
                returnValue = ( (T)result );
            }
            reader.Close();
            return returnValue;
        }

        public static async Task<T?> GetValueOrNull<T>(this string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            return await Task.FromResult((T)Convert.ChangeType(valueAsString, typeof(T)));
        }

        public static string IfEmptyToNull(this string valueAsString)
        {
            return string.IsNullOrEmpty(valueAsString) ? null : valueAsString;
        }

        public static async Task<T?> ToNullable<T>(this string s) where T : struct
        {
            var result = new T?();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    var conv = TypeDescriptor.GetConverter(typeof(T));
                    var convertFrom = conv.ConvertFrom(s);
                    if (convertFrom != null) result = (T)convertFrom;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return await Task.FromResult(result);
        }
        public static int ToInt32(this string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return 0;
        }
        public static long ToInt64(this string s)
        {
            long i;
            if (Int64.TryParse(s, out i)) return i;
            return 0;
        }
        public static int? ToNullableInt32(this string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }

        public static long? ToNullableInt64(this string s)
        {
            long i;
            if (Int64.TryParse(s, out i)) return i;
            return null;
        }

        /// <summary>
        /// Get string value between [first] a and [last] b.
        /// </summary>
        public static string Between(this string value, string a, string b)
        {
            int posA = value.IndexOf(a, StringComparison.Ordinal);
            int posB = value.IndexOf(b, posA + a.Length, StringComparison.Ordinal);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        public static string Before(this string value, string a)
        {
            int posA = value.IndexOf(a, StringComparison.Ordinal);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        public static string After(this string value, string a)
        {
            int posA = value.LastIndexOf(a, StringComparison.Ordinal);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }


        public static string Encrypt(this string value, string passPhrase = null, string initVector = null)
        {
            Check.NotEmpty(value, "value");
            var rijndaelKey = new RijndaelEnhanced(passPhrase ?? PassPhrase, initVector ?? InitVector);
            var cipherText = rijndaelKey.Encrypt(value);
            if (cipherText != null)
            {
                return cipherText;
            }
            throw new Exception("Encryption failed for: " + value);

        }

        public static string Decrypt(this string value, string passPhrase = null, string initVector = null)
        {
            Check.NotEmpty(value, "value");
            var rijndaelKey = new RijndaelEnhanced(passPhrase ?? PassPhrase, initVector ?? InitVector);
            var cipherText = rijndaelKey.Decrypt(value);
            if (cipherText != null)
            {
                return cipherText;
            }
            throw new Exception("Decryption failed for: " + value);

        }

        public static string NormalizeToXml(this string value)
        {
            return value
                   .Replace("&", "&amp;")
                   .Replace("<", "&lt;")
                   .Replace(">", "&gt;")
                   .Replace("\"", "&quot;")
                   .Replace("`", "&#39;")
                   .Replace("'", "&apos;")
                   .Replace("+", " ");
        }

        public static string NormalizeFromXml(this string value)
        {
            return value
                   .Replace("&amp;", "&")
                   .Replace("&lt;", "<")
                   .Replace("&gt;", ">")
                   .Replace("&quot;", "\"")
                   .Replace("&#39;", "`")
                   .Replace("&apos;", "'");
        }

    }
}
