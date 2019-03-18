<template>
  <div>
    <h1>{{pageTitle}}</h1>
    <div>
      <h2>VehicleRegistration</h2>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'VehicleRegistration',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Vehicle Registration page.'

    }
  },
  methods: {
    getString () {
      axios.get('http://ec2-18-191-80-204.us-east-2.compute.amazonaws.com/ParkingMaster/api/Testing')
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
