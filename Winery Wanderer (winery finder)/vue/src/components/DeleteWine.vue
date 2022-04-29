<template>
  <button id="delete-wine" v-on:submit.prevent="deleteWine()">
    Delete Wine
  </button>
</template>

<script>
import WineService from '../services/WineService';
export default {
  name: "delete-wine",
  data() {
    return {
      wine: {
        wine_id:"",
        name: "",
        style: "",
        wineryId: -1,
        image: "",
        description: "",
      },
    };
  },

  methods: {
    deleteCard() {
      if (
        confirm(
          "Are you sure you want to delete this card? This action cannot be undone."
        )
      ) {
        WineService
          .deleteWine(this.wine_id)
          .then(response => {
            if (response.status === 200) {
              alert("Wine successfully deleted, What a Waste?!?!");
              //this.$router.push(`/board/${this.card.boardId}`);
            }
          })
          .catch(error => {
            if (error.response) {
              this.errorMsg =
                "Error deleting Wine. Phew, that was close. Response received was '" +
                error.response.statusText +
                "'.";
            } else if (error.request) {
              this.errorMsg =
                "Error deleting wine. Server could not be reached.";
            } else {
              this.errorMsg =
                "Error deleting wine. Request could not be created.";
            }
          });
      }
    },
  },
};
</script>



<style>
#delete-wine {
    width: 35%;
    padding:10px;
    border-radius: 5px;
    color: #000000;
    background-color: #76c08f;
    font-weight: bold;
    text-align: center;
    text-transform: uppercase;
    
}
#delete-wine:hover {
    font-weight: bold;
    background-color:gold;
}
</style>