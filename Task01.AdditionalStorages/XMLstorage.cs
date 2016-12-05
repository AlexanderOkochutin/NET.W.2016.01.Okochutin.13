using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Task01.Logic;

namespace Task01.AdditionalStorages
{
    /// <summary>
    /// book repository based on xml serialization
    /// </summary>
    public class XMLstorage : IBookStorage
    {
        /// <summary>
        /// path to file for save and load
        /// </summary>
        private readonly string path;

        /// <summary>
        /// custom logger
        /// </summary>
        private readonly ILogger logger;

        public XMLstorage(string path, ILogger logger)
        {
            this.path = path;
            this.logger = logger;
        }

        /// <summary>
        /// load collection of books from repo
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> Load()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Book[]));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                try
                {
                    return new SortedSet<Book>((Book[]) formatter.Deserialize(fs));
                }
                catch (FileNotFoundException ex)
                {
                    throw new Exception("please check path in repo constructor");
                }
                catch
                {
                    throw new InvalidOperationException("xml-repo work only with xml files(path)");
                }
            }
        }

        /// <summary>
        /// save collection of books to the repo
        /// </summary>
        /// <param name="bookList"></param>
        public void Save(IEnumerable<Book> bookList)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Book[]));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                    formatter.Serialize(fs,bookList.ToArray());
            } 
        }
    }
}
