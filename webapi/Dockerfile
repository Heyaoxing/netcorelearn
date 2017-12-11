FROM docker.io/microsoft/dotnet:latest
ARG source
WORKDIR /app
EXPOSE 9521
COPY . /app
ENTRYPOINT ["dotnet", "webapi.dll"]
   