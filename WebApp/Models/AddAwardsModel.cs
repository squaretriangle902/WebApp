using Denis.UserList.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AddAwardsModel
    {
        private List<AwardModel> userAwards;
        private List<AwardModel> availableAwards;
        private int userId;

        public List<AwardModel> UserAwards 
        {
            get
            {
                return userAwards;
            }
            private set
            {
                userAwards = value;
            }
        }

        public List<AwardModel> AvailableAwards
        {
            get
            {
                return availableAwards;
            }
            private set
            {
                availableAwards = value;
            }
        }

        public int UserId 
        { 
            get
            {
                return userId;
            }
            private set
            {
                userId = value;
            }
        }

        public AddAwardsModel(int userId)
        {
            UserId = userId;
            UserAwards = InitializeUserAwards(userId);
            AvailableAwards = InitializeAvailableAwards(userId);
        }

        private static List<AwardModel> InitializeUserAwards(int userId)
        {
            return Logic.UserLogic.GetUserAwards(userId).Select(award => new AwardModel
            {
                Id = award.Id,
                Title = award.Title,
            }).ToList();
        }

        private static List<AwardModel> InitializeAvailableAwards(int userId)
        {
            return Logic.AwardLogic.GetAllAwards().Except(Logic.UserLogic.GetUserAwards(userId)).Select(award => new AwardModel
            {
                Id = award.Id,
                Title = award.Title,
            }).ToList();
        }
    }
}