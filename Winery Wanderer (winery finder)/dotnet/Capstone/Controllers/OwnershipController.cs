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
    [Authorize]
    public class OwnershipController : ControllerBase
    {
        IOwnerRequestDao ownerRequestDao;
        public OwnershipController(IOwnerRequestDao _ownerRequestDao)
        {
            ownerRequestDao = _ownerRequestDao;
        }
        [HttpGet("/request/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetRequestById(int id)
        {
            try
            {
                OwnerRequest request = ownerRequestDao.GetRequestById(id);
                if (request != null)
                {
                    return Ok(request);
                }
                return StatusCode(404);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("/request")]
        [Authorize(Roles = "admin")]
        public IActionResult GetRequestById()
        {
            try
            {
                List<OwnerRequest> request = ownerRequestDao.GetRequests();
                if (request != null)
                {
                    return Ok(request);
                }
                return StatusCode(404);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("approve")]
        [Authorize(Roles = "admin")]
        public IActionResult ApprovedOwnerRequest(List<int> requestid)
        {
            try
            {
                foreach(int id in requestid)
                {
                    ownerRequestDao.ApproveRequest(id);
                }
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("decline")]
        [Authorize(Roles = "admin")]
        public IActionResult DeclineOwnerRequest(List<int> requestid)
        {
            try
            {
                foreach (int id in requestid)
                {
                    ownerRequestDao.DeclineRequest(id);
                }
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("claim")]
        [Authorize(Roles = "user,owner")]
        public ActionResult<OwnerRequest> RequestOwnership(OwnerRequest request)
        {
            try
            {
                request.RequesterId = Convert.ToInt32(User.FindFirst("sub")?.Value);
                return ownerRequestDao.CreateRequestToBecomeOwner(request);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
