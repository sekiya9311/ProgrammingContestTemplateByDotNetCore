using System;
using Xunit;

using static Library.Math.Functions;

namespace Library.Tests.Math
{
    public class Gcd_Lcm_Test
    {

        [Fact]
        public void GcdTest()
        {
            var v1 = Gcd(1, 1);
            Assert.Equal(1, v1);

            var v2 = Gcd(3, 6);
            Assert.Equal(3, v2);

            var v2Rev = Gcd(6, 3);
            Assert.Equal(v2, v2Rev);

            var v3 = Gcd(6, 10);
            Assert.Equal(2, v3);

            var v3Rev = Gcd(10, 6);
            Assert.Equal(v3, v3Rev);
        }

        [Fact]
        public void LcmTest()
        {
            var v1 = Lcm(1, 1);
            Assert.Equal(1, v1);

            var v2 = Lcm(3, 6);
            Assert.Equal(6, v2);

            var v2Rev = Lcm(6, 3);
            Assert.Equal(v2, v2Rev);

            var v3 = Lcm(6, 10);
            Assert.Equal(30, v3);

            var v3Rev = Lcm(10, 6);
            Assert.Equal(v3, v3Rev);
        }
    }
}
