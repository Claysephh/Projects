<template>
  <div v-on:submit.prevent="" id="edit-wine">
    <form>
      <h2>Edit Wine</h2>
      <div>
        <label>Wine Name: </label>

        <select id="name" type="text" required v-model.number="wineId">
          <option name=""></option>
          <option
            v-for="wine in wines"
            v-bind:key="wine.wineId"
            v-bind:value="wine.wineId"
          >
            {{ wine.name }}
          </option>
        </select>
      </div>
      <div>
        <label>Edit Name: </label>
        <br />
        <input id="country" type="text" v-model="wine.newName" />
      </div>
      <div>
        <label>Style: </label>
        <br />
        <select id="country" type="text" required v-model="wine.style">
          <option value="Sparkling Wine">Sparkling Wine</option>
          <option value="Light-Bodied White Wine">
            Light-Bodied White Wine
          </option>
          <option value="Full-Bodied White Wine">Full-Bodied White Wine</option>
          <option value="Aromatic sweet White Wine">
            Aromatic (sweet) White Wine
          </option>
          <option value="Rose Wine">Rosé Wine</option>
          <option value="Light-Bodied Red Wine">Light-Bodied Red Wine</option>
          <option value="Medium-Bodied Red Wine">Medium-Bodied Red Wine</option>
          <option value="Full-Bodied Red Wine">Full-Bodied Red Wine</option>
          <option value="Dessert Wine">Dessert Wine</option>
        </select>
      </div>
      <div>
        <label>Winery: </label>
        <br />
        <select required v-model="wine.wineryId">
          <option
            v-for="winery in wineries"
            v-bind:key="winery.wineryId"
            v-bind:value="winery.wineryId"
          >
            {{ winery.wineryName }}
          </option>
        </select>
      </div>
      <div>
        <label>Image URL: </label>
        <br />
        <input id="country" type="text" v-model="wine.image" />
      </div>
      <div>
        <label>Description: </label>
        <br />
        <input id="phone" type="text" required v-model="wine.description" />
      </div>
      <div id="button">
        <button type="submit" v-on:click="editWine()" value="Save Wine">
          Save Wine
        </button>
        <button type="submit" v-on:click="deleteWine()" value="Delete Wine">
          Delete Wine
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import WineryService from "../services/WineryService";
import WineService from "../services/WineService";

export default {
  name: "edit-wine",
  data() {
    return {
      wineId: undefined,
      wines: [],
      wineries: [],
    };
  },
  computed: {
    wine() {
      if (!this.wines.find((wine) => wine.wineId == this.wineId)) {
        return {
          newName: "",
          name: "",
          style: "",
          image: "",
          description: "",
        };
      } else {
        let x = this.wines.find((wine) => wine.wineId == this.wineId);
        x.newName = "";
        return x;
      }
    },
  },
  created() {
    WineService.getAllWines()
      .then((response) => {
        let allWines = response.data;
        if (this.$store.state.user.role == "admin") {
          this.wines = allWines;
        }
        if (this.$store.state.user.role == "owner") {
          this.wines = allWines.filter((wine) =>
            this.$store.state.wineries.includes(wine.wineryId)
          );
        }
      })
      .catch((error) => {
        alert(error);
      });
    WineryService.getWineries()
      .then((response) => {
        let allWineries = response.data;
        if (this.$store.state.user.role == "admin") {
          this.wineries = allWineries;
        }
        if (this.$store.state.user.role == "owner") {
          this.wineries = allWineries.filter((winery) =>
            this.$store.state.wineries.includes(winery.wineryId)
          );
        }
      })
      .catch((error) => {
        alert(error);
      });
  },
  methods: {
    clear() {
      this.wineId = undefined;
      location.reload();
    },
    editWine() {
      if (this.wine.newName != "") {
        this.wine.name = this.wine.newName;
        this.wine.newName = "";
      }
      WineService.editWine(this.wineId, this.wine)
        .then((response) => {
          if (response.status === 200) {
            console.log(response);
            alert("WooHoo! You've edited a wine!");
            this.clear();
          }
        })
        .catch((error) => {
          alert(error);
          console.log(error.message);
        });

      
    },
    deleteWine() {
      if (
        confirm(
          "Are you sure you want to delete this card? This action cannot be undone."
        )
      ) {
        WineService.deleteWine(this.wineId)
          .then((response) => {
            if (response.status === 204) {
              alert("Wine successfully deleted, What a Waste?!?!");
              //this.$router.push(`/board/${this.card.boardId}`);
            }
          })
          .catch((error) => {
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
        this.wines = this.wines.filter((wine) => wine.wineId != this.wineId);
        this.clear();
      }
    },
  },
};
</script>



<style scoped>
form {
  font-family: Waterfall;
  margin: 30px auto;
  background: rgba(252, 253, 252, 0.4);
  text-align: left;
  padding: 20px;
  border-radius: 15px;
  border-block-width: 5px;
  border-style: outset;
  font-size: 35px;
  margin-right: 20px;
}
h2 {
  text-align: center;
}
input {
  display: block;
  margin: 0 auto;
  justify-content: center;
  width: 70%;
}
input:hover {
  background-color: gold;
}
select {
  display: block;
  margin: 0 auto;
  justify-content: center;
  width: 73%;
}
#button > button {
  width: 35%;
  padding: 10px;
  border-radius: 5px;
  color: #000000;
  background-color: #76c08f;
  font-weight: bold;
  text-align: center;
  text-transform: uppercase;
  margin: 0%;
  margin-right: 10px;
}

#button > button:hover {
  font-weight: bold;
  background-color: gold;
}
#button {
  display: flex;
  justify-content: center;
  margin-top: 10px;
}
label {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  font-size: 20px;
}
</style>