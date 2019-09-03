namespace ArbreBinaire.ArbreBinaireString
{
    public class NoeudAbreBinaireV1
    {
        public string Valeur { get; set; }
        public NoeudAbreBinaireV1 Gauche { get; set; }
        public NoeudAbreBinaireV1 Droit { get; set; }

        public NoeudAbreBinaireV1(string p_valeur, NoeudAbreBinaireV1 p_gauche, NoeudAbreBinaireV1 p_droit)
        {
            this.Valeur = p_valeur;
            this.Gauche = p_gauche;
            this.Droit = p_droit;
        }
    }
}