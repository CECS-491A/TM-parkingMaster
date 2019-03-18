<template>
  <div>
    <h1>{{pageTitle}}</h1>
    <div>
      <h2>LotRegistration</h2>
    </div>
    <v-container grid-list-md text-xs-center>
        
        <v-form
            ref="form"
        >
        <v-layout row wrap>
        <v-text-field
            v-model="make"
            label="Make"
            required
        ></v-text-field>
        <v-text-field
            v-model="model"
            label="Model"
            required
        ></v-text-field>
        <v-text-field
            v-model="year"
            label="Year"
            required
        ></v-text-field>
        <v-text-field
            v-model="vin"
            label="VIN"
            required
        ></v-text-field>
        </v-layout>
        </v-form>
        
    </v-container>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'LotRegistration',
  data () {
    return {
      pageTitle: '',
      msg: 'Our LotRegistration page.'

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
