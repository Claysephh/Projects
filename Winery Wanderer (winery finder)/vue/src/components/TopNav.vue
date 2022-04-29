<template>
  <div class="nav"> 
    <div id="logo">
    <img id="wwLogo" src="../assets/WineryWandererLogo.png"/>
    <h1 id="welcome">  Welcome to Winery Wanderer</h1> 
    </div>  
    <div class="nav-links">
      <router-link v-bind:to="{ name: 'home' }"><p>Home</p></router-link
      >&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'wineries' }">Wineries</router-link
      >&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'wines' }">Wine</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'profile' }">Profile</router-link>
      <div v-if="$store.state.token != ''">&nbsp;|&nbsp;</div>
      <router-link
        v-bind:to="{ name: 'logout' }"
        v-if="$store.state.token != ''"
        >Logout</router-link
      >
    </div>
  </div>
</template>

<script>
export default {
  name: "top-nav",
  methods: {
    logout() {
      this.$store.commit("LOGOUT");
    },
  },
};
</script>

<style>
#logo{
  display: flex;
  flex-direction: row;
}
h1{
  font-size: 50px;
  font-family: 'Waterfall', Arial, Helvetica, sans-serif;
}
#welcome {
  font-size: 50px;
  font-family: 'Waterfall', Arial, Helvetica, sans-serif;
  padding-left: 10px;
}
.nav {
  display: flex;
  justify-content: space-between;
  background: rgba(158, 223, 128, 0.5);
  border-radius: 0px;
  padding: 0px 30px;
}
.nav-links {
  grid-area: nav-links;
  display: inline-flex;
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  text-decoration: none;
  align-items: center;
  justify-content: space-between;
}

.nav-links > a {
  text-decoration: none;
  color: ghostwhite;
  padding: 0px 10px 0px 10px;
  font-size: 18px;
  margin-right: 10px;
}

.nav-links > a:hover {
  color: goldenrod;
  font-weight: bold;
}
#wwLogo{
  height: 100px;
  padding-top: 12px;
}

</style>
