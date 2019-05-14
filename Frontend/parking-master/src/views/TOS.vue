<template>
  <div class='view-tos'>
    <h1>You must accept our Terms of Service before continuing in Parking Master.</h1>
    <br />
    <div class="editable"
      id="tos-editable"
      contenteditable="true"></div>

    <v-btn class="button-accept"
          color="primary"
          v-on:click="acceptTOS">Accept</v-btn>
  </div>
</template>

<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'

export default {
  name: 'tos',
  methods: {
    acceptTOS () {
      axios
        .post(apiCalls.ACCEPT_TOS, {
          Token: sessionStorage.getItem('ParkingMasterToken')
        })
        .then(resp => {
          sessionStorage.setItem('ParkingMasterAcceptedTOS', true)

          if (sessionStorage.getItem('ParkingMasterRole') === 'unassigned') {
            this.$router.push('/RoleChoice')
          } else {
            this.$router.push('/Home')
          }
        })
    }
  },
  beforeMount: function () {
    let acceptedTOS = sessionStorage.getItem('ParkingMasterAcceptedTOS')

    // User must have yet to accept current TOS to access this page
    if (acceptedTOS === 'true') {
      alert('Unauthorized to access requested page.')
      this.$router.push('/Home')
    }
  },
  mounted () {
    axios
      .post(apiCalls.GET_CURRENT_TOS, {
        Token: sessionStorage.getItem('ParkingMasterToken')
      })
      .then(resp => {
        console.log(resp)
        document.getElementById('tos-editable').innerHTML = resp.data.Content
      })
      .catch(e => {
        if (e.response.status === 401) {
          auth.invalidSession(this.$router)
        }
      })
  }
}
</script>

<style>
.editable {
  margin: 0 auto;
  width: 800px;
  height: 300px;
  border: 1px solid #ccc;
  padding: 5px;
  overflow: auto;
}
</style>
