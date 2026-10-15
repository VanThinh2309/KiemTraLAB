using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lab3_XML1_HuongDan
{
   
    internal class Program
    {
        private static void SaveToXmlFile(List<Book> books)
        {
            var serializer = new XmlSerializer(typeof(List<Book>));

            using (var writer = new StreamWriter("books.xml"))
            {
                serializer.Serialize(writer, books, null);
                writer.Close();
            }

        }

        static void Main(string[] args)
        {

            using (XmlWriter writer = XmlWriter.Create("books.xml"))
            {
                // Write Processing Instruction
                String pi = "type=\"text/xsl\" href=\"book.xsl\"";
                writer.WriteProcessingInstruction("xml-stylesheet", pi);
                // Write DocumentType
                writer.WriteDocType("catalog", null, null, "<!ENTITY h \"hardcover\">");
                // Write a Comment
                writer.WriteComment("This is a book sample XML");
                // Root element - start tag
                writer.WriteStartElement("book");
                // Write ISBN attribute
                writer.WriteAttributeString("ISBN", "9831123212");
                // Write year attribute
                writer.WriteAttributeString("yearpublished", "2002");
                // Write title
                writer.WriteElementString("author", "Mahesh Chand");
                // Write author
                writer.WriteElementString("title", "Visual C# Programming");
                // Write price
                writer.WriteElementString("price", "44.95");
                // Root element - end tag
                writer.WriteEndElement();
                // End Documentd
                writer.WriteEndDocument();
                // Flush it
                writer.Flush();

            }
            List<Book> books = new List<Book>
            {
             new Book { ISBN = "9831123212", Title = "Visual C# Programming", Author = "Mahesh Chand", Price = 44.95m, YearPublished = 2002 }
            };
            SaveToXmlFile(books);
        }


    }
}
