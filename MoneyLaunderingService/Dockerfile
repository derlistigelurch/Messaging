FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MoneyLaunderingService/MoneyLaunderingService.csproj", "MoneyLaunderingService/"]
RUN dotnet restore "MoneyLaunderingService/MoneyLaunderingService.csproj"
COPY . .
WORKDIR "/src/MoneyLaunderingService"
RUN dotnet build "MoneyLaunderingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoneyLaunderingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyLaunderingService.dll"]
