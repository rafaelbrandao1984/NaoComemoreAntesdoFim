FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NaoComemoreAntesdoFim.Server/NaoComemoreAntesdoFim.Server.csproj", "NaoComemoreAntesdoFim.Server/"]
RUN dotnet restore "NaoComemoreAntesdoFim.Server/NaoComemoreAntesdoFim.Server.csproj"
COPY NaoComemoreAntesdoFim.Server/ NaoComemoreAntesdoFim.Server/
WORKDIR /src/NaoComemoreAntesdoFim.Server
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "NaoComemoreAntesdoFim.Server.dll"]
