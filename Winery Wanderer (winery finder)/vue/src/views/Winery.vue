<template>
  <div class="wineries">
    <winery-details/>
  </div>

  
</template>
<script>
import WineryDetails from '../components/WineryDetails.vue';

export default {
  name: "winery",
  components: { WineryDetails }
}
  
</script>
<style scoped>
.wineries{
    display: flex;
    flex-direction: column;
    align-items: center;
}

</style>
