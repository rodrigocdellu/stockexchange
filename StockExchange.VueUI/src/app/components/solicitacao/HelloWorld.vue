<script setup lang="ts">
import { ref } from 'vue'

const count = ref(0)
</script>

<template>
  <div style="padding: 2rem; text-align: center;">
    <h1>🎉 Bem-vindo ao Vue!</h1>
    <p>Seu componente está funcionando!</p>
    <button type="button" class="counter" @click="count++">
      Contador: {{ count }}
    </button>
  </div>
</template>
