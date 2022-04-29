<template>
  <div class="card">
    <h2>{{ wine.name }}</h2>
    <p id='wine-style'>{{ wine.style }}</p>
    <div id='wine-image'>
      <img :src="wine.image" />
    </div>
    <p class='wine-description'>{{ wine.description }}</p>
    
    <p class='winery'>This win is brought to you from {{ winery.wineryName }}.</p>
    <router-link
      v-bind:to="{ name: 'winery', params: { wineryID: winery.wineryId } }"
      >Click here to visit {{ winery.wineryName }}</router-link
    >
    <router-link
      v-bind:to="{ name: 'wines' }"
      >Click here to got back to the wine list</router-link
    >
    <br />
  </div>
</template>
<script>
import WineryService from "../services/WineryService";
import WineService from "../services/WineService";

export default {
  name: "wine-details",
  data() {
    return {
      wine: {
        wineId: 0,
        name: "",
        style: "",
        wineryId: 0,
        image: "",
      },
      winery: {
        wineryName: "",
        wineryId: 0,
        venueId: 0,
        venueCountry: "",
        venueAddress: "",
        venueCity: "",
        venueStateAbbr: "",
        venueZip: 0,
        venuePhoneNumber: "",
      },
    };
  },
  created() {
    WineService.getWine(this.$route.params.wineID).then((response) => {
      this.wine = response.data;
      WineryService.getWinery(this.wine.wineryId)
        .then((response) => {
          this.winery = response.data;
        })
        .catch((error) => {
          alert(error);
        });
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
  flex-direction: column;
  align-items: center;
}
.card h2 {
  font-size: 60px;
  margin: 0px;
  font-family: 'Waterfall', Arial, Helvetica, sans-serif;
}
.card p {
  font-size: 50px;
  margin: 0px 30px;
  text-align: center;
}
.card img {
  padding: 0px;
  margin: 0px;
  border-radius: 4px;
  max-width: 600px;
  max-height: 400px;
}
a {
    padding-top:10px;
    font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
    text-decoration: none;
    color: rgb(119, 119, 119);
    font-size: 30px;
}
.card a:hover {
  color: rgb(16, 95, 55);
  font-weight: bold;
}
.card a:hover {
  color: rgb(16, 95, 55);
  font-weight: bold;
}
#app > div.wine > div > a:nth-child(6){
  text-decoration: none;
}
#app > div.wine > div > a:nth-child(7){
  text-decoration: none;
}
#wine-style {
    font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
    font-size:145%;
}
</style>