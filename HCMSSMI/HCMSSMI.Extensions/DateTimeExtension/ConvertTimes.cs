using System;
using System.Globalization;

namespace HCMSSMI.Extensions
{
    public static class ConvertTimes
    {
        public static DateTime? ToDateTime(this string dateTime, string dateFormat = "yyyy-MM-dd HH:mm:ss") 
            => DateTime.ParseExact($"{dateTime}", $"{dateFormat}", CultureInfo.InvariantCulture);
        
    }
}
