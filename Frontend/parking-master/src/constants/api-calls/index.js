const PARKINGMASTER_BASE_URL = 'http://localhost:52324'
const SSO_BASE_URL = 'http://localhost:61348/api'
const HELLO_WORLD = PARKINGMASTER_BASE_URL + '/ParkingMaster/api/testing'
const LOT_REGISTRATION = PARKINGMASTER_BASE_URL + '/ParkingMaster/api/lot/register'
const VEHICLE_REGISTRATION = PARKINGMASTER_BASE_URL + '/ParkingMaster/api/vehicle/register'
const SESSION_CHECK = PARKINGMASTER_BASE_URL + '/api/user/session'
const SSO_LOGIN = SSO_BASE_URL + '/users/login'
const DELETE_FROM_APPS = PARKINGMASTER_BASE_URL + '/api/user/deleteallapps'

export default {
  BASE_URL: PARKINGMASTER_BASE_URL,
  HELLO_WORLD: HELLO_WORLD,
  LOT_REGISTRATION,
  VEHICLE_REGISTRATION,
  SSO_LOGIN,
  SESSION_CHECK,
  DELETE_FROM_APPS
}