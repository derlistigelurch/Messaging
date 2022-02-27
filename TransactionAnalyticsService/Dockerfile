FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TransactionAnalyticsService/TransactionAnalyticsService.csproj", "TransactionAnalyticsService/"]
RUN dotnet restore "TransactionAnalyticsService/TransactionAnalyticsService.csproj"
COPY . .
WORKDIR "/src/TransactionAnalyticsService"
RUN dotnet build "TransactionAnalyticsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionAnalyticsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionAnalyticsService.dll"]
