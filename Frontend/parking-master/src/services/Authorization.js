import axios from 'axios'
import apiCalls from '@/constants/api-calls'

const authorize = (requiredRole, router) => {
  let role = sessionStorage.getItem('ParkingMasterRole')

  if (requiredRole === 'authorized') {
    if (role !== 'unauthorized') {
      return true
    } else {
      alert('Unauthorized to access requested page.')
      router.push('/Home')
    }
  } else if (role === requiredRole) {
    return true
  } else if (role === 'unassigned') {
    router.push('/RoleChoice')
  } else {
    alert('Unauthorized to access requested page.')
    router.push('/Home')
  }
}

const login = (data, router) => {
  sessionStorage.setItem('ParkingMasterToken', data.Token)
  sessionStorage.setItem('ParkingMasterUsername', data.Username)
  sessionStorage.setItem('ParkingMasterRole', data.Role)
  sessionStorage.setItem('ParkingMasterRefresh', 'true')

  if (data.Role === 'unassigned') {
    router.push('/RoleChoice')
  } else {
    router.push('/Home')
  }
}

const logout = (router) => {
  axios
    .post(apiCalls.LOGOUT, {
      Token: sessionStorage.getItem('ParkingMasterToken')
    })
  sessionStorage.clear()
  sessionStorage.setItem('ParkingMasterRefresh', true)
  sessionStorage.setItem('ParkingMasterRole', 'unauthorized')
  alert('Logout successful.')
  router.push('/Home')
}

const invalidSession = (router) => {
  sessionStorage.clear()
  sessionStorage.setItem('ParkingMasterRefresh', true)
  alert('Your session is no longer valid.')
  router.push('/Home')
}

const roleChange = (role, router) => {
  sessionStorage.setItem('ParkingMasterRole', role)
  sessionStorage.setItem('ParkingMasterRefresh', 'true')

  router.push('/Home')
}

export default {
  authorize,
  login,
  logout,
  invalidSession,
  roleChange
}
