
Start SQL Server docker instance:

```docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@assWord" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql mcr.microsoft.com/mssql/server:latest```

Start backend:

`cd .\TestCaseApp\`

`dotnet ef database update`
`dotnet run`


Start frontend:

`cd .\TestCaseAppFrontend\`

`npx vite`