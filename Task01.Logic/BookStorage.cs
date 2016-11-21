using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01.Logic
{
    public class BookStorage:IBookStorage
    {

        private readonly string path;

        public BookStorage(string path)
        {
            this.path = path;
        }

        public void Save(IEnumerable<Book> bookList)
        {
            using (FileStream fileStream = new FileStream(path,FileMode.Open))
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

        public IEnumerable<Book> Load()
        {
            SortedSet<Book> booksSet = new SortedSet<Book>();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    string title = binaryReader.ReadString();
                    string author = binaryReader.ReadString();
                    string genre = binaryReader.ReadString();
                    int year = binaryReader.ReadInt32();
                    int edition = binaryReader.ReadInt32();
                    booksSet.Add(new Book(title, author, genre, year, edition));
                }
            }
            return booksSet;
        }
    }
}
