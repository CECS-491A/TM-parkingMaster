<template>
  <div class="view-role-choice">
     <form class="form-role-choice">
      <h2 class="form-role-choice-heading">User: {{ user }}</h2>
      <h3 class="form-role-choice-prompt.">Before using Parking Master, you must select what type of user you wish to be.</h3>
      <v-form ref="form">
        <v-select v-model="selectedRole"
          label="Select"
          hint="Select your desired role."
          :items="roles"
          item-text="name"
          item-value="value"
          persistent-hint
          offset-y></v-select>

        <v-btn class="button-role-select"
          color="primary"
          v-on:click="submitRole">Confirm Role</v-btn>

        <v-alert
          :value="errorOn"
          color="error"
          transition="scale-transition"><h3> {{error}} </h3></v-alert>
      </v-form>
    </form>
  </div>
</template>
<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization.js'

export default {
  name: 'roleChoice',
  data () {
    return {
      user: sessionStorage.getItem('ParkingMasterUsername'),
      selectedRole: '',
      error: '',
      errorOn: false,
      roles: [{name: 'Standard User', value: 'standard'},
        {name: 'Parking Lot Manager', value: 'lotmanager'}]
    }
  },
  methods: {
    submitRole () {
      this.error = ''
      this.errorOn = false
      axios
        .post(apiCalls.ROLE_SELECTION, {
          Token: sessionStorage.getItem('ParkingMasterToken'),
          Role: this.selectedRole
        })
        .then(resp => {
          auth.roleChange(resp.data.Role, this.$router)
        })
        .catch(e => {
          console.log(e)
          this.error = 'Failed to set your account type.'
          this.errorOn = true
          if (e.response.status === 401) {
            auth.invalidSession(this.$router)
          }
        })
    }
  },
  beforeMount () {
    auth.authorize('unassigned', this.$router)
  }
}
</script>

<style>
.form-role-choice {
  max-width: 330px;
  margin: 0 auto;
}
.button-role-choice {
  width: 330px;
  margin: 0 auto;
}
</style>
