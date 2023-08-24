import { getCurrentInstance } from 'vue'

export function useDate() {
  const instance = getCurrentInstance()
  return instance!.appContext.config.globalProperties.$date
}
