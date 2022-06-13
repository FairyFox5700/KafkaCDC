#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/KafkaCDC.Notifications/KafkaCDC.Notifications.csproj", "src/KafkaCDC.Notifications/"]
COPY ["src/KafkaCDC.Common/KafkaCDC.Common.csproj", "src/KafkaCDC.Common/"]
RUN dotnet restore "src/KafkaCDC.Notifications/KafkaCDC.Notifications.csproj"
COPY . .
WORKDIR "/src/src/KafkaCDC.Notifications"
RUN dotnet build "KafkaCDC.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaCDC.Notifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaCDC.Notifications.dll"]