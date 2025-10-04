using System;
using System.Collections.Generic;
using IngSw_Bdd.Domain;
using Xunit;

namespace IngSw_Bdd.Tests
{
    public sealed class AtencionServiceTests
    {
        private static (AtencionService sut, IDictionary<string, Medico> medicos, IDictionary<string, Ingreso> ingresos, IList<Atencion> atenciones)
            CreateSut()
        {
            var medicos = new Dictionary<string, Medico>(StringComparer.OrdinalIgnoreCase);
            var ingresos = new Dictionary<string, Ingreso>(StringComparer.OrdinalIgnoreCase);
            var atenciones = new List<Atencion>();
            var sut = new AtencionService(medicos, ingresos, atenciones);
            return (sut, medicos, ingresos, atenciones);
        }

        [Fact]
        public void RegistrarAtencion_CuandoIngresoReclamadoYInformeValido_FinalizaIngresoYAgregaAtencion()
        {
            var (sut, medicos, ingresos, atenciones) = CreateSut();
            medicos["DR-001"] = new Medico("DR-001", "Pérez", "María");
            ingresos["ING-123"] = new Ingreso("ING-123", EstadoIngreso.Reclamado, "DR-001");

            sut.RegistrarAtencion("ING-123", "Paciente con traumatismo leve en codo", "DR-001");

            Assert.Equal(EstadoIngreso.Finalizado, ingresos["ING-123"].Estado);
            Assert.Single(atenciones);
            Assert.Equal("ING-123", atenciones[0].IngresoId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void RegistrarAtencion_SinInforme_LanzaOmitido(string informe)
        {
            var (sut, medicos, ingresos, _) = CreateSut();
            medicos["DR-001"] = new Medico("DR-001", "Pérez", "María");
            ingresos["ING-123"] = new Ingreso("ING-123", EstadoIngreso.Reclamado, "DR-001");

            var ex = Assert.Throws<InvalidOperationException>(() => sut.RegistrarAtencion("ING-123", informe, "DR-001"));
            Assert.Equal("omitido", ex.Message);
        }

        [Fact]
        public void RegistrarAtencion_IngresoInexistente_Lanza()
        {
            var (sut, _, _, _) = CreateSut();

            var ex = Assert.Throws<InvalidOperationException>(() => sut.RegistrarAtencion("ING-999", "ok", "DR-001"));
            Assert.Equal("Ingreso inexistente", ex.Message);
        }

        [Fact]
        public void RegistrarAtencion_IngresoNoReclamado_Lanza()
        {
            var (sut, medicos, ingresos, _) = CreateSut();
            medicos["DR-001"] = new Medico("DR-001", "Pérez", "María");
            ingresos["ING-123"] = new Ingreso("ING-123", EstadoIngreso.Pendiente, "DR-001");

            var ex = Assert.Throws<InvalidOperationException>(() => sut.RegistrarAtencion("ING-123", "ok", "DR-001"));
            Assert.Equal("Ingreso no reclamado", ex.Message);
        }
    }
}


