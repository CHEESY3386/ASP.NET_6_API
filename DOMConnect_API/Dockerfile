#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DOMConnect_API.csproj", "."]
COPY ["../DOMConnect_API.Services/DOMConnect_API.Services.csproj", "../DOMConnect_API.Services/"]
COPY ["../DOMConnect_API.Redis/DOMConnect_API.Redis.csproj", "../DOMConnect_API.Redis/"]
COPY ["../dom-box-common/CSRedis/CSRedis.csproj", "../dom-box-common/CSRedis/"]
COPY ["../dom-box-common/dom-box-logger/dom-box-logger.csproj", "../dom-box-common/dom-box-logger/"]
COPY ["../DOMConnect_API.Utilities/DOMConnect_API.Utilities.csproj", "../DOMConnect_API.Utilities/"]
COPY ["../DOMConnect_API.Database/DOMConnect_API.Database.csproj", "../DOMConnect_API.Database/"]
COPY ["../DOMConnect_API.IO/DOMConnect_API.IO.csproj", "../DOMConnect_API.IO/"]
RUN dotnet restore "./DOMConnect_API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DOMConnect_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DOMConnect_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DOMConnect_API.dll"]