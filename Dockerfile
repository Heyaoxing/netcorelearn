FROM microsoft/dotnet:latest
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-bin/Debug/netcoreapp2.0/publish} .
ENTRYPOINT ["dotnet", "webapi.dll"]
