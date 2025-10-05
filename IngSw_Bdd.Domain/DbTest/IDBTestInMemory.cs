using IngSw_Bdd.Domain.Entities;

namespace IngSw_Bdd.Domain.DbTest;

public interface IDBTestInMemory
{
    void SavePatient(Patient patient);
    List<Patient>? GetPatients();
}
