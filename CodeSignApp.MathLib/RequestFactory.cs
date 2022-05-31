using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSignApp.MathLib
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
