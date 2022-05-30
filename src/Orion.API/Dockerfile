#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Orion.API/Orion.API.csproj", "src/Orion.API/"]
COPY ["src/Orion.Application/Orion.Application.csproj", "src/Orion.Application/"]
COPY ["src/Orion.Shared/Orion.Shared.csproj", "src/Orion.Shared/"]
COPY ["src/Orion.Core/Orion.Core.csproj", "src/Orion.Core/"]
COPY ["src/Orion.CrossCutting.IoC/Orion.CrossCutting.IoC.csproj", "src/Orion.CrossCutting.IoC/"]
COPY ["src/Orion.Infra.Data/Orion.Infra.Data.csproj", "src/Orion.Infra.Data/"]
RUN dotnet restore "src/Orion.API/Orion.API.csproj"
COPY . .
WORKDIR "/src/src/Orion.API"
RUN dotnet build "Orion.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orion.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orion.API.dll"]