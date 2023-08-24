<script setup lang="ts"></script>

<template>
  <div class="mt-2 md:flex md:items-center md:justify-between">
    <div class="min-w-0 flex-1">
      <slot></slot>
    </div>
    <div
      class="mt-4 flex flex-shrink-0 md:ml-4 md:mt-0"
      data-testid="actions"
      v-if="$slots.actions">
      <slot name="actions"></slot>
    </div>
  </div>
</template>
