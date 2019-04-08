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
      if (username.length > 0) {
        this.pageTitle = 'Welcome ' + username
      }
    }
  },
  mounted () {
    this.checkLocalStorage()
  }
}
</script>
