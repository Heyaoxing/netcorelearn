FROM microsoft/dotnet:latest
ARG source
WORKDIR /app
EXPOSE 9521
COPY ${source:bin/release/netcoreapp2.0/publish}  .
ENTRYPOINT ["dotnet", "webapi.dll"]
  