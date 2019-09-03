using System;
using System.Collections.Generic;
using System.Text;

namespace ArbreBinaire.ABREntier
{
    public class NoeudArbreBinaireEntier
    {
        public NoeudArbreBinaireEntier Gauche { get; set; }
        public NoeudArbreBinaireEntier Droit { get; set; }
        public int Valeur { get; set; }
    }
}
