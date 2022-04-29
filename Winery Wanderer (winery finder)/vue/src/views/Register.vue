<template>
  <div id="register" class="text-center">
    <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
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
      <input
        type="password"
        id="confirmPassword"
        class="form-control"
        placeholder="Confirm Password"
        v-model="user.confirmPassword"
        required
      />
      <router-link id="loginPage" :to="{ name: 'login' }">Have an account?</router-link>
      <button id="createButton" class="btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
    </form>
  </div>
</template>

<script>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/profile',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style scoped>
h1{
  font-size: 50px;
  font-family: 'Waterfall', Arial, Helvetica, sans-serif;
}
#register{
  padding-top: 70px;
  padding-left: 39%;
}
form {
  font-family: "Lucida Sans", "Lucida Sans Regular", "Lucida Grande",
    "Lucida Sans Unicode", Geneva, Verdana, sans-serif;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 30px auto;
  background: rgba(252, 253, 252, 0.4);
  padding: 40px;
  margin-right: 65%;
  margin-bottom: 5%;
  border-radius: 15px;
  border-block-width: 5px;
  border-style: outset;
  font-size: 25px;
}
input:hover {
 background-color:gold;
}
#loginPage {
  text-decoration: none;
  color: ghostwhite
}
#loginPage:hover {
  color: goldenrod;
  font-weight: bold;
}
#createButton {
    width: 50%;
    padding:10px;
    border-radius: 5px;
    color: #000000;
    background-color: #76c08f;
    font-weight: bold;
    text-align: center;
    text-transform: uppercase;
}
#createButton:hover {
   background-color:gold;
}
#password {
  margin-bottom: 20px;
}
#confirmPassword {
  margin-bottom: 20px;
}
  div.image {
    width: 50px;
  }</style>
