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

        public static bool VerifyStrongName()
        {
            var assemblyCollection = RetrieveRelevantFileList();
            bool notForced = false;

            foreach(FileInfo assemblyFile in assemblyCollection)
            {
                if (!StrongNameNativeMethods.StrongNameSignatureVerificationEx(assemblyFile.FullName, false, ref notForced))
                {
                    throw new SecurityException("Strong name verification failed");
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
    }
}
