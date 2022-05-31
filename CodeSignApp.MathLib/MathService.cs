using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSignApp.MathLib
{
    public class MathService : IMathService
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Substract(int a, int b)
        {
            return a - b;
        }

        public double Divide(int a, int b)
        {
            if (a == 0 || b == 0)
                return 0;
            else
                return a / b;
        }

        public float Multiply(int a, int b)
        {
            return a * b;
        }
    }
}
