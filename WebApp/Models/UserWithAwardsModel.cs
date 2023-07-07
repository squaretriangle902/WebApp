using Denis.UserList.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UserWithAwardsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public int Age { get; set; }

        public List<AwardModel> Awards { get; set; }

        public static void Remove(int id)
        {
            Logic.UserLogic.DeleteUser(id);
        }

        public static object GetUser(int userId)
        {
            var user = Logic.UserLogic.GetUser(userId);
            return new UserWithAwardsModel
            {
                Id = user.Id,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Age = user.Age,
                Awards = GetAwards(user.Id),
            };
        }

        public static List<AwardModel> GetAwards(int userId)
        {
            return Logic.UserLogic.GetUserAwards(userId).Select(award => new AwardModel
            {
                Id = award.Id,
                Title = award.Title
            }).ToList();
        }
    }
}