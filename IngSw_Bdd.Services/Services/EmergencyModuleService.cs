using IngSw_Bdd.Domain.DbTest;
using IngSw_Bdd.Domain.Entities;
using IngSw_Bdd.Services.Interfaces;

namespace IngSw_Bdd.Services.Services;

public class EmergencyModuleService : IEmergencyModule
{
    private readonly IDBTestInMemory _dBTestInMemory;
    public List<Income>? Incomes { get; set; } = new List<Income>();
    public string Message { get; private set; }
    public EmergencyModuleService(IDBTestInMemory dBTestInMemory)
    {
        _dBTestInMemory = dBTestInMemory;
    }

    public void RegisterEmergency(string patientCuil, Nurse nurse, double temperature, string report, 
        EmergencyLevel? emergencyLevel, double frequencyCardiac, double frequencyRespiratory)
    {
        var patientFound = _dBTestInMemory.GetPatients()!
            .Where(p => p.Cuil == patientCuil)
            .FirstOrDefault();

        if(emergencyLevel == null)
        {
            Message = "Ingresar Nivel de Emergencia";
            return;
        }
        if(frequencyRespiratory < 0)
        {
            Message = "La frecuencia respiratoria no puede ser un valor negativo";
            return;
        }
        if (frequencyCardiac < 0)
        {
            Message = "La frecuencia cardiaca no puede ser un valor negativo";
            return;
        }
        var newIncome = new Income
        {
            Patient= patientFound,
            Nurse = nurse,
            Temperature = temperature,
            Report = report,
            EmergencyLevel = emergencyLevel,
            FrequencyCardiac = frequencyCardiac,
            FrequencyRespiratory = frequencyRespiratory,
            StateIncome = StateIncome.PENDIENTE
        };
        Incomes!.Add(newIncome);
    }

    public void RegisterPatient(string cuil, string lastName, string name, string socialWork) => _dBTestInMemory.SavePatient(new Patient
    {
        Cuil = cuil,
        Apellido = lastName,
        Nombre = name,
        ObraSocial = socialWork
    });
    public List<Income>? GetIncomes() => Incomes;
    public string GetErrorMessage() => Message;
}
