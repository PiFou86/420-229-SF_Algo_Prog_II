using System;
using System.Collections.Generic;
using System.IO;

namespace ArbrePrefixe
{
    class Program
    {
        public static void Main(string[] args)
        {
            //LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3 });
            //int s = Somme(list);

            Trie trie = Charger("liste.de.mots.francais.frgut.txt");

            List<string> lstPrefixeATrouver = new List<string>()
            {
                "neut",
                "za",
                "abd",
                "zzz"
            };

            foreach (string prefixeACompleter in lstPrefixeATrouver)
            {
                Console.Out.WriteLine($"Complétion pour {prefixeACompleter}");
                List<string> lstPrefixesNe = trie.CompleterPrefixeV3(prefixeACompleter);
                lstPrefixesNe.ForEach(m => Console.Out.WriteLine($"  -> {m}"));
            }
        }

        public static Trie Charger(string p_nomFichierDictionnaire)
        {
            if (string.IsNullOrWhiteSpace(p_nomFichierDictionnaire))
            {
                throw new ArgumentException("p_nomFichierDictionnaire");
            }

            Trie trie = new Trie();

            using (StreamReader sr = new StreamReader(p_nomFichierDictionnaire))
            {
                while (!sr.EndOfStream)
                {
                    string mot = sr.ReadLine();
                    mot = mot.Trim();
                    if (!string.IsNullOrWhiteSpace(mot))
                    {
                        trie.AjouterMot(mot);
                    }
                }

                sr.Close();
            }

            return trie;
        }


        //public static int Somme(LinkedList<int> p_liste)
        //{
        //    if (p_liste == null)
        //    {
        //        throw new ArgumentNullException("p_liste");
        //    }

        //    //  return Somme_rec(p_list.First);
        //    return SommeTerminale_rec(p_liste.First, 0);
        //}

        //private static int Somme_rec(LinkedListNode<int> p_noeud)
        //{
        //    if (p_noeud == null)
        //    {
        //        return 0;
        //    }

        //    return p_noeud.Value + Somme_rec(p_noeud.Next);
        //}

        //public static bool Existe(LinkedList<int> p_liste, int p_valeur)
        //{
        //    if (p_liste == null)
        //    {
        //        throw new ArgumentNullException("p_valeur");
        //    } 

        //    return Existe_rec(p_liste.First, p_valeur);
        //}

        //private static bool Existe_rec(LinkedListNode<int> p_noeud, int p_valeur)
        //{
        //    if (p_noeud == null)
        //    {
        //        return false;
        //    }

        //    if (p_noeud.Value == p_valeur)
        //    {
        //        return true;
        //    }

        //    return Existe_rec(p_noeud.Next, p_valeur);
        //}

        //private static int SommeTerminale_rec(LinkedListNode<int> p_noeud, int p_somme)
        //{
        //    if (p_noeud == null)
        //    {
        //        return p_somme;
        //    }

        //    return SommeTerminale_rec(p_noeud.Next, p_somme + p_noeud.Value);
        //}
    }
}
