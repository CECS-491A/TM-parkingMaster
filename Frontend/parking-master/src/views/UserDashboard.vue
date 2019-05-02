<template>
  <div class = "view-user-dashboard">
    <v-btn class="error" v-on:click="ssoDelete">User Deletetion from SSO</v-btn>
    <br />

    <h2>Current Reservations</h2>
    <v-list>
      <v-list-tile
        v-for="(reservation, index) in reservations"
        :key="reservation.name"
        :class="{ 'even-reservation-tile' : index%2, 'odd-reservation-tile' : !index%2 }"
      >
        <v-list-tile-title v-text="'Lot: ' + reservation.LotName + '  For spot: ' + reservation.SpotName"></v-list-tile-title>
        <v-list-tile-content>
          <v-layout row wrap>
            <v-flex xs7>
              <v-text-field id="duration"
              v-model="duration"
              class="extension-text-input"
              hint="Enter time in minutes."
              placeholder="Extention Time"
              type="number"></v-text-field>
            </v-flex>
            <v-flex xs5>
              <v-btn class="extension-btn">Extend</v-btn>
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

export default {
  name: 'userDashboard',
  data () {
    return {
      reservations: []
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
    }
  },
  beforeMount () {
    auth.authorize('authorized', this.$router)
  },
  mounted () {
    if (sessionStorage.getItem('ParkingMasterRole') === 'standard') {
      axios
        .post(apiCalls.GET_ALL_RESERVATIONS, {
          Token: sessionStorage.getItem('ParkingMasterToken')
        })
        .then(response => (this.reservations = response.data))
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
