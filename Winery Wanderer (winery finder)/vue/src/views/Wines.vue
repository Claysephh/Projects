<template>
  <div class="wines">
    <h1>Search By Wine Name</h1>
<input type="text" id="wineNameFilter" v-model="filter.name" />
    <h1>Search By Wine Style</h1>
    <div>
    <select type="text" id="wineFlavorFilter"  v-model="filter.style">
      <option value="">Select All</option>
      <option v-for="style in styles" v-bind:key="style" :value="style">{{style}}</option>
    </select>

    </div>

    <wine-card
      class="wine"
      v-for="wine in filteredList"
      v-bind:key="wine.wineId"
      v-bind:wine="wine"
    />

    <!-- <router-link v-bind:to="{ name: 'add-wine-review', params: { id: wine.id}}">Add Review</router-link> -->
  </div>
</template>
<script>
import WineCard from "../components/WineCard.vue";
import WineService from "../services/WineService";
//import WineDetails from "../components/WineDetails.vue";
//import AddWineReview from "../components/AddWineReview.vue"
export default {
  name: "all-wineries",
  props: ["wine"],
  components: { WineCard },
  data() {
    return {
      wines: [
         {
          wineId: 0,
          name: "",
          style: "",
          wineryId: 0,
          image: "",
        },
      

      ],
      styles:[],
       filter: {
          name: "",
          style: "",
        },

    };
  },
  created() {
    WineService.getAllWines()
      .then((response) => {
        this.wines = response.data;
        this.wines.forEach((wine) => {
          if(!this.styles.includes(wine.style)){
            this.styles.push(wine.style)
          }
        })
        
      })
      .catch((error) => {
        alert(error);
      });
  },
  computed: {
    filteredList() {
      let filteredWines = this.wines.filter((wine) => 
         wine.name.toLowerCase().replace(' ', '').includes(this.filter.name.toLowerCase().replace(' ', '')) 
         && wine.style.toLowerCase().replace(' ', '').includes(this.filter.style.toLowerCase().replace(' ', ''))
      );
      
      return filteredWines;
    },
  },
};
</script>
<style scoped>
.wines {
  display: flex;
  flex-direction: column;
  align-items: center;
}
select {
  width: 200px;
}
</style>