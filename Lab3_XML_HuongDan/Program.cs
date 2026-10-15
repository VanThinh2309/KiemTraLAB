using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab3_XML_HuongDan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("../../books.xml"); // Load the XML file

            var nodeList = xmlDoc.DocumentElement.SelectNodes("/catalog/book"); // Select all student nodes
            foreach (XmlNode node in nodeList)
            {
                //Read attribute value 
                var isbn = node.Attributes["ISBN"].Value;
                //Read child node values
                var title = node.SelectSingleNode("title").InnerText;
                var price = node.SelectSingleNode("price").InnerText;
                //Read the descendant node value
                var firstName = node.SelectSingleNode("author/first-name").InnerText;
                var lastName = node.SelectSingleNode("author/last-name").InnerText;
                Console.WriteLine("{0,-15}{1,-50}{2,-15}{3,-15}{4,6}",
                    isbn, title, firstName, lastName, price);
            }
            Console.ReadLine();
        }
    }
}
