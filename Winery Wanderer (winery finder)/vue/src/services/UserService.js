import axios from 'axios';

export default {
  /**
   * gets all the regular users, ie not admins and owners
   * @returns a list of all regular users
   */
  getRegularUsers(){
    return axios.get('/getusers')
  },
  /**
   * a logged in user can edit their profile information. User cannot edit their role
   * @param {user} user the information of a user to update. Username, password, confirmpassword required
   * @returns A promise indicating if the editions were successfully recorded
   */
  editProfile(user){
    return axios.put('/editprofile', user)
  },
  /**
   * Admin can edit the username, password and role of any specific user
   * @param {number} user the users uniques id
   * @returns a promise indicating if the editions were successful
   */
  editUser(user){
    return axios.put('/edituser', user)
  },
  /**
   * Admin can delete a user
   * @param {number} userId the user's identification number
   * @returns a promise indicating if the deletion was successful
   */
  deleteUser(userId){
    return axios.delete(`/deleteuser/${userId}`)
  },
  /**
   * gives a list of winery ids the user can edit
   * @returns a promise with ids for wineries the user can edit
   */
  wineryAuthentication(){
    return axios.get('/user/wineryAuth')
  }
}
