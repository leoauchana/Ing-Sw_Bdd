using System;
using Reqnroll;

namespace IngSw_Bdd.StepDefinitions
{
    [Binding]
    public class ModuloDeAutenticacionStepDefinitions
    {
        [Given("Existen los siguientes usuarios en el sistema:")]
        public void GivenExistenLosSiguientesUsuariosEnElSistema(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Given("que el usuario ingreso los siguientes datos:")]
        public void GivenQueElUsuarioIngresoLosSiguientesDatos(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [When("la contraseña esta hasheada")]
        public void WhenLaContrasenaEstaHasheada()
        {
            throw new PendingStepException();
        }

        [Then("el sistema registra el usuario")]
        public void ThenElSistemaRegistraElUsuario(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Given("que el usuario ingresa los siguientes datos:")]
        public void GivenQueElUsuarioIngresaLosSiguientesDatos(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [When("el usuario se loguea correctamente")]
        public void WhenElUsuarioSeLogueaCorrectamente()
        {
            throw new PendingStepException();
        }

        [Then("el sistema lanza una alerta de logueo exitoso")]
        public void ThenElSistemaLanzaUnaAlertaDeLogueoExitoso()
        {
            throw new PendingStepException();
        }

        [When("el usuario intenta loguearse")]
        public void WhenElUsuarioIntentaLoguearse()
        {
            throw new PendingStepException();
        }

        [Then("el sistema lanza una alerta de usuario invalido")]
        public void ThenElSistemaLanzaUnaAlertaDeUsuarioInvalido()
        {
            throw new PendingStepException();
        }
    }
}
