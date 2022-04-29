<template>
  <div class="wine">
    <wine-details v-bind:wine="wine"/>
  </div>
  
    <!-- <router-link v-bind:to="{ name: 'add-wine-review', params: { id: wine.id}}">Add Review</router-link> -->
    
  <!-- <add-wine-review-list/> -->
    
  
</template>
<script>
 import WineDetails from '../components/WineDetails.vue';
// import AddWineReviewList from "../components/AddWineReviewList.vue"

export default {
  name: "wine",
  components: { WineDetails }
  }
  
</script>
<style scoped>
.wine{
    display: flex;
    flex-direction: column;
    align-items: center;
}
</style>
