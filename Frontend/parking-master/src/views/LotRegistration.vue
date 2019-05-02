<template>
  <div class="view-lot-registration">
     <form class="form-lot-registration">
      <h2 class="form-lot-registration-heading">Lot Registration</h2>
      <v-form ref="form">
        <v-text-field id="lotname" v-model="lotname" class="form-control" placeholder="Lot Name" required></v-text-field>
        <v-text-field id="address" v-model="address" class="form-control" placeholder="Address" required></v-text-field>
        <v-text-field id="cost" v-model="cost" class="form-control" placeholder="Cost" required></v-text-field>
        <!-- <v-text-field label="Spot File" @click='pickFile' v-model="spotfile" prepend-icon='attach_file'></v-text-field> -->
        <!-- <v-text-field label="Map" @click='pickFile' v-model="map" prepend-icon='attach_file'></v-text-field> -->
        <label> Spot File
          <input type="file" id="spotfile" ref="spotfile" v-on:change="csvHandler()"/>
        </label>
        <label> Lot Map
          <input type="file" id="spotmap" ref="spotmap" v-on:change="imageHandler()"/>
        </label>
        <v-btn depressed color="blue" v-on:click="submitlot" type="submit">Submit Lot</v-btn>
      </v-form>
    </form>
  </div>
</template>

<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'
import auth from '@/services/Authorization'

export default {
  name: 'lotRegistration',
  data () {
    return {
      file: '',
      map: ''
    }
  },
  methods: {
    async submitlot () {
      let formData = new FormData()
      let token = sessionStorage.getItem('ParkingMasterToken')
      let username = sessionStorage.getItem('ParkingMasterUsername')
      let role = sessionStorage.getItem('ParkingMasterRole')
      formData.append('file', this.file)
      formData.append('map', this.map)
      formData.append('lotname', document.getElementById('lotname'))
      formData.append('address', document.getElementById('address'))
      formData.append('cost', document.getElementById('cost'))
      formData.append('token', token)
      formData.append('username', username)
      formData.append('role', role)

      await axios
        .put(apiCalls.LOT_REGISTRATION,
          formData,
          {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          }
        )
        .then(function () {
          console.log('OK')
          this.file = ''
          this.map = ''
        })
        .catch(e => {
          auth.invalidSession()
        })
    },
    csvHandler () {
      this.file = this.$refs.spotfile.files[0]
    },
    imageHandler () {
      this.map = this.$refs.spotmap.files[0]
    }
  },
  beforeMount: function () {
    auth.authorize('lotmanager', this.$router)
  }
}
</script>
