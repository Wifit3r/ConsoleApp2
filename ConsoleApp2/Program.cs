using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Xml;
using System.IO;
using System.Xml.Resolvers;
using System.Xml.XPath;
using System.Linq;
using System.Xml.Serialization;
internal class Program
{
    private static void Main(string[] args)
    {
        string xmlPAth = @"C:\Users\Wifit\source\repos\ConsoleApp2\ConsoleApp2\bin\Debug\net8.0\buhalteria.xml";
        using (XmlTextWriter writer = new XmlTextWriter(xmlPAth, null))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("Buhalteria");
            writer.WriteStartElement("Worker");
            writer.WriteAttributeString("Surname", "Ivanenko");
            writer.WriteElementString("Position", "Mananger");
            writer.WriteElementString("Payment", "19000");
            writer.WriteElementString("Childrens", "2");
            writer.WriteElementString("experiance", "2");
            writer.WriteEndElement();
            writer.WriteStartElement("Worker");
            writer.WriteAttributeString("Surname", "Petrenko");
            writer.WriteElementString("Position", "Ingeneer");
            writer.WriteElementString("Payment", "17000");
            writer.WriteElementString("Childrens", "1");
            writer.WriteElementString("experiance", "5");
            writer.WriteEndElement();
            writer.WriteStartElement("Worker");
            writer.WriteAttributeString("Surname", "Stepanenko");
            writer.WriteElementString("Position", "Buhalter");
            writer.WriteElementString("Payment", "21000");
            writer.WriteElementString("Childrens", "0");
            writer.WriteElementString("experiance", "9");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();


        }
        void Display(string Path)
        {
            using (XmlTextReader reader = new XmlTextReader(Path))
            {
                while (reader.Read())
                {
                    if(reader.NodeType == XmlNodeType.Element&& reader.Name == "Worker")
                    {

                        var surname = reader.GetAttribute("Surname");
                        reader.ReadStartElement("Worker");
                        var position = reader.ReadElementString("Position");
                        var payment = reader.ReadElementString("Payment");
                        var children = reader.ReadElementString("Childrens");
                        var exp = reader.ReadElementString("experiance");

                        Console.WriteLine($"{surname}: Payemnt={payment}, position={position}, Children={children}, Eperiance={exp} ");
                    }
                }
            }
        }
        void SearchByName(string Surname, string path)
        {
            using (XmlTextReader reader = new XmlTextReader(path)) { 
                while (reader.Read())
                {
                    if (reader.GetAttribute("Surname") == Surname)
                    {
                        reader.ReadStartElement("Worker");
                        var position = reader.ReadElementString("Position");
                        var payment = reader.ReadElementString("Payment");
                        var children = reader.ReadElementString("Childrens");
                        var exp = reader.ReadElementString("experiance");
                        Console.WriteLine($"{Surname}: Payemnt={payment}, position={position}, Children={children}, Eperiance={exp} ");
                    }
                }

            };
        }
        void FilterFromKeyboard(string path)
        {
            Console.WriteLine("Введіть посаду: ");
            var position = Console.ReadLine();
            Console.WriteLine("Введіть к-сть дітей");
            var childrens = Console.ReadLine();
            using (XmlTextReader reader = new XmlTextReader(path))
            {
                int counter = 0;
                string currentPosition = null;
                string currentChildrens = null;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Position")
                    {
                        currentPosition = reader.ReadElementContentAsString();
                    }

                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Childrens")
                    {
                        currentChildrens = reader.ReadElementContentAsString();

                        if (currentPosition == position && currentChildrens == childrens)
                        {
                            counter++;
                        }
                    }
                }
                Console.WriteLine($"Кількість таких працівників: {counter}");
            }
        }

        Display(xmlPAth);
        Console.WriteLine("-----");
        SearchByName("Stepanenko", xmlPAth);
        FilterFromKeyboard(xmlPAth);
    }
}
