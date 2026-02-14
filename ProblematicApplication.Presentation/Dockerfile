FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProblematicApplication.Presentation/ProblematicApplication.Presentation.csproj", "ProblematicApplication.Presentation/"]
COPY ["ProblematicApplication.Domain/ProblematicApplication.Domain.csproj", "ProblematicApplication.Domain/"]
COPY ["HelpDesk.Contracts/HelpDesk.Contracts.csproj", "HelpDesk.Contracts/"]
COPY ["ProblematicApplication.Application/ProblematicApplication.Application.csproj", "ProblematicApplication.Application/"]
COPY ["ProblematicApplication.Infrastructure/ProblematicApplication.Infrastructure.csproj", "ProblematicApplication.Infrastructure/"]
RUN dotnet restore "ProblematicApplication.Presentation/ProblematicApplication.Presentation.csproj"
COPY . .
WORKDIR "/src/ProblematicApplication.Presentation"
RUN dotnet build "./ProblematicApplication.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProblematicApplication.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProblematicApplication.Presentation.dll"]
