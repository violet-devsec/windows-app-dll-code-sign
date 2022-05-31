# Script to Authenticode sign all Dlls in a directory

$source = "./"
$fileList = Get-ChildItem $source -Filter *.dll

ForEach($file in $fileList)
{
    Write-Output $file.Name
    # Set-AuthenticodeSignature -Certificate .\CodeCrt.pfx -Filepath C:\Build\extraordinary.dll -TimestampServer http://tsa.starfieldtech.com
    SignTool sign /f .\CodeSigningCert.pfx /p <password_placeholder> $file.Name
}