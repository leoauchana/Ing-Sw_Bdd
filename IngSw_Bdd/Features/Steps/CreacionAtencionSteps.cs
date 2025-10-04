using System;
using System.Collections.Generic;
using IngSw_Bdd.Domain;
using Reqnroll;
using Xunit;

namespace IngSw_Bdd.Features.Steps
{
    [Binding]
    public sealed class CreacionAtencionSteps
    {
        private IDictionary<string, Medico> _medicos;
        private IDictionary<string, Ingreso> _ingresos;
        private IList<Atencion> _atenciones;
        private string? _error;

        [BeforeScenario]
        public void Reset()
        {
            _medicos = new Dictionary<string, Medico>(StringComparer.OrdinalIgnoreCase);
            _ingresos = new Dictionary<string, Ingreso>(StringComparer.OrdinalIgnoreCase);
            _atenciones = new List<Atencion>();
            _error = null;
        }

        [Given("que existe el siguiente médico registrado:")]
        public void DadoExisteMedicoRegistrado(Table tabla)
        {
            foreach (var row in tabla.Rows)
            {
                var id = row["id"];
                var apellido = row["apellido"];
                var nombre = row["nombre"];
                _medicos[id] = new Medico(id, apellido, nombre);
            }
        }

        [Given("que existe el siguiente ingreso reclamado por el médico:")]
        public void DadoExisteIngresoReclamado(Table tabla)
        {
            foreach (var row in tabla.Rows)
            {
                var ingresoId = row["ingreso"];
                var estadoTexto = row["estado"];
                var medicoId = row["medico"];
                _ingresos[ingresoId] = new Ingreso(ingresoId, MapEstado(estadoTexto), medicoId);
            }
        }

        [When("registro la atención con los siguientes datos:")]
        public void CuandoRegistroAtencionConTabla(Table tabla)
        {
            var row = tabla.Rows[0];
            var ingresoId = row["ingreso"];
            var informe = row["informe"];
            var medicoId = row["medico"];
            CuandoRegistroAtencion(ingresoId, informe, medicoId);
        }

        [Then("el estado del ingreso queda de la siguiente manera:")]
        public void EntoncesEstadoDelIngresoQueda(Table tabla)
        {
            var row = tabla.Rows[0];
            var ingresoId = row["ingreso"];
            var estadoEsperado = row["estado"];
            EntoncesIngresoQuedaEnEstado(ingresoId, estadoEsperado);
        }

        [Given("el médico {string} con apellido {string} y nombre {string} está registrado")]
        public void DadoMedicoRegistrado(string id, string apellido, string nombre)
        {
            _medicos[id] = new Medico(id, apellido, nombre);
        }

        [Given("el ingreso {string} está {string} por el médico {string}")]
        public void DadoIngresoConEstadoParaMedico(string ingresoId, string estadoTexto, string medicoId)
        {
            _ingresos[ingresoId] = new Ingreso(ingresoId, MapEstado(estadoTexto), medicoId);
        }

        [When("registro la atención del ingreso {string} con informe {string} por el médico {string}")]
        public void CuandoRegistroAtencion(string ingresoId, string informePlano, string medicoId)
        {
            var service = new AtencionService(_medicos, _ingresos, _atenciones);
            try
            {
                service.RegistrarAtencion(ingresoId, informePlano, medicoId);
            }
            catch (InvalidOperationException ex)
            {
                _error = ex.Message;
            }
        }

        [Then("se registra la atención")]
        public void EntoncesSeRegistraLaAtencion()
        {
            Assert.True(_atenciones.Count > 0, "No se registró la atención");
        }

        [Then("el ingreso {string} queda en estado {string}")]
        public void EntoncesIngresoQuedaEnEstado(string ingresoId, string estadoEsperado)
        {
            Assert.True(_ingresos.TryGetValue(ingresoId, out var ingreso), "Ingreso no encontrado");
            Assert.Equal(MapEstado(estadoEsperado), ingreso!.Estado);
        }

        [Then("veo un error {string}")]
        public void EntoncesVeoUnError(string mensajeEsperado)
        {
            Assert.Equal(mensajeEsperado, _error);
        }

        private static EstadoIngreso MapEstado(string estado)
        {
            switch ((estado ?? string.Empty).Trim().ToLowerInvariant())
            {
                case "reclamado": return EstadoIngreso.Reclamado;
                case "finalizado": return EstadoIngreso.Finalizado;
                case "pendiente": return EstadoIngreso.Pendiente;
                default:
                    throw new ArgumentOutOfRangeException(nameof(estado), estado, "Estado desconocido");
            }
        }
    }
}


