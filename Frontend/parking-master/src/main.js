// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import router from './router'
import App from './App'
import Vuetify, {
  VToolbar,
  VApp,
  VBtn,
  VTextField,
  VForm,
  VList,
  VNavigationDrawer,
  VToolbarTitle,
  VSpacer,
  VContent,
  VToolbarItems,
  VCard,
  VFlex,
  VLayout,
  VContainer,
  VSelect,
  VCardTitle,
  VDivider,
  VCardText,
  VDialog,
  VChip,
  VCardActions,
  VAlert,
  VMenu,
  VListTile,
  VListTileTitle,
  VListTileContent,
  VToolbarSideIcon,
  VIcon
} from 'vuetify/lib'
import 'vuetify/src/stylus/app.styl'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import colors from 'vuetify/es5/util/colors'

Vue.use(Vuetify, {
  iconfont: 'md',
  theme: {
    primary: '#4a266f',
    secondary: '#BDBDBD',
    accent: colors.shades.black,
    error: colors.red.accent3
  },
  components: {
    VApp,
    VToolbar,
    VBtn,
    VTextField,
    VForm,
    VList,
    VNavigationDrawer,
    VToolbarTitle,
    VSpacer,
    VContent,
    VToolbarItems,
    VCard,
    VFlex,
    VLayout,
    VContainer,
    VSelect,
    VCardTitle,
    VDivider,
    VCardText,
    VDialog,
    VChip,
    VCardActions,
    VAlert,
    VMenu,
    VListTile,
    VListTileTitle,
    VListTileContent,
    VToolbarSideIcon,
    VIcon
  }
})

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
