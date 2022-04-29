<template>
  <form v-on:submit.prevent="editWinery()">
    <h2>Edit Winery</h2>

    <div>
      <label>Winery Name: </label>

      <select id="name" type="text" v-model="wineryId" required>
        <option name=""></option>
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
      <label>Edit Name: </label>
      <br />
      <input id="newname" type="text" v-model="winery.newName" />
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
      <input
        id="zip"
        type="number"
        required
        v-model.number="winery.wineryZip"
      />
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
      <label>Image Url: </label>
      <br />
      <input id="phone" type="text" required v-model="winery.image" />
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
import WineryService from "@/services/WineryService.js";

export default {
  name: "edit-winery",

  data() {
    return {
      wineries: [],
      wineryId: undefined,
      oldWinery: {},
      stateAbbr:[ 'AL', 'AK', 'AS', 'AZ', 'AR', 'CA', 'CO', 'CT', 'DE', 'DC', 'FM', 'FL', 'GA', 'GU', 'HI', 'ID', 'IL', 'IN', 'IA', 'KS', 'KY', 'LA', 'ME', 'MH', 'MD', 'MA', 'MI', 'MN', 'MS', 'MO', 'MT', 'NE', 'NV', 'NH', 'NJ', 'NM', 'NY', 'NC', 'ND', 'MP', 'OH', 'OK', 'OR', 'PW', 'PA', 'PR', 'RI', 'SC', 'SD', 'TN', 'TX', 'UT', 'VT', 'VI', 'VA', 'WA', 'WV', 'WI', 'WY', 'N/A' ],

    };
  },
  computed: {
    winery() {
      if (!this.wineries.find((winery) => winery.wineryId == this.wineryId)) {
        return {
          description: "",
          wineryAddress: "",
          wineryCity: "",
          wineryCountry: "",
          wineryName: "",
          wineryPhoneNumber: "",
          wineryStateAbbr: "",
          wineryZip: null,
          image: "",
          newName: "",
        };
      } else {
        let x = this.wineries.find((winery) => winery.wineryId == this.wineryId)
        x.newName = "";
        return x;
      }
    },
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
    editWinery() {
      if (this.winery.newName != "") {
        this.winery.wineryName = this.winery.newName;
        this.winery.newName = "";
      }
      WineryService.updateWineryById(this.winery.wineryId, this.winery)
        .then((response) => {
          if (response.status === 200) {
            console.log(response);
            if (this.$store.state.user.role == "owner") {
              alert("You're winery has been updated!");
            } else {
              alert("The winery was updated.");
            }
            this.clear();
          }
        })
        .catch((error) => {
          alert(error);
          console.log(error.message);
        });
      
    },
    clear() {
      this.winery.wineryId = undefined;
      location.reload();
    },
  },
};
</script>

<style scoped>
#edit-winery {
  font-family: Waterfall;
  margin: 30px auto;
  background: rgba(252, 253, 252, 0.4);
  text-align: left;
  padding: 20px;
  border-radius: 15px;
  border-block-width: 5px;
  border-style: outset;
  font-size: 35px;
  width: 85%;
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
#button > input {
  width: 55%;
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

#name {
  display: block;
  margin: 0 auto;
  justify-content: center;
  width: 73%;
}
label {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  font-size: 20px;
}
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}
input[type="number"] {
  -moz-appearance: textfield;
}
#stateSelect{
   display: block;
  margin: 0 auto;
  justify-content: center;
  width: 70%;
}
</style>