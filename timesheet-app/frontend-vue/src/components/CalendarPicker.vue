<script setup lang="ts">
import { computed, ref } from 'vue'
import { useDate } from '@/composables/useDate'
import {
  ChevronRightIcon,
  ChevronLeftIcon,
  EllipsisHorizontalIcon,
  CalendarIcon
} from '@heroicons/vue/24/outline'
import { MenuButton, MenuItem, MenuItems, Menu } from '@headlessui/vue'

interface CalendarDay {
  day: Date
  isWorkingMonth: boolean
  isToday: boolean
  formattedDate: string
  isSelected: boolean
}

const props = withDefaults(
  defineProps<{
    initialDate?: Date
  }>(),
  {
    initialDate: () => new Date()
  }
)

const currentDay = ref(props.initialDate)
const date = useDate()

const timesheets = [
  {
    id: 1,
    date: 'January 10th, 2022',
    time: '5:00 PM',
    datetime: '2022-01-10T17:00',
    project: 'External Client 1',
    location: 'Starbucks'
  }
]

const workingMonthDays = computed(() => {
  const currentDate = date(currentDay.value)
  const startOfWorkingMonth = currentDate.startOf('month')
  const endOfWorkingMonth = currentDate.endOf('month')
  const daysInMonth = currentDate.daysInMonth()

  const extraStartDayPadding = startOfWorkingMonth.day() % 7
  const extraEndDayPadding = (7 - endOfWorkingMonth.day()) % 7

  const days: CalendarDay[] = []

  for (let i = 1 - extraStartDayPadding; i < daysInMonth + extraEndDayPadding; i++) {
    const workingDate = startOfWorkingMonth.add(i, 'day')

    days.push({
      isToday: workingDate.isToday(),
      isWorkingMonth: currentDate.isSame(workingDate, 'month'),
      day: workingDate.toDate(),
      formattedDate: workingDate.toString(),
      isSelected: workingDate.isSame(currentDate, 'day')
    })
  }

  return days
})

function nextMonth() {
  currentDay.value = date(currentDay.value).add(1, 'month').toDate()
}

function previousMonth() {
  currentDay.value = date(currentDay.value).subtract(1, 'month').toDate()
}
</script>

<template>
  <div>
    <div class="lg:grid lg:grid-cols-12 lg:gap-x-16">
      <div
        class="mt-10 text-center lg:col-start-8 lg:col-end-13 lg:row-start-1 lg:mt-9 xl:col-start-9">
        <div class="flex items-center text-gray-900">
          <button
            type="button"
            class="-m-1.5 flex flex-none items-center justify-center p-1.5 text-gray-400 hover:text-gray-500"
            @click="previousMonth">
            <span class="sr-only">Previous month</span>
            <ChevronLeftIcon class="h-5 w-5" aria-hidden="true" />
          </button>
          <div class="flex-auto text-sm font-semibold">
            {{ $date(currentDay).format('MMMM YYYY') }}
          </div>
          <button
            type="button"
            class="-m-1.5 flex flex-none items-center justify-center p-1.5 text-gray-400 hover:text-gray-500"
            @click="nextMonth">
            <span class="sr-only">Next month</span>
            <ChevronRightIcon class="h-5 w-5" aria-hidden="true" />
          </button>
        </div>
        <div class="mt-6 grid grid-cols-7 text-xs leading-6 text-gray-500">
          <div>M</div>
          <div>T</div>
          <div>W</div>
          <div>T</div>
          <div>F</div>
          <div>S</div>
          <div>S</div>
        </div>
        <div
          class="isolate mt-2 grid grid-cols-7 gap-px rounded-lg bg-gray-200 text-sm shadow ring-1 ring-gray-200">
          <button
            v-for="(day, i) in workingMonthDays"
            :key="day.formattedDate"
            type="button"
            @click="currentDay = day.day"
            :class="[
              'py-1.5 hover:bg-gray-100 focus:z-10',
              day.isWorkingMonth ? 'bg-white' : 'bg-gray-50',
              (day.isSelected || day.isToday) && 'font-semibold',
              day.isSelected && 'text-white',
              !day.isSelected && day.isWorkingMonth && !day.isToday && 'text-gray-900',
              !day.isSelected && !day.isWorkingMonth && !day.isToday && 'text-gray-400',
              day.isToday && !day.isSelected && 'text-indigo-600',
              i === 0 && 'rounded-tl-lg',
              i === 6 && 'rounded-tr-lg',
              i === workingMonthDays.length - 7 && 'rounded-bl-lg',
              i === workingMonthDays.length - 1 && 'rounded-br-lg'
            ]">
            <time
              :dateTime="day.formattedDate"
              :class="[
                'mx-auto flex h-7 w-7 items-center justify-center rounded-full',
                day.isSelected && day.isToday && 'bg-indigo-600',
                day.isSelected && !day.isToday && 'bg-blue-600'
              ]">
              {{ day.day.getDate() }}
            </time>
          </button>
        </div>
      </div>
      <ol class="mt-4 divide-y divide-gray-100 text-sm leading-6 lg:col-span-7 xl:col-span-8">
        <li
          v-for="timesheet in timesheets"
          :key="timesheet.id"
          class="relative flex space-x-6 py-6 xl:static">
          <div class="flex-auto">
            <h3 class="pr-10 font-semibold text-gray-900 xl:pr-0">{{ timesheet.project }}</h3>
            <dl class="mt-2 flex flex-col text-gray-500 xl:flex-row">
              <div class="flex items-start space-x-3">
                <dt class="mt-0.5">
                  <span class="sr-only">Date</span>
                  <CalendarIcon class="h-5 w-5 text-gray-400" aria-hidden="true" />
                </dt>
                <dd>
                  <time :dateTime="timesheet.datetime">
                    {{ timesheet.date }} at {{ timesheet.time }}
                  </time>
                </dd>
              </div>
            </dl>
          </div>
          <Menu
            as="div"
            class="absolute right-0 top-6 xl:relative xl:right-auto xl:top-auto xl:self-center">
            <div>
              <MenuButton
                class="-m-2 flex items-center rounded-full p-2 text-gray-500 hover:text-gray-600">
                <span class="sr-only">Open options</span>
                <EllipsisHorizontalIcon class="h-5 w-5" aria-hidden="true" />
              </MenuButton>
            </div>

            <transition
              enter-active-class="transition ease-out duration-100"
              enter-from-class="transform opacity-0 scale-95"
              enter-to-class="transform opacity-100 scale-100"
              leave-active-class="transition ease-in duration-75"
              leave-from-class="transform opacity-100 scale-100"
              leave-to-class="transform opacity-0 scale-95">
              <MenuItems
                class="absolute right-0 z-10 mt-2 w-36 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
                <div class="py-1">
                  <MenuItem v-slot="{ active }">
                    <a
                      href="#"
                      :class="[
                        active ? 'bg-gray-100 text-gray-900' : 'text-gray-700',
                        'block px-4 py-2 text-sm'
                      ]">
                      Edit
                    </a>
                  </MenuItem>
                  <MenuItem v-slot="{ active }">
                    <a
                      href="#"
                      :class="[
                        active ? 'bg-gray-100 text-gray-900' : 'text-gray-700',
                        'block px-4 py-2 text-sm'
                      ]">
                      Cancel
                    </a>
                  </MenuItem>
                </div>
              </MenuItems>
            </transition>
          </Menu>
        </li>
      </ol>
    </div>
  </div>
</template>
