using System;
using System.Collections.Generic;

namespace AlgoAdresseIP
{
    class Program
    {
        static void Main(string[] args)
        {
            InterfacesReseaux interfacesReseaux01 = new InterfacesReseaux();
            interfacesReseaux01.Interfaces.Add(new Interface() { NomInterface = "eth0", AdresseIP = new AdresseIP("10.10.0.23"), MasqueReseau = new MasqueReseau("255.255.0.0") });
            interfacesReseaux01.Interfaces.Add(new Interface() { NomInterface = "eth1", AdresseIP = new AdresseIP("10.20.0.42"), MasqueReseau = new MasqueReseau("255.255.0.0") });
            interfacesReseaux01.Interfaces.Add(new Interface() { NomInterface = "eth2", AdresseIP = new AdresseIP("10.30.0.10"), MasqueReseau = new MasqueReseau("255.255.0.0") });
            interfacesReseaux01.Interfaces.Add(new Interface() { NomInterface = "eth3", AdresseIP = new AdresseIP("192.168.0.34"), MasqueReseau = new MasqueReseau("255.255.255.0") });
            interfacesReseaux01.Interfaces.Add(new Interface() { NomInterface = "eth4", AdresseIP = new AdresseIP("192.168.2.53"), MasqueReseau = new MasqueReseau("255.255.254.0") });

            Console.WriteLine(interfacesReseaux01);

            List<string> lstAdressesATester = new List<string>()
            {
                "192.168.2.34",
                "23.23.23.23",
                "10.20.23.4"
            };

            foreach (string adresse in lstAdressesATester)
            {
                Console.WriteLine($"Interface pour accéder à l'adresse {adresse}");
                Interface interfaceRx = interfacesReseaux01.ObtenirInterface(adresse);
                if (interfaceRx != null)
                {
                    Console.WriteLine(interfaceRx);
                }
                else
                {
                    Console.WriteLine("Utilisez la passerelle par défaut");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Hello World!");
        }


    }
}
