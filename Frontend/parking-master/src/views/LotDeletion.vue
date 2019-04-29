<template>
  <div class="view-lot-deletion">
     <form class="form-lot-deletion">
      <h2 class="form-lot-registration-heading">Lot Deletion</h2>
      <v-form ref="form">
        <v-btn depressed color="blue" v-on:click="deletelot" type="submit">Delete Lot</v-btn>
      </v-form>
    </form>
  </div>
</template>

<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'

export default {
  name: 'lotDeletion',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Lot Deletion page.',
      lots: []
    }
  },
  methods: {
    async deletelot () {
      await axios
        .put(apiCalls.LOT_DELETION)
        .then(function () {
          console.log('OK')
        })
        .catch(e => {
          console.log(e.response)
        })
    }
  }
}
</script>
