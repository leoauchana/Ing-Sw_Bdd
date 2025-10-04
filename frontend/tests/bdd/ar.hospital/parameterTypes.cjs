const { defineParameterType } = require('@cucumber/cucumber')

defineParameterType({
  name: 'entero',
  regexp: /-?\d+/,
  transformer: (s) => parseInt(s, 10)
})

defineParameterType({
  name: 'decimal',
  regexp: /-?\d+(?:[\.,]\d+)?/,
  transformer: (s) => parseFloat(s.replace(',', '.'))
})



