Feature: Modulo de Autenticacion
  Este modulo se enfoca en regitrar al usuario e identificarlo para darle acceso a las
  actuvidades que le corresponden.

  Background:
    Given Existen los siguientes usuarios en el sistema:
      |         email          |          password          |         rol         |
      | usuario@gmail.com      | contrasena                 | Medico              |
      | usuario1@gmail.com     | contrasena1                | Enfermera           |
      | usuario2@gmail.com     | contrasena2                | Medico              |
      | usuario3@gmail.com     | contrasena3                | Enfermera           |

    Scenario: El usuario se registra en el sistema
      Given que el usuario ingreso los siguientes datos:
        |         email          |          password          |         rol         |
        | samuelito@gmail.com    | contrasena123              |                     |
      When la contraseña esta hasheada
      Then el sistema registra el usuario
        |         email          |          password          |         rol         |
        | usuario@gmail.com      | contrasena                 | Medico              |
        | usuario1@gmail.com     | contrasena1                | Enfermera           |
        | usuario2@gmail.com     | contrasena2                | Medico              |
        | usuario3@gmail.com     | contrasena3                | Enfermera           |
        | samuelito@gmail.com    | contrasena123              |                     | 

    Scenario: El usuario se loguea correctamente
      Given que el usuario ingresa los siguientes datos:
        |         email          |          password          |
        | samuelito@gmail.com    | contrasena123              |
      When el usuario se loguea correctamente
      Then el sistema lanza una alerta de logueo exitoso

    Scenario: El usuario o password es invalido
      Given que el usuario ingresa los siguientes datos:
        |         email          |          password          |
        | samuelito@gmail.com    | incorrecta                 |
      When el usuario intenta loguearse
      Then el sistema lanza una alerta de usuario invalido