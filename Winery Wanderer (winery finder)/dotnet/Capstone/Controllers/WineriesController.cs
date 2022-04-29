using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WineriesController : ControllerBase
    {
        IWineryDao wineryDao;
        public WineriesController(IWineryDao _wineryDao)
        {
            wineryDao = _wineryDao;
        }

        [HttpPost("create")]
        [Authorize(Roles = "admin,owner")]
        public ActionResult<Winery> CreateWinery(Winery winery)
        {
            try
            {
                if(wineryDao.GetWinery(winery.WineryName) != null)
                {
                    return BadRequest(new { message = "There is already a winery by that name." });
                }

                Winery newWinery = AutofillWinery(winery);
                newWinery = wineryDao.CreateWinery(newWinery);
                string user = Convert.ToString(User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value);
                if("owner" == user){
                    int userId = Convert.ToInt32(User.FindFirst("sub")?.Value);
                    wineryDao.MakeOwner(newWinery, userId);
                }
                return StatusCode(201, newWinery);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult<List<Winery>> GetWineries()
        {
            try
            {
                List<Winery> wineries = wineryDao.GetWineries();
                return Ok(wineries);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public ActionResult<List<Winery>> GetWinery(int id)
        {
            try
            {
                Winery w = wineryDao.GetWineryById(id);
                return Ok(w);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("{wineryId}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteWinery(int wineryId)
        {
            ActionResult result = StatusCode(500);
            try
            {
                Winery w = wineryDao.GetWineryById(wineryId);
                if(w == null)
                {
                    result = NotFound(new { message = $"The winery with the id of {wineryId} does not exist." });
                }
                else
                {
                    if (wineryDao.DeleteWinery(wineryId))
                    {
                        result =Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            return result;
        }
        /// <summary>
        /// If any of the given wine fields are left blank, this method populates them
        /// </summary>
        /// <param name="wine">the wine to populate</param>
        /// <returns>a fully populated wine</returns>
        private Winery AutofillWinery(Winery winery)
        {
            if (winery.Image == null)
            {
                winery.Image = string.Empty;
            }
            return winery;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,owner")]
        public ActionResult<Winery> UpdateWinery(Winery wineryToUpdate, int id)
        {
            try
            {
                Winery existingWinery = wineryDao.GetWineryById(id);

                if(existingWinery != null)
                {
                    Winery updatedWinery = wineryDao.UpdateWineryById(wineryToUpdate);
                    return Ok(updatedWinery);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
