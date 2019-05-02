<template>
  <div class = "view-reservation">
    <v-card lg12>
      <v-layout row wrap>
        <v-flex lg9>
          <h1 id="lotPortal">Parking Lots</h1>
        </v-flex>
      </v-layout>

      <v-container fluid grid-list-md>
        <v-layout row wrap>
          <v-flex xs12 md6 lg6 v-for="(lot, index) in lots" :key="index">
            <v-card>
              <v-card-title primary-title>
                <div class="content">
                  <!-- Launching to an app can be done by clicking the app title -->
                  <h3
                    id="launchable"
                    class="headline mb-0"
                  >
                    <strong>{{ lot.LotName }}</strong> {{ lot.Address }}
                  </h3>
                </div>
              </v-card-title>
              <div class="lotInfo">
                <v-btn color="primary" flat @click="goToReservations(lot.LotName, lot.Address, lot.LotId)">Reserve a Spot</v-btn>
              </div>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card>
  </div>
</template>

<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'

export default {
  name: 'Reservation',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Reservation page.',
      lots: []
    }
  },
  methods: {
    goToReservations (name, address, id) {
      sessionStorage.setItem('lotId', id)
      sessionStorage.setItem('lotName', name)
      sessionStorage.setItem('lotAddress', address)
      this.$router.push('/Reservation')
    }
  },
  beforeMount () {
    auth.authorize('standard', this.$router)
  },
  async mounted () {
    await axios
      .post(apiCalls.GET_ALL_LOTS, {
        Token: sessionStorage.getItem('ParkingMasterToken')
      })
      .then(response => (this.lots = response.data))
      .catch(e => {
        console.log(e)
        if (e.response.status === 401) {
          auth.invalidSession(this.$router)
        }
      })
  }
}
</script>
