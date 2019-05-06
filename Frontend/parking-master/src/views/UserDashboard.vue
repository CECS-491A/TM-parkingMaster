<template>
  <div class = "view-user-dashboard">
    <v-btn class="error" v-on:click="ssoDelete">User Deletetion from SSO</v-btn>
    <br />

    <v-list v-if="role == 'standard'">
      <h2>Current Reservations</h2>
      <v-list-tile
        v-for="(reservation, index) in reservations"
        :key="reservation.name"
        :id="'reservationTile'+index"
        :class="{ 'even-reservation-tile' : index%2===0, 'odd-reservation-tile' : index%2===1 }"
      >
        <v-list-tile-title v-text="'Lot: ' + reservation.LotName + '  For spot: ' + reservation.SpotName + '    reservation ends at: ' + reservation.EndsAt.format('lll')"></v-list-tile-title>
        <v-list-tile-content>
          <v-layout row wrap>
            <v-flex xs7>
              <v-text-field :name="index"
              v-model="duration[index]"
              class="extension-text-input"
              hint="Enter time in minutes."
              placeholder="Extension Time"
              mask="####"></v-text-field>
            </v-flex>
            <v-flex xs5>
              <v-btn class="extension-btn"
                v-on:click="submitReservation(reservation.SpotId, reservation.VehicleVin, duration[index], index)">Extend</v-btn>
            </v-flex>
          </v-layout>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </div>
</template>
<script>

import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'
import moment from 'moment'

export default {
  name: 'userDashboard',
  data () {
    return {
      reservations: [],
      duration: [],
      now: moment(),
      role: ''
    }
  },
  methods: {
    ssoDelete () {
      axios
        .post(apiCalls.DELETE_FROM_APPS, {
          Token: sessionStorage.getItem('ParkingMasterToken')
        })
        .then(resp => {
          alert('Account has been deleted from SSO and its applications!')
          window.location = 'http://localhost:8081/#/landing'
        })
        .catch(e => {
          alert('Delete failed')
        })
    },
    submitReservation (spotId, vin, length, index) {
      if (length === undefined) {
        alert('Please enter a duration to extend your reservation.')
      } else {
        axios
          .post(apiCalls.EXTEND_RESERVATION, {
            SessionId: sessionStorage.getItem('ParkingMasterToken'),
            SpotId: spotId,
            VehicleVin: vin,
            DurationInMinutes: length
          })
          .then(function () {
            console.log('OK')

            // Update time on dashboard
            let element = document.getElementById('reservationTile' + index).getElementsByClassName('v-list__tile__title')
            this.reservations[index].EndsAt.add(length, 'm')
            element[0].innerHTML = 'Lot: ' + this.reservations[index].LotName + '  For spot: ' + this.reservations[index].SpotName + '    reservation ends at: ' + this.reservations[index].EndsAt.format('lll')
          }.bind(this))
          .catch(e => {
            console.log(e)
            console.log('Failed to extend reservation.')
            if (e.response.status === 401) {
              auth.invalidSession(this.$router)
            }
          })
      }
    }
  },
  beforeMount () {
    auth.authorize('authorized', this.$router)
    this.role = sessionStorage.getItem('ParkingMasterRole')
  },
  mounted () {
    if (this.role === 'standard') {
      axios
        .post(apiCalls.GET_ALL_RESERVATIONS, {
          Token: sessionStorage.getItem('ParkingMasterToken')
        })
        .then(response => {
          this.reservations = response.data
          for (let i = 0; i < this.reservations.length; i++) {
            this.reservations[i].EndsAt = moment().add(this.reservations[i].SecondsLeft, 's')
          }
        })
        .catch(e => {
          console.log(e)
          if (e.response.status === 401) {
            auth.invalidSession(this.$router)
          }
        })
    }
  }
}
</script>

<style>
.odd-reservation-tile {
  background: lightgray;
}
.even-reservation-tile {
  background: white;
}
.extention-text-field {
  width: 150px;
}
.extension-btn {
  position: relative;
  top: 9px;
}
</style>
