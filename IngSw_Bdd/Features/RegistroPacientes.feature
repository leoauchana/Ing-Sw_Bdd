Feature: Registrar Pacientes 

    Como enfermera.
    Quiero registrar pacientes.
    Para poder realizar el ingreso a urgencias o buscarlos durante un ingreso en 
    caso de que el paciente aparezca en urgencia más de una vez.

    Background: 
        Given que la siguiente enfermera esta registrada:
            |Nombre|Apellido|
            |Ana   |Gomez   |
        And esta registrando un nuevo paciente

    Scenario: Registro exitoso con todos los datos y obra social exitente
        Given que se completan todos los datos obligatorios del paciente
            |Cuil         |Apellido|Nombre   |Calle   |Numero|Localidad            |Obra Social   |Numero Afiliado|
            |23-00000000-3|Perez   |Pablo    |Av. Roca|100   |San Miguel de Tucuman|Red de Seguros|174853-0       |
        And la obra social "Red de Seguros" exite
        And el paciente esta afiliado a esa obra social
        When confirma el registro del paciente
        Then el paciente queda registrado como nuevo ingreso
        #And se asocia la obra social al registro

    Scenario: Registro exitoso con todos los datos y sin obra social
        Given que se completan todos los datos obligatorios del paciente
            |Cuil         |Apellido|Nombre   |Calle   |Numero|Localidad            |Obra Social   |Numero Afiliado|
            |23-00000000-3|Perez   |Pablo    |Av. Roca|100   |San Miguel de Tucuman|              |               |
        And no se indica obra social
        When confirma el registro del paciente
        Then el paciente queda registrado como nuevo ingreso 
        And sin obra social 

    Scenario: Registro de paciente con obra social inexistente
        Given que se completan todos los datos obligatorios del paciente
            |Cuil         |Apellido|Nombre   |Calle   |Numero|Localidad            |Obra Social   |Numero Afiliado|
            |23-00000000-3|Perez   |Pablo    |Av. Roca|100   |San Miguel de Tucuman|Cromax Seguros|12345          |
        And la obra social "Cromax Seguros" no existe
        When confirma el registro del paciente
        Then se muestra un mensaje de error notificando: "No se puede registrar al paciente, debido a que no exite la obra social ingresada"
    
    Scenario: Registro del paciente con obra social existente pero sin afiliacion
        Given que se completan todos los datos obligatorios del paciente
            |Cuil         |Apellido|Nombre   |Calle    |Numero|Localidad            |Obra Social   |Numero Afiliado|
            |23-00000000-3|Perez   |Pablo    |Av. Roca |100   |San Miguel de Tucuman|Red de Seguros|174853-0       |
        And el paciente no esta afiliado a la obra social "Red de Seguros"
        When confirma el registro del paciente
        Then se muestra un mensaje de error notificando: "No se puede registrar al paciente, debido a que no esta afiliado a la obra social ingresada"

# Escenarios para el ultimo criterio de aceptacion para los distintos datos mandatorios
    Scenario: Registro de paciente con dato mandatorio "Nombre" omitido
        Given que se completan parcialmente los datos obligatorios del paciente
            |Cuil         |Apellido|Nombre |Calle   |Numero|Localidad            |Obra Social   |Numero Afiliado|
            |23-00000000-3|Perez   |       |Av. Roca|100   |San Miguel de Tucuman|Red de Seguros|174853-0       |
        When confirma el registro del paciente
        Then se notifica que falta completar el campo "Nombre"

    Scenario: Registro de paciente con dato mandatorio "Apellido" omitido
        Given que se completan parcialmente los datos obligatorios del paciente
            |Cuil         |Apellido|Nombre |Calle   |Numero|Localidad            |Obra Social   |Numero Afiliado|
            |23-00000000-3|        |Pablo  |Av. Roca|100   |San Miguel de Tucuman|Red de Seguros|174853-0       |
        When confirma el registro del paciente
        Then se notifica que falta completar el campo "Apellido"

    Scenario: Registro de paciente con dato mandatorio "Cuil" omitido
        Given que se completan parcialmente los datos obligatorios del paciente
            |Cuil|Apellido|Nombre |Calle   |Numero|Localidad            |Obra Social   |Numero Afiliado|
            |    |Perez   |Pablo  |Av. Roca|100   |San Miguel de Tucuman|Red de Seguros|174853-0       |
        When confirma el registro del paciente
        Then se notifica que falta completar el campo "Cuil"

    Scenario: Registro de paciente con dato mandatorio  "Calle" omitido
            Given que se completan parcialmente los datos obligatorios del paciente
                |Cuil         |Apellido|Nombre |Calle   |Numero|Localidad            |Obra Social   |Numero Afiliado|
                |23-00000000-3|Perez   |Pablo  |        |100   |San Miguel de Tucuman|Red de Seguros|174853-0       |
            When confirma el registro del paciente
            Then se notifica que falta completar el campo "Calle"
    
    Scenario: Registro de paciente con dato mandatorio  "Numero" omitido
        Given que se completan parcialmente los datos obligatorios del paciente
            |Cuil         |Apellido|Nombre |Calle   |Numero|Localidad            |Obra Social   |Numero Afiliado|
            |23-00000000-3|        |Pablo  |Av. Roca|100   |San Miguel de Tucuman|Red de Seguros|174853-0       |
        When confirma el registro del paciente
        Then se notifica que falta completar el campo "Numero"

    Scenario: Registro de paciente con dato mandatorio  "Localidad" omitido
        Given que se completan parcialmente los datos obligatorios del paciente
            |Cuil         |Apellido|Nombre |Calle   |Numero|Localidad|Obra Social   |Numero Afiliado|
            |23-00000000-3|        |Pablo  |Av. Roca|100   |         |Red de Seguros|174853-0       |
        When confirma el registro del paciente
        Then se notifica que falta completar el campo "Localidad"


    