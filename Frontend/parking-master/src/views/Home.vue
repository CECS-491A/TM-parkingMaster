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
      var username = sessionStorage.getItem('ParkingMasterUsername')
      if (username !== null) {
        this.pageTitle = 'Welcome ' + username
        this.authenticated = true
        if (sessionStorage.getItem('ParkingMasterRefresh') === 'true') {
          sessionStorage.setItem('ParkingMasterRefresh', 'false')
          document.location.reload(true)
        }
      } else {
        this.authenticated = false
      }
    }
  },
  beforeMount: function () {
    this.checkLocalStorage()
  }
}
</script>
