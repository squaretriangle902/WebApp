using WebApp.Common.Entities;
using WebApp.Common.Libraries;
using WebApp.DAL.Interfaces;
using WebApp.DAL.File;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.BLL.Interfaces;
using WebApp.DAL.SQL;
using System.Drawing;

namespace WebApp.BLL.Core
{
    public class UserLogic : IUserLogic
    {
        private const int MaxAge = 150;

        private readonly IUserDao userDao;
        private readonly IAwardLogic awardLogic;
        private readonly IImageLogic imageLogic;

        public UserLogic(IAwardLogic awardLogic, IUserDao userDao, IImageLogic imageLogic)
        {
            this.awardLogic = awardLogic;
            this.userDao = userDao;
            this.imageLogic = imageLogic;
        }
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return userDao.GetAllUsers();
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot get all users", exception);
            }
        }

        public User GetUser(int userId)
        {
            try
            {
                return userDao.GetUser(userId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot get user by ID", exception);
            }
        }

        public IEnumerable<Award> GetUserAwards(int userId)
        {
            try
            {
                return awardLogic.GetUserAwards(userId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot get award by user ID", exception);
            }
        }

        public void AddUserAward(int userId, int awardId)
        {
            try
            {
                userDao.AddUserAward(userId, awardId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot add award to user", exception);
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                userDao.DeleteUser(userId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot delete user", exception);
            }
        }

        public int AddUser(User user)
        {
            if (IsUserInvalid(user))
            {
                throw new BLLException("User is invalid");
            }
            try
            {
                var userId = userDao.AddUser(user);
                return userId;
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot add user", exception);
            }
        }

        public void DeleteUserAward(int userId, int awardId)
        {
            try
            {
                userDao.DeleteUserAward(userId, awardId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot remove award from user", exception);
            }
        }
        public void UpdateUser(User user)
        {
            try
            {
                userDao.UpdateUser(user);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot update user", exception);
            }
        }

        public Image GetImage(int imageId, int width, int height)
        {
            return imageLogic.GetImage(imageId, width, height);
        }

        public void SetImage(User user, Image image)
        {
            if (user.ImageId is int imageId)
            {
                imageLogic.UpdateImage(image, imageId);
                return;
            }
            user.ImageId = imageLogic.SaveImage(image);
        }

        private static bool IsUserInvalid(User user)
        {
            return string.IsNullOrEmpty(user.Name) ||
                   user.BirthDate >= DateTime.Now ||
                   DateTimeAdditional.CompleteYearDifference(user.BirthDate, DateTime.Now) > MaxAge;
        }

    }
}
