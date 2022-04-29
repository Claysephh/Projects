using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class OwnerRequest
    {
        public int RequestId { get; set; }
        public int RequesterId { get; set; }
        [Required()]
        public int WineryId { get; set; }
        [Required()]
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public string WineryName { get; set; }
    }
}
