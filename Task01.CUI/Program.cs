using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task01.AdditionalStorages;
using Task01.Logic;

namespace Task01.CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Book> test = new List<Book>()
            {
                new Book("clr Via c#","Richter","program",2015,6),
                new Book("Net pro perfomance","Goldshtein","program",2016,5),
                new Book("Solaris","Lem","Fantastic",1961,1),
            };
            BookListService bookService = new BookListService(test,null);
            bookService.AddBook(new Book("test","testAuthor","testGenre",2000,1));
            SerializeStorage serStorage = new SerializeStorage(@"C:\OldbooksStorage.txt",null);
            XMLstorage xmlStorage = new XMLstorage(@"C:\NewbooksStorage.txt", null);
            BookStorage storage = new BookStorage(@"C:\NewbooksStorage.txt", null);
            bookService.SaveToRepo(xmlStorage);
            bookService.SaveToRepo(serStorage);
            bookService.SaveToRepo(storage);
            BookListService testXml = new BookListService(xmlStorage.Load(),null); 
            BookListService testSer = new BookListService(serStorage.Load(),null);
            BookListService testOld = new BookListService(storage.Load(), null);
            Console.ReadLine();
        }
    }
}
