using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSignApp.Security
{
    public class CodeSecurity
    {
        public static bool VerifiyAssemblies()
        {
            try
            {
                bool flag = true;
                // Verifies whether the assembly strong named or not. This validation 
                // ensures only whether assembly has unique identiy or not. 
                flag &= StrongNameVerifier.VerifyStrongName();

                // Verifies the signature of the assembly. This validation ensures 
                // both modification(integrity) and valid origin(authenticity).

                flag &= CodeSignVerifier.VerifyAuthenticodeSignature(); // Uses P/Invoke WinTrust method to check signature [Recommended mechanism]
                //flag &= CodeSignVerifier.VerifyAssemblySignature(); // Uses manual cert validation methods to check signature [Weak mechanism]

                return flag;                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
                //return true; // added for testing, disable this line and re-enable above line after testing
            }
        }
    }
}
