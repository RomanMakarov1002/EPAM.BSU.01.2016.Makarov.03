using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;



namespace Polynomial
{
    public sealed class PolynomialBuilding
    {
        private readonly Polynom[] polynom;
        const double Eps = 00000.1;
        public struct Polynom
        {
            public double Coefficient;
            public int Power;

            public Polynom(double coefficient, int power)
            {
                Coefficient = coefficient;
                Power = power;               
            }
        }

        public PolynomialBuilding(double coefficient, int power)
        {
            polynom = new Polynom[power+1];
            if (Math.Abs(coefficient)>Eps)
                polynom[power] = new Polynom(coefficient,power);
            else
                polynom[0] = new Polynom();
            
        }

        public PolynomialBuilding(params double[] coefficient)
        {
            polynom = new Polynom[coefficient.Length];
            for (int i = 0; i < coefficient.Length; i++)
                if (coefficient[i]>Eps)
                    polynom[i] = new Polynom(coefficient[i],i);
                else
                    polynom[i] = new Polynom();            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.polynom.Length; i++)
            {
                if (!(this.polynom[i].Coefficient < 0+Eps && this.polynom[i].Coefficient > 0 -Eps))
                {
                    if (polynom[i].Coefficient > 0)
                        sb.Append('+');
                    if (polynom[i].Power == 0)
                        sb.AppendFormat("{0}",this.polynom[i].Coefficient);
                    else if (polynom[i].Power == 1)
                        sb.AppendFormat("{0}x", this.polynom[i].Coefficient);
                    else
                    sb.AppendFormat("{0}x^{1}", this.polynom[i].Coefficient, this.polynom[i].Power);
                }
            }
            if (sb[0] == '+')
                sb.Remove(0, 1);
            return sb.ToString();
        }

        private PolynomialBuilding(Polynom[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException();
            }
            if (values.Count() == 0)
            {
                throw new ArgumentNullException();
            }
            polynom = new Polynom[values.Length];
            for (int i = 0; i < values.Length; i++)
                if (Math.Abs(values[i].Coefficient) > Eps)
                    polynom[i] = values[i];                     
        }

        public double Calculate(double x)
        {
            if (ReferenceEquals(this, null))
            {
                return 0;
            }
            double value = 0;
            for (int i = 0; i < this.polynom.Length; i++)
            {
                value += Math.Pow(x, i)*this.polynom[i].Coefficient;
            }
            return value;
        }


        
        public static PolynomialBuilding operator +(PolynomialBuilding first, PolynomialBuilding second)
        {
            if (first == null || second == null)
                throw new NullReferenceException();
            int lastPow = Math.Max(first.polynom.Length, second.polynom.Length);
            Polynom[] resultPolynom = new Polynom[lastPow];

            for (int i = 0; i < first.polynom.Length; i++)
                resultPolynom[i].Coefficient = first.polynom[i].Coefficient;

            for (int i = 0; i < second.polynom.Length; i++)
                resultPolynom[i].Coefficient += second.polynom[i].Coefficient;

            for (int i=0; i<lastPow; i++)
                if (!(resultPolynom[i].Coefficient < 0+Eps && resultPolynom[i].Coefficient >0-Eps))
                    resultPolynom[i].Power = i; 
                                  
            return new PolynomialBuilding(resultPolynom);
        }

        public static PolynomialBuilding operator -(PolynomialBuilding first, PolynomialBuilding second)
        {
            if (first == null || second == null)
                throw new NullReferenceException();
            int lastPow = Math.Max(first.polynom.Length, second.polynom.Length);
            Polynom[] resultPolynom = new Polynom[lastPow];
            for (int i = 0; i < first.polynom.Length; i++)
                resultPolynom[i].Coefficient = first.polynom[i].Coefficient;

            for (int i = 0; i < second.polynom.Length; i++)
                resultPolynom[i].Coefficient -= second.polynom[i].Coefficient;

            for (int i = 0; i < lastPow; i++)
                if (!(resultPolynom[i].Coefficient < 0 + Eps && resultPolynom[i].Coefficient > 0 - Eps))
                    resultPolynom[i].Power = i;

            return new PolynomialBuilding(resultPolynom);
        }


        public static PolynomialBuilding operator *(PolynomialBuilding first, PolynomialBuilding second)
        {
            if (first == null || second == null)
                throw new NullReferenceException();
            Polynom[] resultPolynom = new Polynom[first.polynom.Length+second.polynom.Length-1];
            for (int i = 0; i < first.polynom.Length; i++)
                for (int j = 0; j < second.polynom.Length; j++)
                    resultPolynom[i + j].Coefficient += first.polynom[i].Coefficient*second.polynom[j].Coefficient;

            for (int i=0; i<resultPolynom.Length; i++)
                if (!(resultPolynom[i].Coefficient < 0 + Eps && resultPolynom[i].Coefficient > 0 - Eps))
                    resultPolynom[i].Power = i;
            return new PolynomialBuilding(resultPolynom);
        }



        public static bool operator ==(PolynomialBuilding lhs, PolynomialBuilding rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null)) return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(PolynomialBuilding lhs, PolynomialBuilding rhs)
        {
            return !(lhs == rhs);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(obj, null)) return false;  
            PolynomialBuilding polynom = obj as PolynomialBuilding;
            if (polynom == null) return false;
            return Equals(polynom);
        }

        public bool Equals(PolynomialBuilding otherPolynom)
        {
            if (ReferenceEquals(otherPolynom, null))
                return false;
            if (ReferenceEquals(this, otherPolynom))
                return true;
            return (this.polynom.Length == otherPolynom.polynom.Length &&
                   this.polynom.SequenceEqual(otherPolynom.polynom));
        }

        public override int GetHashCode()
        {
            return this.polynom.GetHashCode();
            //return base.GetHashCode();
        }
    }
}
