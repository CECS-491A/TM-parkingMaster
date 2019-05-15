<template>
  <div class = "toolbar-main">
    <v-toolbar dark color="primary">
      <v-menu :nudge-width="100">
        <template v-slot:activator="{ on }">
          <v-toolbar-title v-on="on">
            <v-toolbar-side-icon></v-toolbar-side-icon>
          </v-toolbar-title>
        </template>

        <v-list>
          <v-list-tile
            v-for="item in items"
            :key="item.name"
            :class="item.class"
            @click="navigate(item.value)"
          >
            <v-list-tile-title v-text="item.name"></v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>

      <v-toolbar-title class="headline text-uppercase">
        <span>ParkingMaster</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>

      <v-toolbar-items class="hidden-sm-and-down" scroll-toolbar-off-screen clipped-right absolute>
          <v-btn
            v-for="item in items"
            :key="item.name"
            :class="item.class"
            @click="navigate(item.value)"
            flat
          >
            {{ item.name }}
          </v-btn>
      </v-toolbar-items>

    </v-toolbar>
  </div>
</template>

<script>
import auth from '@/services/Authorization.js'

export default {
  name: 'NavBar',
  data () {
    return {
      role: sessionStorage.getItem('ParkingMasterRole'),
      home: {name: 'Home', class: 'home-tile', value: '/Home'},
      userDash: {name: 'User Dashboard', class: 'user-dashboard-tile', value: '/UserDashboard'},
      parkingLots: {name: 'Parking Lots', class: 'parking-lots-tile', value: '/ParkingLotDashboard'},
      vehicleReg: {name: 'Vehicle Registration', class: 'vehicle-registration-tile', value: '/VehicleRegistration'},
      lotReg: {name: 'Lot Registration', class: 'lot-registration-tile', value: '/LotRegistration'},
      lotDelete: {name: 'Lot Deletion', class: 'lot-deletion-tile', value: '/LotDeletion'},
      roleChoice: {name: 'Choose Account Type', class: 'role-choice-tile', value: '/RoleChoice'},
      logoutTile: {name: 'Logout', class: 'logout-tile', value: 'logout'},
      items: [],
      authorized: false
    }
  },
  methods: {
    navigate (location) {
      let acceptedTOS = sessionStorage.getItem('ParkingMasterAcceptedTOS')

      if (location === 'logout') {
        auth.logout(this.$router)
      } else if (acceptedTOS === 'false') {
        this.$router.push('/TOS')
      } else {
        this.$router.push(location)
      }
    }
  },
  beforeMount () {
    this.role = sessionStorage.getItem('ParkingMasterRole')
    if (this.role === 'standard') {
      this.items = [
        this.home,
        this.userDash,
        this.parkingLots,
        this.vehicleReg,
        this.logoutTile]
      this.authorized = true
    } else if (this.role === 'lotmanager') {
      this.items = [
        this.home,
        this.userDash,
        this.lotReg,
        this.lotDelete,
        this.logoutTile]
      this.authorized = true
    } else if (this.role === 'unassigned') {
      this.items = [
        this.home,
        this.userDash,
        this.roleChoice,
        this.logoutTile]
      this.authorized = true
    } else if (this.role === 'disabled') {
      this.items = [
        this.home,
        this.logoutTile]
      this.authorized = true
    } else {
      this.items = [
        this.home]
      this.authorized = false
    }
  }
}
</script>

<style>
.logout-tile {
  background: red;
  color: white;
}
</style>
