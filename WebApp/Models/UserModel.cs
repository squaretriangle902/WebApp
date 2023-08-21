using WebApp.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Common.Configuration;

namespace WebApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public int? ImageId { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public static IEnumerable<UserModel> GetAllUsers()
        {
            return Startup.UserLogic.GetAllUsers().Select(user => Common.ConvertToModel(user)).
                OrderBy(userModel => userModel.Name);
        }

        public static void DeleteUser(int userId)
        {
            Startup.UserLogic.DeleteUser(userId);
        }

        public static void AddUser(UserModel userModel)
        {
            var user = Common.ConvertToEntity(userModel);
            if (!(userModel.Image is null))
            {
                var image = System.Drawing.Image.FromStream(userModel.Image.InputStream, true, true);
                Startup.UserLogic.SetImage(user, image);
            }
            Startup.UserLogic.AddUser(user);
        }

        public static void Update(UserModel userModel)
        {
            var user = Common.ConvertToEntity(userModel);
            if (!(userModel.Image is null))
            {
                var image = System.Drawing.Image.FromStream(userModel.Image.InputStream, true, true);
                Startup.UserLogic.SetImage(user, image);
            }
            Startup.UserLogic.UpdateUser(user);
        }

        public static UserModel GetUser(int userId)
        {
            var user = Startup.UserLogic.GetUser(userId);
            return Common.ConvertToModel(user);
        }

        private static System.Drawing.Image GetDefaultImage()
        {
            return System.Drawing.Image.FromFile(@"C:\Users\squar\source\repos\WebApp\WebApp\Content\defaultUserImage.png");
        }

        public static byte[] GetImage(int? imageId)
        {
            if (imageId is int imageIdValue)
            {
                using (var image = Startup.UserLogic.GetImage(
                    imageIdValue,
                    Constants.UserImageResizeWidth,
                    Constants.UserImageResizeHeight))
                {
                    return Common.ConvertToBytes(image);
                }
            }
            return null;
        }
    }
}


