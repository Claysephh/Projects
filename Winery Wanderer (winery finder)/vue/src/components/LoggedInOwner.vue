<template>
<div id="grid">
    <create-winery id='create-winery'></create-winery>
    <edit-winery id='edit-winery'></edit-winery>

    <create-wine id='create-wine'></create-wine>
    <edit-wine id='edit-wine'></edit-wine>

    <claim-winery id="claim-winery"/>
</div>
</template>

<script>
import CreateWinery from "./CreateWinery.vue";
import EditWinery from "./EditWinery";
import CreateWine from "./CreateWine.vue";
import EditWine from "./EditWine";
import ClaimWinery from './ClaimWinery.vue';
export default {
  name:"logged-in-owner",
    components: {
    CreateWinery,
    EditWinery,
    CreateWine,
    EditWine,
        ClaimWinery
  },
};
</script>

<style scoped>

div{
  font-family: 'Courier New', Courier, monospace;
}
#create-winery{
  grid-area: create-winery;
}

#edit-winery{
  grid-area:edit-winery;
}
#create-wine {
  grid-area: create-wine;
}
#claim-winery {
  grid-area:claim-winery;
}

#edit-wine {
  grid-area:edit-wine;

}
#grid{
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas: "create-winery . edit-winery"
                      "create-wine   . edit-wine"
                      "  .   claim-winery   .  ";
}


</style>