$source = "./"
$fileList = Get-ChildItem $source -Filter *.dll

ForEach($file in $fileList)
{
    cmd /c 'sn -Ra $file SgnKey.snk'
}