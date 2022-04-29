using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IWineDao
    {
        public Wine CreateWine(Wine newWine);
        public List<Wine> GetAllWines();
        public bool DeleteWine(int id);
        public bool UpdateWine(Wine wine);
        public Wine GetWine(string Name);
        public Wine GetWine(int id);
    }
}
