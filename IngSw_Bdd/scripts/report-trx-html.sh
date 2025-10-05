#!/usr/bin/env bash
set -euo pipefail

cd "$(dirname "$0")/.."

OUT_DIR="Reports"
mkdir -p "$OUT_DIR"

echo "➡️ Ejecutando pruebas y generando TRX..."
dotnet test --logger "trx;LogFileName=test_results.trx" -v minimal

if ! command -v reportunit >/dev/null 2>&1; then
  echo "❌ ReportUnit no encontrado. Instala con: dotnet tool install -g reportunit"
  exit 1
fi

echo "➡️ Generando HTML con ReportUnit..."
reportunit "TestResults/*.trx" "$OUT_DIR"

echo "✅ Reporte(s) HTML en $OUT_DIR"


