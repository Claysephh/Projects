using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Capstone.DAO;
using Microsoft.AspNetCore.Authorization;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WineController : ControllerBase
    {
        private IWineDao wineDao;
        private IWineryDao wineryDao;
        public WineController(IWineDao _wineDao, IWineryDao _wineryDao)
        {
            wineDao = _wineDao;
            wineryDao = _wineryDao;
        }
        [HttpGet]
        public ActionResult<List<Wine>> GetAllWines()
        {
            try
            {
                List<Wine> returnWines = new List<Wine>();
                returnWines = wineDao.GetAllWines();
                return Ok(returnWines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}" });
            }
        }
        [HttpGet("{wineId}")]
        public ActionResult<Wine> GetWineById(int wineId)
        {
            try
            {
                Wine returnWine;
                returnWine = wineDao.GetWine(wineId);
                return Ok(returnWine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}" });
            }
        }
        [HttpDelete("{wineId}")]
        [Authorize(Roles = "admin,owner")]
        public ActionResult DeleteWine(int wineId)
        {
            try
            {
                if(wineDao.GetWine(wineId) == null)
                {
                    return BadRequest(new { message = $"The wine with the an id {wineId} does not exist" });
                }
                if (wineDao.DeleteWine(wineId))
                {
                    return StatusCode(204);
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}" });
            }
        }
        [HttpPut("{wineId}")]
        [Authorize(Roles = "admin,owner")]
        public ActionResult UpdateWine(Wine updateWine, int wineId)
        {
            try
            {
                updateWine.WineId = wineId;
                updateWine = AutofillWine(updateWine);
                bool returnWine = wineDao.UpdateWine(updateWine);
                if (returnWine)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { message = $"The wine with the an id {wineId} does not exist" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}" });
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin,owner")]
        public IActionResult CreateWine(Wine newWine)
        {
            try
            {
                newWine.WineId = -1;
                Wine returnWine = wineDao.CreateWine(newWine);
                return StatusCode(201, returnWine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}" });
            }
        }
        /// <summary>
        /// If any of the given wine fields are left blanck, this method populates them
        /// </summary>
        /// <param name="wine">the wine to populate</param>
        /// <returns>a fully populated wine</returns>
        private Wine AutofillWine(Wine wine)
        {
            if (wine.Image == null)
            {
                wine.Image = string.Empty;
            }
            wine.Status = 1;
            return wine;
        }
    }
}
