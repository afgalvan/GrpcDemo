FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Greetings/Greetings.Client/Greetings.Client.csproj", "Greetings.Client/"]
RUN dotnet restore "src/Greetings/Greetings.Client/Greetings.Client.csproj"
COPY /src .
WORKDIR "/src/Greetings.Client"
RUN dotnet build "Greetings.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Greetings.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Greetings.Client.dll"]
