using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Helpers
{
    public static class ExtensionMethods
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetTypeInfo().GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static string GetDescription(this Enum value)
        {
            return value.GetAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
        }

        public static int ToEpoch(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1);
            var epochTimeSpan = date - epoch;
            return (int)epochTimeSpan.TotalSeconds;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }






        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        //public static string ObjectToJsonString(this object myObject)
        //{
        //    return JsonConvert.SerializeObject(myObject);
        //}

        //public static string ObjectToJsonStringWithoutChildren(this object myObject)
        //{
        //    return JsonConvert.SerializeObject(myObject, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        //}

        public static string DateToShortDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;

            return ((DateTime)dateTime).ToShortDateString();
        }

        public static string DateToShortDateTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;

            return $"{((DateTime)dateTime).ToShortDateString()} {((DateTime)dateTime).ToShortTimeString()}";
        }

        public static string DateToShortDateTimeString(this DateTimeOffset? dateTime)
        {
            if (dateTime == null)
                return string.Empty;

            var dateTimeToConver = ((DateTimeOffset)dateTime).DateTime;

            return $"{dateTimeToConver.ToShortDateString()} {dateTimeToConver.ToShortTimeString()}";
        }

        //public static string TryToTitleCase(this string str)
        //{
        //    try
        //    {
        //        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }

        //}

        public static string TryToString(this object myObject)
        {
            try
            {
                return myObject?.ToString() ?? "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string TryTrimEnd(this string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                    return string.Empty;

                return str.TrimEnd();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string TrySubstring(this string text, int start, int length)
        {
            if (text == null) return null;

            return text.Length <= start ? ""
                : text.Length - start <= length ? text.Substring(start)
                    : text.Substring(start, length);
        }

        public static string TryTrim(this string str)
        {
            try
            {
                return string.IsNullOrEmpty(str) ? string.Empty : str.Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string TryRight(this string source, int numberOfCharacters)
        {
            if (source == null || numberOfCharacters >= source.Length)
                return source;

            return source.Substring(source.Length - numberOfCharacters);
        }

        public static string TryToUpper(this string str)
        {
            try
            {
                if (str == null)
                    return string.Empty;

                return str.ToUpper();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string TryToLower(this string str)
        {
            try
            {
                if (str == null)
                    return string.Empty;

                return str.ToLower();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string TryReplace(this string str, string textBeingReplaced, string textToReplaceWith)
        {
            try
            {
                return str.Replace(textBeingReplaced, textToReplaceWith);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }        
    }
}