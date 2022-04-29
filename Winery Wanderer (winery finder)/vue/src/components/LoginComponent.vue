<template>
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h2 class="h3 mb-3 font-weight-normal">Please Sign In</h2>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <label for="username" class="sr-only">Username</label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      <label for="password" class="sr-only">Password</label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      
      <router-link :to="{ name: 'register' }">Need an account?</router-link>
      <button type="submit">Sign in</button>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login-component",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$store.commit("SET_WINERIES", response.data.wineries);
            this.$router.push("/profile");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>
<style scoped>
h2{
  font-size: 50px;
  font-family: 'Waterfall', Arial, Helvetica, sans-serif;
}
  
form {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  margin: 30px auto;
  background: rgba(252, 253, 252, 0.4);
  padding: 40px;
  margin-right: 65%;
  margin-bottom: 5%;
  border-radius: 15px;
  border-block-width: 5px;
  border-style: outset;
  font-size: 25px;
  width: 100%;
}
input:hover {
 background-color:gold;
}
#login > form > a {
  text-decoration: none;
  color: ghostwhite
}
#login > form > a:hover {
  color: goldenrod;
  font-weight: bold;
}
#sign-im:hover {
   background-color:gold;
}
#login > form > button {
    width: 50%;
    padding:10px;
    border-radius: 5px;
    color: #000000;
    background-color: #76c08f;
    font-weight: bold;
    text-align: center;
    text-transform: uppercase;
}
#login > form > button:hover {
    font-weight: bold;
    background-color:gold;
}
  div.image {
    width: 50px;
  }
 
</style>