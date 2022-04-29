<template>
  <router-link
    class="card"
    v-bind:to="{ name: 'winery', params: { wineryID: winery.wineryId } }"
  >
    <div>
      <h2>{{ winery.wineryName }}</h2>
    <div id='winery-details'>
      <p>{{ winery.wineryAddress }}</p>
      <p>
        {{ winery.wineryCity }}, {{ winery.wineryStateAbbr }}
        {{ winery.venueZip }}
      </p>
      <p>{{ winery.wineryPhoneNumber }}</p>
      <p id='description'>
        {{winery.description}}
      </p>
      </div>
      <br />
    </div>
    <div id='winery-image' v-if="winery.image != ''">
      <img :src="winery.image" />
    </div>
    <div id='winery-image' v-if="winery.image == ''">
      <img src="../assets/wine barrels.jpeg" />
    </div>
  </router-link>
</template>
<script>
export default {
  name: "winery-card",
  props: ["winery"],
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
  width: 75%;
  margin-top: 30px;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-decoration: none;
  color: black;
  padding-top: 5px;
  padding-bottom: 20px;
}
.card:hover {
  background: rgba(255, 255, 255, 0.7);
}
.card img {
  max-width: 90%;
  margin: 0px;
  border-radius: 4px;
}
.card h2 {
  font-size: 60px;
  margin: 0px;
  text-align: center;
  font-family: 'Waterfall', Arial, Helvetica, sans-serif;
}
.card p {
  margin: 0px 30px;
  text-align: center;
}
a:visited{
  text-decoration: none;
  color: black;
}
#winery-image {
  display: inline-flex;
  flex-direction: row;
  justify-content: space-around;
  width:700px;
  padding-bottom: 30px;
}
#winery-image > img {
  padding: 0px;
  margin: 0px;
  border-radius: 4px;
}
#winery-details {
    font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
    font-size:150%;
}
#description {
  font-size:90%;
}

</style>