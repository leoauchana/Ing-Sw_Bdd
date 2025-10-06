using IngSw_Bdd.Domain.DbTest;
using IngSw_Bdd.Domain.Entities;
using IngSw_Bdd.Services.Interfaces;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Reqnroll;
using System;

namespace IngSw_Bdd.StepDefinitions
{
    [Binding]
    public class ModeloDeUrgenciasStepDefinitions
    {
        private readonly IDBTestInMemory _dbTest;
        private readonly IEmergencyModule _emergencyModule;
        private Nurse? _nurse;
        private Patient? _patient;

        public ModeloDeUrgenciasStepDefinitions(IDBTestInMemory dbTest, IEmergencyModule emergencyModule)
        {
            _dbTest = dbTest;
            _emergencyModule = emergencyModule;
        }

        //Scenary 1
        [Given("que la siguiente enfermera esta registrada:")]
        public void GivenQueLaSiguienteEnfermeraEstaRegistrada(DataTable dataTable)
        {
            _nurse = dataTable.CreateSet<Nurse>().FirstOrDefault();
            if (_nurse == null)
                throw new NullReferenceException("No se obtuvieron los datos de la enferemra registrada");
        }

        [Given("que estan registrados los siguientes pacientes:")]
        public void GivenQueEstanRegistradosLosSiguientesPacientes(DataTable dataTable)
        {
            var patients = dataTable.CreateSet<Patient>();
            if (patients == null)
                throw new NullReferenceException("No se obtuvieron los datos de los pacientes registrados");
            foreach (var patient in patients)
            {
                _dbTest.SavePatient(patient);
            }
        }

        [When("ingreso a urgencias al siguiente paciente:")]
        public void WhenIngresoAUrgenciasAlSiguientePaciente(DataTable dataTable)
        {
            var patientData = dataTable.Rows.FirstOrDefault();
            if (patientData == null)
                throw new NullReferenceException("No se obtuvieron los datos del ingreso del paciente");
            var cuilPatient = patientData!["Cuil"];
            var temperature = double.Parse(patientData["Temperatura"]);
            var report = patientData["Informe"];
            var emergencyLevel = patientData["Nivel de Emergencia"];
            var frequencyCardiac = double.Parse(patientData["Frecuencia Cardiaca"]);
            var frequencyRespiratory = double.Parse(patientData["Frecuencia Respiratoria"]);
            EmergencyLevel? level = Enum.TryParse<EmergencyLevel>(
                patientData["Nivel de Emergencia"], true, out var parsedLevel)
                ? parsedLevel
                : null;
            _emergencyModule.RegisterEmergency(cuilPatient, _nurse, temperature, report, level,
                frequencyCardiac, frequencyRespiratory);
        }

        [Then("La lista de espera esta ordenada por cuil de la siguiente manera:")]
        public void ThenLaListaDeEsperaEstaOrdenadaPorCuilDeLaSiguienteManera(DataTable dataTable)
        {
            var expectedCuil = dataTable.Rows.FirstOrDefault();
            if (expectedCuil == null)
                throw new NullReferenceException("No se obtuvo el cuil esperado en la cola de espera");
            var cuilPendiente = _emergencyModule.GetIncomes()!.FirstOrDefault()!.Patient!.Cuil;

            Assert.Equal(expectedCuil["Cuil"], cuilPendiente);
        }

        // Scenary 2

        [Given("que no existe el paciente registrado")]
        public void GivenQueNoExisteElPacienteRegistrado()
        {
            _patient = null;
        }

        [When("registro al paciente a urgencias con los siguientes datos:")]
        public void WhenRegistroAlPacienteAUrgenciasConLosSiguientesDatos(DataTable dataTable)
        {
            _patient = dataTable.CreateSet<Patient>().FirstOrDefault();
            if (_patient == null)
                throw new NullReferenceException("No se obtuvieron los datos del paciente para su registro");
            _emergencyModule.RegisterPatient(_patient.Cuil, _patient.Apellido, _patient.Nombre, _patient.ObraSocial);
        }

        // Scenary 3

        [Then("se informa la falta del dato mandatario {string}")]
        public void ThenSeInformaLaFaltaDelDatoMandatario(string message)
        {
            Assert.Equal(message, _emergencyModule.GetErrorMessage());
        }

        [Then("La lista de espera no contendrá el cuil:")]
        public void ThenLaListaDeEsperaNoContendraElCuil(DataTable dataTable)
        {
            var expectedCuil = dataTable.Rows.FirstOrDefault();
            if (expectedCuil == null)
                throw new NullReferenceException("No se obtuvo el cuil esperado en la cola de espera");
            var incomesList = _emergencyModule.GetIncomes();
            if (incomesList == null)
                throw new NullReferenceException("La lista de ingresos es nula");
            Assert.False(incomesList.Any(i => i.Patient.Cuil == expectedCuil["Cuil"]));
        }

        // Scenary 4 
        [Then("se informa que la frecuencia respiratorio se cargo de forma incorrecta {string}")]
        public void ThenSeInformaQueLaFrecuenciaRespiratorioSeCargoDeFormaIncorrecta(string message)
        {
            Assert.Equal(message, _emergencyModule.GetErrorMessage());
        }

        //Scenary 5
        [Given("que la lista de espera actual ordenada por nivel es:")]
        public void GivenQueLaListaDeEsperaActualOrdenadaPorNivelEs(DataTable dataTable)
        {
            var patientsList = dataTable.Rows.FirstOrDefault();
            if (patientsList == null)
                throw new NullReferenceException("No se obtuvieron los datos de la lista de espera actual");
            var cuilPatient = patientsList!["Cuil"];
            var temperature = double.Parse(patientsList["Temperatura"]);
            var report = patientsList["Informe"];
            var emergencyLevel = patientsList["Nivel de Emergencia"];
            var frequencyCardiac = double.Parse(patientsList["Frecuencia Cardiaca"]);
            var frequencyRespiratory = double.Parse(patientsList["Frecuencia Respiratoria"]);
            EmergencyLevel? level = Enum.TryParse<EmergencyLevel>(
                patientsList["Nivel de Emergencia"], true, out var parsedLevel)
                ? parsedLevel
                : null;
            _emergencyModule.RegisterEmergency(cuilPatient, _nurse, temperature, report, level,
                frequencyCardiac, frequencyRespiratory);
        }

        // Scenary 6
        [Then("La lista de espera esta ordenada por cuil considerando la prioridad de la siguiente manera:")]
        public void ThenLaListaDeEsperaEstaOrdenadaPorCuilConsiderandoLaPrioridadDeLaSiguienteManera(DataTable dataTable)
        {
            var cuilsListExpected = dataTable.Rows.Select(r => r["Cuil"]).ToList();
            if (cuilsListExpected == null)
                throw new NullReferenceException("No se obtuvo los cuil ordenados en la cola de espera");
            var incomes = _emergencyModule.GetIncomes()!;
            var cuilsEarrings = incomes
                                .OrderBy(i => i.EmergencyLevel)
                                .Select(i => i.Patient!.Cuil)
                                .ToList();
            Assert.Equal(cuilsListExpected, cuilsEarrings!);
        }

    }
}
