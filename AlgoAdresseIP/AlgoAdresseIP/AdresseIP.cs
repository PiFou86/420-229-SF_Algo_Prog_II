using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoAdresseIP
{
    public class AdresseIP : IComparable<AdresseIP>
    {
        private uint m_adresseIP;

        public AdresseIP(string p_adresseIP)
        {
            if (string.IsNullOrWhiteSpace(p_adresseIP))
            {
                throw new ArgumentNullException("p_masque");
            }

            string[] parties = p_adresseIP.Split('.');
            if (parties.Length != 4)
            {
                throw new FormatException("p_masque");
            }

            foreach (string partie in parties)
            {
                this.m_adresseIP <<= 8;
                // Si ce n'est pas une valeur correcte, la méthode Parse va lever une erreur 
                this.m_adresseIP |= byte.Parse(partie);
            }
        }

        private AdresseIP(uint p_adresseIP)
        {
            this.m_adresseIP = p_adresseIP;
        }

        public byte Partie1
        {
            get
            {
                return (byte)((this.m_adresseIP >> 24) & 0xff);
            }
        }

        public byte Partie2
        {
            get
            {
                return (byte)((this.m_adresseIP >> 16) & 0xff);
            }
        }

        public byte Partie3
        {
            get
            {
                return (byte)((this.m_adresseIP >> 8) & 0xff);
            }
        }

        public byte Partie4
        {
            get
            {
                return (byte)((this.m_adresseIP >> 0) & 0xff);
            }
        }

        public AdresseIP ObtenirAdresseBroadcast(MasqueReseau p_masqueReseau)
        {
            if (p_masqueReseau == null)
            {
                throw new ArgumentException("Le paramètre ne doit pas être nul.", "p_masqueReseau");
            }

            AdresseIP adresseIPDebut = this.ObtenirAdresseReseau(p_masqueReseau);

            uint adresseFin = adresseIPDebut.m_adresseIP | ~p_masqueReseau.Masque;

            return new AdresseIP(adresseFin);
        }

        public AdresseIP ObtenirAdresseReseau(MasqueReseau p_masqueReseau)
        {
            if (p_masqueReseau == null)
            {
                throw new ArgumentException("Le paramètre ne doit pas être nul.", "p_masqueReseau");
            }

            uint adresseDebut = this.m_adresseIP & p_masqueReseau.Masque;

            return new AdresseIP(adresseDebut);
        }

        public AdresseIP ObtenirPremiereAdresseReseau(MasqueReseau p_masqueReseau)
        {
            if (p_masqueReseau == null)
            {
                throw new ArgumentException("Le paramètre ne doit pas être nul.", "p_masqueReseau");
            }

            AdresseIP adresseReseau = ObtenirAdresseReseau(p_masqueReseau);

            if (p_masqueReseau.CIDR < 32)
            {
                return new AdresseIP(adresseReseau.m_adresseIP + 1);
            }

            return adresseReseau;
        }

        public AdresseIP ObtenirDerniereAdresseReseau(MasqueReseau p_masqueReseau)
        {
            if (p_masqueReseau == null)
            {
                throw new ArgumentException("Le paramètre ne doit pas être nul.", "p_masqueReseau");
            }

            AdresseIP adresseReseau = ObtenirAdresseBroadcast(p_masqueReseau);

            if (p_masqueReseau.CIDR < 32)
            {
                return new AdresseIP(adresseReseau.m_adresseIP - 1);
            }

            return adresseReseau;
        }

        public int CompareTo(AdresseIP p_autreAdresseIP)
        {
            if (p_autreAdresseIP == null)
            {
                throw new ArgumentException("Le paramètre ne doit pas être nul.", "p_masqueReseau");
            }

            return this.m_adresseIP.CompareTo(p_autreAdresseIP.m_adresseIP);
        }

        public override bool Equals(object p_autreAdresse)
        {
            if (p_autreAdresse == null)
            {
                return false;
            }
            AdresseIP adresseIP = p_autreAdresse as AdresseIP;
            bool egaux = false;
            if (adresseIP != null)
            {
                egaux = this.m_adresseIP == adresseIP.m_adresseIP;
            }

            return egaux;
        }

        public override int GetHashCode()
        {
            return this.m_adresseIP.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.Partie1);
            sb.Append('.');
            sb.Append(this.Partie2);
            sb.Append('.');
            sb.Append(this.Partie3);
            sb.Append('.');
            sb.Append(this.Partie4);

            return sb.ToString();
        }
    }
}
