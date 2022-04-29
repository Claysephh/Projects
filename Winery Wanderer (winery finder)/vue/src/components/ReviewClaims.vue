<template>
  <div class="container">
    <div>
      <h1>Outstanding Requests</h1>
      <div v-for="request in newRequests" v-bind:key="request.requestId">
        <div class="request">
          <h2>{{ request.username }}</h2>
          <h2>{{ request.wineryName }}</h2>
          <h3>{{ request.comment }}</h3>
          <h3>
            Select
            <input
              type="checkbox"
              v-bind:id="request.requestId"
              v-bind:value="request.requestId"
              v-model="list"
            />
          </h3>
          <br />
        </div>
      </div>
      <div class="submit">
        <button value="Approve" v-on:click="approve()">Approve</button>
        <button value="Deny" v-on:click="deny()">Deny</button>
      </div>
    </div>
  </div>
</template>
<script>
import OwnershipService from "@/services/OwnershipService.js";
export default {
  components: {},
  name: "review-claims",
  data() {
    return {
      list: [],
      requests: {
        requestId: 0,
        requester: "",
        wineryName: "",
        comment: "",
        status: "",
        username: "",
      },
      newRequests: {
        requestId: 0,
        requester: "",
        winery: "",
        comment: "",
        status: "",
        username: "",
      },
    };
  },
  created() {
    OwnershipService.getRequests()
      .then((response) => {
        this.requests = response.data;
        this.newRequests = this.requests.filter(
          (request) => request.status == "pending"
        );
      })

      .catch((error) => {
        alert(error);
      });
  },
  methods: {
    approve() {
      OwnershipService.ApproveOwnerRequest(this.list)
        .then((response) => {
          console.log(response.status);
          if (response.status == 204) {
            alert("The claims were Approved!");
            location.reload();
          }
        })
        .catch((error) => {
          alert(error);
        });
    },
    deny() {
      OwnershipService.DeclineOwnerRequest(this.list)
        .then((response) => {
          console.log(response.status);
          if (response.status == 204) {
            alert("The claims were denied!");
            location.reload();
          }
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>
<style scoped>
.request {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  max-width: 820px;
  margin: 30px auto;
  background: rgba(252, 253, 252, 0.4);
  text-align: left;
  padding: 20px;
  border-radius: 15px;
  border-block-width: 5px;
  border-style: outset;
  width: 80%;
}
h1 {
  font-family: Waterfall;
  background: rgba(252, 253, 252, 0.4);
  margin: 30px auto;
  padding: 20px;
  border-radius: 15px;
  border-block-width: 5px;
  border-style: outset;
  font-size: 55px;
  text-align: center;
  width: 80%;
}
.request input {
  margin: 0px;
}
.submit {
  display: flex;
  justify-content: space-evenly;
}
#current-requests > div > div.submit > button:nth-child(1) {
  width: 40%;
  padding: 10px;
  border-radius: 5px;
  color: #000000;
  background-color: #76c08f;
  font-weight: bold;
  text-align: center;
  text-transform: uppercase;
}
#current-requests > div > div.submit > button:nth-child(2) {
  width: 40%;
  padding: 10px;
  border-radius: 5px;
  color: #000000;
  background-color: #76c08f;
  font-weight: bold;
  text-align: center;
  text-transform: uppercase;
}
#current-requests > div > div.submit > button:hover {
  font-weight: bold;
  background-color: gold;
}
.submit > button {
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

.submit > button:hover {
  font-weight: bold;
  background-color: gold;
}
.submit {
  display: flex;
  justify-content: center;
  margin-top: 10px;
}
</style>