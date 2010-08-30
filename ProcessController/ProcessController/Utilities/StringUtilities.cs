using System.Collections.Generic;
using System.Linq;

namespace ProcessController.Utilities
{
    public static class StringUtilities
    {
        public static string ListToString(IEnumerable<string> list)
        {
            return (list.Count() > 0 ? list.Aggregate((x, y) => (x + "; " + y)) : string.Empty);
        }
    }
}