<template>
  <form v-on:submit.prevent="claimOwnership()">
    <div>
      <h2>Claim Winery</h2>
      <label>Winery: </label>
      <select required v-model="request.wineryId">
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
      <label>Why are you claiming this winery? </label>
      <input id="phone" type="text" required v-model="request.comment" />
    </div>
    <div id="button">
      <input type="submit" value="Claim" />
    </div>
  </form>
</template>

<script>
import OwnershipService from "@/services/OwnershipService.js";
import WineryService from "@/services/WineryService.js";

export default {
  name: "claim-winery",
  data() {
    return {
      request: {
        wineryId: 0,
        comment: "",
      },
      wineries: {
        // wineryName: "",
        // wineryId: 0,
        // venueId: 0,
        // venueCountry: "",
        // venueAddress: "",
        // venueCity: "",
        // venueStateAbbr: "",
        // venueZip: 0,
        // venuePhoneNumber: "",
      },
    };
  },
  created() {
    WineryService.getWineries()
      .then((response) => {
        this.wineries = response.data;
      })
      .catch((error) => {
        alert(error);
      });
  },
  methods: {
    claimOwnership() {
      this.request.wineryId = parseInt(this.request.wineryId);
      OwnershipService.claimOwnership(this.request)
        .then((response) => {
          if (response.status === 201) {
            console.log(response);
          }
          alert("Success!");
          location.reload();
        })
        .catch((error) => {
          alert(error);
          console.log(error.message);
        });
    },
  },
};
</script>

<style scoped>
form {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  margin: 30px auto;
  background: rgba(252, 253, 252, 0.4);
  text-align: left;
  padding: 20px;
  border-radius: 15px;
  border-block-width: 5px;
  border-style: outset;
  font-size: 35px;
  width: 80%;
  font-size: 30px;
}
input {
  background-color: ghostwhite;
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
  width: 71%;
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
h2 {
  font-family: Waterfall;
  text-align: center;
  font-size: 50px;
}
</style>