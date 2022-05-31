using System.Runtime.InteropServices;

namespace CodeSignApp.Security
{
    class StrongNameNativeMethods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This is windows API hence cannot change the name.")]
        [DllImport("mscoree.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool StrongNameSignatureVerificationEx(string filePath, [MarshalAs(UnmanagedType.U1)] bool forceVerification, [MarshalAs(UnmanagedType.U1)] ref bool wasVerified);
    }
}
