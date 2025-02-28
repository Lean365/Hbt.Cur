import type { App } from 'vue'
import HbtDictSelect from './src/index.vue'

export default {
  install(app: App) {
    app.component('hbt-dict-select', HbtDictSelect)
  }
} 