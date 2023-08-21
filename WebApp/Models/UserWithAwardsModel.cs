using WebApp.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Common.Configuration;

namespace WebApp.Models
{
    public class UserWithAwardsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public List<AwardModel> Awards { get; private set; }
        public int? ImageId { get; private set; }

        public static UserWithAwardsModel GetUser(int userId)
        {
            var user = Startup.UserLogic.GetUser(userId);
            return new UserWithAwardsModel
            {
                Id = user.Id,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Age = user.Age,
                ImageId = user.ImageId,
                Awards = GetAwards(user.Id),
            };
        }

        public static void RemoveAward(int userId, int awardId)
        {
            Startup.UserLogic.DeleteUserAward(userId, awardId);
        }

        public static void AddAward(int userId, int awardId)
        {
            Startup.UserLogic.AddUserAward(userId, awardId);
        }        
        private static List<AwardModel> GetAwards(int userId)
        {
            return Startup.UserLogic.GetUserAwards(userId).
                Select(award => Common.ConvertToModel(award)).ToList();
        }
    }
}