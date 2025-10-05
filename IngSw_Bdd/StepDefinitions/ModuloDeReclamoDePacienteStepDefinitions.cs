using System;
using Reqnroll;

namespace IngSw_Bdd.StepDefinitions
{
    [Binding]
    public class ModuloDeReclamoDePacienteStepDefinitions
    {
        [Given("que se encuentra registrado el medico:")]
        public void GivenQueSeEncuentraRegistradoElMedico(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Given("la siguiente lista de espera:")]
        public void GivenLaSiguienteListaDeEspera(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [When("el medico reclama el proximo paciente")]
        public void WhenElMedicoReclamaElProximoPaciente()
        {
            throw new PendingStepException();
        }

        [Then("el paciente sale de la lista:")]
        public void ThenElPacienteSaleDeLaLista(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Then("se envia un mensaje de que la lista de espera esta vacia")]
        public void ThenSeEnviaUnMensajeDeQueLaListaDeEsperaEstaVacia()
        {
            throw new PendingStepException();
        }
    }
}
