<template>
  <div class='view-home'>
    <img src='../assets/logo.png'>
    <h1 v-if="authenticated">{{pageTitle}}</h1>
    <h1 v-if="!authenticated">Welcome! Please register/login at:
      <a :href="kfcUrl" >
        kfc-sso.com
      </a>
    </h1>
  </div>
</template>

<script>
import apiCalls from '@/constants/api-calls'

export default {
  name: 'Home',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Home page.',
      authenticated: false,
      kfcUrl: apiCalls.SSO_FRONTEND_URL
    }
  },
  methods: {
    checkLocalStorage () {
      let acceptedTOS = sessionStorage.getItem('ParkingMasterAcceptedTOS')
      let username = sessionStorage.getItem('ParkingMasterUsername')
      let role = sessionStorage.getItem('ParkingMasterRole')

      // Most importantly, check if the user account is currently disabled
      if (role === 'disabled') {
        this.pageTitle = 'Sorry ' + username + ' it seems your account is currently disabled.'
        this.authenticated = true

      // Check if user has accepted the TOS
      } else if (acceptedTOS === 'false') {
        this.$router.push('/TOS')
        return
      } else if (role !== 'unauthorized') {
        this.pageTitle = 'Welcome ' + username
        this.authenticated = true
      } else {
        this.authenticated = false
      }

      if (sessionStorage.getItem('ParkingMasterRefresh') === 'true') {
        sessionStorage.setItem('ParkingMasterRefresh', 'false')
        document.location.reload(true)
      }
    }
  },
  beforeMount: function () {
    this.checkLocalStorage()
  }
}
</script>
