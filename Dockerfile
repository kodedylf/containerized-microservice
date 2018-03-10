FROM node AS frontend-build-env
RUN npm install webpack -g
WORKDIR /app
COPY ./frontend/package.json ./
RUN npm install
COPY ./frontend/ ./
RUN npm run build

FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
COPY ./backend/*.csproj ./
RUN dotnet restore
COPY ./backend/ ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=build-env /app/out ./
COPY --from=frontend-build-env /app/dist/ ./wwwroot
ENTRYPOINT ["dotnet", "postgres-service.dll"]
