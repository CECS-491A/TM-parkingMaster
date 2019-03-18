<template>
  <v-toolbar app>
    <v-toolbar-title class="headline text-uppercase">
      <span>ParkingMaster</span>
    </v-toolbar-title>
    <v-spacer></v-spacer>

    <v-btn to="home" flat>Home</v-btn>
    <v-btn to="reservation" flat>Reservation</v-btn>
    <v-btn to="VehicleRegistration" flat>Vehicle Registration</v-btn>
    <v-btn to="LotRegistration" flat>Lot Registration</v-btn>

  </v-toolbar>
</template>

<script>
export default {
  name: 'NavBar'
}
</script>