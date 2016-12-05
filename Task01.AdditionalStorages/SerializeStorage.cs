using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Task01.Logic;

namespace Task01.AdditionalStorages
{
    /// <summary>
    /// storage based on binary serialization
    /// </summary>
    public class SerializeStorage : IBookStorage
    {
        /// <summary>
        /// custom logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// path to file for save and load
        /// </summary>
        private readonly string path;


        public SerializeStorage(string path, ILogger logger)
        {
            this.path = path;
            this.logger = logger ?? new CustomNLogger(); ;
        }

        /// <summary>
        /// Load collection of book's from storage
        /// </summary>
        /// <returns> IEnumerable<Book> collection </returns>
        public IEnumerable<Book> Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                try
                {
                    return new SortedSet<Book>((Book[]) formatter.Deserialize(fs));
                }
                catch (SerializationException)
                {
                    throw new Exception("please check path arg in repo constructor");
                }
                catch(FileNotFoundException)
                {
                    throw new Exception("invalid path or file is no such file");
                }
            }
        }
    

        /// <summary>
        /// Save collection
        /// </summary>
        /// <param name="bookList"></param>
        public void Save(IEnumerable<Book> bookList)
        {
            if(bookList==null) throw new ArgumentNullException(nameof(bookList));
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs,bookList.ToArray());
            }
        }
    }
}
