using System.Collections.Generic;

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
