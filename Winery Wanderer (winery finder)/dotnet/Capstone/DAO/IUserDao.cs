using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        List<ReturnUser> GetRegularUsers();
        User AddUser(string username, string password, string role);
        User GetUser(int id);
        bool Disable(int id);
        RegisterUser Edit(RegisterUser user);
        List<int> GetWineryIdsFromOwnerId(int ownerId);

    }
}
