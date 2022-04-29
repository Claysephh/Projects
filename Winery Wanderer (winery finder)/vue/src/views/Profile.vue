<template>
  <div class="container">
    
    <div class="background">
      <div v-if="$store.state.token == ''">
        <login-component />
      </div>
       <div v-if="$store.state.user.role === 'user'">
          <logged-in-user />
        </div>

        <div v-if="$store.state.user.role === 'owner'">
          <logged-in-owner />
        </div>

        <div id=logged-in-admin v-if="$store.state.user.role === 'admin'">
          <logged-in-admin />
        </div>

        <div v-if="$store.state.user.role === 'user'">
          <!--<view-my-comments/>-->
        </div>
    </div>
   
  </div>
</template>
        
<script>
import LoginComponent from "../components/LoginComponent.vue";
import LoggedInUser from "../components/LoggedInUser.vue";
import LoggedInOwner from "../components/LoggedInOwner.vue";
import LoggedInAdmin from "../components/LoggedInAdmin.vue";
//import UserLogged from '../components/UserLogged.vue';


export default {
  name: "profile",
  components: {
    LoginComponent,
    LoggedInUser,
    LoggedInOwner,
    LoggedInAdmin,
    //   UserLogged
  },
  data() {
    return {};
  },
  created() {},
};
</script>

<style scoped>
.container {
  display: flex;
  justify-content: center;
}
.background {
  margin-top: 50px;
  width: 100%;
  border-radius: 5px;
  display: flex;
  justify-content: center;
  align-content: center;
}

#logged-in-admin{

  width:100%;

}


</style>