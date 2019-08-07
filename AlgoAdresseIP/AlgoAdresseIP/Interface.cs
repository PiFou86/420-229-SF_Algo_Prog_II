using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoAdresseIP
{
    public class Interface
    {
        public string NomInterface { get; set; }
        public AdresseIP AdresseIP { get; set; }
        public MasqueReseau MasqueReseau { get; set; }

        public bool EstBonneInterfacePourIP(string p_autreAdresseIP)
        {
            if (string.IsNullOrWhiteSpace(p_autreAdresseIP))
            {
                throw new ArgumentException("Le paramètre ne doit pas être nul.", "p_autreAdresseIP");
            }

            return this.EstBonneInterfacePourIP(new AdresseIP(p_autreAdresseIP));
        }

        public bool EstBonneInterfacePourIP(AdresseIP p_autreAdresseIP)
        {
            if (p_autreAdresseIP == null)
            {
                throw new ArgumentException("Le paramètre ne doit pas être nul.", "p_autreAdresseIP");
            }

            AdresseIP adresseDebut = this.AdresseIP.ObtenirAdresseReseau(this.MasqueReseau);
            AdresseIP adresseFin = this.AdresseIP.ObtenirAdresseBroadcast(this.MasqueReseau);

            return adresseDebut.CompareTo(p_autreAdresseIP) <= 0
                && adresseFin.CompareTo(p_autreAdresseIP) >= 0;
        }

        public AdresseIP ObtenirPremiereAdresseReseau()
        {
            ValiderPresenceAdresseIPEtMasque();
            return this.AdresseIP.ObtenirPremiereAdresseReseau(this.MasqueReseau);
        }

        public AdresseIP ObtenirDerniereAdresseReseau()
        {
            return this.AdresseIP.ObtenirDerniereAdresseReseau(this.MasqueReseau);
        }

        public AdresseIP ObtenirAdresseReseau()
        {
            return this.AdresseIP.ObtenirAdresseReseau(this.MasqueReseau);
        }

        public AdresseIP ObtenirAdresseBroadcast()
        {
            return this.AdresseIP.ObtenirAdresseBroadcast(this.MasqueReseau);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Interface {this.NomInterface}");
            sb.AppendLine($"IPv4 : {this.AdresseIP}");
            sb.AppendLine($"Adresse réseau : {this.ObtenirAdresseReseau()}");
            sb.AppendLine($"Adresse broadcast : {this.ObtenirAdresseBroadcast()}");
            sb.AppendLine($"Première adresse réseau : {this.ObtenirPremiereAdresseReseau()}");
            sb.AppendLine($"Derniere adresse réseau : {this.ObtenirDerniereAdresseReseau()}");

            return sb.ToString();
        }

        private void ValiderPresenceAdresseIPEtMasque()
        {
            if (this.AdresseIP == null || this.MasqueReseau == null)
            {
                throw new InvalidOperationException("L'adresse IP et le masque réseau doivent être définies avant les appels de méthodes");
            }
        }

    }
}
