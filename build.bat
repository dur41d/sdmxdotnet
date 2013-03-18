cd src\Common
msbuild /p:Configuration=Release
cd ..\..\src\OXM
msbuild /p:Configuration=Release
cd ..\..\src\SDMX
msbuild /p:Configuration=Release
cd ..\..

mkdir build
lib\ilmerge  /target:library /internalize /log:output.txt /out:build\SDMX.dll src\SDMX\bin\Release\SDMX.dll src\SDMX\bin\Release\OXM.dll src\SDMX\bin\Release\Common.dll src\SDMX\bin\Release\System.Linq.dll src\SDMX\bin\Release\System.Xml.Linq.dll