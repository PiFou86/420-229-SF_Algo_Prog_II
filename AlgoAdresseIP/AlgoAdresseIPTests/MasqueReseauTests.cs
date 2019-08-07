using AlgoAdresseIP;
using System;
using System.Collections.Generic;
using Xunit;

namespace AlgoAdresseIPTests
{
    public class MasqueReseauTests
    {
        [Theory]
        [MemberData(nameof(CIDRMasque))]
        public void Ctr_CIDR_ValidObj(int p_cidr, string p_masqueAttendu)
        {
            // Arranger

            // Agir
            MasqueReseau mr = new MasqueReseau(p_cidr);

            // Auditer
            Assert.Equal(p_masqueAttendu, mr.ToString());
            Assert.Equal(p_cidr, mr.CIDR);
        }

        [Fact]
        public void Ctr_CIDRNeg_ArgumentOutOfRangeException()
        {
            // Arranger

            // Agir & Auditer
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                MasqueReseau mr = new MasqueReseau(-1);
            });
        }

        [Fact]
        public void Ctr_CIDRSupp32_ArgumentOutOfRangeException()
        {
            // Arranger

            // Agir & Auditer
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                MasqueReseau mr = new MasqueReseau(33);
            });
        }

        [Theory]
        [MemberData(nameof(CIDRMasque))]
        public void Ctr_String_ValidObj(int p_cidr, string p_masque)
        {
            // Arranger

            // Agir
            MasqueReseau mr = new MasqueReseau(p_masque);

            // Auditer
            Assert.Equal(p_masque, mr.ToString());
            Assert.Equal(p_cidr, mr.CIDR);
        }

        [Theory]
        [MemberData(nameof(MasqueErreurFormatException))]
        public void Ctr_MasqueFormatInvalid_FormatException(string p_masque)
        {
            // Arranger

            // Agir & Auditer
            Assert.Throws<FormatException>(() => {
                MasqueReseau aIP = new MasqueReseau(p_masque);
            });
        }

        public static IEnumerable<object[]> CIDRMasque = new List<object[]>() {
            new object[] { 0, "0.0.0.0"},
            new object[] { 1, "128.0.0.0"},
            new object[] { 2, "192.0.0.0"},
            new object[] { 3, "224.0.0.0"},
            new object[] { 4, "240.0.0.0"},
            new object[] { 5, "248.0.0.0"},
            new object[] { 6, "252.0.0.0"},
            new object[] { 7, "254.0.0.0"},
            new object[] { 8, "255.0.0.0"},
            new object[] { 9, "255.128.0.0"},
            new object[] { 10, "255.192.0.0"},
            new object[] { 11, "255.224.0.0"},
            new object[] { 12, "255.240.0.0"},
            new object[] { 13, "255.248.0.0"},
            new object[] { 14, "255.252.0.0"},
            new object[] { 15, "255.254.0.0"},
            new object[] { 16, "255.255.0.0"},
            new object[] { 17, "255.255.128.0"},
            new object[] { 18, "255.255.192.0"},
            new object[] { 19, "255.255.224.0"},
            new object[] { 20, "255.255.240.0"},
            new object[] { 21, "255.255.248.0"},
            new object[] { 22, "255.255.252.0"},
            new object[] { 23, "255.255.254.0"},
            new object[] { 24, "255.255.255.0"},
            new object[] { 25, "255.255.255.128"},
            new object[] { 26, "255.255.255.192"},
            new object[] { 27, "255.255.255.224"},
            new object[] { 28, "255.255.255.240"},
            new object[] { 29, "255.255.255.248"},
            new object[] { 30, "255.255.255.252"},
            new object[] { 31, "255.255.255.254"},
            new object[] { 32, "255.255.255.255"},
        };

        public static IEnumerable<object[]> MasqueErreurFormatException = new List<object[]>() {
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
            new object[] {"193.0.0.0"},
            new object[] {"0.255.255.255"},
            new object[] {"254.255.255.0"},
        };
    }
}
