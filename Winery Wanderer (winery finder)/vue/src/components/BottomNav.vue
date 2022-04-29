<template>
  <div class="bottom-nav">
    <div>
    <a href="https://youtu.be/dQw4w9WgXcQ"
      ><img src="../assets/instagram.png" class="smp"
    /></a>
    <a href="https://youtu.be/dQw4w9WgXcQ"
      ><img src="../assets/facebook.png" class="smp"
    /></a>
    <a href="https://youtu.be/dQw4w9WgXcQ"
      ><img src="../assets/twitter.png" class="smp"
    /></a>
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'wineries' }">Wineries</router-link
      >&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'wines' }">Wine</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'profile' }">Profile</router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "bottom-nav",
};
</script>

<style scoped>
.bottom-nav {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  background: rgba(158, 223, 128, 1);
  width: 100%;
  position: fixed;
  bottom: 0;

  padding:15px;
}
.bottom-nav > div > a {
  text-decoration: none;
  color: ghostwhite;
  padding: 0px 10px 0px 10px;
}
.bottom-nav > div > a:hover {
  color: goldenrod;
  font-weight: bold;
}

.smp {
  height: 30px;
  padding-left: 5px;
  padding-top: 3px;
}
</style>