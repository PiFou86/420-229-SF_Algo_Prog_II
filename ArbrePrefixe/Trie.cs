using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArbrePrefixe
{
    public class Trie
    {
        public NoeudTrie Racine { get; set; }

        public Trie()
        {
            this.Racine = new NoeudTrie('*', "", false);
        }

        public void AjouterMot(string p_mot)
        {
            if (string.IsNullOrWhiteSpace(p_mot))
            {
                throw new ArgumentException("p_mot");
            }

            p_mot = p_mot.ToLower();

            AjouterSuiteMot(p_mot, this.Racine);
        }

        public List<string> CompleterPrefixeV1(string p_prefixe)
        {
            if (string.IsNullOrWhiteSpace(p_prefixe))
            {
                throw new ArgumentException("p_prefixe");
            }

            List<string> resultat = null;
            p_prefixe = p_prefixe.ToLower();

            NoeudTrie noeudPrefixe = RechercherNoeudPrefixe(p_prefixe, this.Racine);
            if (noeudPrefixe != null)
            {
                resultat = CollecterMotsV1(noeudPrefixe);
            }
            else
            {
                resultat = new List<string>();
            }

            return resultat;
        }

        public List<string> CompleterPrefixeV2(string p_prefixe)
        {
            if (string.IsNullOrWhiteSpace(p_prefixe))
            {
                throw new ArgumentException("p_prefixe");
            }

            List<string> resultat = new List<string>();
            p_prefixe = p_prefixe.ToLower();

            NoeudTrie noeudPrefixe = RechercherNoeudPrefixe(p_prefixe, this.Racine);
            if (noeudPrefixe != null)
            {
                CollecterMotsV2(noeudPrefixe, resultat);
            }

            return resultat;
        }

        public List<string> CompleterPrefixeV3(string p_prefixe)
        {
            if (string.IsNullOrWhiteSpace(p_prefixe))
            {
                throw new ArgumentException("p_prefixe");
            }

            List<string> resultat = new List<string>();
            p_prefixe = p_prefixe.ToLower();

            NoeudTrie noeudPrefixe = RechercherNoeudPrefixe(p_prefixe, this.Racine);
            if (noeudPrefixe != null)
            {
                ParcoursPrefixe(noeudPrefixe, (lettre, prefixe, estMot) => {
                    if (estMot)
                    {
                        resultat.Add(prefixe);
                    }
                });
            }

            return resultat;
        }

        public void ParcoursPrefixe(Action<char, string, bool> p_visiter)
        {
            if (p_visiter == null)
            {
                throw new ArgumentNullException(nameof(p_visiter));
            }

            ParcoursPrefixe(this.Racine, p_visiter);
        }

        private void ParcoursPrefixe(NoeudTrie p_noeudPrefixe, Action<char, string, bool> p_visiter)
        {
            p_visiter(p_noeudPrefixe.Lettre, p_noeudPrefixe.ValeurPrefixe, p_noeudPrefixe.EstMotValide);
            foreach (NoeudTrie enfant in p_noeudPrefixe.Enfants)
            {
                ParcoursPrefixe(enfant, p_visiter);
            }
        }

        private List<string> CollecterMotsV1(NoeudTrie p_noeudPrefixe)
        {
            List<string> res = new List<string>();

            if (p_noeudPrefixe.EstMotValide)
            {
                res.Add(p_noeudPrefixe.ValeurPrefixe);
            }

            foreach (NoeudTrie enfant in p_noeudPrefixe.Enfants)
            {
                res.AddRange(CollecterMotsV1(enfant));
            }
            return res;
        }

        private void CollecterMotsV2(NoeudTrie p_noeudPrefixe, List<string> p_lisetMots)
        {
            if (p_noeudPrefixe.EstMotValide)
            {
                p_lisetMots.Add(p_noeudPrefixe.ValeurPrefixe);
            }

            foreach (NoeudTrie enfant in p_noeudPrefixe.Enfants)
            {
                CollecterMotsV2(enfant, p_lisetMots);
            }
        }

        private NoeudTrie RechercherNoeudPrefixe(string p_prefixe, NoeudTrie p_noeudCourant)
        {
            char premiereLettre = p_prefixe[0];
            NoeudTrie noeudDepart = p_noeudCourant
                .Enfants
                .Where(n => n.Lettre == premiereLettre)
                .SingleOrDefault();

            if (noeudDepart == null)
            {
                return null;
            }

            if (p_prefixe.Length == 1)
            {
                return noeudDepart;
            }

            return RechercherNoeudPrefixe(p_prefixe.Substring(1), noeudDepart);
        }

        private void AjouterSuiteMot(string p_resteMot, NoeudTrie p_noeudCourant)
        {
            char premiereLettre = p_resteMot[0];
            // Recherche du noeud
            NoeudTrie noeudDepart = p_noeudCourant
                .Enfants
                .Where(n => n.Lettre == premiereLettre)
                .SingleOrDefault();

            // Si non existant
            if (noeudDepart == null)
            {
                noeudDepart = new NoeudTrie(premiereLettre,
                    p_noeudCourant.ValeurPrefixe + premiereLettre.ToString(),
                    p_resteMot.Length == 1);
                p_noeudCourant.Enfants.Add(noeudDepart);
            }

            if (p_resteMot.Length > 1)
            {
                AjouterSuiteMot(p_resteMot.Substring(1), noeudDepart);
            }
            else
            {
                noeudDepart.EstMotValide = true;
            }
        }
    }
}
