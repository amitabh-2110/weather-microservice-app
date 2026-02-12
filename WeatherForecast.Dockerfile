#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeatherForcastApp.Web.csproj", "WeatherForcastApp.Web/"]
RUN dotnet restore "WeatherForcastApp.Web/WeatherForcastApp.Web.csproj"
COPY . "/src/WeatherForcastApp.Web"
WORKDIR "/src/WeatherForcastApp.Web"
RUN dotnet build "WeatherForcastApp.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherForcastApp.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherForcastApp.Web.dll"]