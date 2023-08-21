using System;
using System.IO;
using System.Web;
using WebApp.Common.Entities;

namespace WebApp.Models
{
    public static class Common
    {
        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            if (image is null)
            {
                return new byte[0];
            }
            var bytes = new byte[image.ContentLength];
            image.InputStream.Read(bytes, offset: 0, bytes.Length);
            return bytes;
        }

        public static byte[] ConvertToBytes(System.Drawing.Image image)
        {
            var resultStream = new MemoryStream();
            image.Save(resultStream, System.Drawing.Imaging.ImageFormat.Png);
            return resultStream.ToArray();
        }

        public static Award ConvertToEntity(AwardModel awardModel)
        {
            return new Award()
            {
                Id = awardModel.Id,
                Title = awardModel.Title,
                ImageId = awardModel.ImageId
            };
        }

        public static AwardModel ConvertToModel(Award award)
        {
            return new AwardModel()
            {
                Id = award.Id,
                Title = award.Title,
                ImageId = award.ImageId,
            };
        }

        public static User ConvertToEntity(UserModel userModel)
        {
            return new User()
            {
                Id = userModel.Id,
                Name = userModel.Name,
                BirthDate = userModel.BirthDate,
                ImageId = userModel.ImageId,
            };
        }

        public static UserModel ConvertToModel(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Age = user.Age,
                ImageId = user.ImageId
            };
        }

    }
}