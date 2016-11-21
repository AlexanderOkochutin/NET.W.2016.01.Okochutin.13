using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            BookListService bookService = new BookListService(test);
            bookService.AddBook(new Book("test","testAuthor","testGenre",2000,1));
            bookService.Remove(new Book("Net pro perfomance", "Goldshtein", "program", 2016, 5));
            BookStorage storage = new BookStorage(@"C:\booksStorage.txt");
            bookService.SaveToRepo(storage);
            Console.ReadLine();

        }
    }
}
