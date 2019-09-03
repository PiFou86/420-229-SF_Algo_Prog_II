using System;
using System.Collections.Generic;
using System.Text;

namespace ArbreBinaire.Expression
{
    public class NoeudArbreExpression
    {
        public NoeudArbreExpression Gauche { get; set; }
        public NoeudArbreExpression Droite { get; set; }
        public TypeNoeudArbreExpression TypeNoeud { get; }
        public decimal Valeur { get; set; }
        public char Operateur { get; }

        public NoeudArbreExpression(decimal p_valeur)
        {
            this.TypeNoeud = TypeNoeudArbreExpression.Valeur;
            this.Valeur = p_valeur;
        }

        public NoeudArbreExpression(
            char p_operation,
            NoeudArbreExpression p_gauche,
            NoeudArbreExpression p_droite
            )
        {
            this.TypeNoeud = TypeNoeudArbreExpression.Operation;
            this.Operateur = p_operation;
            this.Gauche = p_gauche;
            this.Droite = p_droite;
        }

        public override string ToString()
        {
            string resultat = "";
            switch (this.TypeNoeud)
            {
                case TypeNoeudArbreExpression.Valeur:
                    resultat = this.Valeur.ToString();
                    break;
                case TypeNoeudArbreExpression.Operation:
                    resultat = this.Operateur.ToString();
                    break;
                default:
                    break;
            }

            return resultat;
        }
    }
}
