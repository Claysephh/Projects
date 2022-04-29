using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IOwnerRequestDao
    {
        OwnerRequest CreateRequestToBecomeOwner(OwnerRequest request);
        void ApproveRequest(int id);
        void DeclineRequest(int id);
        OwnerRequest GetRequestById(int id);
        List<OwnerRequest> GetRequests();
    }
}
