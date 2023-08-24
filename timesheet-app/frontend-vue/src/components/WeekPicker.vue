<script setup lang="ts">
import { ChevronLeftIcon, ChevronRightIcon } from '@heroicons/vue/24/outline'
import { computed, ref } from 'vue'
import { useDate } from '@/composables/useDate'

export interface Props {
  initialDate?: Date
}

const props = withDefaults(defineProps<Props>(), {
  initialDate: () => new Date()
})

const emit = defineEmits<{
  week: [value: Date]
}>()

const currentDay = ref(props.initialDate)

const date = useDate()

const week = computed(() => {
  return {
    start: date(currentDay.value).startOf('week').format('DD/MM'),
    end: date(currentDay.value).endOf('week').format('DD/MM')
  }
})

function nextWeek() {
  currentDay.value = date(currentDay.value).add(1, 'week').toDate()
  emit('week', currentDay.value)
}

function previousWeek() {
  currentDay.value = date(currentDay.value).subtract(1, 'week').toDate()
  emit('week', currentDay.value)
}
</script>

<template>
  <div class="flex items-center text-gray-900 w-48">
    <button
      @click="previousWeek"
      type="button"
      class="-m-1.5 flex flex-none items-center justify-center p-1.5 text-gray-400 hover:text-gray-500">
      <span class="sr-only">Previous month</span>
      <ChevronLeftIcon class="h-5 w-5" aria-hidden="true" />
    </button>
    <div class="flex-auto text-sm font-semibold text-center">{{ week.start }} - {{ week.end }}</div>
    <button
      @click="nextWeek"
      type="button"
      class="-m-1.5 flex flex-none items-center justify-center p-1.5 text-gray-400 hover:text-gray-500">
      <span class="sr-only">Next month</span>
      <ChevronRightIcon class="h-5 w-5" aria-hidden="true" />
    </button>
  </div>
</template>

<style scoped></style>
