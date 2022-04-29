<template>
  <div class="card">
    <h2>{{ winery.wineryName }}</h2>
    <p>{{ winery.wineryAddress }}</p>
    <p>
      {{ winery.wineryCity }}, {{ winery.wineryStateAbbr }}
      {{ winery.wineryZip }}
    </p>
    <p>{{ winery.wineryPhoneNumber }}</p>
    <p>
      {{ winery.description }}
    </p>
        <div id='winery-image' v-if="winery.image != ''">
      <img :src="winery.image" />
    </div>
    <div id='winery-image' v-if="winery.image == ''">
      <img src="../assets/wine barrels.jpeg" />
    </div>
    <br />
    <router-link v-bind:to="{ name: 'wineries' }"
      ><p>Back to Wineries List</p></router-link
    >
    <br />
  </div>
</template>
<script>
import WineryService from "../services/WineryService";

export default {
  name: "winery-details",
  data() {
    return {
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
    WineryService.getWinery(this.$route.params.wineryID)
      .then((response) => {
        this.winery = response.data;
      })
      .catch((error) => {
        alert(error);
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
.card h2{
  font-size: 60px;
  margin:0;
  font-family: 'Waterfall', Arial, Helvetica, sans-serif;
}
.card p {
  margin: 0;
  font-size: 25px;
  width:100%;
  text-align: center;
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
</style>