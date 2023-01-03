using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Extras
{
    public static class ObjectExtensions
    {
        public static void UniqueIfNotEmpty(this List<string> collection, string value)
        {
                if (string.IsNullOrWhiteSpace(value) || collection.Contains(value)) return;
                collection.Add(value);
        }
    }
}
