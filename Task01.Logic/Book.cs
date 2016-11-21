using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Task01.Logic
{
    public class Book : IEquatable<Book>, IComparable<Book>,IComparable, IFormattable
    {
        #region properties

        public string Title { get; }
        public string Author { get; }
        public string Genre { get; }
        public int Year { get; }
        public int Edition { get; }

        #endregion

        #region constructors

        public Book(string title, string author, string genre, int year, int edition)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Edition = edition;
        }

        #endregion

        #region Equals methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Book) obj);
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Title == other.Title)
                if (Author == other.Author)
                    if (Genre == other.Genre)
                        if (Year == other.Year)
                            if (Edition == other.Edition)
                                return true;
            return false;
        }

        #endregion

        #region GetHashCode

        public override int GetHashCode()
        {
            int hashcode = Title.GetHashCode();
            hashcode = 31*hashcode + Author.GetHashCode();
            hashcode = 31*hashcode + Genre.GetHashCode();
            hashcode = 31*hashcode + Year.GetHashCode();
            hashcode = 31*hashcode + Edition.GetHashCode();
            return hashcode;
        }

        #endregion

        #region CompareTo


        public int CompareTo(object obj)
        {
            if (obj.GetType() != GetType()) throw new ArgumentException("obj type is not book",nameof(obj));
            return CompareTo((Book) obj);
        }

        public int CompareTo(Book other)
        {
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this,  other)) return 0;
            return string.Compare(Title, other.Title, StringComparison.Ordinal);
        }

        #endregion

        #region ToString

        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "TAGYE":
                    return $"Title: {Title}\nAuthor: {Author}\nGenre: {Genre}\nYear: {Year}\nEdition: {Edition}";
                case "TAE":
                    return $"Title: {Title}\nAuthor: {Author}\nEdition: {Edition}";
                case "TAG":
                    return $"Title: {Title}\nAuthor: {Author}\nGenre: {Genre}";
                case "TY":
                    return $"Title: {Title}\nYear: {Year}";
                case "TA":
                    return $"Title: {Title}\nAuthor: {Author}";
                case "T":
                    return $"Title: {Title}";
                default:
                    throw new FormatException("such format string not supported");
            }
        }

        #endregion
    }
}
