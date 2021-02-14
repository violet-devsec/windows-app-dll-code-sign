using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSignApp.Lib
{    
    public class RequestFactory
    {
        private static MathService mService = null;

        public static MathService GetMathServiceHandler()
        {
            mService = new MathService();

            return mService;
        }
    }
}
