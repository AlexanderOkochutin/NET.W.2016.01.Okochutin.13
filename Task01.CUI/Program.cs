﻿using System;
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
                new Book("Solaris","Lem","Fantastic",1961,1)
            };
            BookListService bookService = new BookListService(test);
            BookStorage storage = new BookStorage(@"C:\boo5ksStorage.txt");
            bookService.LoadFromRepo(storage);
            Console.ReadLine();

        }
    }
}
