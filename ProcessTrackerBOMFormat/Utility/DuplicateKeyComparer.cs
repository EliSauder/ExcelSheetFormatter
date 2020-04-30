using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formatter.Processing {

    [Obsolete]
    public class DuplicateKeyComparer<T> : IComparer<T> where T : IComparable {
        public int Compare(T x, T y) {
            int result = x.CompareTo(y);
            return result == 0 ? 1 : result;
        }
    }
}
