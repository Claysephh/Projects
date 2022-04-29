<template>
  <div id="grid">
    <create-winery id='create-winery'></create-winery>
    <edit-winery id='edit-winery'></edit-winery>

    <create-wine id='create-wine'></create-wine>
    <edit-wine id='edit-wine'></edit-wine>

    <review-claims class='current-requests'></review-claims>
    <past-requests class='previous-requests'></past-requests>
    

  </div>
</template>

<script>
import CreateWinery from "./CreateWinery.vue";
import EditWinery from "./EditWinery";
import CreateWine from "./CreateWine.vue";
import EditWine from "./EditWine";
import PastRequests from "./PastRequests.vue";
import ReviewClaims from "./ReviewClaims.vue";

export default {
  name: "logged-in-admin",
  components: {
    CreateWinery,
    EditWinery,
    CreateWine,
    EditWine,
    PastRequests,
    ReviewClaims
  },
};
</script>

<style scoped>

#create-winery{
  grid-area: create-winery;
}

#edit-winery{
  grid-area:edit-winery;
}
#create-wine {
  grid-area: create-wine;

}

#edit-wine {
  grid-area:edit-wine;

}
.current-requests {
  grid-area:current-requests;
  font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
}
.previous-requests {
  grid-area:previous-requests;
  font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;

}
#grid{
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas: "create-winery . edit-winery"
                      "create-wine   . edit-wine"
                    "current-requests . previous-requests";


}

</style>