using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxtGenXAMLNugget
{
    public class DVD
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

        public String Title { get; set; }
        public Classification Classification { get; set; }
    }
}

