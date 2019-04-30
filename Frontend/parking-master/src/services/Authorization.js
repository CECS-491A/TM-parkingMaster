import axios from 'axios'
import apiCalls from '@/constants/api-calls'

const authorize = requiredRole => {
  let role = sessionStorage.getItem('ParkingMasterRole')

  if (requiredRole === 'authorized') {
    if (role !== 'unauthorized') {
      return true
    } else {
      alert('Unauthorized to access requested page.')
      window.location.replace(apiCalls.HOME)
    }
  } else if (role === requiredRole) {
    return true
  } else if (role === 'unassigned') {
    window.location.replace(apiCalls.RoleChoice)
  } else {
    alert('Unauthorized to access requested page.')
    window.location.replace(apiCalls.HOME)
  }
}

const logout = () => {
  axios
    .post(apiCalls.LOGOUT, {
      Token: sessionStorage.getItem('ParkingMasterToken')
    })
  sessionStorage.clear()
  sessionStorage.setItem('ParkingMasterRefresh', true)
  sessionStorage.setItem('ParkingMasterRole', 'unauthorized')
  alert('Logout successful.')
  window.location.replace(apiCalls.HOME)
}

const invalidSession = () => {
  sessionStorage.clear()
  sessionStorage.setItem('ParkingMasterRefresh', true)
  alert('Your session is no longer valid.')
  window.location.replace(apiCalls.HOME)
}

export default {
  authorize,
  logout,
  invalidSession
}
