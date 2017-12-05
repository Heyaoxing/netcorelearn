FROM microsoft/dotnet:latest
ARG source
WORKDIR /app
EXPOSE 9521
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "webapi.dll"]
