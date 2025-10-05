using IngSw_Bdd.Domain.DbTest;
using IngSw_Bdd.Repository;
using IngSw_Bdd.Services.Interfaces;
using IngSw_Bdd.Services.Services;
using Reqnroll.BoDi;

namespace IngSw_Bdd.Support;

[Binding]
public class IngSwDependencies
{

    private readonly IObjectContainer _container;

    public IngSwDependencies(IObjectContainer container)
    {
        _container = container;
    }

    [BeforeScenario]
    public void RegisterServices()
    {
        _container.RegisterTypeAs<DBTestInMemory, IDBTestInMemory>();
        _container.RegisterTypeAs<EmergencyModuleService, IEmergencyModule>();
    }
}
