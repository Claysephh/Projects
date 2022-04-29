<template>
  <form v-on:submit.prevent="saveWine()">
    <h2>Create Wine</h2>
    <div>
      <label>Wine Name: </label>
      <br>
      <input id="name" type="text" required v-model="wine.name" />
    </div>
    <div>
      <label>Style: </label>
      <br>
      <select id="country" type="text" required v-model="wine.style" >
        <option value="Sparkling Wine">Sparkling Wine</option>
        <option value="Light-Bodied White Wine">Light-Bodied White Wine</option>
        <option value="Full-Bodied White Wine">Full-Bodied White Wine</option>
        <option value="Aromatic sweet White Wine">Aromatic (sweet) White Wine</option>
        <option value="Rose Wine">Rosé Wine</option>
        <option value="Light-Bodied Red Wine">Light-Bodied Red Wine</option>
        <option value="Medium-Bodied Red Wine">Medium-Bodied Red Wine</option>
        <option value="Full-Bodied Red Wine">Full-Bodied Red Wine</option>
        <option value="Dessert Wine">Dessert Wine</option>
      </select>
    </div>
    <div>
      <label>Winery: </label>
      <br>
      <select required v-model="wine.wineryID">
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
      <br>
      <input id="country" type="text" v-model="wine.image" />
    </div>
    <div>
      <label>Description: </label>
      <br>
      <input id="phone" type="text" required v-model="wine.description" />
    </div>
    <div id='button'>
    <input type="submit" value="Save Wine" />
    </div>
  </form>
</template>

<script>
import wineService from "@/services/WineService.js";
import WineryService from "@/services/WineryService.js";

export default {
  name: "create-wine",
  data() {
    return {
      wine: {
        // wineId: 0,
        name: "",
        style: "",
        wineryId: -1,
        image: "",
        description: "",
      },
      wineries: {
        wineryName: "",
        // wineryId: 0,
        wineryId: 0,
        wineryCountry: "",
        wineryAddress: "",
        wineryCity: "",
        wineryStateAbbr: "",
        wineryZip: 0,
        wineryPhoneNumber: "",
      },
      stateAbbr:[ 'AL', 'AK', 'AS', 'AZ', 'AR', 'CA', 'CO', 'CT', 'DE', 'DC', 'FM', 'FL', 'GA', 'GU', 'HI', 'ID', 'IL', 'IN', 'IA', 'KS', 'KY', 'LA', 'ME', 'MH', 'MD', 'MA', 'MI', 'MN', 'MS', 'MO', 'MT', 'NE', 'NV', 'NH', 'NJ', 'NM', 'NY', 'NC', 'ND', 'MP', 'OH', 'OK', 'OR', 'PW', 'PA', 'PR', 'RI', 'SC', 'SD', 'TN', 'TX', 'UT', 'VT', 'VI', 'VA', 'WA', 'WV', 'WI', 'WY' ],

    };
  },
  created() {
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
    saveWine() {
      this.wine.wineryId = parseInt(this.wine.wineryId);
      wineService
        .createWine(this.wine)
        .then((response) => {
          if (response.status === 201) {
            alert("Wine Created, Congratulations");
             location.reload();

          }
        })
        .catch((error) => {
          alert(error);
          console.log(error.message);
        });
    
        this.wine.name = "";
        this.wine.style = "clear";
        this.wine.wineryID = "clear";
        this.wine.image = "";
        this.wine.description = "";
    },
  },
};

</script>

<style scoped>
form {
  font-family: Waterfall;
  margin: 35px auto;
  background: rgba(252, 253, 252, 0.4);
  text-align: left;
  padding: 20px;
  border-radius: 15px;
  border-block-width: 5px;
  border-style: outset;
  font-size: 35px;
  width: 80%;
}
h2 {
  text-align: center;
}
input {
  display: block;
  margin: 0 auto;
  justify-content: center;
  width:70%;
}
input:hover {
  background-color:gold;
}
select {
  display: block;
  margin: 0 auto;
  justify-content: center;
  width:71%;
}
#button > input {
    width: 50%;
    padding:10px;
    border-radius: 5px;
    color: #000000;
    background-color: #76c08f;
    font-weight: bold;
    text-align: center;
    text-transform: uppercase;
}
#button > input:hover {
  font-weight: bold;
  background-color:gold;
}
#button {
  display: flex;
  justify-content: center;
  margin-top: 10px;
}
label {
  font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
  font-size: 20px;;
}
</style>