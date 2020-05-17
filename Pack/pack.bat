set PACKAGEVERSION=1.2.70
msbuild ..\WPFControlNetCore\WPFControlNetCore.csproj -p:Configuration=Release 
msbuild ..\WPFControlNetCore.DesignTools\WPFControlNetCore.DesignTools.csproj -p:Configuration=Release 
msbuild ..\WPFControlNetCore.ConsoleApp\WPFControlNetCore.ConsoleApp.csproj -p:Configuration=Release 
dotnet pack -p:PackageVersion=%PACKAGEVERSION% ..\WPFControlNetCore\WPFControlNetCore.csproj --configuration=Release --output Packages
