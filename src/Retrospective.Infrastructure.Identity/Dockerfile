FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Retrospective.Infrastructure.Identity/Retrospective.Infrastructure.Identity.csproj", "src/Retrospective.Infrastructure.Identity/"]
RUN dotnet restore "src/Retrospective.Infrastructure.Identity/Retrospective.Infrastructure.Identity.csproj"
COPY . .
WORKDIR "/src/src/Retrospective.Infrastructure.Identity"
RUN dotnet build "Retrospective.Infrastructure.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Retrospective.Infrastructure.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retrospective.Infrastructure.Identity.dll"]