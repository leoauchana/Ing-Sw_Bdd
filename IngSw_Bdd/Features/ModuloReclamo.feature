  Feature: Modulo de Reclamo de Paciente
    Esta caracteristica esta relacionada con que el medico pueda visualizar los pacientes en
    la lista de ingresos que debe atender, poder seleccionar uno de la lista para atenderlo y
    registrar un informe de atencion.

  Background:
    Given que se encuentra registrado el medico:
    |     Nombre      |     Apellido      |
    | Enrique         | Olmos             |


    Scenario: El medico reclama un paciente de la lista
      Given la siguiente lista de espera:
        |      CUIL       |       Enfermera      |           Informe           |  Nivel de Emergencia   |
        | 20-12345679-8   | Maria Luz, Del Valle | Tiene dengue                | Urgencia               |
        | 20-12345678-8   | Maria Luz, Del Valle | Tiene covid                 | Urgencia               |
        | 20-12345677-8   | Maria Luz, Del Valle | Tiene diarrea               | Urgencia               |
        | 20-12345676-8   | Maria Luz, Del Valle | Tiene vomitos               | Urgencia               |
      When el medico reclama el proximo paciente
      Then el paciente sale de la lista:
        |      CUIL       |      Enfermera       |           Informe           |  Nivel de Emergencia   |
        | 20-12345678-8   | Maria Luz, Del Valle | Tiene covid                 | Urgencia               |
        | 20-12345677-8   | Maria Luz, Del Valle | Tiene diarrea               | Urgencia               |
        | 20-12345676-8   | Maria Luz, Del Valle | Tiene vomitos               | Urgencia               |

    Scenario: El medico reclama un paciente y no hay ninguno en espera
      Given la siguiente lista de espera:
          |      CUIL       |       Enfermera      |           Informe           |  Nivel de Emergencia   |
      When el medico reclama el proximo paciente
      Then se envia un mensaje de que la lista de espera esta vacia