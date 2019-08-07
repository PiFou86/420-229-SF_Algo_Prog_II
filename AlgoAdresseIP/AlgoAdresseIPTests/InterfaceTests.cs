using AlgoAdresseIP;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AlgoAdresseIPTests
{
    public class InterfaceTests
    {
        [Theory]
        [MemberData(nameof(IP_Masque_Rx_Broadcast_Debut_Fin))]
        public void ObtenirAdresseReseau_CasNormaux_BonneAdresseRx(AdresseIP p_adresseIP, MasqueReseau p_masqueReseau,
          AdresseIP p_adresseReseau, AdresseIP p_adresseBroadcast,
          AdresseIP p_adresseDebut, AdresseIP p_adresseFin
          )
        {
            // Arranger
            Interface interfaceRx = new Interface() { NomInterface = "Foo", AdresseIP = p_adresseIP, MasqueReseau = p_masqueReseau }; 

            // Agir
            AdresseIP adresseReseau = interfaceRx.ObtenirAdresseReseau();

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
            Interface interfaceRx = new Interface() { NomInterface = "Foo", AdresseIP = p_adresseIP, MasqueReseau = p_masqueReseau };

            // Agir
            AdresseIP adresseBroadcast = interfaceRx.ObtenirAdresseBroadcast();

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
            Interface interfaceRx = new Interface() { NomInterface = "Foo", AdresseIP = p_adresseIP, MasqueReseau = p_masqueReseau };

            // Agir
            AdresseIP premiereAdresseReseau = interfaceRx.ObtenirPremiereAdresseReseau();

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
            Interface interfaceRx = new Interface() { NomInterface = "Foo", AdresseIP = p_adresseIP, MasqueReseau = p_masqueReseau };

            // Agir
            AdresseIP derniereAdresseReseau = interfaceRx.ObtenirDerniereAdresseReseau();

            // Auditer
            Assert.Equal(p_adresseFin, derniereAdresseReseau);
        }

        [Theory]
        [MemberData(nameof(InterfaceRx_AdresseIP_ResultatBonneInterface))]
        public void EstBonneInterfacePourIP_CasNormaux_BonneInterfaceOuNon(Interface p_interface, AdresseIP p_adresseATester, bool p_resultatAttendu)
        {
            // Arranger

            // Agir
            bool resultatCalcule = p_interface.EstBonneInterfacePourIP(p_adresseATester);

            // Auditer
            Assert.Equal(p_resultatAttendu, resultatCalcule);
        }

        public static IEnumerable<object[]> IP_Masque_Rx_Broadcast_Debut_Fin = new List<object[]>()
        {
            new object[] { new AdresseIP("1.2.3.4"), new MasqueReseau(0), new AdresseIP("0.0.0.0"), new AdresseIP("255.255.255.255"), new AdresseIP("0.0.0.1"), new AdresseIP("255.255.255.254") },
            new object[] { new AdresseIP("192.168.0.42"), new MasqueReseau(24), new AdresseIP("192.168.0.0"), new AdresseIP("192.168.0.255"), new AdresseIP("192.168.0.1"), new AdresseIP("192.168.0.254") },
            new object[] { new AdresseIP("192.168.2.34"), new MasqueReseau(23), new AdresseIP("192.168.2.0"), new AdresseIP("192.168.3.255"), new AdresseIP("192.168.2.1"), new AdresseIP("192.168.3.254") },
        };

        public static IEnumerable<object[]> InterfaceRx_AdresseIP_ResultatBonneInterface = new List<object[]>()
        {
            new object[] { DonneesInterfaces.ObtenirInterface_1010023_16(), new AdresseIP("10.10.255.34"), true },
            new object[] { DonneesInterfaces.ObtenirInterface_1010023_16(), new AdresseIP("10.20.255.34"), false },
            new object[] { DonneesInterfaces.ObtenirInterface_1010023_16(), new AdresseIP("10.10.255.255"), true },
            new object[] { DonneesInterfaces.ObtenirInterface_1010023_16(), new AdresseIP("10.10.0.0"), true },
        };
    }
}
