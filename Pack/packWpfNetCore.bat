msbuild ..\WPFControlNetCore.DesignTools\WPFControlNetCore.DesignTools.csproj -p:Configuration=Release 
dotnet pack -p:PackageVersion=%PACKAGEVERSION% ..\WPFControlNetCore\WPFControlNetCore.csproj --configuration=Release --output Packages
