<template>
  <form v-on:submit.prevent="saveWinery()">
    <h2>Create Winery</h2>
    <div>
      <label>Winery Name: </label>
      <br />
      <input id="name" type="text" required v-model="winery.wineryName" />
    </div>
    <div>
      <label>Country: </label>
      <br />
      <input id="country" type="text" required v-model="winery.wineryCountry" />
    </div>
    <div>
      <label>Address: </label>
      <br />
      <input id="address" type="text" required v-model="winery.wineryAddress" />
    </div>
    <div>
      <label>City: </label>
      <br />
      <input id="country" type="text" required v-model="winery.wineryCity" />
    </div>
    <div>
      <label>State: </label>
      <br/>
      <select id="stateSelect" required v-model="winery.wineryStateAbbr">
        <option value=""/>
        <option
          v-for="state in stateAbbr"
          v-bind:key="state"
          v-bind:value="state"
        >
          {{ state }}
        </option>
      </select>
    </div>
    <div>
      <label>Zip Code: </label>
      <br />
      <input id="zip" type="number" required v-model="winery.wineryZip" />
    </div>
    <div>
      <label>Phone Number: </label>
      <br />
      <input
        id="phone"
        type="text"
        required
        v-model="winery.wineryPhoneNumber"
      />
    </div>
    <div>
      <label>Description: </label>
      <br />
      <input id="phone" type="text" required v-model="winery.description" />
    </div>
    <div id="button">
      <input type="submit" value="Save Winery" />
    </div>
  </form>
</template>

<script>
import wineryService from "@/services/WineryService.js";

export default {
  name: "create-winery",
  data() {
    return {
      winery: {
        wineryName: "",
        wineryAddress: "",
        wineryCity: "",
        wineryStateAbbr: "",
        wineryZip: null,
        wineryPhoneNumber: "",
        wineryCountry: "",
        description: "",
      },
      newWineries: [],
      stateAbbr:[ 'AL', 'AK', 'AS', 'AZ', 'AR', 'CA', 'CO', 'CT', 'DE', 'DC', 'FM', 'FL', 'GA', 'GU', 'HI', 'ID', 'IL', 'IN', 'IA', 'KS', 'KY', 'LA', 'ME', 'MH', 'MD', 'MA', 'MI', 'MN', 'MS', 'MO', 'MT', 'NE', 'NV', 'NH', 'NJ', 'NM', 'NY', 'NC', 'ND', 'MP', 'OH', 'OK', 'OR', 'PW', 'PA', 'PR', 'RI', 'SC', 'SD', 'TN', 'TX', 'UT', 'VT', 'VI', 'VA', 'WA', 'WV', 'WI', 'WY', 'N/A' ],
    };
  },
  methods: {
    saveWinery() {
      this.winery.wineryZip = parseInt(this.winery.wineryZip);
      wineryService
        .createWinery(this.winery)
        .then((response) => {
          if (response.status === 201) {
            console.log(response);
            this.id = response.data.wineryId
            let newWineries = this.$store.state.wineries;
            newWineries.push(this.id);
            this.$store.commit("SET_WINERIES", newWineries);
            alert("WooHoo! You've created a winery!");
            location.reload();
          }
        })
        .catch((error) => {
          alert(error);
          console.log(error.message);
        });
      this.winery.wineryName = "";
      this.winery.wineryCountry = "";
      this.winery.wineryAddress = "";
      this.winery.wineryCity = "";
      this.winery.wineryStateAbbr = "";
      this.winery.wineryZip = "";
      this.winery.wineryPhoneNumber = "";
      this.winery.description = "";
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
  background-color: gold;
}
#button > input {
  width: 50%;
  padding: 10px;
  border-radius: 5px;
  color: #000000;
  background-color: #76c08f;
  font-weight: bold;
  text-align: center;
  text-transform: uppercase;
}
#button > input:hover {
  font-weight: bold;
  background-color: gold;
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

#zip input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

#zip input[type="number"] {
  -moz-appearance: textfield;
}
#stateSelect{
   display: block;
  margin: 0 auto;
  justify-content: center;
  width: 70%;
}
</style>