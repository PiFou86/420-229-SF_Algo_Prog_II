using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoAdresseIP
{
    public class InterfacesReseaux
    {
        public List<Interface> Interfaces { get; private set; }

        public InterfacesReseaux()
        {
            this.Interfaces = new List<Interface>();
        }

        public Interface ObtenirInterface(string p_adresse)
        {
            if (string.IsNullOrWhiteSpace(p_adresse))
            {
                throw new ArgumentException("Le paramètre ne doit pas être vide ou nul.", "p_adresse");
            }

            return ObtenirInterface(new AdresseIP(p_adresse));
        }

        public Interface ObtenirInterface(AdresseIP p_adresse)
        {
            if (p_adresse == null)
            {
                throw new ArgumentException("Le paramètre ne doit pas être nul.", "p_adresse");
            }

            foreach (Interface interfaceRx in this.Interfaces)
            {
                if (interfaceRx.EstBonneInterfacePourIP(p_adresse))
                {
                    return interfaceRx;
                }
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Interface element in Interfaces)
            {
                sb.AppendLine(element.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}
