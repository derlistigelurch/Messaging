FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoreBankingService/CoreBankingService.csproj", "CoreBankingService/"]
RUN dotnet restore "CoreBankingService/CoreBankingService.csproj"
COPY . .
WORKDIR "/src/CoreBankingService"
RUN dotnet build "CoreBankingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreBankingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreBankingService.dll"]
