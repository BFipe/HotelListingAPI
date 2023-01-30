#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HotelListingAPI-MC/HotelListingAPI-MC.csproj", "HotelListingAPI-MC/"]
RUN dotnet restore "HotelListingAPI-MC/HotelListingAPI-MC.csproj"
COPY . .
WORKDIR "/src/HotelListingAPI-MC"
RUN dotnet build "HotelListingAPI-MC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelListingAPI-MC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelListingAPI-MC.dll"]