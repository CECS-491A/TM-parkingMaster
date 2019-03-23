import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home'
import Reservation from '@/views/Reservation'
import LotRegistration from '@/views/LotRegistration'
import VehicleRegistration from '@/views/VehicleRegistration'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      redirect: '/Home'
    },
    {
      path: '/Home',
      name: 'Home',
      component: Home
    },
    {
      path: '/Reservation',
      name: 'Reservation',
      component: Reservation
    },
    {
      path: '/LotRegistration',
      name: 'lotRegistration',
      component: LotRegistration
    },
    {
      path: '/VehicleRegistration',
      name: 'vehicleRegistration',
      component: VehicleRegistration
    }
  ]
})