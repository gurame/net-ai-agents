build:
	dotnet build

build-app:
	dotnet build src/InvoiceApp/InvoiceApp.csproj

run-app:
	dotnet run --project src/InvoiceApp/InvoiceApp.csproj --launch-profile "https"

build-agent-api:
	dotnet build src/InvoiceAgentApi/InvoiceAgentApi.csproj

run-agent-api:
	dotnet run --project src/InvoiceAgentApi/InvoiceAgentApi.csproj --launch-profile "https"