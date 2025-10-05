using IngSw_Bdd.Domain.Entities;

namespace IngSw_Bdd.Services.Interfaces;

public interface IEmergencyModule
{
    void RegisterEmergency(string patientCuil,
                                    Nurse nurse,
                                    double temperature,
                                    string report,
                                    EmergencyLevel emergencyLevel,
                                    double frequencyCardiac,
                                    double frequencyRespiratory);
    List<Income>? GetIncomes();
}
