using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NxtGenXAMLNugget
{
    public interface IFilm
    {
        String Title
        {
            get;
            set;
        }

        Classification Classification
        {
            get;
            set;
        }
    }
}
