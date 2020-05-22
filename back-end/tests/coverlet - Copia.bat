dotnet test Agilis.Domain.Tests.Unidade\Agilis.Domain.Tests.Unidade.csproj

coverlet Agilis.Domain.Tests.Unidade\bin\Debug\netcoreapp3.1\Agilis.Domain.Tests.Unidade.dll --target "dotnet" --targetargs "test Agilis.Domain.Tests.Unidade --no-build" --output "coverlet\Agilis.Domain.Tests.Unidade-Coverage.json" --format json

pause