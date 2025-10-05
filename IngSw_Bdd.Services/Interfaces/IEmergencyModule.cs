using IngSw_Bdd.Domain.Entities;

namespace IngSw_Bdd.Services.Interfaces;

public interface IEmergencyModule
{
    void RegisterEmergency(string patientCuil,
                                    Nurse nurse,
                                    double temperature,
                                    string report,
                                    EmergencyLevel? emergencyLevel,
                                    double frequencyCardiac,
                                    double frequencyRespiratory);
    void RegisterPatient(string cuil,
                                string lastName, 
                                string name, 
                                string socialWork);
    string GetErrorMessage();
    List<Income>? GetIncomes();
}
