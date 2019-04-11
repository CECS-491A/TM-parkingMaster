<template>
  <div class = "toolbar-main">
    <v-toolbar dark color="primary">
      <v-toolbar-title class="headline text-uppercase">
        <span>ParkingMaster</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>

      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn flat key="Home" to="home"> Home </v-btn>
        <v-btn flat key="Login" to="login"> Login </v-btn>
        <v-btn flat key="Login" to="login" v-if="role === 'unauthorized'"> Login </v-btn>
        <v-btn flat key="Reservation" to="reservation" v-if="role === 'standard'"> Reservation </v-btn>
        <v-btn flat key="Vehicle Registration" to="vehicleRegistration" v-if="role === 'standard'"> Vehicle Registration </v-btn>
        <v-btn flat key="Lot Registration" to="lotRegistration" v-if="role === 'lotmanager'"> Lot Registration </v-btn>
      </v-toolbar-items>

    </v-toolbar>
  </div>
</template>

<script>
export default {
  name: 'NavBar',
  data () {
    return {
      role: sessionStorage.getItem('ParkingMasterRole')
    }
  },
  updated () {
    this.role = sessionStorage.getItem('ParkingMasterRole')
  }
}
</script>
