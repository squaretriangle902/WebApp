using WebApp.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Common.Configuration;

namespace WebApp.Models
{
    public class AddAwardsModel
    {
        public int UserId { get; private set; }
        public List<AwardModel> AvailableAwards { get; private set; }
        public List<AwardModel> UserAwards { get; private set; }

        public AddAwardsModel(int userId)
        {
            UserId = userId;
            UserAwards = InitializeUserAwards(userId);
            AvailableAwards = InitializeAvailableAwards(userId);
        }

        private static List<AwardModel> InitializeUserAwards(int userId)
        {
            return Startup.UserLogic.GetUserAwards(userId).
                Select(award => Common.ConvertToModel(award)).ToList();
        }

        private static List<AwardModel> InitializeAvailableAwards(int userId)
        {
            return Startup.AwardLogic.GetAllAwards().
                Except(Startup.UserLogic.GetUserAwards(userId)).
                Select(award => Common.ConvertToModel(award)).ToList();
        }
    }
}