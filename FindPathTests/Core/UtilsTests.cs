using System;
using FindPath.Core;
using NUnit.Framework;

namespace Tests.Core
{
    [TestFixture]
    public class UtilsTests
    {
        [Test]
        public void TestIntSqrt_1()
        {
            int n = 101;
            for (int i = -n; i < n; i++)
            {
                Assert.AreEqual(Math.Abs(i), Utils.IntSqrt(i * i));
            }
        }

        [Test]
        public void TestIntCubeRoot_1()
        {
            int n = 101;
            for (int i = -n; i < n; i++)
            {
                Assert.AreEqual(i, Utils.IntCubeRoot(i * i * i));
            }
        }
    }
}