# Etapa 1: Construcción (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ["MexNature.Api.csproj", "./"]
RUN dotnet restore "MexNature.Api.csproj"

# Copiar el resto del código y compilar
COPY . .
RUN dotnet publish "MexNature.Api.csproj" -c Release -o /app/publish

# Etapa 2: Ejecución (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MexNature.Api.dll"]