<template>
  <div class="view-home">
    <img src="../assets/logo.png">
    <h1>{{pageTitle}}</h1>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'Home',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Home page.'

    }
  },
  methods: {
    getString () {
      axios.get('http://ec2-18-218-97-81.us-east-2.compute.amazonaws.com/ParkingMaster/api/Testing')
        .then(response => {
          this.pageTitle = response.data
        })
    }
  },
  created () {
    this.getString()
  }
}
</script>
