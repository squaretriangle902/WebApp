﻿using WebApp.Common.Entities;
using WebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace WebApp.DAL.File
{
    public struct UserAward
    {
        public int userID;
        public int awardID;

        public UserAward(int userID, int awardID)
        {
            this.userID = userID;
            this.awardID = awardID;
        }
    }

    public class FileUserDAO
    {
        private readonly IAwardDao awardDAO;

        public int MaxUserID { get; private set ;}

        public FileUserDAO(IAwardDao awardDAO)
        {
            InitializeFileSources();
            this.awardDAO = awardDAO;
            var users = GetAllUsers().ToList();
            MaxUserID = users.Any() ? users.Max(user => user.Id) : 0;
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return System.IO.File.ReadAllLines(Common.UserFileLocation).
                    Select(line => line.Split(',')).
                    Select(strArr => new User(int.Parse(strArr[0]), strArr[1], DateTime.Parse(strArr[2])));
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot get all users", exception);
            }
        }

        public void DeleteUser(int userID)
        {
            try
            {
                var tempFile = Path.GetTempFileName();
                var linesToKeep = System.IO.File.ReadLines(Common.UserFileLocation).
                    Where(l => l.Split(',')[0] != userID.ToString());
                System.IO.File.WriteAllLines(tempFile, linesToKeep);
                System.IO.File.Delete(Common.UserFileLocation);
                System.IO.File.Move(tempFile, Common.UserFileLocation);
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot delete user", exception);
            }
        }

        public void AddUserAwards(IEnumerable<User> users)
        {
            try
            {
                var databaseUserAwardEntries = System.IO.File.ReadAllLines(Common.UsersAwardsFileLocation).
                    Select(line => line.Split(',')).
                    Select(array => new UserAward(int.Parse(array[0]), int.Parse(array[1])));

                var inputUserAwardEntries = users.Select(user => new
                {
                    userID = user.Id,
                    awardIDs = user.GetAwards().Select(award => award.Id)
                }).SelectMany(x => x.awardIDs.Select(awardID => new UserAward(x.userID, awardID)));

                var newUserAwardEntryStrings = inputUserAwardEntries.Except(databaseUserAwardEntries).
                    Select(userAward => UserAwardDatabaseEntryString(userAward)).ToList();

                System.IO.File.AppendAllLines(Common.UsersAwardsFileLocation, newUserAwardEntryStrings);
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add award to user", exception);
            }
        }

        public void AddUsers(IEnumerable<User> users)
        {
            try
            {
                var databaseIDs = GetAllUsers().Select(user => user.Id);
                System.IO.File.AppendAllLines(Common.UserFileLocation,
                    UserDatabaseEntryStrings(users.Where(user => !databaseIDs.Contains(user.Id))));
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add award", exception);
            }
        }

        private IEnumerable<string> UserDatabaseEntryStrings(IEnumerable<User> users)
        {
            var userDatabaseEntries = new List<string>();
            foreach (var user in users)
            {
                user.Id = ++MaxUserID;
                userDatabaseEntries.Add(UserDatabaseEntryString(user));
            }
            return userDatabaseEntries;
        }

        private static void InitializeFileSources()
        {
            Common.CreateTableIfNotExists(Common.UserFileLocation);
            Common.CreateTableIfNotExists(Common.UsersAwardsFileLocation);
        }

        private static string UserDatabaseEntryString(User user)
        {
            return string.Format("{0},{1},{2}", user.Id.ToString(), user.Name, user.BirthDate.ToShortDateString());
        }

        private static string UserAwardDatabaseEntryString(UserAward userAward)
        {
            return string.Format("{0},{1}", userAward.userID.ToString(), userAward.awardID.ToString());
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void AddUserAward(int userId, int awardId)
        {
            throw new NotImplementedException();
        }

        public string ImagePath(int imageId)
        {
            throw new NotImplementedException();
        }
    }
}