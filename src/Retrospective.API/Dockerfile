#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Retrospective.API/Retrospective.API.csproj", "src/Retrospective.API/"]
RUN dotnet restore "src/Retrospective.API/Retrospective.API.csproj"
COPY . .
WORKDIR "/src/src/Retrospective.API"
RUN dotnet build "Retrospective.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Retrospective.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retrospective.API.dll"]