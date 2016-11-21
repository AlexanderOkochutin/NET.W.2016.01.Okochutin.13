using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Task01.Logic
{
    /// <summary>
    /// Service for work with book collections(set), and save and load to binary repo
    /// </summary>
    public class BookListService
    {

        private Logger logger = LogManager.GetCurrentClassLogger();
        private SortedSet<Book> bookSet;

        #region Constructors

        /// <summary>
        /// default constructor
        /// </summary>
        public BookListService()
        {
            bookSet = new SortedSet<Book>();
        }

        /// <summary>
        /// constructor based on collection
        /// </summary>
        /// <param name="books">input collection of books</param>
        public BookListService(IEnumerable<Book> books ): this()
        {
            try
            {
                if (books == null)
                    throw new ArgumentNullException(nameof(books), "given to constructor collection cant be null");
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }

            foreach (var book in books)
            {
                try
                {
                    if (book == null) throw new ArgumentNullException(nameof(book), "book cant be null");
                    if (!bookSet.Add(book))
                        throw new ArgumentException("this element already exist in set", nameof(book));
                }
                catch (ArgumentNullException exception)
                {
                    logger.Info(exception.Message);
                    logger.Trace(exception.StackTrace);
                    throw;
                }
                catch (ArgumentException exception)
                {
                    logger.Info(exception.Message);
                    logger.Trace(exception.StackTrace);
                    throw;
                }
            }
        }

        #endregion

        #region ServiceFunctionality

        public void AddBook(Book book)
        {
            try
            {
                if (book == null) throw new ArgumentNullException(nameof(book), "book cant be null");
                if (!bookSet.Add(book)) throw new ArgumentException("this element already exist in set", nameof(book));
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
            catch (ArgumentException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
           
        }

        public void Remove(Book book)
        {
            try
            {
                if(book == null) throw new ArgumentNullException(nameof(book),"book cant be null");
                if (!bookSet.Remove(book)) throw new ArgumentException("no such element in set", nameof(book));
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
            catch (ArgumentException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
            }
        }

        Book FindByTag(Predicate<Book> predicate)
        {
            try
            {
                if (predicate == null) throw new ArgumentNullException(nameof(predicate), "predicate cant be null");
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
            try
            {
                foreach (var book in bookSet.Where(book => predicate(book)))
                {
                    if (book == null) throw new ArgumentNullException(nameof(book), "book cant be null");
                    return new Book(book.Title, book.Author, book.Genre, book.Year, book.Edition);
                }
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
            return null;
        }

        public void SortByTag(IComparer<Book> comparer)
        {
            try
            {
                if (comparer == null) throw new ArgumentNullException(nameof(comparer), "comparer cant be null");
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
            bookSet = new SortedSet<Book>(bookSet,comparer);
        }

        public void SortByTag(Comparison<Book> comparison)
        {
            try
            {
                if(comparison == null) throw new ArgumentNullException(nameof(comparison),"comparision cant be null");
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
            bookSet = new SortedSet<Book>(bookSet,new ComparerComparisionAdapter(comparison));
        }

        #endregion


        #region RepoFunctionality

        public void SaveToRepo(IBookStorage storage)
        {
            try
            {
                if(storage == null) throw new ArgumentNullException(nameof(storage),"storage cant be null");
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
            storage.Save(bookSet);
        }


        public void LoadFromRepo(IBookStorage storage)
        {
            try
            {
                if (storage == null) throw new ArgumentNullException(nameof(storage), "storage cant be null");
            }
            catch (ArgumentNullException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
            bookSet = new SortedSet<Book>(storage.Load());
        }

        #endregion
    }
}
