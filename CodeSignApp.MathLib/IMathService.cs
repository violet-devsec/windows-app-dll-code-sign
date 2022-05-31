using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSignApp.MathLib
{
    public interface IMathService
    {
        int Add(int a, int b);
        int Substract(int a, int b);
        double Divide(int a, int b);
        float Multiply(int a, int b);
    }
}
