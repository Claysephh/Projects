<template>
  <div>
    <top-nav/>
    <router-view class='view'/>
    <bottom-nav/>
  </div>
</template>

<script>
import BottomNav from './components/BottomNav.vue'
import TopNav from './components/TopNav.vue'

export default {
    components: { TopNav, BottomNav }
}
</script>

<style>

html, body {
  margin:0px;
  padding:0px;
  background-image: url("../src/assets/katiewinepic.jpg");
  background-size: cover;
  background-attachment: fixed;
  background-repeat: no-repeat;
  padding: 0px;
  padding-bottom: 40px;
}
html {
  overflow-y: scroll;
}
</style>
