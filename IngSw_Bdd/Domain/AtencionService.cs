using System;
using System.Collections.Generic;

namespace IngSw_Bdd.Domain
{
    public sealed class Medico
    {
        public string Id { get; }
        public string Apellido { get; }
        public string Nombre { get; }

        public Medico(string id, string apellido, string nombre)
        {
            Id = id;
            Apellido = apellido;
            Nombre = nombre;
        }
    }

    public enum EstadoIngreso
    {
        Pendiente,
        Reclamado,
        Finalizado
    }

    public sealed class Ingreso
    {
        public string Id { get; }
        public EstadoIngreso Estado { get; set; }
        public string MedicoId { get; }

        public Ingreso(string id, EstadoIngreso estado, string medicoId)
        {
            Id = id;
            Estado = estado;
            MedicoId = medicoId;
        }
    }

    public sealed class Atencion
    {
        public string IngresoId { get; }
        public string Informe { get; }
        public string MedicoId { get; }

        public Atencion(string ingresoId, string informe, string medicoId)
        {
            IngresoId = ingresoId;
            Informe = informe;
            MedicoId = medicoId;
        }
    }

    public sealed class AtencionService
    {
        private readonly IDictionary<string, Medico> _medicos;
        private readonly IDictionary<string, Ingreso> _ingresos;
        private readonly IList<Atencion> _atenciones;

        public AtencionService(
            IDictionary<string, Medico> medicos,
            IDictionary<string, Ingreso> ingresos,
            IList<Atencion> atenciones)
        {
            _medicos = medicos;
            _ingresos = ingresos;
            _atenciones = atenciones;
        }

        public void RegistrarAtencion(string ingresoId, string informePlano, string medicoId)
        {
            var informe = (informePlano ?? string.Empty).Trim();

            if (!_ingresos.TryGetValue(ingresoId, out var ingreso))
            {
                throw new InvalidOperationException("Ingreso inexistente");
            }

            if (ingreso.Estado != EstadoIngreso.Reclamado)
            {
                throw new InvalidOperationException("Ingreso no reclamado");
            }

            if (string.IsNullOrWhiteSpace(informe))
            {
                throw new InvalidOperationException("omitido");
            }

            _atenciones.Add(new Atencion(ingresoId, informe, medicoId));
            ingreso.Estado = EstadoIngreso.Finalizado;
        }
    }
}


