using System;
using System.Collections.Generic;
using System.Text;

namespace ArbreBinaire.Expression
{
    public class ArbreExpression
    {
        public NoeudArbreExpression Racine { get; set; }

        public void ParcoursPrefixe(Action<string> p_action)
        {
            ParcoursPrefixe(p_action, this.Racine);
        }

        private void ParcoursPrefixe(Action<string> p_action, NoeudArbreExpression p_noeud)
        {
            p_action?.Invoke(p_noeud.ToString());

            if (p_noeud.Gauche != null)
            {
                ParcoursPrefixe(p_action, p_noeud.Gauche);
            }

            if (p_noeud.Droite != null)
            {
                ParcoursPrefixe(p_action, p_noeud.Droite);
            }
        }

        public void ParcoursPostfixe(Action<string> p_action)
        {
            ParcoursPostfixe(p_action, this.Racine);
        }

        private void ParcoursPostfixe(Action<string> p_action, NoeudArbreExpression p_noeud)
        {            
            if (p_noeud.Gauche != null)
            {
                ParcoursPostfixe(p_action, p_noeud.Gauche);
            }

            if (p_noeud.Droite != null)
            {
                ParcoursPostfixe(p_action, p_noeud.Droite);
            }

            p_action?.Invoke(p_noeud.ToString());
        }

        public void ParcoursInfixe(Action<string> p_action)
        {
            ParcoursInfixe(p_action, this.Racine);
        }

        private void ParcoursInfixe(Action<string> p_action, NoeudArbreExpression p_noeud)
        {
            if (p_noeud.Gauche != null)
            {
                ParcoursInfixe(p_action, p_noeud.Gauche);
            }

            p_action?.Invoke(p_noeud.ToString());

            if (p_noeud.Droite != null)
            {
                ParcoursInfixe(p_action, p_noeud.Droite);
            }
        }

        public static ArbreExpression ParsePrefixeString2ArbreExpression(string p_expression)
        {
            Tuple<NoeudArbreExpression, string> noeudRacine = ParsePrefixeString2ArbreExpression_rec(p_expression);

            if (!string.IsNullOrWhiteSpace(noeudRacine.Item2))
            {
                throw new ArgumentException("Expression non reconnue {p_expression}", "p_expression");
            }

            ArbreExpression res = new ArbreExpression();
            res.Racine = noeudRacine.Item1;

            return res;
        }

        private static Tuple<NoeudArbreExpression, string> ParsePrefixeString2ArbreExpression_rec(string p_expression)
        {
            p_expression = p_expression.Trim();
            char premierCaractere = p_expression[0];

            if (EstOperateur(premierCaractere))
            {
                string resteChaine = p_expression.Substring(1);
                Tuple<NoeudArbreExpression, string> resGauche = ParsePrefixeString2ArbreExpression_rec(resteChaine);
                Tuple<NoeudArbreExpression, string> resDroite = ParsePrefixeString2ArbreExpression_rec(resGauche.Item2);

                return new Tuple<NoeudArbreExpression, string>(new NoeudArbreExpression(premierCaractere, resGauche.Item1, resDroite.Item1),
                    resDroite.Item2);
            }

            string nombre = "";
            char prochainCaractere = premierCaractere;
            while (nombre.Length < p_expression.Length
                && !EstOperateur(p_expression[nombre.Length]) 
                && p_expression[nombre.Length] != ' ')
            {
                nombre += p_expression[nombre.Length];
            }
            decimal valeurNombre = 0m;
            if (!decimal.TryParse(nombre, out valeurNombre))
            {
                throw new ArgumentException($"Expression non reconnue : {p_expression}");
            }

            return new Tuple<NoeudArbreExpression, string>(
                new NoeudArbreExpression(valeurNombre),
                p_expression.Substring(nombre.Length)
                );
        }

        private static bool EstOperateur(char p_valeur)
        {
            return p_valeur == '+'
                || p_valeur == '-'
                || p_valeur == '*'
                || p_valeur == '/';
        }

        public decimal Evaluer()
        {
            return Evaluer(this.Racine);
        }

        private decimal Evaluer(NoeudArbreExpression p_noeud)
        {
            //if (p_noeud.TypeNoeud == TypeNoeudArbreExpression.Operation) {
            //    if (p_noeud.Gauche == null || p_noeud.Droite == null)
            //    {
            //        throw new ArgumentException("Noeud incohérent", "p_noeud");
            //    }
            //    return EvaluerOperation(p_noeud);
            //} else if (p_noeud.TypeNoeud == TypeNoeudArbreExpression.Valeur)
            //{
            //    if (p_noeud.Gauche != null || p_noeud.Droite != null)
            //    {
            //        throw new ArgumentException("Noeud incohérent", "p_noeud");
            //    }
            //    return p_noeud.Valeur;
            //}
            //else
            //{
            //    throw new InvalidProgramException();
            //}

            // <=>

            switch (p_noeud.TypeNoeud)
            {
                case TypeNoeudArbreExpression.Operation:
                    if (p_noeud.Gauche == null || p_noeud.Droite == null) {
                        throw new ArgumentException("Noeud incohérent", "p_noeud");
                    }
                    return EvaluerOperation(p_noeud);
                case TypeNoeudArbreExpression.Valeur:
                    if (p_noeud.Gauche != null || p_noeud.Droite != null)
                    {
                        throw new ArgumentException("Noeud incohérent", "p_noeud");
                    }
                    return p_noeud.Valeur;
                default:
                    throw new InvalidProgramException();
            }
        }

        private decimal EvaluerOperation(NoeudArbreExpression p_noeud)
        {
            decimal valeurGauche = Evaluer(p_noeud.Gauche);
            decimal valeurDroite = Evaluer(p_noeud.Droite);

            switch (p_noeud.Operateur)
            {
                case '+':
                    return valeurGauche + valeurDroite;
                case '-':
                    return valeurGauche - valeurDroite;
                case '/':
                    if (valeurDroite == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return valeurGauche / valeurDroite;
                case '*':
                    return valeurGauche * valeurDroite;
                default:
                    throw new ArgumentException("Opérateur inconnu");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            ParcoursInfixe((v) => sb.Append(v + " "));

            return sb.ToString();
        }
    }
}
