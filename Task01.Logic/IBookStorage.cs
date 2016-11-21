using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Logic
{
    /// <summary>
    /// Interface for different book storages
    /// </summary>
    public interface IBookStorage
    {
        /// <summary>
        /// method for save coolection of books to storage
        /// </summary>
        /// <param name="bookList">collection of books</param>
        void Save(IEnumerable<Book> bookList);

        /// <summary>
        /// method which load sequence of books from storage
        /// </summary>
        IEnumerable<Book> Load();
    }
}
