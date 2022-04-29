import axios from 'axios';


export default {
  /**
   * anonymous user can get a list of all the wineries
   * @returns a list of all wineries
   */
  getWineries(){
    return axios.get('/wineries');
  },
  /**
   * anonymous user can get the information of a specific winery 
   * @param {number} wineryId the unique id of a specific winery
   * @returns a promise with the information of a specific winery
   */
  getWinery(wineryId){
    return axios.get(`/wineries/${wineryId}`);
  },
  /**
   * owner or admin can create a new winery
   * @param {winery} winery the information of a winery to create
   * @returns a promise with data of the created winery
   */
  createWinery(winery){
    return axios.post('/wineries/create', winery);
  },
    /**
   * owner or admin can update winery details
   * @param {number} wineryId the information of a winery to update
   * @returns a promise with data of the updated winery
   */
  updateWineryById(wineryId, winery){
    return axios.put(`/wineries/${wineryId}`, winery);
  }
}
