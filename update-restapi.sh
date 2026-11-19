
#!/bin/bash

set -euo pipefail

cd "$(dirname "$0")"

echo "=================================="
echo " REST API Docker Güncellemesi"
echo "=================================="

echo "[1/4] Docker imajı oluşturuluyor..."
docker compose build restapi

echo "[2/4] REST API container yeniden oluşturuluyor..."
docker compose up -d --no-deps --force-recreate restapi

echo "[3/4] Container durumu kontrol ediliyor..."
docker compose ps restapi

echo "[4/4] Son loglar..."
docker compose logs --tail=30 restapi

echo "=================================="
echo " Güncelleme tamamlandı!"
echo "=================================="
