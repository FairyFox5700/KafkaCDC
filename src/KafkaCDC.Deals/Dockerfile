#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 4040
ENV ASPNETCORE_URLS=http://*:4040

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/KafkaCDC.Deals/KafkaCDC.Deals.csproj", "src/KafkaCDC.Deals/"]
COPY ["src/KafkaCDC.Common/KafkaCDC.Common.csproj", "src/KafkaCDC.Common/"]
RUN dotnet restore "src/KafkaCDC.Deals/KafkaCDC.Deals.csproj"
COPY . .
WORKDIR "/src/src/KafkaCDC.Deals"
RUN dotnet build "KafkaCDC.Deals.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaCDC.Deals.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaCDC.Deals.dll"]