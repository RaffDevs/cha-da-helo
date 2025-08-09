# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 8443
# Se quiser forçar só HTTP:
# ENV ASPNETCORE_URLS=http://+:8080
RUN mkdir -p /app/data
# USER app  # já é o default no aspnet:8.0

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia csproj para cache do restore
COPY ["ChaHelo.Webapp/ChaHelo.Webapp.csproj", "ChaHelo.Webapp/"]
COPY ["ChaHelo.Application/ChaHelo.Application.csproj", "ChaHelo.Application/"]
COPY ["ChaHelo.Infra/ChaHelo.Infra.csproj", "ChaHelo.Infra/"]
COPY ["ChaHelo.Domain/ChaHelo.Domain.csproj", "ChaHelo.Domain/"]
RUN dotnet restore "ChaHelo.Webapp/ChaHelo.Webapp.csproj"

# Copia package.json e package-lock.json para cache do npm install
COPY ChaHelo.Webapp/package*.json ChaHelo.Webapp/

# Instala Node.js no estágio build (para gerar CSS)
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g npm@latest

WORKDIR /src/ChaHelo.Webapp
RUN npm i  # instala dependências front-end

# Copia o restante do código
WORKDIR /src
COPY . .

# Build do .NET
WORKDIR /src/ChaHelo.Webapp
RUN dotnet build "ChaHelo.Webapp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Build do Tailwind/Vite
RUN npm run build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChaHelo.Webapp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChaHelo.Webapp.dll"]
