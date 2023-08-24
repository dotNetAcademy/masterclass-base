<script setup lang="ts">
import BreadcrumbsComponent from '@/components/BreadcrumbsComponent.vue'
import PageHeader from '@/components/PageHeader.vue'
import CalendarPicker from '@/components/CalendarPicker.vue'
</script>

<template>
  <main>
    <BreadcrumbsComponent />
    <PageHeader>
      <h2 data-testid="header-title">Timesheets</h2>
      <template v-slot:actions>
        <button role="button" type="button">New registration</button>
      </template>
    </PageHeader>
    <CalendarPicker />
  </main>
</template>

<style scoped>
h2 {
  @apply text-2xl font-bold leading-7 text-gray-900 sm:truncate sm:text-3xl sm:tracking-tight;
}

button {
  @apply ml-3 inline-flex items-center rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600;
}
</style>
