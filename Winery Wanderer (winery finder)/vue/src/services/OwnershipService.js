import axios from 'axios';

export default {
  /**
  * User or Owner can send a request to claim the ownership of a winery
  * @param {ownerShipRequest} ownerRequest The information needed to claim ownership of a winery
  * @returns a promise with data of a ownerShipRequest
  */
  claimOwnership(ownerRequest){
    return axios.post('ownership/claim', ownerRequest)
  },
  /**
   * Admin can get all the outstanding, approved or declined ownership requests
   * @returns a promise with all of the ownership requests
   */
  getRequests(){
    return axios.get('/request')
  },
  /**
   * Admin can get a specific request
   * @param {number} requestId the ownershipRequest id of a specific request
   * @returns a promise with data of a specific ownership request
   */
  getRequestById(requestId){
   return axios.get(`/request/${requestId}`)
  },
  /**
   * Admin can approve a user to become an Owner. The user will become an owner after the request is approved
   * @param {number} requestId the identification number of a specific request
   * @returns a promise indicating if the approval was successful
   */
  ApproveOwnerRequest(requestId){
    return axios.put(`/ownership/approve`, requestId)
  },
  /**
   * Admin can decline the request of a user to become an owner
   * @param {number} requestId the id of a specific request
   * @returns a promise indicating if the request was successfully denied
   */
  DeclineOwnerRequest(requestId){
    return axios.put(`/ownership/decline`, requestId)
  }
}
