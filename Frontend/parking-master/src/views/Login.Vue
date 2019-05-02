<template>
  <div class = "view-login">
    <form class="form-login">
      <h2 class="form-login-heading">Login</h2>
    </form>
  </div>
</template>
<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'

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
    login () {
      // If token is defined, then there is an attempted login
      if (this.token !== undefined) {
        axios
          .post(apiCalls.SESSION_CHECK, {
            Token: this.token
          })
          .then(resp => {
            auth.login(resp.data, this.$router)
          })
          .catch(e => {
            console.log(e.response)
            this.$router.push('/Home')
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
