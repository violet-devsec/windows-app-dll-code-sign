using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CodeSignApp.Security
{
    public static class StrongNameVerifier
    {
        public static void VerifiyAssemblies()
        {
            try
            {
                // Verifies whether the assembly strong named or not. This validation 
                // ensures only whether assembly has unique identiyy or not. 
                VerifyStrongName();

                // Verifies the signature of the assembly. This validation ensures 
                // both modification(integrity) and valid origin(authenticity).
                VerifyAssemblySignature();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void VerifyStrongName()
        {
            var assemblyCollection = RetrieveRelevantFileList();
            bool notForced = false;

            Parallel.ForEach(assemblyCollection, (assemblyFile) =>
                {
                    if (!StrongNameNativeMethods.StrongNameSignatureVerificationEx(assemblyFile.FullName, false, ref notForced))
                    {
                        throw new SecurityException("Strong name verification failed");
                    }
                });
          }

        private static void VerifyAssemblySignature()
        {
            var assemblyCollection = RetrieveRelevantFileList();

            Parallel.ForEach(assemblyCollection, (assemblyFile) =>
            {
                if (!VerifySignature(assemblyFile))
                {
                    throw new SecurityException("Signature verification failed");
                }
            });
        }

        private static IList<FileInfo> RetrieveRelevantFileList()
        {
            var a = Assembly.GetEntryAssembly();
            var directoryPath = Path.GetDirectoryName(a.Location);
            var directoryInfoInstance = new DirectoryInfo(directoryPath);

            var assemblyCollection = directoryInfoInstance.GetFiles("*.dll").ToList();
            var executableCollection = directoryInfoInstance.GetFiles("*.exe");

            if (executableCollection?.Length > 0)
            {
                assemblyCollection.AddRange(executableCollection);
            }

            return assemblyCollection;
        }

        private static bool VerifySignature(FileInfo assembly)
        {
            var certificate = FetchCertificate();

            if (certificate == null)
            {
                throw new Exception("Certificate not found!");
            }

            var publickey = certificate.GetRSAPublicKey();

            X509Certificate codeSignedCert = X509Certificate.CreateFromSignedFile(assembly.FullName);

            var publickey2 = codeSignedCert.GetPublicKey();

            if(publickey == publickey2)
            {
                return true;
            }

            return false;            
        }

        private static X509Certificate2 FetchCertificate()
        {
            string fileName = "";
            X509Certificate2 cert = new X509Certificate2(fileName);

            if (cert != null && cert.PublicKey != null && cert.Verify())
            {
                return cert;
            }

            return null;
        }
    }
}
