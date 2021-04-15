FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Developer.API/Developer.API.csproj", "Services/Developer.API/"]
RUN dotnet restore "Services/Developer.API/Developer.API.csproj"
COPY . .
WORKDIR "/src/Services/Developer.API"
RUN dotnet build "Developer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Developer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Developer.API.dll"]
