<template>
  <div class="view-lot-deletion">
     <form class="form-lot-deletion">
      <h2 class="form-lot-registration-heading">Lot Deletion</h2>
      <option v-for="lot in lots" v-bind:key="lot.id" v-bind:lot= "lot">
        {{lot.id}} {{lot.name}}
      </option>
      <v-form ref="form">
        <v-btn depressed color="blue" v-on:click="deletelot" type="submit">Delete Lot</v-btn>
      </v-form>
    </form>
  </div>
</template>

<script>
import axios from 'axios'
import apiCalls from '@/constants/api-calls'

var lotComponent = {
  props: {
    lot: Object
  },
  template: `
  <div class="lot">
    name: {{lot.name}}
    id: {{lot.id}} 
    cost: {{lot.cost}}
  </div>
  `
}

export default {
  name: 'lotDeletion',
  data () {
    return {
      pageTitle: '',
      msg: 'Our Lot Deletion page.',
      lots: [
        {
          id: 1,
          name: 'Lot 1',
          cost: '2.65'
        },
        {
          id: 2,
          name: 'Lot 2',
          cost: '3.50'
        }
      ]
    }
  },
  components: {
    'lot-component': lotComponent
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
