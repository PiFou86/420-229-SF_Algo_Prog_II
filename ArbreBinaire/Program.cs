using ArbreBinaire.ArbreBinaireString;
using ArbreBinaire.ABREntier;
using System;
using ArbreBinaire.Expression;

namespace ArbreBinaire
{
    class Program
    {
        static void Main(string[] args)
        {
            //DemoArbreBinaireExpressionV1();
            DemoArbreBinaireRechercheEntier();
            //DemoArbreExpression();
        }

        static void DemoArbreBinaireExpressionV1()
        {
            NoeudAbreBinaireV1 racine = new NoeudAbreBinaireV1("+",
                                                               new NoeudAbreBinaireV1("3", null, null),
                                                               new NoeudAbreBinaireV1("4", null, null)
                                                              );
            ArbreBinaireV1 abr = new ArbreBinaireV1(racine);

            abr.AffichagePrefixe();
            Console.ReadKey();
        }

        static void DemoArbreBinaireRechercheEntier()
        {
            ArbreBinaireRechercheEntier abre = new ArbreBinaireRechercheEntier();
            abre.AjouterEntier(3);
            abre.AjouterEntier(-1);
            abre.AjouterEntier(17);
            abre.AjouterEntier(5);
            abre.AjouterEntier(23);
            abre.AjouterEntier(42);

            abre.AffichagePrefixe();
            Console.WriteLine();
            abre.AffichageInfixe();
            Console.WriteLine();
            abre.AffichagePostfixe();
            Console.ReadKey();
        }

        static void DemoArbreExpression()
        {
            // "3 + 4 * 18 – 6 / 2";
            string expression1 = "+ 3 - * 4 18 / 6 2";
            ArbreExpression a = ArbreExpression.ParsePrefixeString2ArbreExpression(expression1);

            string expression2 = "+ 3.2 - * 4 18 / 6 2";
            ArbreExpression b = ArbreExpression.ParsePrefixeString2ArbreExpression(expression2);

            Console.Write("Parcours préfixe : ");
            a.ParcoursPrefixe((n) => Console.Out.Write(n + " "));
            Console.Out.WriteLine();

            Console.WriteLine("Parcours infixe : " + a.ToString());

            Console.Write("Parcours postfixe : ");
            a.ParcoursPostfixe((n) => Console.Out.Write(n + " "));
            Console.WriteLine();

            Console.Write(a.ToString() + " = " + a.Evaluer());

            Console.Read();
        }
    }
}
