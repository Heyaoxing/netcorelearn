FROM microsoft/dotnet:latest

WORKDIR /publish

EXPOSE 9521

COPY ./bin/release/netcoreapp2.0/publish /publish 

ENTRYPOINT ["dotnet", "webapi.dll"]
  