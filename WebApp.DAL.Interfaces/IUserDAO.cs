using WebApp.Common.Entities;
using System.Collections.Generic;

namespace WebApp.DAL.Interfaces
{
    public interface IUserDao
    {
        IEnumerable<User> GetAllUsers();
        int AddUser(User user);
        User GetUser(int userId);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void AddUserAward(int userId, int awardId);
        void DeleteUserAward(int userId, int awardId);
    }
}