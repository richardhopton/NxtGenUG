using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NxtGenXAMLNugget
{
    [Serializable]
    public class DVD : IFilm
    {
        #region Constructors
        public DVD() : this(String.Empty) { }
        public DVD(String title) : this(title, Classification.None) { }
        public DVD(String title, Classification classification)
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
