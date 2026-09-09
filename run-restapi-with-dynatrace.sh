#!/usr/bin/env bash
# Runs RestApi locally with 'dotnet run' while streaming live OpenTelemetry data to Dynatrace,
# using the same DT_OTLP_ENDPOINT / DT_OTEL_TOKEN values already configured in .env for Docker.
set -euo pipefail
cd "$(dirname "$0")"

if [ ! -f .env ]; then
  echo "Error: .env not found in repo root. Copy .env.example to .env and fill in real values first." >&2
  exit 1
fi

set -a
source .env
set +a

: "${DT_OTLP_ENDPOINT:?DT_OTLP_ENDPOINT is not set in .env}"
: "${DT_OTEL_TOKEN:?DT_OTEL_TOKEN is not set in .env}"

export OTEL_SERVICE_NAME="${OTEL_SERVICE_NAME:-logo-2026-restapi}"
export OTEL_EXPORTER_OTLP_ENDPOINT="$DT_OTLP_ENDPOINT"
export OTEL_EXPORTER_OTLP_PROTOCOL="http/protobuf"
export OTEL_EXPORTER_OTLP_HEADERS="Authorization=Api-Token%20${DT_OTEL_TOKEN}"
export OTEL_METRIC_EXPORT_INTERVAL="5000"
export OTEL_BSP_SCHEDULE_DELAY="5000"

echo "Starting RestApi with live OTLP export to: $OTEL_EXPORTER_OTLP_ENDPOINT"
dotnet run --project RestApi "$@"
