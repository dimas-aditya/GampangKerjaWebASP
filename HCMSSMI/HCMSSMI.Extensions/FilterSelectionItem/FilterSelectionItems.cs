using System;
using System.Collections.Generic;
using System.Linq;

namespace HCMSSMI.Extensions
{
    public static class FilterSelectionItems
    {
        public static List<string> FilterItemsByComma(string source)
        {
            List<string> dataSource = source.Split(';').ToList();
            return dataSource;
        }
    }
}
