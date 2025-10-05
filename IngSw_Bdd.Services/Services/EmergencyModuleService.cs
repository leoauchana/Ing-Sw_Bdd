using IngSw_Bdd.Domain.DbTest;
using IngSw_Bdd.Domain.Entities;
using IngSw_Bdd.Services.Interfaces;

namespace IngSw_Bdd.Services.Services;

public class EmergencyModuleService : IEmergencyModule
{
    private readonly IDBTestInMemory _dBTestInMemory;
    public List<Income>? Incomes { get; set; } = new List<Income>();
    public EmergencyModuleService(IDBTestInMemory dBTestInMemory)
    {
        _dBTestInMemory = dBTestInMemory;
    }

    public void RegisterEmergency(string patientCuil,
                                    Nurse nurse,
                                    double temperature,
                                    string report,
                                    EmergencyLevel emergencyLevel,
                                    double frequencyCardiac,
                                    double frequencyRespiratory)
    {
        var patientFound = _dBTestInMemory.GetPatients()!
            .Where(p => p.Cuil == patientCuil)
            .FirstOrDefault();

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

    public List<Income>? GetIncomes() => Incomes;
}
