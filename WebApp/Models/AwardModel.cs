using Denis.UserList.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AwardModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public static IEnumerable<AwardModel> GetAll()
        {
            return Logic.AwardLogic.GetAllAwards().Select(award => new AwardModel 
            { 
                Id = award.Id, 
                Title = award.Title 
            });
        }

        public static void Add(AwardModel awardModel)
        {
            var award = new Award(0, awardModel.Title);
            Logic.AwardLogic.AddAward(award);
        }
    }
}