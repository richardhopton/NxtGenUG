using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NxtGenXAMLNugget
{
    public class BluRay : IFilm
    {
        #region Constructors
        public BluRay() : this(String.Empty) { }
        public BluRay(String title) : this(title, Classification.None) { }
        public BluRay(String title, Classification classification)
        {
            this.Title = title;
            this.Classification = classification;
        }
        #endregion

        [XmlAttribute()]
        public String Title { get; set; }
        [XmlAttribute()]
        public Classification Classification { get; set; }
    }
}
