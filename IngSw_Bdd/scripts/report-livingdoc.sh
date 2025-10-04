#!/usr/bin/env bash
set -euo pipefail

# Ubicación del proyecto .NET
cd "$(dirname "$0")/.."

OUT_DIR="Reports"
mkdir -p "$OUT_DIR"

echo "➡️ Ejecutando pruebas (.NET)..."
dotnet test -v minimal

if ! command -v reqnroll >/dev/null 2>&1; then
  echo "❌ Reqnroll CLI no encontrado. Instala con: dotnet tool install --global Reqnroll.Tools"
  exit 1
fi

ASSEMBLY="bin/Debug/net8.0/IngSw_Bdd.dll"

echo "➡️ Generando LivingDoc..."
# Nota: Sin archivo de ejecución de pruebas (-t), se genera documentación de features.
reqnroll livingdoc test-assembly "$ASSEMBLY" -o "$OUT_DIR/LivingDoc.html"

echo "✅ Reporte generado en $OUT_DIR/LivingDoc.html"


