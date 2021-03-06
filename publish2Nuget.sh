dotnet pack src/Luna.Core/Luna.Core.csproj -c Release
dotnet pack src/Luna.Web/Luna.Web.csproj -c Release
dotnet pack src/Luna.Dapper/Luna.Dapper.csproj -c Release
dotnet pack src/Luna.MongoDb/Luna.MongoDb.csproj -c Release
dotnet pack src/Luna.Redis.AspNetCore/Luna.Redis.AspNetCore.csproj -c Release
dotnet pack src/Luna.Nlog.AspNetCore/Luna.Nlog.AspNetCore.csproj -c Release

for line in `ls package/*`
do
        dotnet nuget push $line -k $1 -s https://api.nuget.org/v3/index.json --skip-duplicate
done