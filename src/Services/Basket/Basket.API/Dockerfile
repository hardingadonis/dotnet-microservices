FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
ENV PROTOBUF_PROTOC=/usr/bin/protoc
ENV gRPC_PluginFullPath=/usr/bin/grpc_csharp_plugin
RUN apk add protobuf protobuf-dev grpc grpc-plugins
WORKDIR /src
COPY ["src/Services/Basket/Basket.API/Basket.API.csproj", "src/Services/Basket/Basket.API/"]
COPY ["src/Services/Basket/Basket.Application/Basket.Application.csproj", "src/Services/Basket/Basket.Application/"]
COPY ["src/Services/Basket/Basket.Domain/Basket.Domain.csproj", "src/Services/Basket/Basket.Domain/"]
COPY ["src/Services/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "src/Services/Basket/Basket.Infrastructure/"]
RUN dotnet restore "./src/Services/Basket/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/Basket.API"
RUN dotnet build "./Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]