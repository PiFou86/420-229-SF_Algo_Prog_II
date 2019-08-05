using System;
// <copyright file="" company="">
// <author>Pierre-François Léon</author>
// </copyright>

namespace ComparaisonAlgorithmesEtStD
{
    public class TableauVariable<TypeElement> where TypeElement : IComparable<TypeElement>
    {
        public const int CapaciteInitiale = 1;

        public int Capacite { get; private set; }
        public int NbElements { get; private set; }
        private TypeElement[] m_donnees;

        // this(CapaciteInitiale) = appelle le constructeur TableauVariable(int p_capacite)
        public TableauVariable() : this(CapaciteInitiale)
        {
            ;
        }

        public TableauVariable(TableauVariable<TypeElement> p_tableau)
        {
            this.Capacite = p_tableau.Capacite;
            this.NbElements = p_tableau.NbElements;
            this.m_donnees = new TypeElement[this.Capacite];

            for (int i = 0; i < this.NbElements; i++)
            {
                this.m_donnees[i] = p_tableau.m_donnees[i];
            }
        }

        public TableauVariable(TypeElement[] p_valeurs) : this(CapaciteInitiale)
        {
            foreach (TypeElement valeur in p_valeurs)
            {
                this.AjouterFin(valeur);
            }
        }

        public TableauVariable(int p_capacite)
        {
            this.Capacite = p_capacite;
            this.m_donnees = new TypeElement[p_capacite];
            this.NbElements = 0;
        }

        public void AjouterFin(TypeElement p_element)
        {
            this.AgrandirTableau();

            this.m_donnees[NbElements] = p_element;
            this.NbElements += 1;
        }

        public void AjouterDebut(TypeElement p_element)
        {
            this.AgrandirTableau();

            for (int i = this.NbElements; i > 0; i--)
            {
                this.m_donnees[i] = this.m_donnees[i - 1];
            }

            this.m_donnees[0] = p_element;
        }

        public TypeElement ObtenirElement(int p_indice)
        {
            if (p_indice < 0 || p_indice > this.NbElements)
            {
                throw new IndexOutOfRangeException();
            }

            return this.m_donnees[p_indice];
        }
        private void AgrandirTableau()
        {
            if (this.NbElements == this.Capacite)
            {
                this.Capacite *= 2;
                TypeElement[] futurTableauDonnees = new TypeElement[this.Capacite];

                for (int i = 0; i < this.NbElements; i++)
                {
                    futurTableauDonnees[i] = this.m_donnees[i];
                }

                this.m_donnees = futurTableauDonnees;
            }
        }

        public void SupprimerFin()
        {
            this.NbElements -= 1;
        }

        public TableauVariable<TypeElement> CopierTableau()
        {
            return new TableauVariable<TypeElement>(this);
        }

        public void InsererValeur(int p_indice, TypeElement p_valeur)
        {
            if (p_indice < 0 || p_indice > this.NbElements)
            {
                throw new IndexOutOfRangeException();
            }
            this.AgrandirTableau();

            for (int i = this.NbElements; i > p_indice; --i)
            {
                this.m_donnees[i] = this.m_donnees[i - 1];
            }

            this.m_donnees[p_indice] = p_valeur;

            this.NbElements += 1;
        }

        public void SupprimerValeur(int p_indice, int p_valeur)
        {
            if (p_indice < 0 || p_indice >= this.NbElements)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = p_indice; i < this.NbElements - 1; ++i)
            {
                this.m_donnees[i] = this.m_donnees[i + 1];
            }

            this.NbElements -= 1;
        }

        public bool EstEgalA(TypeElement[] p_tableauAComparer)
        {
            bool egaux = this.NbElements == p_tableauAComparer.Length;

            for (int i = 0; egaux && i < this.NbElements; i++)
            {
                if (!this.m_donnees[i].Equals(p_tableauAComparer[i]))
                {
                    egaux = false;
                }
            }

            return egaux;
        }

        public TypeElement Minimum()
        {
            if (this.NbElements == 0)
            {
                throw new InvalidOperationException("Le tableau ne doit pas être vide");
            }

            TypeElement minimum = this.m_donnees[0];

            for (int i = 0; i < this.NbElements; i++)
            {
                if (this.m_donnees[i].CompareTo(minimum) < 0)
                // <=> this.m_donnees[i] < minimum
                {
                    minimum = this.m_donnees[i];
                }
            }

            return minimum;
        }
    }
}
