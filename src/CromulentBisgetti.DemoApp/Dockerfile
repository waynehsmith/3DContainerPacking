#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
WORKDIR /src
COPY ["CromulentBisgetti.DemoApp/CromulentBisgetti.DemoApp.csproj", "CromulentBisgetti.DemoApp/"]
COPY ["CromulentBisgetti.ContainerPacking/CromulentBisgetti.ContainerPacking.csproj", "CromulentBisgetti.ContainerPacking/"]
RUN dotnet restore "CromulentBisgetti.DemoApp/CromulentBisgetti.DemoApp.csproj"
COPY . .
WORKDIR "/src/CromulentBisgetti.DemoApp"
RUN dotnet build "CromulentBisgetti.DemoApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CromulentBisgetti.DemoApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CromulentBisgetti.DemoApp.dll"]