FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.csproj ./
COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
ENV TZ=America/Buenos_Aires

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","TallerMecanicoDiWork.dll"]

CMD ASPNETCORE_URLS=https://*:$PORT dotnet TallerMecanicoDiWork.dll