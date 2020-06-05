@for %%i in (.\package\*.nupkg) do (
dotnet nuget push %%i -k %1 -s https://api.nuget.org/v3/index.json --skip-duplicate
)