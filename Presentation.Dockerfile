# STAGE 1 — build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копируем csproj для кеша
COPY src/LegacyLego.Domain/LegacyLego.Domain.csproj LegacyLego.Domain/
COPY src/LegacyLego.Application/LegacyLego.Application.csproj LegacyLego.Application/
COPY src/LegacyLego.Infrastructure/LegacyLego.Infrastructure.csproj LegacyLego.Infrastructure/
COPY src/LegacyLego.Presentation/LegacyLego.Presentation.csproj LegacyLego.Presentation/

# Restore внешних зависимостей
RUN dotnet restore LegacyLego.Presentation/LegacyLego.Presentation.csproj

# Копируем весь код
COPY src/LegacyLego.Domain/ LegacyLego.Domain/
COPY src/LegacyLego.Application/ LegacyLego.Application/
COPY src/LegacyLego.Infrastructure/ LegacyLego.Infrastructure/
COPY src/LegacyLego.Presentation/ LegacyLego.Presentation/

# Устанавливаем инструмент EF Core и собираем бандл миграций
RUN dotnet tool install --global dotnet-ef --version 10.0.*
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef migrations bundle \
    --project LegacyLego.Infrastructure/LegacyLego.Infrastructure.csproj \
    --startup-project LegacyLego.Presentation/LegacyLego.Presentation.csproj \
    -o /app/efbundle

# Publish
WORKDIR /src/LegacyLego.Presentation
RUN dotnet publish -c Release -o /app/publish --no-restore -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS migrator
WORKDIR /app

# Копируем бандл миграций
COPY --from=build /app/efbundle .

# Точкой входа является сам бандл
ENTRYPOINT ["./efbundle"]

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Копируем опубликованный API
COPY --from=build /app/publish .

# ASP.NET
# aspnet:10.0 делает ASPNETCORE_URLS=http://+:8080 автоматически, но пусть будет
ENV ASPNETCORE_URLS=http://+:8080 
EXPOSE 8080

ENTRYPOINT ["dotnet", "LegacyLego.Presentation.dll"]