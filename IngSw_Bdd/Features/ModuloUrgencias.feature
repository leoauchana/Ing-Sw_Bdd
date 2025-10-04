Feature: Modelo de Urgencias
    Esta feature esta relacionada al registro de ingresos de pacientes en la sala 
    de urgencias respetando su nivel de preioridad y el horario de llegada.

    Background: 
        Given que la siguiente enfermera esta registrada:
            |Nombre|Apellido|
            |Ana   |Gomez   |

    Scenario: Ingreso del primer paciente a la lsita de espera de urgencias
        Given que estan registrados los siguientes pacientes:
            |cuil           |apellido|nombre|obra social  |
            |20-4562556352-3|Perez   |Maria |Swiss Medical|
            |20-4562556353-9|Gomez   |Ana   |Galeno       |
        When ingreso a urgencias al siguiente paciente:
            |cuil           |Informe    |Nivel de Emergencia|Temperatura|Frecuencia Cardiaca|Frecuencia Respiratoria|
            |20-4562556353-9|Tiene gripe|Emergencia         |38         |70                 |15                     |                              
        Then La lista de espera esta ordenada por cuil de la siguiente manera:
            |20-4562556353-9|

    Scenario: Ingreso de un paciente sin registro previo a la lista de espera de urgencias
        Given que se deben registrar un nuevo paciente con los siguientes datos: cuil, apellido, nombre, obra social.
        When ingreso al paciente a urgencias con los siguientes datos:
            |cuil           |apellido|nombre|obra social|
            |20-4562556351-4|Auchana |Leonel|OSDE       |                              
        Then se registra al paciente nuevo 
        And se agrega a la lista de espera de urgencias por cuil de la siguiente manera:
            |20-4562556351-4|

    Scenario: Ingreso de un paciente con datos mandatorios faltantes
        Given que estan registrados los siguientes pacientes:
            |cuil           |apellido|nombre|obra social  |
            |20-4562556352-3|Perez   |Maria |Swiss Medical|
            |20-4562556353-9|Gomez   |Ana   |Galeno       |
        When ingreso a urgencias al siguiente paciente:
            |cuil           |Informe    |Nivel de Emergencia|Temperatura|Frecuencia Cardiaca|Frecuencia Respiratoria|
            |20-4562556353-9|Tiene gripe|                   |38         |70                 |15                     |                              
        Then se informa la falta del dato mandatario "Ingresar Nivel de Emergencia"
        And no se agrega a la lista de espera de guardia

     Scenario: Ingreso de un paciente frecuencia respiratoria negativa
        Given que estan registrados los siguientes pacientes:
            |cuil           |apellido|nombre|obra social  |
            |20-4562556352-3|Perez   |Maria |Swiss Medical|
            |20-4562556353-9|Gomez   |Ana   |Galeno       |
        When ingreso a urgencias al siguiente paciente:
            |cuil           |Informe    |Nivel de Emergencia|Temperatura|Frecuencia Cardiaca|Frecuencia Respiratoria|
            |20-4562556353-9|Tiene gripe|Emergencia         |38         |70                 |-15                     |                              
        Then se informa que la frecuencia respiratorio se cargo de forma incorrecta "La frecuencia respiratoria no puede ser un valor negativo"
        And no se agrega a la lista de espera de guardia

    Scenario: Ingreso de un paciente con nivel de emergencia mayor a otro paciente ya en la lista de espera
        Given que estan registrados los siguientes pacientes:
            |cuil           |apellido|nombre|obra social  |
            |20-4562556352-3|Perez   |Maria |Swiss Medical|
            |20-4562556353-9|Gomez   |Ana   |Galeno       |
            |20-4562556351-4|Auchana |Leonel|OSDE         |
        And que esta es la lista de espera de guardia actual ordenada por nivel:
            |Nivel de Emergencia|cuil           |nombre|apellido|
            |Emergencia         |20-4562556353-9|Ana   |Gomez   |
        When ingreso a urgencias al siguiente paciente:
            |cuil           |Informe    |Nivel de Emergencia|Temperatura|Frecuencia Cardiaca|Frecuencia Respiratoria|
            |20-4562556351-4|apuñalada  |Critico            |38         |70                 |15                     |                              
        Then La lista de espera esta ordenada por cuil considerando la prioridad de la siguiente manera:
            |20-4562556351-4|
            |20-4562556353-9|

    Scenario: Ingreso de un paciente con nivel de emergencia menor a otro paciente ya en la lista de espera
        Given que estan registrados los siguientes pacientes:
            |cuil           |apellido|nombre|obra social  |
            |20-4562556352-3|Perez   |Maria |Swiss Medical|
            |20-4562556353-9|Gomez   |Ana   |Galeno       |
            |20-4562556351-4|Auchana |Leonel|OSDE         |
        And que esta es la lista de espera de guardia actual ordenada por nivel:
            |Nivel de Emergencia|cuil           |nombre|apellido|
            |Emergencia         |20-4562556353-9|Ana   |Gomez   |
        When ingreso a urgencias al siguiente paciente:
            |cuil           |Informe          |Nivel de Emergencia     |Temperatura|Frecuencia Cardiaca|Frecuencia Respiratoria|
            |20-4562556351-4|dolor de estomago|urgencia menor          |38         |70                 |15                     |                              
        Then La lista de espera esta ordenada por cuil considerando la prioridad de la siguiente manera:
            |20-4562556353-9|
            |20-4562556351-4|

      Scenario: Ingreso de un paciente con el mismo nivel de emergencia que otro paciente ya en la lista de espera
        Given que estan registrados los siguientes pacientes:
            |cuil           |apellido|nombre|obra social  |
            |20-4562556352-3|Perez   |Maria |Swiss Medical|
            |20-4562556353-9|Gomez   |Ana   |Galeno       |
            |20-4562556351-4|Auchana |Leonel|OSDE         |
        And que esta es la lista de espera de guardia actual ordenada por nivel:
            |Nivel de Emergencia|cuil           |nombre|apellido|
            |Emergencia         |20-4562556353-9|Ana   |Gomez   |
        When ingreso a urgencias al siguiente paciente:
            |cuil           |Informe          |Nivel de Emergencia     |Temperatura|Frecuencia Cardiaca|Frecuencia Respiratoria|
            |20-4562556351-4|caida            |Emergencia              |38         |70                 |15                     |                              
        Then La lista de espera esta ordenada por cuil considerando la prioridad de la siguiente manera:
            |20-4562556353-9|
            |20-4562556351-4|