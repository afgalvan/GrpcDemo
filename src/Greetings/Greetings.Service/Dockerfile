﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Greetings/Greetings.Service/Greetings.Service.csproj", "Greetings.Service/"]
RUN dotnet restore "src/Greetings/Greetings.Service/Greetings.Service.csproj"
COPY /src .
WORKDIR "/src/Greetings.Service"
RUN dotnet build "Greetings.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Greetings.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Greetings.Service.dll"]
