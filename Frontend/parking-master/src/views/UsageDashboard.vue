<template>
    <v-layout row wrap>
            <v-flex xs12>
                <v-toolbar-title>Average succesful login per month vs Total registered users</v-toolbar-title>
            </v-flex>
            <v-flex xs12>
                <AverageSuccessfulLogin></AverageSuccessfulLogin>
            </v-flex>
            <v-flex xs12>
                <v-toolbar-title>Success Vs Failed Logins</v-toolbar-title>
            </v-flex>
            <v-flex xs12>
                <SuccessVsFailedLogin></SuccessVsFailedLogin>
            </v-flex>
    </v-layout>
</template>

<script>
import AverageSuccessfulLogin from '@/components/dashboards/AverageSuccessfulLogin'
import SuccessVsFailedLogin from '@/components/dashboards/SuccessVsFailedLogin'
import AverageSession from '@/components/dashboards/AverageSession'
import Top5MostUsed from '@/components/dashboards/Top5MostUsed'
import Top5PageSessions from '@/components/dashboards/Top5PageSessions'
import AverageSessionTimeline from '@/components/dashboards/AverageSessionTimeline'
import LoggedInTimeline from '@/components/dashboards/LoggedInTimeline'

export default{
  name: 'UsageDashboard',
  data () {
    return {
      dataset: null
    }
  },
  components: {
    AverageSuccessfulLogin,
    SuccessVsFailedLogin,
    AverageSession,
    Top5MostUsed,
    Top5PageSessions,
    AverageSessionTimeline,
    LoggedInTimeline
  }
}
</script>

<style>
</style>
