# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY SICP/SICP.csproj SICP/
RUN dotnet restore SICP/SICP.csproj

COPY SICP/ SICP/
RUN dotnet publish SICP/SICP.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "SICP.dll"]
