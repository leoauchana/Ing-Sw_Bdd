using IngSw_Bdd.Domain.DbTest;
using IngSw_Bdd.Domain.Entities;
using IngSw_Bdd.Services.Interfaces;


namespace IngSw_Bdd.StepDefinitions;

[Binding]
public class ModeloDeUrgenciasStepDefinitions
{
    private readonly IDBTestInMemory _dbTest;
    private readonly IEmergencyModule _emergencyModule;
    private Nurse? _nurse;

    public ModeloDeUrgenciasStepDefinitions(IDBTestInMemory dbTest, IEmergencyModule emergencyModule)
    {
        _dbTest = dbTest;
        _emergencyModule = emergencyModule;
    }

    // Scenary 1

    [Given("que la siguiente enfermera esta registrada:")]
    public void GivenQueLaSiguienteEnfermeraEstaRegistrada(DataTable dataTable)
    {
        _nurse = dataTable.CreateSet<Nurse>().FirstOrDefault();
    }

    [Given("que estan registrados los siguientes pacientes:")]
    public void GivenQueEstanRegistradosLosSiguientesPacientes(DataTable dataTable)
    {
        var patients = dataTable.CreateSet<Patient>();
        foreach(var patient in patients)
        {
            _dbTest.SavePatient(patient);
        }
    }

    [When("ingreso a urgencias al siguiente paciente:")]
    public void WhenIngresoAUrgenciasAlSiguientePaciente(DataTable dataTable)
    {
        var data = dataTable.Rows.FirstOrDefault();
        var cuilPatient = data!["Cuil"];
        var temperature = double.Parse(data["Temperatura"]);
        var report = data["Informe"];
        var emergencyLevel = data["Nivel de Emergencia"];
        var frequencyCardiac = double.Parse(data["Frecuencia Cardiaca"]);
        var frequencyRespiratory= double.Parse(data["Frecuencia Respiratoria"]);
        EmergencyLevel level = (EmergencyLevel)Enum.Parse(typeof(EmergencyLevel), emergencyLevel, true);

        _emergencyModule.RegisterEmergency(cuilPatient, _nurse, temperature, report, level,
            frequencyCardiac, frequencyRespiratory);
    }

    [Then("La lista de espera esta ordenada por cuil de la siguiente manera:")]
    public void ThenLaListaDeEsperaEstaOrdenadaPorCuilDeLaSiguienteManera(DataTable dataTable)
    {
        var expectedCuil = dataTable.Rows.FirstOrDefault();

        var cuilPendiente = _emergencyModule.GetIncomes()!.FirstOrDefault()!.Patient!.Cuil;

        Assert.Equal(expectedCuil["Cuil"], cuilPendiente);
    }

    // Scenary 2

    [Given("que se deben registrar un nuevo paciente con los siguientes datos: cuil, apellido, nombre, obra social.")]
    public void GivenQueSeDebenRegistrarUnNuevoPacienteConLosSiguientesDatosCuilApellidoNombreObraSocial_()
    {
        throw new PendingStepException();
    }

    [When("ingreso al paciente a urgencias con los siguientes datos:")]
    public void WhenIngresoAlPacienteAUrgenciasConLosSiguientesDatos(DataTable dataTable)
    {
        throw new PendingStepException();
    }

    [Then("se registra al paciente nuevo")]
    public void ThenSeRegistraAlPacienteNuevo()
    {
        throw new PendingStepException();
    }

    [Then("se agrega a la lista de espera de urgencias por cuil de la siguiente manera:")]
    public void ThenSeAgregaALaListaDeEsperaDeUrgenciasPorCuilDeLaSiguienteManera(DataTable dataTable)
    {
        throw new PendingStepException();
    }

    [Then("se informa la falta del dato mandatario {string}")]
    public void ThenSeInformaLaFaltaDelDatoMandatario(string p0)
    {
        throw new PendingStepException();
    }

    [Then("no se agrega a la lista de espera de guardia")]
    public void ThenNoSeAgregaALaListaDeEsperaDeGuardia()
    {
        throw new PendingStepException();
    }

    [Then("se informa que la frecuencia respiratorio se cargo de forma incorrecta {string}")]
    public void ThenSeInformaQueLaFrecuenciaRespiratorioSeCargoDeFormaIncorrecta(string p0)
    {
        throw new PendingStepException();
    }

    [Given("que esta es la lista de espera de guardia actual ordenada por nivel:")]
    public void GivenQueEstaEsLaListaDeEsperaDeGuardiaActualOrdenadaPorNivel(DataTable dataTable)
    {
        throw new PendingStepException();
    }

    [Then("La lista de espera esta ordenada por cuil considerando la prioridad de la siguiente manera:")]
    public void ThenLaListaDeEsperaEstaOrdenadaPorCuilConsiderandoLaPrioridadDeLaSiguienteManera(DataTable dataTable)
    {
        throw new PendingStepException();
    }
}
