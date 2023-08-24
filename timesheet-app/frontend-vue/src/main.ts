import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import dayjs from 'dayjs'
import 'dayjs/locale/nl-be'
import isToday from 'dayjs/plugin/isToday'

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $date(date?: dayjs.ConfigType, option?: dayjs.OptionType, locale?: string): dayjs.Dayjs
  }
}

dayjs.locale('nl-be')
dayjs.extend(isToday)

const app = createApp(App)

app.provide('date', dayjs)
app.config.globalProperties.$date = dayjs

app.use(router).mount('#app')
