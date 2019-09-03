using System;

namespace ArbreBinaire.ArbreBinaireString
{
    public class ArbreBinaireV1
    {
        public NoeudAbreBinaireV1 Racine { get; set; }
        public ArbreBinaireV1(NoeudAbreBinaireV1 p_racine)
        {
            this.Racine = p_racine;
        }

        public void AffichagePrefixe()
        {
            this.AffichagePrefixe(this.Racine);
        }

        private void AffichagePrefixe(NoeudAbreBinaireV1 p_noeud)
        {
            if (p_noeud != null)
            {
                Console.Write(p_noeud.Valeur);
                AffichagePrefixe(p_noeud.Gauche);
                AffichagePrefixe(p_noeud.Droit);

            }
        }
    }
}