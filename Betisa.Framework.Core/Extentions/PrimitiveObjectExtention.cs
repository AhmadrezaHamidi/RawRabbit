using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Betisa.Framework.Core.Extentions
{
    public static class PrimitiveObjectExtention
    {

        public static int Count(this int number)
        {
            if (number == 0)
                return 1;
            if (number < 0)
                return (int)Math.Floor(Math.Log10(Math.Abs(number)) + 1);
            return (int)Math.Floor(Math.Log10(number) + 1);
        }

        public static int Count(this decimal number)
        {
            return Count((double)number);
        }

        public static int Count(this double number)
        {
            if (number == 0)
                return 1;
            if (number < 0)
                return (int)Math.Floor(Math.Log10(Math.Abs(number)) + 1);
            return (int)Math.Floor(Math.Log10(number) + 1);
        }

        public static int ToInt(this string value)
        {
            if (int.TryParse(value, out var number))
                return number;
            return 0;
        }

        public static decimal ToDecimal(this string value)
        {
            if (decimal.TryParse(value, out var result))
                return result;
            return 0;
        }

        public static long ToLong(this string value)
        {
            if (long.TryParse(value, out var result))
                return result;
            return 0;
        }

        public static double ToDouble(this string value)
        {
            if (double.TryParse(value, out var result))
                return result;
            return 0;
        }

        public static IEnumerable<T> Insert<T>(this IEnumerable<T> source, int index, T item)
        {
            var list = new List<T>(source);
            list.Insert(index, item);
            return list;
        }

        public static string Underscore(this string value) => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }

        public static long ToLong(this Guid g)
        {
            return BitConverter.ToInt64(g.ToByteArray(), 0);
        }
        public static long ToLong(this Guid g, int lenght)
        {
            return BitConverter.ToInt64(g.ToByteArray(), 0).ToString().Substring(0, lenght).ToLong();
        }

        public static int ToInt(this Guid g)
        {
            return BitConverter.ToInt32(g.ToByteArray(), 0);
        }

        public static string StrJoin(this IEnumerable<string> source, string seperator)
        {
            return string.Join(seperator, source);
        }

        public static int RemoveWhen<T>(this ObservableCollection<T> collection, Func<T, bool> predict)
        {
            var founded = collection.Where(predict).Select((x, i) => new { Index = i, Item = x }).FirstOrDefault();
            if (founded != null)
            {
                collection.Remove(founded.Item);
                return founded.Index;
            }
            return -1;
        }

        public static void RemoveWhen<T>(this List<T> collection, Func<T, bool> predict)
        {
            var item = collection.Where(predict).FirstOrDefault();
            if (item != null)
            {
                collection.Remove(item);
            }
        }

        public static Task<TResult> ContinueOn<T, TResult>(this Task<T> t, Func<TResult> predic)
        {
            return t.ContinueWith<TResult>(x => predic());
        }

        public static TResult ContinueOn<T, TResult>(this Task<T> t, Func<T, TResult> predic)
        {
            return t.ContinueWith<TResult>(x => predic(x.Result)).Result;
        }

        public static Task ContinueWithOnUI<TResult>(this Task<TResult> t, Action<TResult> predic)
        {

            return t.ContinueWith((cont) =>
            {
                predic(cont.Result);
            });
        }

        public static Task TryContinueWithOnUI<TResult>(this Task<TResult> t, Action<TResult> predic)
        {
            Task tresult = null;
            try
            {
                tresult = ContinueWithOnUI(t, predic);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} {e.InnerException?.Message}");
            }
            return tresult;
        }

        public static Task ContinueWithOnUI(this Task t, Action predic)
        {
            return t.ContinueWith((cont) =>
            {
                predic();
            });
        }

        public static Task<TResult> ContinueWithOnUI<TResult>(this Task<TResult> t)
        {
            return t.ContinueWith<TResult>((cont) =>
            {
                return cont.Result;
            });
        }

        public static List<string> ToList(this Enum source)
        {
            return Enum.GetNames(source.GetType()).ToList();
        }

        public static int ToInt(this Enum source)
        {
            return Convert.ToInt32(source);
        }

        public static TEnum ToEnum<TEnum>(this int number) where TEnum : struct
        {
            var obj = Enum.ToObject(typeof(TEnum), number);
            return (TEnum)obj;
        }

        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            var obj = Enum.Parse(typeof(TEnum), value);
            return (TEnum)obj;
        }

        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> source)
        {
            if (source is null)
                return new ObservableCollection<T>();
            return new ObservableCollection<T>(source);
        }

        public static byte[] GetBytesFromPostedFile(this IFormFile file)
        {
            byte[] data = null;
            if (file != null && file.Length > 0)
                using (var ms = new MemoryStream())
                {
                    data = new byte[file.Length];
                    using (var fs = file.OpenReadStream())
                    {
                        fs.Read(data, 0, data.Length);
                    }
                    //file.CopyTo(ms);
                    //return ms.ToArray();
                }
            return data;
        }

        public static string ToPersianDate(this DateTime dt)
        {
            PersianCalendar p = new PersianCalendar();
            return $"{p.GetYear(dt)}/{p.GetMonth(dt)}/{p.GetDayOfMonth(dt)}";
        }

        public static string GetEnumDescription(this Enum enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
                return enumObj.ToString();
            else
            {
                DescriptionAttribute attrib = null;

                foreach (var att in attribArray)
                    if (att is DescriptionAttribute)
                        attrib = att as DescriptionAttribute;

                if (attrib != null)
                    return attrib.Description;

                return enumObj.ToString();
            }
        }

        public static string ValidatePhoneNumber(this string value)
        {
            var str = "";
            foreach (var item in value)
            {
                switch (item)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        str += item;
                        break;
                }
            }
            return Regex.Replace(str, "^98", "0");
        }
    }
}
