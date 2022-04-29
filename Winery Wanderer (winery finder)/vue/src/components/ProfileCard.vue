<template>
  <router-link
    class="card"
    v-bind:to="{ name: 'user', params: { userID: user.userId } }"
  >
    
   
  
  </router-link>
</template>
<script>
export default {
  name: "profile-card",
  props: ["user"],
  data() {
    return {};
  },
  created() {},
};
</script>
<style scoped>
.card {
  background: rgba(255, 255, 255, 0.5);
  border-radius: 5px;
  /*background: rgba(191, 64, 191, .58);
    border-radius: 100%;
    */
  /*background-image: url('../assets/WineBottle.png');*/
  width: 75%;
  margin-top: 30px;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-decoration: none;
  color: black;
}
.card:hover {
  background: rgba(255, 255, 255, 0.7);
}
.card img {
  max-width: 90%;
  margin: 10px;
}
.card h2 {
  font-size: 60px;
  margin: 0px;
  text-align: center;
}
.card p {
  font-size: 30px;
  margin: 0px 30px;
  text-align: center;
}
.card:visited{
  text-decoration: none;
  color: black;
}
#winery-image {
  display: inline-flex;
  flex-direction: row;
  justify-content: space-around;
}
#winery-image > img {

  border: 5px solid rgba(158, 223, 128, 0.7);
  padding: 0px;
  margin: 0px;
  border-radius: 4px;
}

</style>