#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ApiCamisa10.csproj", ""]
RUN dotnet restore "./ApiCamisa10.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ApiCamisa10.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiCamisa10.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ApiCamisa10.dll"]

CMD ASPNETCORE_URLS = http://*:$PORT dotnet ApiCamisa10.dll
