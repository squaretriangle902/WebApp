using Denis.UserList.Common.Entities;
using System.Collections.Generic;

namespace Denis.UserList.DAL.Fake
{
    public interface IUserDAO
    {
        int MaxUserID { get; }
        void AddUserAwards(IEnumerable<User> users);
        IEnumerable<User> GetAllUsers();
        void AddUsers(IEnumerable<User> users);
        void DeleteUser(int userID);
    }
}