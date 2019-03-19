<template>
  <div class = "view-vehicle-registration">
    <form class="form-vehicle-register">
      <h2 class="form-vehicle-register-heading">Vehicle Registration</h2>
      <v-form ref="form">
        <v-text-field id="make" class="for-control" placeholder="Make" required></v-text-field>
        <v-text-field id="model" class="for-control" placeholder="Model" required></v-text-field>
        <v-text-field id="year" class="for-control" placeholder="Year" required></v-text-field>
        <v-text-field id="vin" class="for-control" placeholder="VIN" required></v-text-field>
      </v-form>
    </form>
  </div>
</template>

<script>

export default {
  name: 'vehicleRegistration',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Vehicle Registration page.'

    }
  }
}
</script>
