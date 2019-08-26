using System.Collections.Generic;

namespace ArbrePrefixe
{
    public class NoeudTrie
    {
        public char Lettre { get; set; }
        public List<NoeudTrie> Enfants { get; set; }

        public bool EstMotValide { get; set; }
        public string ValeurPrefixe { get; set; }

        public NoeudTrie(char p_valeurNoeud, string p_valeurCourante, bool p_estMot)
        {
            this.Lettre = p_valeurNoeud;
            this.Enfants = new List<NoeudTrie>();
            this.EstMotValide = p_estMot;
            this.ValeurPrefixe = p_valeurCourante;
        }
    }
}