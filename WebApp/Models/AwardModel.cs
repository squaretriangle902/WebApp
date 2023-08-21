using WebApp.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Common.Configuration;
using System;

namespace WebApp.Models
{
    public class AwardModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ImageId { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public static IEnumerable<AwardModel> GetAllAwards()
        {
            return Startup.AwardLogic.GetAllAwards().Select(award => Common.ConvertToModel(award)).
                OrderBy(awardModel => awardModel.Title);
        }

        public static AwardModel GetAward(int userId)
        {
            return Common.ConvertToModel(Startup.AwardLogic.GetAward(userId));
        }

        public static void AddAward(AwardModel awardModel)
        {
            var award = Common.ConvertToEntity(awardModel);
            if (!(awardModel.Image is null))
            {
                var image = System.Drawing.Image.FromStream(awardModel.Image.InputStream, true, true);
                Startup.AwardLogic.SetImage(award, image);
            }
            Startup.AwardLogic.AddAward(award);
        }

        public static byte[] GetImage(int? imageId)
        {
            if (imageId is int imageIdValue)
            {
                using (var image = Startup.AwardLogic.GetImage(
                    imageIdValue, 
                    Constants.AwardImageResizeWidth,
                    Constants.AwardImageResizeHeight))
                {
                    return Common.ConvertToBytes(image);
                }
            }
            return null;
        }

        public static void DeleteAward(int awardId)
        {
            Startup.AwardLogic.DeleteAward(awardId);
        }

        public static void UpdateAward(AwardModel awardModel)
        {
            var award = Common.ConvertToEntity(awardModel);
            if (!(awardModel.Image is null))
            {
                var image = System.Drawing.Image.FromStream(awardModel.Image.InputStream, true, true);
                Startup.AwardLogic.SetImage(award, image);
            }
            Startup.AwardLogic.UpdateAward(award);
        }
    }
}