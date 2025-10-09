using IngSw_Bdd.Domain.DbTest;
using IngSw_Bdd.Domain.Entities;
using IngSw_Bdd.Services.Interfaces;

namespace IngSw_Bdd.StepDefinitions
{
    [Binding]
    public class ModeloDeUrgenciasStepDefinitions
    {
        private readonly IDBTestInMemory _dbTest;
        private readonly IEmergencyModule _emergencyModule;
        private Nurse? _nurse;
        private Patient? _patient;
        private Exception? _exceptionExpected;
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
            _exceptionExpected = null;
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
            var (frequencySystolic, frequensyDiastolic) = (patientData["Tension Arterial"].Split('/') is var p) ? (double.Parse(p[0]), double.Parse(p[1])) : (0, 0);
            try
            {
                _emergencyModule.RegisterEmergency(cuilPatient, _nurse, temperature, report, level,
                    frequencyCardiac, frequencyRespiratory, frequencySystolic, frequensyDiastolic);
            }
            catch (Exception e)
            {
                _exceptionExpected = e;
            }
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
            Assert.Equal(message, _exceptionExpected!.Message);
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
            Assert.Equal(message, _exceptionExpected!.Message);
        }

        //Scenary 5
        [Given("que la lista de espera actual ordenada por nivel es:")]
        public void GivenQueLaListaDeEsperaActualOrdenadaPorNivelEs(DataTable dataTable)
        {
            var patientsList = dataTable.Rows;
            if (patientsList == null)
                throw new NullReferenceException("No se obtuvieron los datos de la lista de espera actual");
            foreach (var patientRow in patientsList)
            {
                var cuilPatient = patientRow!["Cuil"];
                var temperature = double.Parse(patientRow["Temperatura"]);
                var report = patientRow["Informe"];
                var emergencyLevel = patientRow["Nivel de Emergencia"];
                var frequencyCardiac = double.Parse(patientRow["Frecuencia Cardiaca"]);
                var frequencyRespiratory = double.Parse(patientRow["Frecuencia Respiratoria"]);
                EmergencyLevel? level = Enum.TryParse<EmergencyLevel>(
                    patientRow["Nivel de Emergencia"], true, out var parsedLevel)
                    ? parsedLevel
                    : null;
                var (frequencySystolic, frequensyDiastolic) = (patientRow["Tension Arterial"].Split('/') is var p)
                    ? (double.Parse(p[0]), double.Parse(p[1]))
                    : (0, 0);
                _emergencyModule.RegisterEmergency(cuilPatient, _nurse, temperature, report, level,
                    frequencyCardiac, frequencyRespiratory, frequencySystolic, frequensyDiastolic);
            }
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
