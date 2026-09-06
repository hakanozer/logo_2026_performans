# Full codemodules image ref, copy exact value from Dynatrace UI -> Deploy Dynatrace -> Docker.
# Provide with: docker build --build-arg DT_CODEMODULES_IMAGE=<env>.live.dynatrace.com/linux/oneagent-codemodules:dotnet ...
ARG DT_CODEMODULES_IMAGE

# ---- Build & publish the app ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY RestApi.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# ---- Dynatrace OneAgent codemodules (requires prior `docker login` to the registry) ----
FROM ${DT_CODEMODULES_IMAGE} AS dynatrace-codemodules

# ---- Final runtime image ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=dynatrace-codemodules / /
ENV LD_PRELOAD=/opt/dynatrace/oneagent/agent/lib64/liboneagentproc.so

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RestApi.dll"]