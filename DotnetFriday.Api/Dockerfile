FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotnetFriday.Api.csproj", "/"]
COPY ["../DotnetFriday.Domain/DotnetFriday.Domain.csproj", "../DotnetFriday.Domain/"]
RUN dotnet restore "DotnetFriday.Api/DotnetFriday.Api.csproj"
COPY . .
WORKDIR "/src/DotnetFriday.Api"
RUN dotnet build "DotnetFriday.Api.csproj" -c Release -o /app/build
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID

FROM build AS publish
RUN dotnet publish "DotnetFriday.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
ARG RESOURCE_REAPER_SESSION_ID="00000000-0000-0000-0000-000000000000"
LABEL "org.testcontainers.resource-reaper-session"=$RESOURCE_REAPER_SESSION_ID

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetFriday.Api.dll"]
