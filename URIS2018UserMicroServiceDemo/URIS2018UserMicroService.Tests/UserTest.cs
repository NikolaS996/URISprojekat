using System;
using NUnit.Framework;

namespace URIS2018UserMicroService.Tests
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void Add_Success()
        {
            Controllers.Calc calc = new Controllers.Calc();
            int result = calc.Add(1, 2);

            Assert.AreEqual(3, result);
        }

        [TestCase(1, 2, 3)]
        [TestCase(2, 2, 4)]
        [TestCase(2, 4, 5)]
        public void Add_Success_Novo(int a, int b, int expected)
        {
            Controllers.Calc calc = new Controllers.Calc();
            int result = calc.Add(a, b);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Gadjanje_Rute()
        {

        }
    }
}
