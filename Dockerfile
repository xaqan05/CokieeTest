
# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
 
# csproj-u kopyala (build context = repo root, layihə TestLogin/ qovluğundadır)
COPY TestLogin/*.csproj ./TestLogin/
RUN dotnet restore ./TestLogin/TestLogin.csproj
 
# Qalan bütün faylları kopyala və publish et
COPY TestLogin/. ./TestLogin/
RUN dotnet publish ./TestLogin/TestLogin.csproj -c Release -o /app/publish
 
# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
 
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
EXPOSE 8080
 
ENTRYPOINT ["dotnet", "TestLogin.dll"]