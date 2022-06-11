#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/KafkaCDC.Traders/KafkaCDC.Traders.csproj", "src/KafkaCDC.Traders/"]
COPY ["src/KafkaCDC.Common/KafkaCDC.Common.csproj", "src/KafkaCDC.Common/"]
RUN dotnet restore "src/KafkaCDC.Traders/KafkaCDC.Traders.csproj"
COPY . .
WORKDIR "/src/src/KafkaCDC.Traders"
RUN dotnet build "KafkaCDC.Traders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaCDC.Traders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaCDC.Traders.dll"]