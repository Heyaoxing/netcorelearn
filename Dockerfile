FROM docker.io/microsoft/dotnet-samples:latest
ARG source
WORKDIR /app
EXPOSE 9521
COPY . /app
ENTRYPOINT ["dotnet", "webapi.dll"]
   