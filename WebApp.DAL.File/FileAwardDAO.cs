﻿using WebApp.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebApp.DAL.Interfaces;

namespace WebApp.DAL.File
{
    public class FileAwardDao
    {
        public int MaxAwardID { get; private set; }

        public FileAwardDao()
        {
            InitializeFileSources();
            var awards = GetAllAwards().ToList();
            MaxAwardID = awards.Any() ? awards.Max(award => award.Id) : 0;
        }

        public IEnumerable<Award> GetAllAwards()
        {
            try
            {
                return System.IO.File.ReadAllLines(Common.AwardFileLocation).
                    Select(line => line.Split(',')).
                    Select(strArr => new Award(int.Parse(strArr[0]), strArr[1]));
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot get all awards", exception);
            }
        }

        public IEnumerable<Award> GetUserAwards(int userID)
        {
            try
            {
                var awardsID = System.IO.File.ReadAllLines(Common.UsersAwardsFileLocation).
                    Select(line => line.Split(',')).
                    Where(stringArray => stringArray[0] == userID.ToString()).
                    Select(strArr => strArr[1]);
                return System.IO.File.ReadAllLines(Common.AwardFileLocation).
                    Select(line => line.Split(',')).
                    Where(stringArray => awardsID.Contains(stringArray[0])).
                    Select(stringArray => new Award(int.Parse(stringArray[0]), stringArray[1]));
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot get award by user ID", exception);
            }
        }

        public int AddAward(Award award)
        {
            try
            {
                award.Id = ++MaxAwardID;
                System.IO.File.AppendAllLines(Common.AwardFileLocation, new[] { AwardDatabaseEntry(award) });
                return award.Id;
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add award", exception);
            }
        }

        public void AddAwards(IEnumerable<Award> awards)
        {
            try
            {
                var databaseIDs = GetAllAwards().Select(award => award.Id);
                System.IO.File.AppendAllLines(Common.AwardFileLocation, 
                    AwardDatabaseEntries(awards.Where(award => !databaseIDs.Contains(award.Id))));
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add award", exception);
            }
        }

        private IEnumerable<string> AwardDatabaseEntries(IEnumerable<Award> awards)
        {
            var awardDatabaseEntries = new List<string>();
            foreach (var award in awards)
            {
                award.Id = ++MaxAwardID;
                awardDatabaseEntries.Add(AwardDatabaseEntry(award));
            }
            return awardDatabaseEntries;
        }

        private static string AwardDatabaseEntry(Award award)
        {
            return string.Format("{0},{1}", award.Id.ToString(), award.Title);
        }

        private static void InitializeFileSources()
        {
            Common.CreateTableIfNotExists(Common.AwardFileLocation);
            Common.CreateTableIfNotExists(Common.UsersAwardsFileLocation);
        }

        public Award GetAward(int awardId)
        {
            var awards = GetAllAwards().Where(award => award.Id == awardId);
            if (awards.Any())
            {
                return awards.First();
            }
            throw new DalException("Cannot find award by ID");
        }

        public void DeleteAward(int awardId)
        {
            throw new NotImplementedException();
        }
    }
}
