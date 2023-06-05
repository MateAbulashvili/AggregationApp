FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /AggregationApp
EXPOSE 85
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /AggregationApp
COPY ["AggregationApp.csproj", "."]
RUN dotnet restore "AggregationApp.csproj"
COPY . .
RUN dotnet build "AggregationApp.csproj" -c Release -o /AggregationApp/build
FROM build AS publish
RUN dotnet publish "AggregationApp.csproj" -c Release -o /AggregationApp/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /AggregationApp
COPY --from=publish /AggregationApp/publish .
ENTRYPOINT ["dotnet", "AggregationApp.dll"]