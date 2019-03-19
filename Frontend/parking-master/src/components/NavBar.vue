<template>
  <div class = "toolbar-main">
    <v-toolbar app>
      <v-toolbar-title class="headline text-uppercase">
        <span>ParkingMaster</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>

      <v-btn to="home" flat>Home</v-btn>
      <v-btn to="reservation" flat>Reservation</v-btn>
      <v-btn to="vehicleRegistration" flat>Vehicle Registration</v-btn>
      <v-btn to="lotRegistration" flat>Lot Registration</v-btn>

    </v-toolbar>
  </div>
</template>

<script>
export default {
  name: 'NavBar'
}
</script>
