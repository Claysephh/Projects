<template>
  <div class="wineries">
    <h1>Search Wineries By Name</h1>
    <input type="text" id="wineryNameFilter" v-model="filter.wineryName" />
    <h1>Search By State</h1>
    <div>
      <select
        type="text"
        id="WineryStateFilter"
        v-model="filter.wineryStateAbbr"
      >
        <option value="">Select All</option>
        <option v-for="state in states" v-bind:key="state" :value="state">
          {{ state }}
        </option>
      </select>
    </div>

      <winery-card
        class="winery"
        v-for="winery in filterList"
        v-bind:key="winery.wineryId"
        v-bind:winery="winery"
      />

  </div>
</template>
<script>
import WineryCard from "../components/WineryCard.vue";
import WineryService from "../services/WineryService";
export default {
  name: "all-wineries",
  components: { WineryCard },
  data() {
    return {
      wineries: [
        //{
        // description:"proin nibh nisl condimentum id venenatis a condimentum vitae sapien pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas sed tempus urna et pharetra pharetra massa massa ultricies mi quis hendrerit dolor magna eget est lorem ipsum dolor sit amet consectetur adipiscing elit pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas integer eget aliquet nibh praesent tristique magna sit amet purus gravida quis blandit turpis cursus in hac habitasse platea dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc congue nisi vitae suscipit tellus mauris a diam maecenas sed enim"
        // wineryAddress:"123 Vine St."
        // wineryCity:"Grape Town"
        // wineryCountry:"United States"
        // wineryId:1000
        // wineryName:"Winery1"
        // wineryPhoneNumber:"867-5301"
        // wineryStateAbbr:"OH"
        // wineryZip:10000
        //}
      ],
      states: [],
      filter: {
        wineryName: "",
        wineryStateAbbr: "",
      },
    };
  },
  created() {
    WineryService.getWineries()
      .then((response) => {
        this.wineries = response.data;
        this.wineries.forEach((winery) => {
          if (!this.states.includes(winery.wineryStateAbbr)) {
            this.states.push(winery.wineryStateAbbr);
          }
          this.states = this.states.sort();
        });
      })
      .catch((error) => {
        alert(error);
      });
  },
  computed: {
    filterList() {
      let filteredWineries = this.wineries.filter(
        (winery) =>
          winery.wineryName
            .toLowerCase()
            .replace(" ", "")
            .includes(this.filter.wineryName.toLowerCase().replace(" ", "")) &&
          winery.wineryStateAbbr
            .toLowerCase()
            .replace(" ", "")
            .includes(
              this.filter.wineryStateAbbr.toLowerCase().replace(" ", "")
            )
      );
      return filteredWineries;
    },
  },
};
</script>
<style scoped>
.wineries {
  display: flex;
  flex-direction: column;
  align-items: center;
}
select {
  width: 200px;
}
</style>