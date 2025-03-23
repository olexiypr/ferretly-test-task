FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ferretly.TestTask.TimeTrackingApi/Ferretly.TestTask.TimeTrackingApi.csproj", "Ferretly.TestTask.TimeTrackingApi/"]
RUN dotnet restore "Ferretly.TestTask.TimeTrackingApi/Ferretly.TestTask.TimeTrackingApi.csproj"
COPY . .
WORKDIR "/src/Ferretly.TestTask.TimeTrackingApi"
RUN dotnet build "Ferretly.TestTask.TimeTrackingApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ferretly.TestTask.TimeTrackingApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ferretly.TestTask.TimeTrackingApi.dll"]
