# BDD en .NET con Reqnroll/xUnit

## Prerrequisitos
- .NET SDK 8.0 instalado

## Ejecutar pruebas
```bash
cd IngSw_Bdd
dotnet restore
dotnet test -v minimal
```

## Estructura
- `Features/CreacionAtencion.feature`: escenarios Gherkin
- `Features/Steps/CreacionAtencionSteps.cs`: step definitions
- `Domain/AtencionService.cs`: lógica de dominio
- `Support/ParameterTransforms.cs`: transformaciones de parámetros

## Reportes
- Reqnroll LivingDoc (requiere CLI):
  ```bash
  dotnet tool install --global Reqnroll.Tools
  ./scripts/report-livingdoc.sh
  ```
- Alternativa con TRX y ReportUnit:
  ```bash
  dotnet tool install -g reportunit
  ./scripts/report-trx-html.sh
  ```


