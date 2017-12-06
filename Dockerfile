FROM microsoft/dotnet:latest
ARG source
WORKDIR /app
EXPOSE 9521
ENTRYPOINT ["dotnet", "webapi.dll"]
  