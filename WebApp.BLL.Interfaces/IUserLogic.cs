using WebApp.Common.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace WebApp.BLL.Interfaces
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int userId);
        int AddUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void AddUserAward(int userId, int awardId);
        void DeleteUserAward(int userId, int awardId);
        IEnumerable<Award> GetUserAwards(int userId);
        void SetImage(User user, Image image);
        Image GetImage(int imageId, int width, int height);
    }
}