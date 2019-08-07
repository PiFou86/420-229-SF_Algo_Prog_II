using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoAdresseIP
{
    public class MasqueReseau
    {
        public uint Masque { get; private set; }
        public int CIDR { get; private set; }

        public MasqueReseau(int p_cidr)
        {
            if (_CIDR2MASQUE.ContainsKey(p_cidr))
            {
                this.Masque = _CIDR2MASQUE[p_cidr];
                this.CIDR = p_cidr;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Le CIDR est invalide", "p_cidr");
            }
        }

        public MasqueReseau(string p_masque)
        {
            if (string.IsNullOrWhiteSpace(p_masque))
            {
                throw new ArgumentNullException("p_masque");
            }

            string[] parties = p_masque.Split('.');
            if (parties.Length != 4)
            {
                throw new FormatException("Le format doit être w.x.y.z");
            }

            bool dernierDiff255 = true;
            this.Masque = 0;
            this.CIDR = 0;
            foreach (string partie in parties)
            {
                this.Masque <<= 8;
                // Si ce n'est pas une valeur correcte, la méthode Parse va lever une erreur 
                byte valeurPartie = byte.Parse(partie);
                int indexValeursPossiblesMasque = PARTIEMASQUEVALEURSPOSSIBLES.IndexOf(valeurPartie);
                if ( (!dernierDiff255 && valeurPartie != 0) 
                    || indexValeursPossiblesMasque < 0)
                {
                    throw new FormatException("p_masque");
                }
                this.Masque |= valeurPartie;
                this.CIDR += indexValeursPossiblesMasque;
                dernierDiff255 = valeurPartie == 255;
            }
        }

        public byte Partie1
        {
            get
            {
                return (byte)((this.Masque >> 24) & 0xff);
            }
        }

        public byte Partie2
        {
            get
            {
                return (byte)((this.Masque >> 16) & 0xff);
            }
        }

        public byte Partie3
        {
            get
            {
                return (byte)((this.Masque >> 8) & 0xff);
            }
        }

        public byte Partie4
        {
            get
            {
                return (byte)((this.Masque >> 0) & 0xff);
            }
        }

        public override bool Equals(object p_autreMasque)
        {
            MasqueReseau masque = p_autreMasque as MasqueReseau;
            bool egaux = false;
            if (masque != null)
            {
                egaux = this.Masque == masque.Masque;
            }

            return egaux;
        }

        public override int GetHashCode()
        {
            return this.Masque.GetHashCode();
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

        private readonly static Dictionary<int, uint> _CIDR2MASQUE = new Dictionary<int, uint>() {
            {0,  0b00000000_00000000_00000000_00000000},
            {1,  0b10000000_00000000_00000000_00000000},
            {2,  0b11000000_00000000_00000000_00000000},
            {3,  0b11100000_00000000_00000000_00000000},
            {4,  0b11110000_00000000_00000000_00000000},
            {5,  0b11111000_00000000_00000000_00000000},
            {6,  0b11111100_00000000_00000000_00000000},
            {7,  0b11111110_00000000_00000000_00000000},
            {8,  0b11111111_00000000_00000000_00000000},
            {9,  0b11111111_10000000_00000000_00000000},
            {10, 0b11111111_11000000_00000000_00000000},
            {11, 0b11111111_11100000_00000000_00000000},
            {12, 0b11111111_11110000_00000000_00000000},
            {13, 0b11111111_11111000_00000000_00000000},
            {14, 0b11111111_11111100_00000000_00000000},
            {15, 0b11111111_11111110_00000000_00000000},
            {16, 0b11111111_11111111_00000000_00000000},
            {17, 0b11111111_11111111_10000000_00000000},
            {18, 0b11111111_11111111_11000000_00000000},
            {19, 0b11111111_11111111_11100000_00000000},
            {20, 0b11111111_11111111_11110000_00000000},
            {21, 0b11111111_11111111_11111000_00000000},
            {22, 0b11111111_11111111_11111100_00000000},
            {23, 0b11111111_11111111_11111110_00000000},
            {24, 0b11111111_11111111_11111111_00000000},
            {25, 0b11111111_11111111_11111111_10000000},
            {26, 0b11111111_11111111_11111111_11000000},
            {27, 0b11111111_11111111_11111111_11100000},
            {28, 0b11111111_11111111_11111111_11110000},
            {29, 0b11111111_11111111_11111111_11111000},
            {30, 0b11111111_11111111_11111111_11111100},
            {31, 0b11111111_11111111_11111111_11111110},
            {32, 0b11111111_11111111_11111111_11111111},
        };

        private readonly List<byte> PARTIEMASQUEVALEURSPOSSIBLES = new List<byte>() { 0, 128, 192, 224, 240, 248, 252, 254, 255 };
    }
}
