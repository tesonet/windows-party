using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class NavigatePatameters
    {
        private static Dictionary<int, object> paramList =
            new Dictionary<int, object>();

        public static void Save(int hash, object value)
        {
            if (!paramList.ContainsKey(hash))
                paramList.Add(hash, value);
        }

        public static object Request(int hash)
        {
            return ((KeyValuePair<int, object>)paramList.
                        Where(x => x.Key == hash).FirstOrDefault()).Value;
        }
    }
}
