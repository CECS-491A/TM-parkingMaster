<template>
  <div class="view-lot-deletion">
     <form class="form-lot-deletion">
      <h2 class="form-lot-deletion-heading">Lot Deletion</h2>
      <v-form ref="form">
        <v-select v-model="selectedLot"
          label="Select"
          hint="Select the parking lot you wish to delete."
          :items="lots"
          item-text="LotName"
          item-value="LotId"
          v-if="!worked"
          no-data-text="No lots found"
          persistent-hint
          offset-y></v-select>
        <v-btn depressed color="primary"
          v-on:click="deletelot"
          type="submit"
          v-if="!worked">Delete Lot</v-btn>

        <v-alert :value="errorOn"
          color="error"
          transition="scale-transition">
            <h3> {{error}} </h3></v-alert>
      </v-form>
    </form>
    <div id="responseMessage" v-if="worked">
        <h3>Successfully deleted parking lot.</h3>
        <h2>Please note that all reservations in this lot have been deleted accordingly.</h2>
        <br />
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'

export default {
  name: 'lotDeletion',
  props: [],
  data () {
    return {
      lots: [],
      selectedLot: '',
      worked: false,
      username: '',
      error: '',
      errorOn: false
    }
  },
  methods: {
    async deletelot () {
      this.error = ''
      this.errorOn = false
      let token = sessionStorage.getItem('ParkingMasterToken')
      if (this.selectedLot === '') {
        this.error = 'Please select a lot.'
        this.errorOn = true
      } else {
        await axios
          .post(apiCalls.LOT_DELETION, {
            Token: token,
            LotName: this.selectedLot
          }
          )
          .then(function () {
            console.log('OK')
            this.worked = true
          }.bind(this))
          .catch(e => {
            console.log(e.response)
            this.error = 'Failed to delete parking lot. Please try again later.'
            this.errorOn = true
            if (e.response.status === 401) {
              auth.invalidSession(this.$router)
            }
          })
      }
    }
  },
  beforeMount () {
    auth.authorize('lotmanager', this.$router)
  },
  async mounted () {
    let uToken = sessionStorage.getItem('ParkingMasterToken')
    let uUsername = sessionStorage.getItem('ParkingMasterUsername')
    let uRole = sessionStorage.getItem('ParkingMasterRole')
    let fData = new FormData()
    fData.append('Token', uToken)
    fData.append('Username', uUsername)
    fData.append('Role', uRole)
    await axios
      .post(apiCalls.GET_ALL_LOTS_BY_OWNER, {
        Token: uToken
      })
      .then(function (response) {
        this.lots = response.data
      }.bind(this))
      .catch(e => {
        console.log(e)
        this.error = 'Could not retrieve lots. Please try again later.'
        this.errorOn = true
        if (e.response.status === 401) {
          auth.invalidSession(this.$router)
        }
      })
  }
}
</script>
