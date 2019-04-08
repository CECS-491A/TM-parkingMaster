<template>
  <div class = "toolbar-main">
    <v-toolbar dense>
      <v-toolbar-title class="headline text-uppercase">
        <span>ParkingMaster</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>

      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn to="home" flat>Home</v-btn>
        <v-btn to="reservation" flat>Reservation</v-btn>
        <v-btn to="vehicleRegistration" flat>Vehicle Registration</v-btn>
        <v-btn to="lotRegistration" flat>Lot Registration</v-btn>
        <v-btn to="login" flat>Login</v-btn>
      </v-toolbar-items>

    </v-toolbar>
  </div>
</template>

<script>
export default {
  name: 'NavBar'
}
</script>
