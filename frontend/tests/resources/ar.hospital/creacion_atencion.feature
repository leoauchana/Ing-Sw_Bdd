# language: en
Feature: Módulo de creación de atención
  Esta feature permite registrar la atención de un paciente reclamado por un médico
  dejando constancia mediante un informe obligatorio y finalizando el ingreso.

  Background:
    Given el médico "DR-001" con apellido "Pérez" y nombre "María" está registrado
    And el ingreso "ING-123" está "reclamado" por el médico "DR-001"

  Scenario: Registrar atención con informe mandatorio
    When registro la atención del ingreso "ING-123" con informe "Paciente con traumatismo leve en codo" por el médico "DR-001"
    Then se registra la atención
    And el ingreso "ING-123" queda en estado "finalizado"

  Scenario: Intento registrar atención sin informe
    When registro la atención del ingreso "ING-123" con informe "" por el médico "DR-001"
    Then veo un error "omitido"



