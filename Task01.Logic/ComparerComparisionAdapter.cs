using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Logic
{
    /// <summary>
    /// Adapter for using comparision? when Icomparer need
    /// </summary>
    class ComparerComparisionAdapter: IComparer<Book>
    {

        private readonly Comparison<Book> comparision;

        /// <summary>
        /// constructor based on comparision
        /// </summary>
        /// <param name="comparision">input comparision delegate</param>
        public ComparerComparisionAdapter(Comparison<Book> comparision)
        {
            this.comparision += comparision;
        }

        public int Compare(Book x, Book y)
        {
            return comparision(x,y);
        }
    }
}
