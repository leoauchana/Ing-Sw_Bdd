# Cucumber (Gherkin) - Suite BDD

Este `frontend` ahora está enfocado exclusivamente en pruebas BDD con Cucumber usando Gherkin.

## Requisitos
- Node.js 18+
- npm

## Instalación
```bash
npm install
```

## Ejecutar BDD y generar reporte HTML
```bash
npm run bdd
```
- Los features están en `tests/resources/`.
- Los step definitions están en `tests/bdd/`.
- El reporte HTML se genera en `reports/cucumber-report.html`.
- La configuración de Cucumber está en `cucumber.mjs`.

## Estructura relevante
- `tests/resources/**/*.feature`: escenarios Gherkin
- `tests/bdd/**/*.cjs`: steps y world
- `scripts/generate-cucumber-report.cjs`: generador de reporte HTML desde `report.ndjson`

## Notas
- No hay UI ni bundler; React/Vite fueron removidos.
- Para ejecutar únicamente las pruebas BDD: `npm run bdd`.

