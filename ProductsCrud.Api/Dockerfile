FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR .
COPY . .
WORKDIR "ProductsCrud.Api"
RUN dotnet build "ProductsCrud.Api.csproj" -c Release -o /app/build

# Publish the app to a folder
FROM build AS publish
RUN dotnet publish "ProductsCrud.Api.csproj" -c Release -o /app/publish

# Use the official .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Set the working directory in the final image
WORKDIR /app

# Copy the published output from the previous stage
COPY --from=publish /app/publish .

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ProductsCrud.Api.dll"]
