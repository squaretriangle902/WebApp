using Denis.UserList.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UserModel
    {
        const int ImageResizeWidth = 100;
        const int ImageResizeHeight = 100;

        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public int Age;

        public HttpPostedFileBase Image { get; set; }

        public static IEnumerable<UserModel> GetAll()
        {
            return Logic.UserLogic.GetAllUsers().Select(user => new UserModel
            {
                Id = user.Id, 
                Name = user.Name, 
                BirthDate = user.BirthDate, 
                Age = user.Age
            }).OrderBy(user => user.Name);
        }

        public static void Remove(int id)
        {
            Logic.UserLogic.DeleteUser(id);
        }

        public static void Add(UserModel userModel)
        {
            var user = new User()
            { 
                Id = 0,
                Name = userModel.Name,
                BirthDate = userModel.BirthDate,
                Image = Common.ImageToBytes(userModel.Image)
            };
            Logic.UserLogic.Add(user);
        }

        public static void Edit(UserModel userModel)
        {
            var user = Logic.UserLogic.GetUser(userModel.Id);
            user.BirthDate = userModel.BirthDate;
            user.Name = userModel.Name;
        }

        public static UserModel GetUser(int userId)
        {
            var user = Logic.UserLogic.GetUser(userId);
            return new UserModel { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Age = user.Age };
        }

        public static byte[] GetImage(int awardId)
        {
            var image = Logic.UserLogic.GetImage(awardId);
            if (image.Length != 0)
            {
                return Common.ResizeImage(image, ImageResizeWidth, ImageResizeHeight);
            }
            return Common.ResizeImage(GetDefaultImage(), 100, 100);
        }

        private static System.Drawing.Image GetDefaultImage()
        {
            return System.Drawing.Image.FromFile(@"C:\Users\squar\source\repos\WebApp\WebApp\Content\defaultUserImage.png");
        }

    }
}


