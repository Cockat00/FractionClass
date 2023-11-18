using Fraction_NameSpace;
using NUnit.Framework;

namespace FractionTest
{
    public class Tests
    {
        const int Times = 50;
        private Fraction? _fraction1;
        private Fraction? _fraction2;

        [TearDown]
        public void TearDown()
        {
            _fraction1 = null;
            _fraction2 = null;
        }


        //TESTS ON CONSTRUCTOR - BEGIN

        [Test]
        public void FractionReturnCorrectNumerator([Random(-100,100,Times)] int numerator)
        {
            _fraction1 = new Fraction(numerator, 1);
            Assert.That(_fraction1.Numerator,Is.EqualTo(numerator));
        }

        [Test]
        public void FractionReturnCorrectDenominator([Values(-10,10)]int denominator)         //Cannot test with [Random()], it could give 0 as denominator
        {
            _fraction1 = new Fraction(1, denominator);
            Assert.That(_fraction1.Denominator, Is.EqualTo(Math.Abs(denominator)));
        }

        [Test]
        public void FractionThrowsOnDenominator_0([Values(-1, 1, 0)] int numerator)
        {
            Assert.That(() => new Fraction(numerator,0), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void FractionIsPositiveWhenBothNegative([Random(-100,-1,Times)]int numerator, [Random(-100,-1,Times)]int denominator)
        {
            _fraction1 = new Fraction(numerator, denominator);
            Assert.Multiple(() =>
            {
                Assert.That(_fraction1.Numerator, Is.Positive);         //Cannot test with ".EqualTo", fraction normalization would cause a fail
                Assert.That(_fraction1.Denominator, Is.Positive);
            });
        }

        [TestCase(1,-1,-1,1)]
        [TestCase(-4,3,-4,3)]
        public void FractionNumeratorIsNegativeCorrectly(int numerator, int denominator, int expectedNum, int expectedDenom)
        {
            _fraction1 = new Fraction(numerator, denominator);
            Assert.Multiple(() =>
            {
                Assert.That(_fraction1.Numerator, Is.EqualTo(expectedNum));
                Assert.That(_fraction1.Denominator, Is.EqualTo(expectedDenom));
            });
        }

        [TestCase(2,4,1,2)]
        [TestCase(100,100,1,1)]
        [TestCase(90,-525,-6,35)]
        [TestCase(-2933,-3333,2933,3333)]
        [TestCase(0,20,0,20)]
        public void FractionGetNormalized(int numerator, int denominator, int numNormalized, int denomNormalized)
        {
            _fraction1 = new Fraction(numerator, denominator);
            Assert.Multiple(() =>
                {
                    Assert.That(_fraction1.Numerator, Is.EqualTo(numNormalized));
                    Assert.That(_fraction1.Denominator, Is.EqualTo(denomNormalized));
                }
            );
        }

        //TEST ON CONSTRUCTOR - END
        

        //TEST ON OPERATIONS - BEGIN

        [TestCase(1,2,2,5,9,10)]
        [TestCase(-18,20,33,-11,-39,10)]
        [TestCase(0,1,1,1,1,1)]
        public void TestSumReturnCorrectResult(int numer1, int denom1, int numer2, int denom2, int expNumer, int expDenom)
        {
            _fraction1 = new Fraction(numer1, denom1);
            _fraction2 = new Fraction(numer2, denom2);

            var result = _fraction1 + _fraction2;
            Assert.Multiple(() =>
            {
                Assert.That(result.Numerator, Is.EqualTo(expNumer));
                Assert.That(result.Denominator, Is.EqualTo(expDenom));
            });
        }

        [TestCase(4,1,33,7,-5,7)]
        [TestCase(0,1,0,1,0,1)]
        [TestCase(-18,-9,2,1,0,1)]
        public void TestMinusReturnCorrectResult(int numer1, int denom1, int numer2, int denom2, int expNumer, int expDenom)
        {
            _fraction1 = new Fraction(numer1, denom1);
            _fraction2 = new Fraction(numer2, denom2);

            var result = _fraction1 - _fraction2;
            Assert.Multiple(() =>
            {
                Assert.That(result.Numerator, Is.EqualTo(expNumer));
                Assert.That(result.Denominator, Is.EqualTo(expDenom));
            });
        }

        [TestCase(1,11,11,1,1,1)]
        [TestCase(42,1,0,1,0,1)]
        [TestCase(-22,-46,42,-8,-231,92)]
        public void TestMultiplyReturnCorrectResult(int numer1, int denom1, int numer2, int denom2, int expNumer, int expDenom)
        {
            _fraction1 = new Fraction(numer1, denom1);
            _fraction2 = new Fraction(numer2, denom2);

            var result = _fraction1 * _fraction2;
            Assert.Multiple(() =>
            {
                Assert.That(result.Numerator, Is.EqualTo(expNumer));
                Assert.That(result.Denominator, Is.EqualTo(expDenom));
            });
        }

        [TestCase(33,42,111,8,44,777)]
        [TestCase(11,9,11,9,1,1)]
        [TestCase(0,1,1,1,0,1)]
        public void TestDivisionReturnCorrectResult(int numer1, int denom1, int numer2, int denom2, int expNumer, int expDenom)
        {
            _fraction1 = new Fraction(numer1, denom1);
            _fraction2 = new Fraction(numer2, denom2);

            var result = _fraction1 / _fraction2;
            Assert.Multiple(() =>
            {
                Assert.That(result.Numerator, Is.EqualTo(expNumer));
                Assert.That(result.Denominator, Is.EqualTo(expDenom));
            });
        }

        [Test]
        public void TestDivideByZeroThrows()
        {
            _fraction1 = new Fraction(42, 1);
            _fraction2 = 0;

            Assert.That(() => _fraction1/_fraction2, Throws.TypeOf<DivideByZeroException>());
        }

        //TEST ON OPERATIONS - END


        //TEST ON EQUALS - BEGIN

        [TestCase(0,1,0,22)]
        [TestCase(1,2,2,4)]
        [TestCase(-3,7,33,-77)]
        [TestCase(3,5,-3,-5)]
        public void TestFractionsAreEquals(int numer1, int denom1, int numer2, int denom2)
        {
            _fraction1 = new Fraction(numer1, denom1);
            _fraction2 = new Fraction(numer2, denom2);

            Assert.That(_fraction1.Equals(_fraction2), Is.True);
        }

        [TestCase(-3,7,-3,-7)]
        [TestCase(0,1,1,1)]
        [TestCase(14,7,7,14)]
        [TestCase(33,11,333,11)]
        public void TestFractionsAreNotEquals(int numer1, int denom1, int numer2, int denom2)
        {
            _fraction1 = new Fraction(numer1, denom1);
            _fraction2 = new Fraction(numer2, denom2);

            Assert.That(_fraction1.Equals(_fraction2), Is.False);
        }

        [Test]
        public void TestFractionNotEqualsToOtherObjects([Values(null,"1/3",2)]object obj)
        {
            _fraction1 = new Fraction(1, 3);
            Assert.That(_fraction1.Equals(obj), Is.False);
        }

        [TestCase(1,2)]
        [TestCase(0,1)]
        [TestCase(2,-3)]
        public void TestFractionsAreTheSameInstance(int numerator, int denominator)
        {
            _fraction1 = new Fraction(numerator, denominator);
            _fraction2 = _fraction1;

            Assert.That(_fraction1.Equals(_fraction2), Is.True);
        }

        [TestCase(1,2,2,4)]
        [TestCase(33,7,-33,-7)]
        [TestCase(-10,9,20,-18)]
        public void TestFractionsSameHashCode(int numer1, int denom1, int numer2, int denom2)
        {
            _fraction1 = new Fraction(numer1, denom1);
            _fraction2 = new Fraction(numer2, denom2);

            Assert.That(_fraction1.GetHashCode(),Is.EqualTo(_fraction2.GetHashCode()));
        }

        [TestCase(-1,2,2,4)]
        [TestCase(33,7,-33,-77)]
        [TestCase(-10,9,-20,-18)]
        public void TestFractionsDifferentHashCode(int numer1, int denom1, int numer2, int denom2)
        {
            _fraction1 = new Fraction(numer1, denom1);
            _fraction2 = new Fraction(numer2, denom2);

            Assert.That(_fraction1.GetHashCode(), Is.Not.EqualTo(_fraction2.GetHashCode()));
        }

        //TEST ON EQUALS - END


        //TEST ON CASTING - BEGIN

        [Test]
        public void TestImplicitCastIntToFraction([Random(-100, 100, Times)] int value)
        {
            _fraction1 = value;
            Assert.Multiple(() =>
            {
                Assert.That(_fraction1.Numerator, Is.EqualTo(value));
                Assert.That(_fraction1.Denominator, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestExplicitCastToIntSuccess([Random(-100, 100, Times)] int numerator)
        {
            _fraction1 = new Fraction(numerator, 1);
            Assert.That((int)_fraction1,Is.EqualTo(numerator));

        }

        [TestCase(42,11)]
        [TestCase(0,11)]
        public void TestExplicitCastToIntThrows(int numerator, int denominator)
        {
            _fraction1 = new Fraction(numerator, denominator);
            Assert.That(() => (int)_fraction1, Throws.TypeOf<InvalidCastException>());

        }

        [TestCase(11,5,"11/5")]
        [TestCase(22,11,"2")]
        [TestCase(22,-11,"-2")]
        public void TestFractionToStringIsEqualToString(int numer, int denom, string expected)
        {
            _fraction1 = new Fraction(numer, denom);

            Assert.That(_fraction1.ToString(),Is.EqualTo(expected));
        }

        [TestCase(11, 5, "-11/5")]
        [TestCase(22, 11, "22/11")]
        [TestCase(22, -11, "-2/1")]
        public void TestFractionToStringIsNotEqualToString(int numer, int denom, string expected)
        {
            _fraction1 = new Fraction(numer, denom);

            Assert.That(_fraction1.ToString(), Is.Not.EqualTo(expected));
        }

        //TEST ON CASTING - END
    }
}