using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Logic
{
    public class BookStorage : IBookStorage
    {
        private ILogger logger;

        /// <summary>
        /// path to file-storage
        /// </summary>
        private readonly string path;

        public BookStorage(string path,ILogger logger)
        {
            this.path = path;
            this.logger = logger ?? new CustomNLogger();
        }

        /// <summary>
        /// save-method collection to the binary storage
        /// </summary>
        /// <param name="bookList"> input collection of books</param>
        /// <exception cref="FileNotFoundException">when BookStorage construct with incorrect path</exception>
        public void Save(IEnumerable<Book> bookList)
        {
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                    {
                        foreach (Book book in bookList)
                        {
                            binaryWriter.Write(book.Title);
                            binaryWriter.Write(book.Author);
                            binaryWriter.Write(book.Genre);
                            binaryWriter.Write(book.Year);
                            binaryWriter.Write(book.Edition);
                        }
                    }
                }
            }

            catch (FileNotFoundException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// load-method from binary storage
        /// </summary>
        /// <returns>collection of books</returns>
        /// <exception cref="FileNotFoundException">when BookStorage construct with incorrect path</exception>
        public IEnumerable<Book> Load()
        {
            SortedSet<Book> booksSet = new SortedSet<Book>();
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                        {
                            string title = binaryReader.ReadString();
                            string author = binaryReader.ReadString();
                            string genre = binaryReader.ReadString();
                            int year = binaryReader.ReadInt32();
                            int edition = binaryReader.ReadInt32();
                            booksSet.Add(new Book(title, author, genre, year, edition));
                        }
                    }
                }
                return booksSet;
            }

            catch (FileNotFoundException exception)
            {
                logger.Info(exception.Message);
                logger.Trace(exception.StackTrace);
                throw;
            }
        }
    }
}
