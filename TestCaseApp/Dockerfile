FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestCaseApp/TestCaseApp.csproj", "TestCaseApp/"]
RUN dotnet restore "TestCaseApp/TestCaseApp.csproj"
COPY . .
WORKDIR "/src/TestCaseApp"
RUN dotnet build "TestCaseApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestCaseApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestCaseApp.dll"]
