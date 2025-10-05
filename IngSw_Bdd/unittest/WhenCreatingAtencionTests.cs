using System;
using System.Collections.Generic;
using IngSw_Bdd.Domain;
using Xunit;

namespace IngSw_Bdd.unittest
{
    public sealed class WhenCreatingAtencionTests
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
        public void Debe_registrar_atencion_y_finalizar_ingreso_cuando_informe_es_mandatorio()
        {
            var (sut, medicos, ingresos, atenciones) = CreateSut();
            medicos["DR-001"] = new Medico("DR-001", "Pérez", "María");
            ingresos["ING-123"] = new Ingreso("ING-123", EstadoIngreso.Reclamado, "DR-001");

            sut.RegistrarAtencion("ING-123", "Paciente con traumatismo leve en codo", "DR-001");

            Assert.Equal(EstadoIngreso.Finalizado, ingresos["ING-123"].Estado);
            Assert.Single(atenciones);
        }

        [Fact]
        public void Debe_mostrar_error_omitido_cuando_informe_vacio()
        {
            var (sut, medicos, ingresos, _) = CreateSut();
            medicos["DR-001"] = new Medico("DR-001", "Pérez", "María");
            ingresos["ING-123"] = new Ingreso("ING-123", EstadoIngreso.Reclamado, "DR-001");

            var ex = Assert.Throws<InvalidOperationException>(() => sut.RegistrarAtencion("ING-123", "", "DR-001"));
            Assert.Equal("omitido", ex.Message);
        }
    }
}


