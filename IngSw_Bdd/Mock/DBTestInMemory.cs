using IngSw_Bdd.Domain.DbTest;
using IngSw_Bdd.Domain.Entities;
namespace IngSw_Bdd.Repository;

public class DBTestInMemory : IDBTestInMemory
{
    public List<Patient>? Patients { get; set; } = new List<Patient>();
    public void SavePatient(Patient patient)
    {
        Patients!.Add(patient);
    }
    public List<Patient>? GetPatients()
    {
        return Patients;
    }
}
