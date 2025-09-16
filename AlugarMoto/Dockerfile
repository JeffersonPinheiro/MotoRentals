# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o csproj primeiro (cache mais eficiente)
COPY ["MotorcycleRentals.csproj", "./"]

# Restaura dependências
RUN dotnet restore "MotorcycleRentals.csproj"

# Copia todo o restante do código
COPY . .

WORKDIR "/src"
RUN dotnet build "MotorcycleRentals.csproj" -c Release -o /app/build

# Etapa de publish
FROM build AS publish
RUN dotnet publish "MotorcycleRentals.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final - runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotorcycleRentals.dll"]