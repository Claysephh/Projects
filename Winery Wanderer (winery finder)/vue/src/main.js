import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'
//import CreateWinery from './components/CreateWinery'
//import CreateWine from './components/CreateWine'

Vue.config.productionTip = false

//Vue.component('create-winery', CreateWinery);
//Vue.component('create-wine', CreateWine);

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
