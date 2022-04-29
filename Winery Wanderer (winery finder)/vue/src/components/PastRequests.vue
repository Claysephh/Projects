<template>
  <div class="container">
    <div>
      <h1>Past Requests</h1>
      <div v-if="!show" v-on:click="show = !show" class="submit">
        <button>Show Past Requests</button>
      </div>
      <div v-if="show">
        <div
          v-for="request in oldRequests"
          v-bind:key="request.requestId" 
        >
          <div class="request">
            <h2>{{ request.username }}</h2>
            <h2>{{ request.wineryName }}</h2>
            <h3>{{ request.comment }}</h3>
            <h3>{{ request.status }}</h3>
            <br />
          </div>
        </div>
        <div v-on:click="show = !show" class="submit">
        <button>Hide Past Requests</button>
      </div>
      </div>
    </div>
  </div>
</template>
<script>
import OwnershipService from "@/services/OwnershipService.js";
export default {
  components: {},
  name: "past-requests",
  data() {
    return {
      show: false,
      requests: {
        requestId: 0,
        requester: "",
        wineryName: "",
        comment: "",
        status: "",
        username: "",
      },
      oldRequests: {
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
        this.oldRequests = this.requests.filter(
          (request) =>
            request.status == "approved" || request.status == "declined"
        );
      })
      .catch((error) => {
        alert(error);
      });
  },
  methods: {
    // approve() {
    //   this.list.forEach((element) => {
    //     OwnershipService.ApproveOwnerRequest(element).then((response) => {
    //       console.log(response.status);
    //     });
    //   });
    // },
    // deny() {
    //   this.list.forEach((element) => {
    //     OwnershipService.DeclineOwnerRequest(element).then((response) => {
    //       console.log(response.status);
    //     });
    //   });
    // },
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
  margin-right: 20px;
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
  margin-right: 20px;
}

.request input {
  margin: 0px;
}
.submit {
  display: flex;
  justify-content: space-evenly;
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