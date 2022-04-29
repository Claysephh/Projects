import axios from 'axios';

export default {
  //Methods available to all anonymous and logged in users
  //Allows user to log in
  login(user) {
    return axios.post('/login', user)
  },
  //registers a new user
  register(user) {
    return axios.post('/register', user)
  }
}
