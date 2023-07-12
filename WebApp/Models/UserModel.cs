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
            var bytes = new byte[userModel.Image.ContentLength];
            userModel.Image.InputStream.Read(bytes, 0, bytes.Length);

            var user = new User(0, userModel.Name, userModel.BirthDate);
            Logic.UserLogic.Add(user);
            Logic.UserLogic.SetImage(userModel.Id, bytes);
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

        public static byte[] GetImage(int userId)
        {
            return Logic.UserLogic.GetImage(userId);
        }
    }
}


