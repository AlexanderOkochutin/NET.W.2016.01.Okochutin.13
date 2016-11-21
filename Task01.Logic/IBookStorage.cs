using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Logic
{
    public interface IBookStorage
    {
        void Save(IEnumerable<Book> bookList);
        IEnumerable<Book> Load();
    }
}
