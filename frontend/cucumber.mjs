export default {
  default: {
    require: [
      'tests/bdd/**/*.cjs'
    ],
    format: [
      'progress',
      'message:reports/report.ndjson'
    ],
    paths: ['tests/resources/**/*.feature'],
    publishQuiet: true,
    parallel: 2
  }
}


