using AlgoAdresseIP;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AlgoAdresseIPTests
{
    // Je ne suis pas spéciliste en réseau Claude me tirerait surement les oreilles sur mes cas et approximations
    public class AdresseIPTests
    {
        [Theory]
        [MemberData(nameof(IPTests))]
        public void Ctr_IP_ValidObj(string p_adresseIP)
        {
            // Arranger

            // Agir
            AdresseIP aIP = new AdresseIP(p_adresseIP);

            // Auditer
            Assert.Equal(p_adresseIP, aIP.ToString());
        }

        [Theory]
        [MemberData(nameof(IPErreurFormatException))]
        public void Ctr_IPFormatInvalid_FormatException(string p_adresseIP)
        {
            // Arranger

            // Agir & Auditer
            Assert.Throws<FormatException>(() =>
            {
                AdresseIP aIP = new AdresseIP(p_adresseIP);
            });
        }

        [Theory]
        [MemberData(nameof(IP_Masque_Rx_Broadcast_Debut_Fin))]
        public void ObtenirAdresseReseau_CasNormaux_BonneAdresseRx(AdresseIP p_adresseIP, MasqueReseau p_masqueReseau,
            AdresseIP p_adresseReseau, AdresseIP p_adresseBroadcast,
            AdresseIP p_adresseDebut, AdresseIP p_adresseFin
            )
        {
            // Arranger

            // Agir
            AdresseIP adresseReseau = p_adresseIP.ObtenirAdresseReseau(p_masqueReseau);

            // Auditer
            Assert.Equal(p_adresseReseau, adresseReseau);
        }

        [Theory]
        [MemberData(nameof(IP_Masque_Rx_Broadcast_Debut_Fin))]
        public void ObtenirAdresseBroadcast_CasNormaux_BonneAdresseRx(AdresseIP p_adresseIP, MasqueReseau p_masqueReseau,
                        AdresseIP p_adresseReseau, AdresseIP p_adresseBroadcast,
                        AdresseIP p_adresseDebut, AdresseIP p_adresseFin)
        {
            // Arranger

            // Agir
            AdresseIP adresseBroadcast = p_adresseIP.ObtenirAdresseBroadcast(p_masqueReseau);

            // Auditer
            Assert.Equal(p_adresseBroadcast, adresseBroadcast);
        }

        [Theory]
        [MemberData(nameof(IP_Masque_Rx_Broadcast_Debut_Fin))]
        public void ObtenirPremiereAdresseReseau_CasNormaux_BonneAdresseRx(AdresseIP p_adresseIP, MasqueReseau p_masqueReseau,
                        AdresseIP p_adresseReseau, AdresseIP p_adresseBroadcast,
                        AdresseIP p_adresseDebut, AdresseIP p_adresseFin)
        {
            // Arranger

            // Agir
            AdresseIP premiereAdresseReseau = p_adresseIP.ObtenirPremiereAdresseReseau(p_masqueReseau);

            // Auditer
            Assert.Equal(p_adresseDebut, premiereAdresseReseau);
        }

        [Theory]
        [MemberData(nameof(IP_Masque_Rx_Broadcast_Debut_Fin))]
        public void ObtenirDerniereAdresseReseau_CasNormaux_BonneAdresseRx(AdresseIP p_adresseIP, MasqueReseau p_masqueReseau,
                        AdresseIP p_adresseReseau, AdresseIP p_adresseBroadcast,
                        AdresseIP p_adresseDebut, AdresseIP p_adresseFin)
        {
            // Arranger

            // Agir
            AdresseIP derniereAdresseReseau = p_adresseIP.ObtenirDerniereAdresseReseau(p_masqueReseau);

            // Auditer
            Assert.Equal(p_adresseFin, derniereAdresseReseau);
        }

        public static IEnumerable<object[]> IP_Masque_Rx_Broadcast_Debut_Fin = new List<object[]>() {
            new object[] { new AdresseIP("1.2.3.4"), new MasqueReseau(0), new AdresseIP("0.0.0.0"), new AdresseIP("255.255.255.255"), new AdresseIP("0.0.0.1"), new AdresseIP("255.255.255.254") },
            new object[] { new AdresseIP("192.168.0.42"), new MasqueReseau(24), new AdresseIP("192.168.0.0"), new AdresseIP("192.168.0.255"), new AdresseIP("192.168.0.1"), new AdresseIP("192.168.0.254") },
            new object[] { new AdresseIP("192.168.2.34"), new MasqueReseau(23), new AdresseIP("192.168.2.0"), new AdresseIP("192.168.3.255"), new AdresseIP("192.168.2.1"), new AdresseIP("192.168.3.254") },
        };

        public static IEnumerable<object[]> IPTests = new List<object[]>() {
            new object[] {"192.168.0.0"},
            new object[] {"8.8.8.8"},
            new object[] {"0.0.0.0"},
        };

        public static IEnumerable<object[]> IPErreurFormatException = new List<object[]>() {
            new object[] {"42"},
            new object[] {"42."},
            new object[] {"42.42"},
            new object[] {"42.42."},
            new object[] {"42.42.42"},
            new object[] {"42.42.42."},
            new object[] {"42.42.42.42."},
            new object[] {"42.42.42.42.42"},
            new object[] {"42.42.42.42.42."},
            new object[] {"42.42.42.42.42.42"},
        };
    }
}
