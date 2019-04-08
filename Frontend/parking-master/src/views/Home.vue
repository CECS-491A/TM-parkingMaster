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
      if (localStorage.parkingMasterUsername.length > 0) {
        this.pageTitle = 'Welcome ' + localStorage.parkingMasterUsername
      }
    }
  },
  created () {
    this.checkLocalStorage()
  }
}
</script>
