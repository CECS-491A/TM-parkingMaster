<template>
  <div class='view-home'>
    <img src='../assets/logo.png'>
    <h1>{{pageTitle}}</h1>
  </div>
</template>

<script>
export default {
  name: 'Home',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Home page.'
    }
  },
  methods: {
    checkLocalStorage () {
      var username = sessionStorage.getItem('ParkingMasterUsername')
      if (username !== null) {
        this.pageTitle = 'Welcome ' + username
      } else {
        this.pageTitle = 'Welcome!'
      }
    }
  },
  mounted () {
    this.checkLocalStorage()
  }
}
</script>
