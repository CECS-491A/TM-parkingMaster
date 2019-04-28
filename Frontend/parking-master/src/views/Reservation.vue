<template>
  <div class="view-reservation">
     <form class="form-reservation">
      <h2 class="form-reservation-heading">Reservations: {{ lotName }}</h2>
      <h3 class="form-reservation-address">Address: {{ lotAddress }}</h3>
      <v-form ref="form">
        <select v-model="selectedSpot">
          <option disabled value="" v-if="!worked" >Please Select a Parking Spot</option>
          <option v-for="(spot, index) in spots" :key="index" v-bind:value="spot" :class="{ 'taken-spot' : !spot.IsAvailable, 'available-spot' : spot.IsAvailable }" v-if="!worked">{{ spot.SpotName }}</option>
        </select>
        <select v-model="selectedVehicle">
          <option disabled value="" v-if="!worked">Please Select a Your Vehicle Plate</option>
          <option v-for="(vehicle, index) in vehicles" :key="index" v-bind:value="vehicle" v-if="!worked">{{ vehicle.Plate }}</option>
        </select>
        <v-text-field id="duration" v-model="duration" class="form-control" placeholder="Length of Reservation (minutes)" required v-if="!worked"></v-text-field>
        <v-btn class="button-reservation" color="primary" v-on:click="submitReservation" v-if="!worked">Submit Reservation</v-btn>

        <v-alert :value="errorOn" color="error" transition="scale-transition"><h3> {{error}} </h3></v-alert>
      </v-form>

      <div id="responseMessage" v-if="worked">
        <h3>Successfully reserved parking spot: {{ selectedSpot.SpotName }}</h3>
        <br />
      </div>
      <div id="licensePlateMessage" v-if="worked">
        <h3>License plate on reservation: {{ selectedVehicle.Plate }}</h3>
        <br />
      </div>
      <div id="reservationMessage" v-if="worked">
        <h3>Reservation ends at: {{ reservationEndsAt }}</h3>
      </div>
    </form>
  </div>
</template>
<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'

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
      axios
        .post(apiCalls.RESERVE_PARKING_SPOT, {
          SessionId: sessionStorage.getItem('ParkingMasterToken'),
          SpotId: this.selectedSpot.SpotId,
          VehicleVin: this.selectedVehicle.Vin,
          DurationInMinutes: this.duration
        })
        .then(function () {
          console.log('OK')
          var date = new Date()
          this.reservationEndsAt = new Date(date.getTime() + (this.duration * 60000)).toString()
          this.worked = true
        }.bind(this))
        .catch(e => {
          console.log(e)
          this.error = 'Failed to reserve parking spot.'
          this.errorOn = true
          if (e.response.status === 401) {
            sessionStorage.clear()
            this.$router.push('/Home')
          }
        })
    }
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
      .then(response => (this.spots = response.data))
    await axios
      .post(apiCalls.GET_ALL_USER_VEHICLES, {
        Token: sessionStorage.getItem('ParkingMasterToken')
      })
      .then(response => (this.vehicles = response.data))
      .catch(e => {
        console.log(e)
        if (e.response.status === 401) {
          sessionStorage.clear()
          this.$router.push('/Home')
        }
      })
  }
}
</script>

<style>
.form-reservation {
  max-width: 330px;
  margin: 0 auto;
}
.button-reservation {
  width: 330px;
  margin: 0 auto;
}
.taken-spot {
  background: red;
  color: white;
}
.available-spot {
  background: green;
  color: white;
}
</style>
