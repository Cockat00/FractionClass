namespace Fraction_NameSpace
{
    public class Fraction
    {
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        public Fraction(int num, int denom)
        {
            if (denom == 0)
                throw new ArgumentException($"{nameof(Denominator)} cannot be 0.");

            Numerator = num;
            Denominator = denom;

            FixSignedValues();
            NormalizeFraction();
        }

        public Fraction(int num)
        {
            Numerator = num;
            Denominator = 1;
        }

        private void FixSignedValues()
        {
            if (Denominator < 0)
            {
                Numerator *= -1;
                Denominator = Math.Abs(Denominator);
            }
        }

        private void NormalizeFraction()
        {
            if (Numerator != 0)         //Preventing to enter a loop when not necessary              
            {
                int absNum = Math.Abs(Numerator);

                for (int i = Denominator; i > 0; i--)
                {
                    if (absNum % i == 0 && Denominator % i == 0)
                    {
                        absNum /= i;
                        Denominator /= i;
                    }
                }
                Numerator = (Numerator * absNum) / Math.Abs(Numerator);
            }
        }

        public static explicit operator int(Fraction fraction)
        {
            if (fraction.Denominator > 1)
                throw new InvalidCastException(
                    $"Cannot convert {nameof(fraction)} of type '{typeof(Fraction)}' to '{typeof(int)}' with {fraction.Denominator} as denominator.");

            return fraction.Numerator;
        }

        public static implicit operator Fraction(int value) => new(value, 1);

        public static Fraction operator +(Fraction a, Fraction b)
        {
            int commonDenom = a.Denominator * b.Denominator;
            int num1 = a.Numerator * b.Denominator;
            int num2 = b.Numerator * a.Denominator;
            return new Fraction(num1+num2, commonDenom);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            int commonDenom = a.Denominator * b.Denominator;
            int num1 = a.Numerator * b.Denominator;
            int num2 = b.Numerator * a.Denominator;
            return new Fraction(num1 - num2, commonDenom);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            int numerator = a.Numerator * b.Numerator;
            int denominator = a.Denominator * b.Denominator; 
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Numerator == 0)
                throw new DivideByZeroException($"Cannot divide by {b.Numerator}");

            int numerator = a.Numerator * b.Denominator;
            int denominator = a.Denominator * b.Numerator;
            return new Fraction(numerator, denominator);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Fraction)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        public bool Equals(Fraction other)
        {
            if (Numerator == 0 && other.Numerator == 0)
                return true;
            return Numerator==other.Numerator && Denominator==other.Denominator;
        }

        public override string ToString()
        {
            if (Denominator == 1)
                return Numerator.ToString();
            return $"{Numerator}/{Denominator}";
        }
    }
}