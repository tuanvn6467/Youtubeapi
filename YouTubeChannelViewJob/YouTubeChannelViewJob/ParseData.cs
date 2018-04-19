using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeChannelViewJob
{
    public static class ParseData
    {
        public static int? GetInt(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return int.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static long? GetLong(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return long.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static double? GetDouble(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return double.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static decimal? GetDecimal(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return decimal.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static short? GetShort(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return short.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool? GetBool(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return bool.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static float? GetFloat(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return float.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static byte? GetByte(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return byte.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static TimeSpan? GetTimeSpan(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return TimeSpan.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DateTime? GetDateTime(this object value)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return DateTime.Parse(value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DateTime? GetDateTime(this object value, string format, CultureInfo cultureinfo)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                return DateTime.ParseExact(value.ToString(), format, cultureinfo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetString(object value)
        {
            if (value != null)
            {
                return value.ToString();
            }
            return string.Empty;
        }
    }
}
