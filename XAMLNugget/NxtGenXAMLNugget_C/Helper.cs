using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Markup;
using System.Xml;

namespace NxtGenXAMLNugget
{
    public class Helper
    {
        public static void XmlSerialize(Object o, String fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                new XmlSerializer(o.GetType()).Serialize(writer, o);
                writer.Close();
            }
        }

        public static void XamlSerialize(Object o, String fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(XamlWriter.Save(o));
                writer.Close();
            }
        }

        public static object XmlDeserialize(Type t, String fileName)
        {
            using (StreamReader fileReader = new StreamReader(fileName))
            {
                return new XmlSerializer(t).Deserialize(fileReader.BaseStream);
            }
        }

        public static object XamlDeserialize(String fileName)
        {

            using (StreamReader fileReader = new StreamReader(fileName))
            {
                return XamlReader.Load(XmlReader.Create(fileReader));
            }
        }

        public static void TestXmlSerialize(Object o, String fileName, Action<String> output, Action prompt)
        {
            try
            {
                DateTime startTimer = DateTime.Now;
                Helper.XmlSerialize(o, String.Format(fileName, "xml"));
                output(String.Format("XmlSerialize (ticks): {0}", DateTime.Now.Subtract(startTimer)));
            }
            catch (Exception e)
            {
                output(String.Format("TestXmlSerialize failed: {0}", e.ToString()));
            }
            prompt();
        }

        public static void TestXamlSerialize(Object o, String fileName, Action<String> output, Action prompt)
        {
            try
            {
                DateTime startTimer = DateTime.Now;
                Helper.XamlSerialize(o, String.Format(fileName, "xaml"));
                output(String.Format("XamlSerialize (ticks): {0}", DateTime.Now.Subtract(startTimer)));
            }
            catch (Exception e)
            {
                output(String.Format("TestXamlSerialize failed: {0}", e.ToString()));
            }
            prompt();
        }

        public static void TestXmlDeserialize(Type t, String fileName, Action<String> output, Action prompt)
        {
            try
            {
                DateTime startTimer = DateTime.Now;
                Helper.XmlDeserialize(t, String.Format(fileName, "xml"));
                output(String.Format("XmlDeserialise (ticks): {0}", DateTime.Now.Subtract(startTimer)));
            }
            catch (Exception e)
            {
                output(String.Format("TestXmlDeserialize failed: {0}", e.ToString()));
            }
            prompt();
        }

        public static void TestXamlDeserialize(String fileName, Action<String> output, Action prompt)
        {
            try
            {
                DateTime startTimer = DateTime.Now;
                Helper.XamlDeserialize(String.Format(fileName, "xaml"));
                output(String.Format("XamlDeserialise (ticks): {0}", DateTime.Now.Subtract(startTimer)));
            }
            catch (Exception e)
            {
                output(String.Format("TestXamlDeserialize failed: {0}", e.ToString()));
            }
            prompt();
        }

        private static object _testObject = null;
        public static object TestObject
        {
            get{
                if (_testObject == null)
                {
                    FilmCollection films = new FilmCollection();
                    films.Add(new DVD("Identity", Classification.bbfc15));
                    films.Add(new DVD("Anti Trust", Classification.bbfc15));
                    films.Add(new DVD("Shawshank Redemption", Classification.bbfc15));
                    films.Add(new DVD("Butterfly Effect", Classification.bbfc15));
                    films.Add(new DVD("Matrix", Classification.bbfc15));
                    films.Add(new DVD("Sneakers", Classification.bbfc15));
                    films.Add(new DVD("Hackers", Classification.bbfc12));
                    films.Add(new BluRay("Dark Knight", Classification.bbfc12));
                    films.Add(new BluRay("Aeon Flux", Classification.bbfc15));
                    _testObject = films;
                }
                return _testObject;
            }
        }

        public static void RunTests(Action<String> output, Action prompt)
        {
            String fileName = @"D:\Nugget_C_FilmList.xml";
            Helper.TestXmlSerialize(TestObject, fileName, output, prompt);
            Helper.TestXamlDeserialize(fileName, output, prompt);

            Helper.TestXamlSerialize(TestObject, @"D:\Nugget_C_FilmList.xaml", output, prompt);
        }

    }
}
