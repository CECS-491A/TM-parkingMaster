<template>
  <div class = "view-login">
    <form class="form-login">
      <h2 class="form-login-heading">Login</h2>
      <v-form ref="form">
        <v-text-field id="email" v-model="email" class="form-control" placeholder="Email" required></v-text-field>
        <v-text-field id="password" v-model="password" class="form-control" placeholder="Password" required></v-text-field>
        <v-btn class="button-login" v-on:click="ssoLogin" color="blue">Login</v-btn>
      </v-form>
    </form>
  </div>
</template>
<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'

export default {
  name: 'login',
  props: ['token'],
  data () {
    return {
      email: '',
      password: ''
    }
  },
  methods: {
    ssoLogin () {
      axios
        .post(apiCalls.SSO_LOGIN, {
          email: this.email,
          password: this.password
        })
        .then(resp => {
          localStorage.email = this.email
          localStorage.token = resp.data
          alert(localStorage.email + ' is logged in.')
        })
        .catch(e => {
          console.log(e)
          if (e.response.status === 404) {
            alert('User Not Found')
          } else if (e.response.status === 401) {
            alert('User is Disabled')
          } else if (e.response.status === 400) {
            alert('Invalid Password')
          } else {
            alert('Bad Request or Conflict')
          }
        })
    },
    login () {
      // If token is defined, then there is an attempted login
      if (this.token !== undefined) {
        axios
          .post(apiCalls.SESSION_CHECK, {
            Token: this.token
          })
          .then(resp => {
            sessionStorage.setItem('ParkingMasterToken', resp.data.Token)
            sessionStorage.setItem('ParkingMasterUsername', resp.data.Username)
            sessionStorage.setItem('ParkingMasterRole', resp.data.parkingMasterRole)
            this.$router.push('/Home')
          })
          .catch(e => {
            console.log(e.response)
          })
      }
    }
  },
  beforeMount: function () {
    this.login()
  }
}
</script>

<style>
.form-login {
  max-width: 330px;
  margin: 0 auto;
}
.button-login {
  width: 330px;
  margin: 0 auto;
}
</style>
