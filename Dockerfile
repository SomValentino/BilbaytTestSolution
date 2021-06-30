#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Bilbayt.Web.API/Bilbayt.Web.API.csproj", "Bilbayt.Web.API/"]
COPY ["Bilbayt.Domain/Bilbayt.Domain.csproj", "Bilbayt.Domain/"]
COPY ["Bilbayt.Data/Bilbayt.Data.csproj", "Bilbayt.Data/"]
RUN dotnet restore "Bilbayt.Web.API/Bilbayt.Web.API.csproj"
COPY . .
WORKDIR "/src/Bilbayt.Web.API"

FROM build AS publish
RUN dotnet publish "Bilbayt.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bilbayt.Web.API.dll"]