using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IWineryDao
    {
        public Winery GetWinery(string username);
        public Winery CreateWinery(Winery newWinery);
        public List<Winery> GetWineries();
        public Winery GetWineryById(int wineryid);
        public Winery UpdateWineryById(Winery updatedWinery);
        public bool DeleteWinery(int wineryId);
        public bool MakeOwner(Winery winery, int userId);
    }
}

