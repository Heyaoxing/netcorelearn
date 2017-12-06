FROM microsoft/dotnet:latest
ARG source
WORKDIR /app
EXPOSE 9521
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop';"]
ENTRYPOINT ["dotnet", "webapi.dll"]
  