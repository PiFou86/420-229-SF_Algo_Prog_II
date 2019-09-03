using System;
using System.Collections.Generic;
using System.Text;

namespace ArbreBinaire.ABREntier
{
    public class ArbreBinaireRechercheEntier
    {
        public NoeudArbreBinaireEntier Racine { get; set; }

        public void AjouterEntier(int p_valeur)
        {
            if (this.Racine == null)
            {
                this.Racine = new NoeudArbreBinaireEntier() { Valeur = p_valeur };
            }
            else
            {
                AjouterEntier(p_valeur, this.Racine);
            }
        }

        public bool RechercherEntier(int p_valeur)
        {
            return RechercherEntier(p_valeur, this.Racine);
        }

        private void AjouterEntier(int p_valeur, NoeudArbreBinaireEntier p_noeud)
        {
            if (p_valeur == p_noeud.Valeur)
            {
                return;
            }
            if (p_valeur < p_noeud.Valeur)
            {
                if (p_noeud.Gauche == null)
                {
                    p_noeud.Gauche = new NoeudArbreBinaireEntier() { Valeur = p_valeur };
                }
                else
                {
                    AjouterEntier(p_valeur, p_noeud.Gauche);
                }
            }
            else // p_valeur > racine.Valeur
            {
                if (p_noeud.Droit == null)
                {
                    p_noeud.Droit = new NoeudArbreBinaireEntier() { Valeur = p_valeur };
                }
                else
                {
                    AjouterEntier(p_valeur, p_noeud.Droit);
                }
            }
        }

        private bool RechercherEntier(int p_valeur, NoeudArbreBinaireEntier p_noeud)
        {
            if (p_noeud == null)
            {
                return false;
            }

            if (p_noeud.Valeur == p_valeur)
            {
                return true;
            }

            if (p_noeud.Valeur < p_valeur)
            {
                return RechercherEntier(p_valeur, p_noeud.Gauche);
            }

            return RechercherEntier(p_valeur, p_noeud.Droit);
        }

        public void AffichagePrefixe()
        {
            this.AffichagePrefixe(this.Racine);
        }

        private void AffichagePrefixe(NoeudArbreBinaireEntier p_noeud)
        {
            if (p_noeud != null)
            {
                Console.Write(p_noeud.Valeur + " ");
                AffichagePrefixe(p_noeud.Gauche);
                AffichagePrefixe(p_noeud.Droit);

            }
        }

        public void AffichagePostfixe()
        {
            this.AffichagePostfixe(this.Racine);
        }

        private void AffichagePostfixe(NoeudArbreBinaireEntier p_noeud)
        {
            if (p_noeud != null)
            {
                AffichagePostfixe(p_noeud.Gauche);
                AffichagePostfixe(p_noeud.Droit);
                Console.Write(p_noeud.Valeur + " ");
            }
        }

        public void AffichageInfixe()
        {
            this.AffichageInfixe(this.Racine);
        }

        private void AffichageInfixe(NoeudArbreBinaireEntier p_noeud)
        {
            if (p_noeud != null)
            {
                AffichageInfixe(p_noeud.Gauche);
                Console.Write(p_noeud.Valeur + " ");
                AffichageInfixe(p_noeud.Droit);
            }
        }
    }
}
