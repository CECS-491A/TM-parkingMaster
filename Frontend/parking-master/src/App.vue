<template>
  <v-app>
    <nav-bar />

    <v-content>
      <router-view />
    </v-content>
  </v-app>
</template>

<script>
import NavBar from '@/components/NavBar.vue'

export default {
  name: 'app',
  components: {
    NavBar
  }
}
</script>

<style>
  div[class^='view-'] {
    padding-top: 65px
  }
</style>
