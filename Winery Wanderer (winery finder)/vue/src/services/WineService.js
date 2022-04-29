import axios from 'axios'

export default {
    //wine:{
    //  "wineId": number not necessary when creating,
    //  "name": string not null
    //  "style": string not null
    //  "wineryId": number valid winery id
    //  "image": optional image url
    //  "description": description
    //}

    /**
     * Unauthorized users to get a list of all the wines
     * @returns a promise with the data of all the wines
     */
    getAllWines(){
        return axios.get('/wine')
    },
    /**
     * Unauthorized uses can get any wine by its unique wine identification number
     * @param {number} wineId the identification number associated with a specific wine
     * @returns a promise with the data of a specific wine
     */
    getWine(wineId){
        return axios.get(`/wine/${wineId}`)
    },
    /**
     * Admin or Owner can delete a wine. This is a soft delete
     * @param {number} wineId the identification number associated with a specific wine to delete
     * @returns a promise indicating that the wine was deleted
     */
    deleteWine(wineId){
        return axios.delete(`/wine/${wineId}`)
    },
    /**
     * Admin and owner can edit the information associated with a specific wine
     * @param {number} wineId the identification number associated with a specific wine to update
     * @param {wine} wineId the information of the wine to edit
     * @returns a promise indicating that the wine was edited
     */
    editWine(wineId, wine){
        return axios.put(`wine/${wineId}`, wine)
    },
    /**
     * Admin or Owner can create a new wine and log it in the server
     * @param {wine} wine the information of the wine to create
     * @returns a promise of the data of the new wine
     */
    createWine(wine){
        return axios.post(`wine`, wine)
    }
  }