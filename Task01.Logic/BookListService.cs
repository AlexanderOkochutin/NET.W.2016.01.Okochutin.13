using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Logic
{
    public class BookListService
    {
        private SortedSet<Book> bookSet;

        #region Constructors

        public BookListService()
        {
            bookSet = new SortedSet<Book>();
        }

        public BookListService(IEnumerable<Book> books ): this()
        {
            foreach (var book in books)
            {
                if (!bookSet.Add(book)) throw new ArgumentException();
            }
        }

        #endregion

        #region ServiceFunctionality

        public void AddBook(Book book)
        {
            if(!bookSet.Add(book)) throw new ArgumentException();
        }

        public void Remove(Book book)
        {
            if(!bookSet.Remove(book)) throw new ArgumentException();
        }

        Book FindByTag(Predicate<Book> predicate)
        {
            foreach (var book in bookSet.Where(book => predicate(book)))
            {
                return new Book(book.Title,book.Author,book.Genre,book.Year,book.Edition);
            }
            return null;

        }

        public void SortByTag(IComparer<Book> comparer)
        {
            bookSet = new SortedSet<Book>(bookSet,comparer);
        }

        public void SortByTag(Comparison<Book> comparison)
        {
            bookSet = new SortedSet<Book>(bookSet,new ComparerComparisionAdapter(comparison));
        }

        #endregion


        #region RepoFunctionality

        public void SaveToRepo(IBookStorage storage)
        {
            storage.Save(bookSet);
        }

        public void LoadFromRepo(IBookStorage storage)
        {
            bookSet = new SortedSet<Book>(storage.Load());
        }

        #endregion

        #region GetCollection

         

        #endregion
    }
}
