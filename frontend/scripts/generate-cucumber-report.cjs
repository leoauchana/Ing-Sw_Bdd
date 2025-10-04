#!/usr/bin/env node
// Genera reports/cucumber-report.html a partir de reports/report.ndjson
const fs = require('node:fs')
const path = require('node:path')
const { pipeline } = require('node:stream')
const { promisify } = require('node:util')

const pump = promisify(pipeline)

async function main() {
  const inputPath = process.env.CUCUMBER_NDJSON || path.resolve(__dirname, '../reports/report.ndjson')
  const outputPath = process.env.CUCUMBER_HTML || path.resolve(__dirname, '../reports/cucumber-report.html')

  // Cargamos dinámicamente para evitar exigirlos si no se usa el script
  const { NdjsonToMessageStream } = require('@cucumber/message-streams')
  const { CucumberHtmlStream } = require('@cucumber/html-formatter')

  await fs.promises.mkdir(path.dirname(outputPath), { recursive: true })

  const ndjsonStream = fs.createReadStream(inputPath, 'utf-8')
  const toMessage = new NdjsonToMessageStream()
  const htmlStream = new CucumberHtmlStream()
  const out = fs.createWriteStream(outputPath)

  await pump(ndjsonStream, toMessage, htmlStream, out)
  console.log(`HTML report generado en: ${outputPath}`)
}

main().catch((err) => {
  console.error('Error generando reporte HTML:', err)
  process.exitCode = 1
})


