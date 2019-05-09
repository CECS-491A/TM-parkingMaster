<template>
  <div class="view-reservation">
     <form class="form-reservation">
      <h2 class="form-reservation-heading">Reservations: {{ lotName }}</h2>
      <h3 class="form-reservation-address">Address: {{ lotAddress }}</h3>
      <img :src="map">
      <v-form ref="form">

        <v-select v-model="selectedSpot"
          label="Select"
          hint="Select the parking spot you wish to reserve."
          :items="spots"
          item-text="SpotName"
          item-value="SpotId"
          item-disabled="IsTaken"
          v-if="!worked"
          persistent-hint
          offset-y></v-select>

        <v-select v-model="selectedVehicle"
          label="Select"
          hint="Select the vehicle you wish to put on the reservation."
          no-data-text="Please register a vehicle before making a reservation."
          :items="vehicles"
          item-text="Plate"
          item-value="Vin"
          v-if="!worked"
          persistent-hint
          offset-y></v-select>

        <v-text-field id="duration"
          v-model="duration"
          class="form-control"
          hint="Enter the length of your reservation in minutes."
          placeholder="Duration"
          mask="#####"
          required v-if="!worked"
          persistent-hint></v-text-field>
        <br />

        <v-btn class="button-reservation"
          color="primary"
          v-on:click="submitReservation"
          v-if="!worked">Submit Reservation</v-btn>

        <v-alert :value="errorOn"
          color="error"
          transition="scale-transition">
            <h3> {{error}} </h3></v-alert>
      </v-form>

      <div id="responseMessage" v-if="worked">
        <h3>Successfully reserved parking spot: {{ selectedSpot.SpotName }}</h3>
        <br />
      </div>
      <div id="licensePlateMessage" v-if="worked">
        <h3>License plate on reservation: {{ selectedVehicle.Plate }}</h3>
        <br />
      </div>
      <div id="reservationMessage" class="reservation-message" v-if="worked">
        <h3>Reservation lasts until: {{ reservationEndsAt }}</h3>
      </div>
    </form>
  </div>
</template>
<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'
import moment from 'moment'

export default {
  name: 'reservations',
  props: ['id', 'name1', 'address'],
  data () {
    return {
      file: '',
      map: '',
      lotName: '',
      lotAddress: '',
      lotId: '',
      spots: [],
      selectedSpot: '',
      vehicles: [],
      selectedVehicle: '',
      duration: null,
      vehicleVin: '',
      worked: false,
      reservationEndsAt: '',
      error: '',
      errorOn: false
    }
  },
  methods: {
    submitReservation () {
      this.error = ''
      this.errorOn = false
      if (this.selectedSpot === '' || this.selectedVehicle === '' || this.duration === null) {
        this.error = 'Please fill every box.'
        this.errorOn = true
      } else {
        axios
          .post(apiCalls.RESERVE_PARKING_SPOT, {
            SessionId: sessionStorage.getItem('ParkingMasterToken'),
            SpotId: this.selectedSpot,
            VehicleVin: this.selectedVehicle,
            DurationInMinutes: this.duration
          })
          .then(function () {
            console.log('OK')
            let now = moment()
            let endDate = now.add(this.duration, 'm')
            this.reservationEndsAt = endDate.format('l') + ' at ' + endDate.format('LTS')
            this.worked = true
          }.bind(this))
          .catch(e => {
            console.log(e)
            this.error = 'Failed to reserve parking spot.'
            this.errorOn = true
            if (e.response.status === 401) {
              auth.invalidSession(this.$router)
            }
          })
      }
    }
  },
  beforeMount () {
    auth.authorize('standard', this.$router)
  },
  async mounted () {
    this.lotId = sessionStorage.getItem('lotId')
    this.lotName = sessionStorage.getItem('lotName')
    this.lotAddress = sessionStorage.getItem('lotAddress')
    await axios
      .post(apiCalls.GET_ALL_SPOTS_FOR_LOT, {
        SessionId: sessionStorage.getItem('ParkingMasterToken'),
        LotId: this.lotId
      })
      .then(function (response) {
        this.spots = response.data.SpotsList
        this.map = 'data:image/png;base64,' + response.data.LotMap
      }.bind(this))
    await axios
      .post(apiCalls.GET_ALL_USER_VEHICLES, {
        Token: sessionStorage.getItem('ParkingMasterToken')
      })
      .then(response => (this.vehicles = response.data))
      .catch(e => {
        console.log(e)
        if (e.response.status === 401) {
          auth.invalidSession(this.$router)
        }
      })
  }
}
</script>

<style>
.form-reservation {
  max-width: 350px;
  margin: 0 auto;
}
.button-reservation {
  width: 350px;
  margin: 0 auto;
}
.reservation-message {
  width: 400px;
  margin: 0 auto;
}

</style>
