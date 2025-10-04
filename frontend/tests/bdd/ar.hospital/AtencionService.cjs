class AtencionService {
  constructor(db) {
    this.db = db || {
      medicos: new Map(),
      ingresos: new Map(),
      atenciones: []
    }
  }

  /**
   * Registra una atención para un ingreso reclamado.
   * Reglas:
   * - Debe existir el ingreso
   * - El ingreso debe estar en estado 'reclamado'
   * - El informe es obligatorio (no vacío)
   * Efectos:
   * - Agrega a this.db.atenciones
   * - Cambia estado del ingreso a 'finalizado'
   *
   * @param {{ ingreso: string, informe: string, medico: string }} datos
   * @returns {void}
   */
  registrarAtencion(datos) {
    const informe = (datos.informe || '').trim()
    const ingreso = this.db.ingresos.get(datos.ingreso)
    if (!ingreso) throw new Error('Ingreso inexistente')
    if (ingreso.estado !== 'reclamado') throw new Error('Ingreso no reclamado')
    if (!informe) throw new Error('omitido')

    this.db.atenciones.push({ ingreso: datos.ingreso, informe, medico: datos.medico })
    ingreso.estado = 'finalizado'
  }
}

module.exports = { AtencionService }



