<template>
  <div class = "view-vehicle-registration">
    <form class="form-vehicle-register">
      <h2 class="form-vehicle-register-heading">Vehicle Registration</h2>
      <v-form ref="form">
        <v-text-field id="make" v-model="make" class="form-control" placeholder="Make" v-if="!worked"></v-text-field>
        <v-text-field id="model" v-model="model" class="form-control" placeholder="Model" v-if="!worked"></v-text-field>
        <v-text-field id="year" v-model="year" class="form-control" placeholder="Year" v-if="!worked"></v-text-field>
        <v-text-field id="licensePlate" v-model="licensePlate" class="form-control" placeholder="License Plate" v-if="!worked"></v-text-field>
        <v-text-field id="state" v-model="state" class="form-control" placeholder="State" v-if="!worked"></v-text-field>
        <v-text-field id="vin" v-model="vin" class="form-control" placeholder="VIN" v-if="!worked"></v-text-field>
        <v-btn color="primary" flat @click="registerVehicle()" v-if="!worked">Register Vehicle</v-btn>

        <v-alert :value="errorOn" color="error" transition="scale-transition"><h3> {{error}} </h3></v-alert>

        <div id="responseMessage" v-if="worked">
          <h3>Successfully registered car.</h3>
          <br />
        </div>
        <div id="makeMessage" v-if="worked">
          <h3>Make: {{ make }}</h3>
        </div>
        <div id="modelMessage" v-if="worked">
          <h3>Model: {{ model }}</h3>
        </div>
        <div id="yearMessage" v-if="worked">
          <h3>Year: {{ year }}</h3>
        </div>
        <div id="licensePlateMessage" v-if="worked">
          <h3>License Plate: {{ licensePlate }}</h3>
        </div>
        <div id="stateMessage" v-if="worked">
          <h3>State: {{ state }}</h3>
        </div>
        <div id="vinMessage" v-if="worked">
          <h3>VIN: {{ vin }}</h3>
        </div>
      </v-form>

    </form>
  </div>
</template>

<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'

export default {
  name: 'vehicleRegistration',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Vehicle Registration page.',
      make: '',
      model: '',
      year: '',
      licensePlate: '',
      state: '',
      vin: '',
      worked: false,
      errorOn: false
    }
  },
  methods: {
    registerVehicle () {
      if (this.make.length === 0 || this.model.length === 0 || this.year.length === 0 || this.licensePlate.length === 0 || this.state.length === 0 || this.vin.length === 0) {
        this.error = 'All text fields required.'
        this.errorOn = true
        console.log(this.error)
      } else {
        this.error = ''
        this.errorOn = false
        axios
          .post(apiCalls.VEHICLE_REGISTRATION, {
            SessionId: sessionStorage.getItem('ParkingMasterToken'),
            Make: this.make,
            Model: this.model,
            Year: this.year,
            Plate: this.licensePlate,
            State: this.state,
            Vin: this.vin
          })
          .then(function () {
            console.log('Vehicle Registered.')
            this.worked = true
          }.bind(this))
          .catch(e => {
            console.log(e)

            if (e.response.status === 401) {
              auth.invalidSession(this.$router)
            } else if (e.response.status === 418) {
              sessionStorage.setItem('ParkingMasterRole', 'disabled')
              sessionStorage.setItem('ParkingMasterRefresh', 'true')
              this.$router.push('/Home')
            } else if (e.response.status === 419) {
              sessionStorage.setItem('ParkingMasterAcceptedTOS', 'false')
              this.$router.push('/TOS')
            }
          })
      }
    }
  },
  beforeMount () {
    auth.authorize('standard', this.$router)
  }
}
</script>

<style>
  .form-vehicle-register {
    max-width: 330px;
    margin: 0 auto;
  }
</style>
