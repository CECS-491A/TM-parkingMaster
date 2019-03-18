import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Reservation from '@/components/Reservation'
import LotRegistration from '@/components/LotRegistration'
import VehicleRegistration from '@/components/VehicleRegistration'

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
      name: 'LotRegistration',
      component: LotRegistration
    },
    {
      path: '/VehicleRegistration',
      name: 'VehicleRegistration',
      component: VehicleRegistration
    }
  ]
})
