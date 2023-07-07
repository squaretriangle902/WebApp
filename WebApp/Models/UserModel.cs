using Denis.UserList.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Common.Entities;

namespace WebApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public int Age;

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
            var user = new User(0, userModel.Name, userModel.BirthDate);
            Logic.UserLogic.Add(user);
        }
    }
}


