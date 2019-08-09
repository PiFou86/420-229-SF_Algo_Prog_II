using System;
using System.Collections.Generic;
using System.Text;

namespace ComparaisonAlgorithmesEtStD
{
    public class FusionListeUtilitaire<TypeElement> where TypeElement : IComparable<TypeElement>
    {
        public static List<TypeElement> FusionnerDeuxListesTrieesV1(List<TypeElement> p_liste1, List<TypeElement> p_liste2)
        {
            if (p_liste1 == null || p_liste2 == null)
            {
                throw new ArgumentNullException();
            }

            if (!EstTriee(p_liste1) || !EstTriee(p_liste2))
            {
                throw new ArgumentException("Les listes doivent être triées");
            }

            int capacite = p_liste1.Count + p_liste2.Count;
            List<TypeElement> ListeFusionee = new List<TypeElement>(capacite);

            int y = 0;
            int j = 0;
            while (ListeFusionee.Count < ListeFusionee.Capacity)
            {
                if (y < p_liste1.Count && j < p_liste2.Count)
                {
                    if (p_liste1[y].CompareTo(p_liste2[j]) < 0)
                    {
                        ListeFusionee.Add(p_liste1[y]);
                        y += 1;
                    }
                    else if (p_liste1[y].CompareTo(p_liste2[j]) > 0)
                    {
                        ListeFusionee.Add(p_liste2[j]);
                        j += 1;
                    }
                    else
                    {
                        ListeFusionee.Add(p_liste1[y]);
                        ListeFusionee.Add(p_liste2[j]);
                        y += 1;
                        j += 1;
                    }
                }
                else
                {
                    if (j >= p_liste2.Count)
                    {
                        ListeFusionee.Add(p_liste1[y]);
                        y += 1;
                    }
                    else
                    {
                        ListeFusionee.Add(p_liste2[j]);
                        j += 1;
                    }

                }

                if (j >= p_liste2.Count && y < p_liste1.Count)
                {
                    ListeFusionee.Add(p_liste1[y]);
                    y += 1;
                }

            }
            return ListeFusionee;
        }

        public static List<TypeElement> FusionnerDeuxListesTrieesV2(List<TypeElement> p_liste1, List<TypeElement> p_liste2)
        {
            if (p_liste1 == null || p_liste2 == null)
            {
                throw new ArgumentNullException();
            }

            if (!EstTriee(p_liste1) || !EstTriee(p_liste2))
            {
                throw new ArgumentException("Les listes doivent être triées");
            }

            int capacite = p_liste1.Count + p_liste2.Count;
            List<TypeElement> ListeFusionee = new List<TypeElement>(capacite);

            int y = 0;
            int j = 0;
            while (y < p_liste1.Count && j < p_liste2.Count)
            {
                if (p_liste1[y].CompareTo(p_liste2[j]) <= 0)
                {
                    ListeFusionee.Add(p_liste1[y]);
                    y += 1;
                }
                else
                {
                    ListeFusionee.Add(p_liste2[j]);
                    j += 1;
                }
            }

            for (int i = y; i < p_liste1.Count; ++i)
            {
                ListeFusionee.Add(p_liste1[i]);
            }

            for (int i = j; i < p_liste2.Count; ++i)
            {
                ListeFusionee.Add(p_liste2[i]);
            }

            return ListeFusionee;
        }

        private static bool EstTriee(List<TypeElement> p_liste)
        {
            bool estTriee = true;
            for (int i = 0; estTriee && i < p_liste.Count - 1; ++i)
            {
                estTriee = p_liste[i].CompareTo(p_liste[i + 1]) <= 0;
            }

            return estTriee;
        }
    }
}
