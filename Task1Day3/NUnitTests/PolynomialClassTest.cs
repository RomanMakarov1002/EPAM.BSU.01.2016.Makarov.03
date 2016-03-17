using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Polynomial;

namespace NUnitTests
{
    [TestFixture]
    public class PolynomialClassTest
    {
        public double[] coeffForPolynom1 = { 1, 2, 3 };
        private double[] coeffForPolynom2 = { 1, 2, 3, 4 };
        private double[] coeffForAdd = { 2, 4, 6, 4 };
        private double[] coeffForSub = { 0, 0, 0, 4 };
        private double[] coeffForMul = { 1, 4, 10, 16, 17, 12 };


        [Test]
        public void Add()
        {
            PolynomialBuilding polynom1 = new PolynomialBuilding(coeffForPolynom1);
            PolynomialBuilding polynom2 = new PolynomialBuilding(coeffForPolynom2);
            PolynomialBuilding expectedPolynom = new PolynomialBuilding(coeffForAdd);
            Assert.That(polynom1 + polynom2, Is.EqualTo(expectedPolynom));
        }

        [Test]
        public void Sub()
        {
            PolynomialBuilding polynom1 = new PolynomialBuilding(coeffForPolynom1);
            PolynomialBuilding polynom2 = new PolynomialBuilding(coeffForPolynom2);
            PolynomialBuilding expectedPolynom = new PolynomialBuilding(coeffForSub);
            Assert.That(polynom2 - polynom1, Is.EqualTo(expectedPolynom));
        }

        [Test]
        public void Mul()
        {
            PolynomialBuilding polynom1 = new PolynomialBuilding(coeffForPolynom1);
            PolynomialBuilding polynom2 = new PolynomialBuilding(coeffForPolynom2);
            PolynomialBuilding expectedPolynom = new PolynomialBuilding(coeffForMul);
            Assert.That(polynom2 * polynom1, Is.EqualTo(expectedPolynom));
        }

        [Test]
        public void Equals()
        {
            PolynomialBuilding polynom1 = new PolynomialBuilding(coeffForPolynom1);
            PolynomialBuilding polynom2 = new PolynomialBuilding(coeffForPolynom1);
            Assert.That(polynom1.Equals(polynom2), Is.True);
        }

        

        [Test]
        public void NotEqualsOperator()
        {
            PolynomialBuilding polynom1 = new PolynomialBuilding(coeffForPolynom1);
            PolynomialBuilding polynom2 = new PolynomialBuilding(coeffForPolynom2);
            Assert.That(polynom1 != polynom2, Is.True);
        }

        [Test]
        public void Calculating()
        {
            PolynomialBuilding polynom1 = new PolynomialBuilding(coeffForPolynom2);

            Assert.That(polynom1.Calculate(2)==49, Is.True);
        }

    }
}
