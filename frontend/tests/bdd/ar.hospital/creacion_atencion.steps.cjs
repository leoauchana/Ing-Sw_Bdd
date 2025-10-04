const { Given, When, Then } = require('@cucumber/cucumber')
const assert = require('node:assert')

/** @typedef {{ id: string, apellido: string, nombre: string }} Medico */
/** @typedef {{ ingreso: string, estado: 'reclamado'|'finalizado'|'pendiente', medico: string }} Ingreso */
/** @typedef {{ ingreso: string, informe: string, medico: string }} Atencion */

Given('que existe el siguiente médico registrado:', function (tabla) {
  const rows = tabla.hashes()
  for (const m of rows) {
    this.db.medicos.set(m.id, m)
  }
})

// Parametrizado: médico registrado
Given('el médico {string} con apellido {string} y nombre {string} está registrado', function (id, apellido, nombre) {
  this.db.medicos.set(id, { id, apellido, nombre })
})

Given('que existe el siguiente ingreso reclamado por el médico:', function (tabla) {
  const rows = tabla.hashes()
  for (const r of rows) {
    this.db.ingresos.set(r.ingreso, {
      ingreso: r.ingreso,
      estado: /** @type {Ingreso['estado']} */ (r.estado),
      medico: r.medico
    })
  }
})

// Parametrizado: ingreso con estado para un médico
Given('el ingreso {string} está {string} por el médico {string}', function (ingresoId, estado, medicoId) {
  this.db.ingresos.set(ingresoId, {
    ingreso: ingresoId,
    estado: /** @type {Ingreso['estado']} */ (estado),
    medico: medicoId
  })
})

When('registro la atención con los siguientes datos:', function (tabla) {
  const [row] = tabla.hashes()
  const informe = (row.informe || '').trim()
  const ingreso = this.db.ingresos.get(row.ingreso)
  assert.ok(ingreso, 'Ingreso inexistente')
  assert.strictEqual(ingreso.estado, 'reclamado', 'Ingreso no reclamado')

  if (!informe) {
    this.error = 'omitido'
    return
  }

  this.db.atenciones.push({ ingreso: row.ingreso, informe, medico: row.medico })
  ingreso.estado = 'finalizado'
})

// Parametrizado: registrar atención
When('registro la atención del ingreso {string} con informe {string} por el médico {string}', function (ingresoId, informePlano, medicoId) {
  const informe = (informePlano || '').trim()
  const ingreso = this.db.ingresos.get(ingresoId)
  assert.ok(ingreso, 'Ingreso inexistente')
  assert.strictEqual(ingreso.estado, 'reclamado', 'Ingreso no reclamado')
  if (!informe) { this.error = 'omitido'; return }
  this.db.atenciones.push({ ingreso: ingresoId, informe, medico: medicoId })
  ingreso.estado = 'finalizado'
})

Then('se registra la atención', function () {
  assert.ok(this.db.atenciones.length > 0, 'No se registró la atención')
})

Then('el estado del ingreso queda de la siguiente manera:', function (tabla) {
  const [esperado] = tabla.hashes()
  const ingreso = this.db.ingresos.get(esperado.ingreso)
  assert.ok(ingreso, 'Ingreso no encontrado')
  assert.strictEqual(ingreso.estado, esperado.estado)
})

// Parametrizado: aserción de estado
Then('el ingreso {string} queda en estado {string}', function (ingresoId, estadoEsperado) {
  const ingreso = this.db.ingresos.get(ingresoId)
  assert.ok(ingreso, 'Ingreso no encontrado')
  assert.strictEqual(ingreso.estado, estadoEsperado)
})

Then('veo un mensaje de error indicando que el informe del paciente se ha omitido', function () {
  assert.strictEqual(this.error, 'omitido')
})

// Parametrizado: mensaje de error genérico
Then('veo un error {string}', function (mensajeEsperado) {
  assert.strictEqual(this.error, mensajeEsperado)
})



