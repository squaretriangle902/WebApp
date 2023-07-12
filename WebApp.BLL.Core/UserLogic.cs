using Denis.UserList.Common.Entities;
using Denis.UserList.Common.Libraries;
using Denis.UserList.DAL.Fake;
using Denis.UserList.DAL.File;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denis.UserList.BLL.Core
{
    public class UserLogic : IUserLogic
    {
        private const int maxAge = 150;

        private readonly IUserDAO userDAO;
        private readonly IAwardLogic awardLogic;
        private readonly Dictionary<int, User> userCache;
        private bool isCacheActual;
        private int maxID;

        public UserLogic(IAwardLogic awardLogic)
        {
            userCache = new Dictionary<int, User>();
            isCacheActual = false;
            switch (Common.ReadConfigFile("user_database"))
            {
                case "userDAO":
                    userDAO = new FileUserDAO(awardLogic.AwardDAO);
                    break;
                default:
                    throw new BLLException("Cannot get user database");
            }
            this.awardLogic = awardLogic;
            maxID = userDAO.MaxUserID;
        }

        public void DeleteUser(int userID)
        {
            if (userCache.Remove(userID))
            {
                return;
            }
            try
            {
                userDAO.DeleteUser(userID);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot delete user", exception);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            if (!isCacheActual)
            {
                CacheAddAllUsers();
            }
            foreach (var user in userCache.Values)
            {
                yield return user;
            }
        }

        public User GetUser(int userID)
        {
            return GetUserInternal(userID);
        }

        public IEnumerable<Award> GetUserAwards(int userID)
        {
            foreach (var award in GetUserInternal(userID).GetAwards())
            {
                yield return award;
            }
        }

        public int Add(string name, DateTime birthDate)
        {
            if (IsUserValid(name, birthDate))
            {
                throw new BLLException("Incorrect user state");
            }
            userCache.Add(++maxID, new User(maxID, name, birthDate));
            return maxID;
        }

        public void AddUserAward(int userId, int awardId)
        {
            GetUserInternal(userId).AddAward(awardLogic.GetAward(awardId));
        }

        public void DatabaseUpdate()
        {
            awardLogic.DatabaseUpdate();
            var users = userCache.Values;
            userDAO.AddUsers(users);
            userDAO.AddUserAwards(users);
        }

        private void CacheAddUser(int userID)
        {
            try
            {
                var user = userDAO.GetAllUsers().First(x => x.Id == userID);
                user.AddAwards(awardLogic.GetUserAwards(userID));
                userCache.Add(userID, user);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot find user by ID", exception);
            }
        }

        private void CacheAddAllUsers()
        {
            try
            {
                foreach (var user in userDAO.GetAllUsers())
                {
                    user.AddAwards(awardLogic.GetUserAwards(user.Id));
                    userCache.Add(user.Id, user);
                }
                isCacheActual = true;
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot get all users", exception);
            }
        }

        private User GetUserInternal(int userID)
        {
            if (!userCache.ContainsKey(userID))
            {
                CacheAddUser(userID);
            }
            if (!userCache.TryGetValue(userID, out User user) || user is null)
            {
                throw new BLLException("Cannot find user by ID");
            }
            return user;
        }

        private static bool IsUserValid(string name, DateTime birthDate)
        {
            return string.IsNullOrEmpty(name) || 
                   birthDate >= DateTime.Now || 
                   DateTimeAdditional.CompleteYearDifference(birthDate, DateTime.Now) > maxAge;
        }

        public void Add(User user)
        {
            Add(user.Name, user.BirthDate);
        }

        public void RemoveAward(int userId, int awardId)
        {
            GetUserInternal(userId).RemoveAward(awardLogic.GetAward(awardId));
        }

        public byte[] GetImage(int userId)
        {
            return GetUserInternal(userId).Image;
        }

        public void SetImage(int userId, byte[] image)
        {
            GetUserInternal(userId).Image = image;
        }
    }
}
