using System;
using System.Collections.Generic;
using System.Text; 
using System.Data.SqlClient;

namespace WineryPopulator.DAO
{
    public interface ISqlDao
    {
        public bool LogWineries(List<WineryData> wineries);
        public void CreateDatabase();
        public void CreateWinery(WineryData winery);
        public void FinishDatabase();
    }
}
