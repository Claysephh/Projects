<template>
  <router-link
    class="card"
    v-bind:to="{ name: 'wine', params: { wineID: wine.wineId } }"
  >
  <div class="text">
    <h2>{{ wine.name }}</h2>
    <p class='wine-description'>{{ wine.style }}</p>
    <p class='winery'>From {{ winery.wineryName }}</p>
  </div>
    <div class='wine-image' v-if="wine.image != ''">
      <img :src="wine.image" />
    </div>
    <div class='wine-image' v-else>
      <img src="../assets/woman-drinking-red-wine-vineyard.webp" />
    </div>
    <br />
  </router-link>
</template>
<script>
import WineryService from "../services/WineryService";
export default {
  name: "wine-card",
  props: ["wine"],
  data() {
    return {
      winery: {},
      
    };
  },
  created() {
    WineryService.getWinery(this.wine.wineryId).then((response) => {
      this.winery = response.data;
    });
  },
  
};
</script>
<style scoped>
.card {
  background: rgba(255, 255, 255, 0.5);
  border-radius: 5px;
  width: 75%;
  margin-top: 30px;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-around;
  text-decoration: none;
  color: black;
  height: 300px;
  text-align: center;
}
.card:hover {
  background: rgba(255, 255, 255, 0.7);
}
.card h2 {
  font-size: 60px;
  margin: 0px;
  font-family: 'Waterfall', Arial, Helvetica, sans-serif;
  
}
.text p {
  margin: 0px;
  font-size: 30px;
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande", "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
}
.card img {
  margin: 10px;
  border-radius: 4px;
  max-width: 400px;
  max-height: 250px;
}
.card:visited {
  text-decoration: none;
  color: black;
}
#wine-description {
    font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
    font-size:145%;
}
#winery {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  font-size:120%;
}
#wine-image > img {
  padding: 0px;
  margin: 0px;
  border-radius: 4px;
  max-width: 400px;
  max-height: 250px;
}

</style>