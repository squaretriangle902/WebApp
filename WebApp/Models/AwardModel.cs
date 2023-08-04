using Denis.UserList.Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace WebApp.Models
{
    public class AwardModel
    {
        const int ImageResizeWidth = 100;
        const int ImageResizeHeight = 100;

        public int Id { get; set; }
        public string Title { get; set; }
        public HttpPostedFileBase Image { get; set; }

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
            var award = new Award()
            {
                Id = 0,
                Title = awardModel.Title,
                Image = Common.ImageToBytes(awardModel.Image)
            };
            Logic.AwardLogic.AddAward(award);
        }

        public static byte[] GetImage(int awardId)
        {
            var image = Logic.AwardLogic.GetImage(awardId);
            if (image.Length != 0)
            {
                return Common.ResizeImage(image, ImageResizeWidth, ImageResizeHeight);
            }
            return Common.ResizeImage(GetDefaultImage(), 100, 100);
        }

        private static System.Drawing.Image GetDefaultImage()
        {
            return System.Drawing.Image.FromFile(@"C:\Users\squar\source\repos\WebApp\WebApp\Content\defaultAwardImage.png");
        }
    }
}