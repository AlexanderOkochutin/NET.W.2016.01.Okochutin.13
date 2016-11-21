using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Logic
{
    class ComparerComparisionAdapter: IComparer<Book>
    {

        private readonly Comparison<Book> comparision;

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
