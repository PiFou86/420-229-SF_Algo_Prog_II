using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
// <copyright file="" company="">
// <author>Pierre-François Léon</author>
// </copyright>

/// <summary>
/// Affichage graphique des statistiques :
/// - Ouvrir le site http://gnuplot.respawned.com
/// - Copier / coller des données dans le champ data
/// - Modification du "Plot script" comme suit : 
/// # Scale font and line width (dpi) by changing the size! It will always display stretched.
//set terminal svg size 800,600 enhanced fname 'arial'  fsize 10 butt solid
//set output 'out.svg'

//# Key means label...
//set key inside bottom right
//set xlabel 'Volume données (nb éléments)'
//set ylabel 'Temps (ms)'
//set title 'Comparaison algorithmes'
//plot  "data.txt" using 1:2 title 'TVInsererFinCasIdeal' with lines, "data.txt" using 1:3 title 'TVInsererFinPireCas' with lines, "data.txt" using 1:4 title 'TVInsererDebut' with lines, "data.txt" using 1:5 title 'LCC#InsererFinCasIdeal' with lines, "data.txt" using 1:6 title 'LCC#InsererFinPireCas' with lines, "data.txt" using 1:7 title 'LCC#InsererDebut' with lines

/// </summary>
namespace ComparaisonAlgorithmesEtStD
{

    class Program
    {
        static void Main(string[] args)
        {
            // Préparation du tableau de stats
            Dictionary<int, List<long>> lstTemps = new Dictionary<int, List<long>>();
            int[] colNbValeurs = new int[] { 10, 100, 1000, 10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000 };
            foreach (var nbValeur in colNbValeurs)
            {
                lstTemps.Add(nbValeur, new List<long>());
            }

            // Choisir le test à effectuer
            //TestsStdTableauVariable_ListInserer(colNbValeurs, 100, lstTemps);
            TestsFusionnerDeuxListesTriees(colNbValeurs, 100, lstTemps);

            // Affichage des stats pour gnuplot
            foreach (var nbValeur in colNbValeurs)
            {
                Console.WriteLine($"{nbValeur.ToString().PadRight(8)} {string.Join(" ", lstTemps[nbValeur].Select(v => v.ToString().PadRight(10)))}");
            }

            Console.ReadKey();
        }

        private static void TestsFusionnerDeuxListesTriees(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            TestFusionnerDeuxListesTrieesV1(p_nbValeurs, p_nbTests, p_lstTemps);
            TestFusionnerDeuxListesTrieesV2(p_nbValeurs, p_nbTests, p_lstTemps);
        }


        private static void TestFusionnerDeuxListesTrieesV1(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            MesureMedianAppel(p_nbValeurs, p_nbTests, p_lstTemps,
                nbValeurs =>
                {
                    List<int> lst1 = CreerListeAleatoire(nbValeurs, false); lst1.Sort();
                    List<int> lst2 = CreerListeAleatoire(nbValeurs, false); lst2.Sort();
                    return Tuple.Create(lst1, lst2);
                },
                (lst) =>
                {
                    FusionListeUtilitaire<int>.FusionnerDeuxListesTrieesV1(lst.Item1, lst.Item2);
                });
        }

        private static void TestFusionnerDeuxListesTrieesV2(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            MesureMedianAppel(p_nbValeurs, p_nbTests, p_lstTemps,
                nbValeurs =>
                {
                    List<int> lst1 = CreerListeAleatoire(nbValeurs, false); lst1.Sort();
                    List<int> lst2 = CreerListeAleatoire(nbValeurs, false); lst2.Sort();
                    return Tuple.Create(lst1, lst2);
                },
                (lst) =>
                {
                    FusionListeUtilitaire<int>.FusionnerDeuxListesTrieesV2(lst.Item1, lst.Item2);
                });
        }

        private static void TestsStdTableauVariable_ListInserer(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            TestTableauVariableInsererFinCasIdeal(p_nbValeurs, p_nbTests, p_lstTemps);
            TestTableauVariableInsererFinPireCas(p_nbValeurs, p_nbTests, p_lstTemps);
            TestTableauVariableInsererDebut(p_nbValeurs, p_nbTests, p_lstTemps);

            TestListeChaineeCSharpInsererFinCasIdeal(p_nbValeurs, p_nbTests, p_lstTemps);
            TestListeChaineeCSharpInsererFinPireCas(p_nbValeurs, p_nbTests, p_lstTemps);
            TestListeChaineeCSharpInsererDebut(p_nbValeurs, p_nbTests, p_lstTemps);
        }

        private static void TestTableauVariableInsererFinCasIdeal(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            MesureMedianAppel(p_nbValeurs, p_nbTests, p_lstTemps,
                nbValeurs => CreerTableauVariableAleatoire(nbValeurs, true),
                (tv) =>
                {
                    tv.AjouterFin(42);
                });
        }

        private static void TestTableauVariableInsererFinPireCas(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            MesureMedianAppel(p_nbValeurs, p_nbTests, p_lstTemps,
                nbValeurs => CreerTableauVariableAleatoire(nbValeurs, false),
                (tv) =>
                {
                    tv.AjouterFin(42);
                });
        }

        private static void TestTableauVariableInsererDebut(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            MesureMedianAppel(p_nbValeurs, p_nbTests, p_lstTemps,
                nbValeurs => CreerTableauVariableAleatoire(nbValeurs, true),
                (tv) =>
                {
                    tv.AjouterDebut(42);
                });
        }

        private static void TestListeChaineeCSharpInsererFinCasIdeal(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            MesureMedianAppel(p_nbValeurs, p_nbTests, p_lstTemps,
                nbValeurs => CreerListeAleatoire(nbValeurs, true),
                (tv) =>
                {
                    tv.Add(42);
                });
        }

        private static void TestListeChaineeCSharpInsererFinPireCas(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            MesureMedianAppel(p_nbValeurs, p_nbTests, p_lstTemps,
                nbValeurs => CreerListeAleatoire(nbValeurs, false),
                (tv) =>
                {
                    tv.Add(42);
                });
        }

        private static void TestListeChaineeCSharpInsererDebut(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps)
        {
            MesureMedianAppel(p_nbValeurs, p_nbTests, p_lstTemps,
                nbValeurs => CreerListeAleatoire(nbValeurs, true),
                (tv) =>
                {
                    tv.Insert(0, 42);
                });
        }


        /// <summary>
        /// Prépare et effectue une suite de tests.
        /// </summary>
        /// <typeparam name="TypeStructure"></typeparam>
        /// <param name="p_nbValeurs">Collection des différents volumes de données à tester</param>
        /// <param name="p_nbTests">Nombre de tests par volume de données à effecter > 0</param>
        /// <param name="p_lstTemps">Dictionnaire de résultats associant un volume à un temps (ici médian)</param>
        /// <param name="fctCreationCas">Fonction qui permet de créer un cas de test</param>
        /// <param name="fctAMesurer">Action à mesurer</param>
        private static void MesureMedianAppel<TypeStructure>(int[] p_nbValeurs, int p_nbTests, Dictionary<int, List<long>> p_lstTemps,
            Func<int, TypeStructure> fctCreationCas, Action<TypeStructure> fctAMesurer)
        {
            foreach (var nbValeurs in p_nbValeurs)
            {
                TypeStructure[] tableauxVariables = new TypeStructure[p_nbTests];
                for (int i = 0; i < p_nbTests; i++)
                {
                    tableauxVariables[i] = fctCreationCas(nbValeurs);
                }

                List<long> valeursTemps = new List<long>();
                Stopwatch sw = null;
                for (int i = 0; i < p_nbTests; i++)
                {
                    sw = Stopwatch.StartNew();
                    fctAMesurer(tableauxVariables[i]);
                    sw.Stop();
                    valeursTemps.Add(sw.ElapsedTicks);
                }

                valeursTemps.Sort();
                long mediane = valeursTemps[valeursTemps.Count / 2];
                p_lstTemps[nbValeurs].Add(mediane);
            }
        }

        /// <summary>
        /// Crée un objet de type TableauVariable d'une capacité passée en paramètres rempli avec des valeurs aléatoires
        /// </summary>
        /// <param name="p_nbValeurs">Nombre de valeurs à générer</param>
        /// <param name="p_capaciteEnPlus">ajouter au moins un espace à la capacité initiale afin de ne pas être dans le cas
        /// où le tableau est recopié afin de ne pas fausser le meilleur des cas</param>
        /// <returns></returns>
        private static TableauVariable<int> CreerTableauVariableAleatoire(int p_nbValeurs, bool p_capaciteEnPlus)
        {
            Random rnd = new Random();
            int capacite = p_nbValeurs;
            if (p_capaciteEnPlus)
            {
                capacite += 1;
            }
            TableauVariable<int> res = new TableauVariable<int>(capacite);
            for (int i = 0; i < p_nbValeurs; i++)
            {
                res.AjouterFin(rnd.Next());
            }

            return res;
        }

        /// <summary>
        /// Crée une liste C# d'une capacité passée en paramètres remplie avec des valeurs aléatoires
        /// </summary>
        /// <param name="p_nbValeurs">Nombre de valeurs à générer</param>
        /// <param name="p_capaciteEnPlus">ajouter au moins un espace à la capacité initiale afin de ne pas être dans le cas
        /// où le tableau est recopié afin de ne pas fausser le meilleur des cas</param>
        /// <returns></returns>
        private static List<int> CreerListeAleatoire(int p_nbValeurs, bool p_capaciteEnPlus)
        {
            Random rnd = new Random();
            int capacite = p_nbValeurs;
            if (p_capaciteEnPlus)
            {
                capacite += 1;
            }
            List<int> res = new List<int>(capacite);
            for (int i = 0; i < p_nbValeurs; i++)
            {
                res.Add(rnd.Next());
            }

            return res;
        }
    }
}
