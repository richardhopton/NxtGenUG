using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
