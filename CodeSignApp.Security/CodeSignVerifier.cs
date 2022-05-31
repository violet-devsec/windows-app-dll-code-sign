using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CodeSignApp.Security
{
    class CodeSignVerifier
    {
        public static bool VerifyAssemblySignature()
        {
            var assemblyCollection = RetrieveRelevantFileList();

            Parallel.ForEach(assemblyCollection, (assemblyFile) =>
            {
                if (!VerifySignature(assemblyFile))
                {
                    throw new SecurityException("Signature verification failed");
                }
            });

            return true;
        }

        public static bool VerifyAuthenticodeSignature()
        {
            var assemblyCollection = RetrieveRelevantFileList();

            foreach(FileInfo assemblyFile in assemblyCollection)
            {
                if (!AuthenticodeVerifier.VerifySignature(assemblyFile.FullName))
                {
                    throw new SecurityException("Signature verification failed");
                }
            }

            return true;
        }

        private static IList<FileInfo> RetrieveRelevantFileList()
        {
            var a = Assembly.GetEntryAssembly();
            var directoryPath = Path.GetDirectoryName(a.Location);
            var directoryInfoInstance = new DirectoryInfo(directoryPath);

            var assemblyCollection = directoryInfoInstance.GetFiles("*.dll").ToList();
            //var executableCollection = directoryInfoInstance.GetFiles("*.exe");

            //if (executableCollection?.Length > 0)
            //{
            //    assemblyCollection.AddRange(executableCollection);
            //}

            return assemblyCollection;
        }

        private static bool VerifySignature(FileInfo assembly)
        {
            //X509Certificate2 cert = new X509Certificate2("CodeSignApp.MathLib.dll");

            X509Certificate cert = X509Certificate2.CreateFromSignedFile("CodeSignApp.MathLib.dll");

            if (cert.Issuer.ToString().Contains("Microsoft"))
            {
                return true;
            }

            return false;
        }

        private static X509Certificate2 FetchCertificate()
        {
            string fileName = "CodeSigningCert.pfx";
            X509Certificate2 cert = new X509Certificate2(fileName, "Passw0rd");

            if (cert != null && cert.PublicKey != null && cert.Verify())
            {
                return cert;
            }

            return null;
        }
    }
}
