using System;
using CodeSignApp.MathLib;
using CodeSignApp.Security;

namespace CodeSignApp.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!CodeSecurity.VerifiyAssemblies())
                {
                    System.Console.WriteLine("Security validation for DLLs failed!");
                    System.Console.ReadLine();
                }
                else
                {
                    System.Console.WriteLine("Enter first number:");
                    int a = Convert.ToInt32(System.Console.ReadLine());

                    System.Console.WriteLine("Enter second number:");
                    int b = Convert.ToInt32(System.Console.ReadLine());

                    MathOp mathOp = new MathOp();

                    int c = mathOp.Add(a, b);

                    System.Console.WriteLine("Sum: " + c);

                    System.Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                System.Console.ReadLine();
            }
        }
    }
    class MathOp
    {
        private IMathService _mathHandler;
        public int Add(int a, int b)
        {
            try
            {
                _mathHandler = RequestFactory.GetMathServiceHandler();

                return _mathHandler.Add(a, b);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return 0;
            }
        }
    }
}
