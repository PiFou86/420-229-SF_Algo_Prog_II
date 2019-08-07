using AlgoAdresseIP;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoAdresseIPTests
{
    public class DonneesInterfaces
    {
        public static Interface ObtenirInterface_1010023_16()
        {
            return new Interface() { NomInterface = "eth0", AdresseIP = new AdresseIP("10.10.0.23"), MasqueReseau = new MasqueReseau("255.255.0.0") };
        }

        public static Interface ObtenirInterface_1020010_16()
        {
            return new Interface() { NomInterface = "eth1", AdresseIP = new AdresseIP("10.20.0.42"), MasqueReseau = new MasqueReseau("255.255.0.0") };
        }

        public static Interface ObtenirInterface_1030010_16()
        {
            return new Interface() { NomInterface = "eth2", AdresseIP = new AdresseIP("10.30.0.10"), MasqueReseau = new MasqueReseau("255.255.0.0") };
        }

        public static Interface ObtenirInterface_192168034_24()
        {
            return new Interface() { NomInterface = "eth3", AdresseIP = new AdresseIP("192.168.0.34"), MasqueReseau = new MasqueReseau("255.255.255.0") };
        }

        public static Interface ObtenirInterface_192168034_23()
        {
            return new Interface() { NomInterface = "eth4", AdresseIP = new AdresseIP("192.168.2.53"), MasqueReseau = new MasqueReseau("255.255.254.0") };
        }

        public static List<Interface> ObtenirListe01Interfaces()
        {
            return new List<Interface>()
            {
                ObtenirInterface_1010023_16(),
                ObtenirInterface_1020010_16(),
                ObtenirInterface_1030010_16(),
                ObtenirInterface_192168034_23(),
                ObtenirInterface_192168034_24()
            };
        }
    }
}
