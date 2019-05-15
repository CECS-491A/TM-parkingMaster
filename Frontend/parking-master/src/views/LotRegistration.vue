<template>
  <div class="view-lot-registration">
     <form class="form-lot-registration">
      <h2 class="form-lot-registration-heading">Lot Registration</h2>
      <v-form ref="form" v-if="!worked">
        <v-text-field id="lotname" v-model="lotname" class="form-control" placeholder="Lot Name" required></v-text-field>
        <v-text-field id="address" v-model="address" class="form-control" placeholder="Address" required></v-text-field>
        <v-text-field id="cost" v-model="cost" class="form-control" placeholder="Cost" required></v-text-field>
        <div class="file-upload-div">
          <label for="spotfile" class="upload-label">
            <span id="spotfiletext">Upload Parking Spot File</span>
            <v-icon right dark>cloud_upload</v-icon>
          </label>
          <input type="file" class="file-upload" id="spotfile" ref="spotfile" accept=".txt,.csv" v-on:change="csvHandler()"/>
        </div>
        <div class="file-upload-div">
          <label for="spotmap" class="upload-label" id="spotfilelabel">
            <span id="mapfiletext">Upload Parking Lot Map</span>
            <v-icon right dark>cloud_upload</v-icon>
          </label>
          <input type="file" class="file-upload" id="spotmap" ref="spotmap" accept=".png" v-on:change="imageHandler()"/>
        </div>
        <v-btn depressed color="primary" v-on:click="submitlot" type="submit" v-if="!worked">Submit Lot</v-btn>
        <v-alert :value="errorOn"
          color="error"
          transition="scale-transition">
            <h3> {{error}} </h3></v-alert>
      </v-form>
    </form>
    <div id="responseMessage" v-if="worked">
        <h2>Successfully added parking lot.</h2>
        <br />
    </div>
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
      map: '',
      lotname: '',
      address: '',
      cost: '',
      error: '',
      errorOn: false,
      worked: false
    }
  },
  methods: {
    async submitlot () {
      this.error = ''
      this.errorOn = false
      let formData = new FormData()
      let token = sessionStorage.getItem('ParkingMasterToken')
      let username = sessionStorage.getItem('ParkingMasterUsername')
      let role = sessionStorage.getItem('ParkingMasterRole')
      if (this.lotname === '' || this.address === '' || this.cost === '') {
        this.error = 'Please fill each field.'
        this.errorOn = true
      } else {
        formData.append('file', this.file)
        formData.append('map', this.map)
        formData.append('lotname', document.getElementById('lotname').value)
        formData.append('address', document.getElementById('address').value)
        formData.append('cost', document.getElementById('cost').value)
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
            this.worked = true
          }.bind(this))
          .catch(e => {
            this.error = 'Failed to register parking lot. Please try again later.'
            this.errorOn = true
            if (e.response.status === 401) {
              auth.invalidSession(this.$router)
            } else if (e.response.status === 418) {
              sessionStorage.setItem('ParkingMasterRole', 'disabled')
              sessionStorage.setItem('ParkingMasterRefresh', 'true')
              this.$router.push('/Home')
            } else if (e.response.status === 419) {
              sessionStorage.setItem('ParkingMasterAcceptedTOS', 'false')
              this.$router.push('/TOS')
            }
          })
      }
    },
    csvHandler () {
      this.file = this.$refs.spotfile.files[0]
      document.getElementById('spotfiletext').textContent = this.$refs.spotfile.files[0].name
    },
    imageHandler () {
      this.map = this.$refs.spotmap.files[0]
      document.getElementById('mapfiletext').textContent = this.$refs.spotmap.files[0].name
    }
  },
  beforeMount: function () {
    auth.authorize('lotmanager', this.$router)
  }
}
</script>

<style>
  .form-lot-registration {
    max-width: 330px;
    margin: 0 auto;
  }
  .file-upload-div {
    width: 330px;
  }
  input[type="file"] {
    width: 0.1px;
    height: 0.1px;
    opacity: 0;
    overflow: hidden;
    position: absolute;
    z-index: -1;
  }
  .upload-label{
    border: 1px solid #ccc;
    display: block;
    padding: 6px 12px;
    cursor: pointer;
    background-color: rgb(95, 95, 95);
    color: white
  }
</style>
