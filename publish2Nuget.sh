dotnet pack src/Luna.Core/Luna.Core.csproj -c Release
dotnet pack src/Luna.Web/Luna.Web.csproj -c Release
dotnet pack src/Luna.Dapper/Luna.Dapper.csproj -c Release

for line in `ls package/*`
do
        dotnet nuget push $line -k $1 -s https://api.nuget.org/v3/index.json --skip-duplicate
done