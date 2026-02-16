FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Category.Presentation/Category.Presentation.csproj", "Category.Presentation/"]
COPY ["Category.Domain/Category.Domain.csproj", "Category.Domain/"]
COPY ["HelpDesk.Contracts/HelpDesk.Contracts.csproj", "HelpDesk.Contracts/"]
COPY ["Category.Application/Category.Application.csproj", "Category.Application/"]
COPY ["Category.Infrastructure/Category.Infrastructure.csproj", "Category.Infrastructure/"]
RUN dotnet restore "Category.Presentation/Category.Presentation.csproj"
COPY . .
WORKDIR "/src/Category.Presentation"
RUN dotnet build "./Category.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Category.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Category.Presentation.dll"]
