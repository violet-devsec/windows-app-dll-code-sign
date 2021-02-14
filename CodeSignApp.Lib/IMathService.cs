using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeSignApp.Lib
{
    public interface IMathService
    {
        int Add(int a, int b);
        int Substract(int a, int b);
        double Divide(int a, int b);
        float Multiply(int a, int b);

    }
}
