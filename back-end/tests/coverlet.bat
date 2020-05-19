dotnet test Agilis.WebAPI.Tests.Integracao\Agilis.WebAPI.Tests.Integracao.csproj
coverlet Agilis.WebAPI.Tests.Integracao\bin\Debug\netcoreapp3.1\Agilis.WebAPI.Tests.Integracao.dll --target "dotnet" --targetargs "test Agilis.WebAPI.Tests.Integracao --no-build" --output "coverlet\Agilis.WebAPI.Tests.Integracao-Coverage.json" --format json

pause