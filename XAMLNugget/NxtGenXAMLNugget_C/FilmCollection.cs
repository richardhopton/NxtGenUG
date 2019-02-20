using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NxtGenXAMLNugget
{
    public class FilmCollection : List<IFilm>, IXmlSerializable
    {
        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                reader.Read();
                while (reader.Name != "FilmCollection")
                {
                    switch (reader.Name)
                    {
                        case "DVD":
                            {
                                this.Add(new XmlSerializer(typeof(DVD)).Deserialize(reader) as IFilm);
                                break;
                            }
                        case "BluRay":
                            {
                                this.Add(new XmlSerializer(typeof(BluRay)).Deserialize(reader) as IFilm);
                                break;
                            }
                    }
                }
                reader.Read();
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Capacity", this.Capacity.ToString());
            writer.WriteAttributeString("xmlns", String.Format("clr-namespace:{0};assembly={1}",this.GetType().Namespace, this.GetType().Assembly.GetName().Name));

            foreach (IFilm film in this)
            {
                new XmlSerializer(film.GetType()).Serialize(writer, film);
            }
        }

        #endregion
    }
}
