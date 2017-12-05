FROM microsoft/dotnet:latest
WORKDIR /publish
EXPOSE 9521
COPY . /publish 
ENTRYPOINT ["dotnet", "webapi.dll"] 
