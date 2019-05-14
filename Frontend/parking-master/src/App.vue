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

// Set default user values
let userRole = sessionStorage.getItem('ParkingMasterRole')
if (userRole === null) {
  sessionStorage.setItem('ParkingMasterRole', 'unauthorized')
  sessionStorage.setItem('ParkingMasterAcceptedTOS', true)
}
</script>

<style>
  div[class^='view-'] {
    padding: 10px;
    text-align: center;
  }
</style>
