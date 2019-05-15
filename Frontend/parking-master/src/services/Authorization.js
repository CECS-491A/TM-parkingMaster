import axios from 'axios'
import apiCalls from '@/constants/api-calls'

const authorize = (requiredRole, router) => {
  let role = sessionStorage.getItem('ParkingMasterRole')
  let acceptedTOS = sessionStorage.getItem('ParkingMasterAcceptedTOS')

  // Check if user is allowed to access any page
  if (role === 'disabled') {
    router.push('/Home')

  // Check if user has accepted TOS
  } else if (acceptedTOS === 'false') {
    router.push('/TOS')

  // Checking if the user is logged in at all is a special case
  // This is for pages that any user type can access
  } else if (requiredRole === 'authorized') {
    if (role !== 'unauthorized') {
      return true
    } else {
      alert('Unauthorized to access requested page.')
      router.push('/Home')
    }

  // Check if user can access role specific page
  } else if (role === requiredRole) {
    return true

  // Check if user still needs to choose a role
  } else if (role === 'unassigned') {
    router.push('/RoleChoice')

  // Default to unauthorized access
  } else {
    alert('Unauthorized to access requested page.')
    router.push('/Home')
  }
}

const login = (data, router) => {
  // Store all user information
  sessionStorage.setItem('ParkingMasterToken', data.Token)
  sessionStorage.setItem('ParkingMasterUsername', data.Username)
  sessionStorage.setItem('ParkingMasterRole', data.Role)
  sessionStorage.setItem('ParkingMasterAcceptedTOS', data.AcceptedTOS)
  sessionStorage.setItem('ParkingMasterRefresh', 'true')

  // Check were the user should be routed next
  if (!data.AcceptedTOS) {
    router.push('/TOS')
  } else if (data.Role === 'unassigned') {
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
