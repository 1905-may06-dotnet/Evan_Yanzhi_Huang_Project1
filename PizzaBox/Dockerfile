FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS PizzaDomain
WORKDIR /app
COPY PizzaBoxDomain/ ../PizzaBoxDomain
RUN dotnet restore ../PizzaBoxDomain/*.csproj --no-dependencies
RUN dotnet build ../PizzaBoxDomain/*.csproj --no-restore -c Release



FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS PizzaData
WORKDIR /app
COPY PizzaBoxData/ ../PizzaBoxData
COPY --from=PizzaDomain ./PizzaBoxDomain/ ../PizzaBoxDomain
RUN dotnet restore ../PizzaBoxData/*.csproj 
RUN dotnet build ../PizzaBoxData/*.csproj --no-restore -c Release

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS PizzaWeb
WORKDIR /app
COPY PizzaBoxWeb/ ../PizzaBoxWeb
COPY --from=PizzaDomain ./PizzaBoxDomain/ ../PizzaBoxDomain
COPY --from=PizzaData ./PizzaBoxData/ ../PizzaBoxData
RUN dotnet restore ../PizzaBoxWeb/*.csproj 
RUN dotnet build ../PizzaBoxWeb/*.csproj --no-restore -c Release
RUN dotnet publish ../PizzaBoxWeb/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS deploy

WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY --from=PizzaWeb PizzaBoxWeb/out ./

CMD [ "dotnet", "PizzaBoxWeb.dll" ]