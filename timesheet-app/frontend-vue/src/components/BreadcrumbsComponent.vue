<script setup lang="ts">
import { ChevronLeftIcon, ChevronRightIcon } from '@heroicons/vue/24/outline'
</script>

<template>
  <div>
    <nav class="sm:hidden" aria-label="Back">
      <a href="#" class="flex items-center text-sm font-medium text-gray-500 hover:text-gray-700">
        <ChevronLeftIcon
          class="-ml-1 mr-1 h-5 w-5 flex-shrink-0 text-gray-400"
          aria-hidden="true" />
        Back
      </a>
    </nav>
    <nav class="hidden sm:flex" aria-label="Breadcrumb">
      <ol role="list" class="flex items-center space-x-4">
        <li role="listitem">
          <div class="flex">
            <a href="#" class="text-sm font-medium text-gray-500 hover:text-gray-700">Home</a>
          </div>
        </li>
        <li role="listitem">
          <div class="flex items-center">
            <ChevronRightIcon class="h-5 w-5 flex-shrink-0 text-gray-400" aria-hidden="true" />
            <a href="#" class="ml-4 text-sm font-medium text-gray-500 hover:text-gray-700">
              Timesheets
            </a>
          </div>
        </li>
      </ol>
    </nav>
  </div>
</template>

<style scoped></style>
