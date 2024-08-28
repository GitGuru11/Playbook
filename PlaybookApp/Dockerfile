# Use the official .NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# The /app directory should act as the main application directory
WORKDIR /app

# Define environment variables
ENV ASPNETCORE_ENVIRONMENT=Development

ENV ASPNETCORE_URLS=http://+:8000

# Copy local directories to the current local directory of our docker image (/app)
COPY ./publish .
#COPY ./public ./public

EXPOSE 8000

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "Playbook.Service.dll"]