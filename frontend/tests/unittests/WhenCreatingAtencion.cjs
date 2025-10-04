const assert = require('node:assert')
const { AtencionService } = require('../bdd/ar.hospital/AtencionService.cjs')

function createDbFixture() {
  return {
    medicos: new Map([
      ['DR-001', { id: 'DR-001', apellido: 'Pérez', nombre: 'María' }]
    ]),
    ingresos: new Map([
      ['ING-123', { ingreso: 'ING-123', estado: 'reclamado', medico: 'DR-001' }]
    ]),
    atenciones: []
  }
}

; (function run() {
  // Caso feliz
  {
    const db = createDbFixture()
    const svc = new AtencionService(db)
    svc.registrarAtencion({ ingreso: 'ING-123', informe: 'OK', medico: 'DR-001' })
    assert.strictEqual(db.atenciones.length, 1)
    assert.strictEqual(db.ingresos.get('ING-123').estado, 'finalizado')
  }

  // Informe omitido
  {
    const db = createDbFixture()
    const svc = new AtencionService(db)
    assert.throws(() => svc.registrarAtencion({ ingreso: 'ING-123', informe: '   ', medico: 'DR-001' }), /omitido/)
  }

  // Ingreso inexistente
  {
    const db = createDbFixture()
    const svc = new AtencionService(db)
    assert.throws(() => svc.registrarAtencion({ ingreso: 'ING-999', informe: 'OK', medico: 'DR-001' }), /Ingreso inexistente/)
  }

  // Ingreso no reclamado
  {
    const db = createDbFixture()
    db.ingresos.set('ING-124', { ingreso: 'ING-124', estado: 'pendiente', medico: 'DR-001' })
    const svc = new AtencionService(db)
    assert.throws(() => svc.registrarAtencion({ ingreso: 'ING-124', informe: 'OK', medico: 'DR-001' }), /Ingreso no reclamado/)
  }

  console.log('Unit tests (AtencionService) OK')
})()


