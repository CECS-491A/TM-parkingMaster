<template>
  <div class = "view-user-dashboard">
    <v-btn class="error" v-on:click="ssoDelete">User Deletetion from SSO</v-btn>
  </div>
</template>
<script>

import axios from 'axios'
import apiCalls from '@/constants/api-calls'
export default {
  name: 'userDashboard',
  methods: {
    ssoDelete () {
      axios
        .post(apiCalls.DELETE_FROM_APPS, {
          Token: sessionStorage.getItem('ParkingMasterToken')
        })
        .then(resp => {
          alert('Account has been deleted from SSO and its applications!')
          window.location = 'http://localhost:8081/#/clearuserroute'
        })
        .catch(e => {
          alert('Delete failed')
        })
    }
  }
}
</script>
