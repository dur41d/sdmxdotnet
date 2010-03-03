cd src\Common
msbuild
cd ..\..\src\OXM
msbuild
cd ..\..\src\SDMX
msbuild
cd ..\..

mkdir build
lib\ilmerge  /target:library /internalize /log:output.txt /out:build\SDMX.dll src\SDMX\bin\Debug\SDMX.dll src\SDMX\bin\Debug\OXM.dll src\SDMX\bin\Debug\Common.dll