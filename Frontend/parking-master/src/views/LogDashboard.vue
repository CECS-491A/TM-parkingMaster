<template>
  <div class = "view-log-dashboard">
    <v-list v-if="role == 'admin'">
      <h2>Logs</h2>

    </v-list>
  </div>
</template>

<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'

export default {
  name: 'logDashboard',
  data () {
    return {
      logs: [],
      role: ''
    }
  },
  methods: {

  },
  beforeMount () {
    auth.authorize('authorized', this.$router)
    this.role = sessionStorage.getItem('ParkingMasterRole')
  },
  mounted () {
    axios
      .post(apiCalls.GET_ALL_LOGS, {
        Token: sessionStorage.getItem('ParkingMasterToken')
      })
      .then(response => {
        this.logs = response.data
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
</style>
