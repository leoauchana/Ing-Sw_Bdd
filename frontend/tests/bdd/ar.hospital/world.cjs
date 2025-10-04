const { setWorldConstructor, Before } = require('@cucumber/cucumber')

class CustomWorld {
  constructor() {
    this.reset()
  }

  reset() {
    this.db = {
      medicos: new Map(),
      ingresos: new Map(),
      atenciones: []
    }
    this.error = undefined
  }
}

setWorldConstructor(CustomWorld)

Before(function () {
  this.reset()
})



