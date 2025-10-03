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
            |20-4562556351-4|Tiene gripe|Emergencia         |38         |70                 |15                     |                              
        Then La lista de espera esta ordenada por cuil de la siguiente manera:
            |20-4562556351-4|

    Scenario: Ingreso de un paciente sin registro previo a la lista de espera de urgencias
        Given que se deben registrar un nuevo paciente con los siguientes datos: cuil, apellido, nombre, obra social.
        When ingreso al paciente a urgencias con los siguientes datos:
            |cuil           |apellido|nombre|obra social|
            |20-4562556351-4|Auchana |Leonel|OSDE       |                              
        Then se registra al paciente nuevo 
        And se agrega a la lista de espera de urgencias por cuil de la siguiente manera:
            |20-4562556351-4|

    