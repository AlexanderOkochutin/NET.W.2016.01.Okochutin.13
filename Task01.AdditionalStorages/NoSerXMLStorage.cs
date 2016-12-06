using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Task01.Logic;

namespace Task01.AdditionalStorages
{
    public class NoSerXMLStorage : IBookStorage
    {
        private readonly string path;
        private readonly ILogger logger;

        public NoSerXMLStorage(string path, ILogger logger)
        {
            this.path = path;
            this.logger = logger;
        }

        public IEnumerable<Book> Load()
        {
            SortedSet<Book> booksSet = new SortedSet<Book>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRootElement = xDoc.DocumentElement;
            foreach (XmlNode xNode in xRootElement)
            {
                Book newBook = new Book();
                foreach (XmlNode childNodes in xNode.ChildNodes)
                {
                    switch (childNodes.Name)
                    {
                        case "Title":
                            newBook.Title = childNodes.InnerText;
                            break;
                        case "Author":
                            newBook.Author = childNodes.InnerText;
                            break;
                        case "Genre":
                            newBook.Genre = childNodes.InnerText;
                            break;
                        case "Year":
                            newBook.Year = int.Parse(childNodes.InnerText);
                            break;
                        case "Edition":
                            newBook.Edition = int.Parse(childNodes.InnerText);
                            break;
                    }
                }
                booksSet.Add(newBook);
            }
            return booksSet;
        }

        public void Save(IEnumerable<Book> bookList)
        {
            if(bookList == null) throw new ArgumentNullException(nameof(bookList));
            XDocument xdoc = new XDocument();
            XElement books = new XElement("Books");
            foreach (var book in bookList)
            {
                AddElemToRoot(books, book);
            }
            xdoc.Add(books);
            xdoc.Save(path);         
        }

        private void AddElemToRoot(XElement root, Book book)
        {
            XElement xmlBook = new XElement("Book");
            XElement bookTitle = new XElement("Title", book.Title);
            XElement bookAuthor = new XElement("Author", book.Author);
            XElement bookGenre = new XElement("Genre", book.Genre);
            XElement bookYear = new XElement("Year", book.Year);
            XElement bookEdition = new XElement("Edition", book.Edition);
            xmlBook.Add(bookTitle);
            xmlBook.Add(bookAuthor);
            xmlBook.Add(bookGenre);
            xmlBook.Add(bookYear);
            xmlBook.Add(bookEdition);
            root.Add(xmlBook);
        }
    }
}
