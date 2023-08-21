using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using WebApp.Common.Entities;
using WebApp.DAL.Interfaces;
using System.Configuration;

namespace WebApp.DAL.SQL
{
    public class UserDao : IUserDao
    {
        private readonly string connectionString;
        private readonly IAwardDao awardDao;

        public UserDao(IAwardDao awardDao)
        {
            connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            this.awardDao = awardDao;
        }

        public int AddUser(User user)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("CreateUser", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter("Name", user.Name));
                    command.Parameters.Add(new SqlParameter("BirthDate", user.BirthDate));
                    if (user.ImageId is null)
                    {
                        command.Parameters.Add(new SqlParameter("ImageId", DBNull.Value));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("ImageId", user.ImageId.Value));
                    }
                    command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    connection.Open();
                    command.ExecuteNonQuery();
                    user.Id = (int)command.Parameters["Id"].Value;
                    return user.Id;
                }
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add user", exception);
            }
        }

        public void AddUserAward(int userId, int awardId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("AddUserAward", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter("UserId", userId));
                    command.Parameters.Add(new SqlParameter("AwardId", awardId));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add award to user", exception);
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("DeletUser", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter("Id", userId));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot delete user", exception);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("UpdateUser", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter("Id", user.Id));
                    command.Parameters.Add(new SqlParameter("Name", user.Name));
                    command.Parameters.Add(new SqlParameter("BirthDate", user.BirthDate));
                    if (user.ImageId is null)
                    {
                        command.Parameters.Add(new SqlParameter("ImageId", DBNull.Value));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("ImageId", user.ImageId.Value));
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add user", exception);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("ReadUsers", connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return GetUserFromReader(reader);
                }
            }
        }
        public User GetUser(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("ReadUser", connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                command.Parameters.Add(new SqlParameter("Id", userId));
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return GetUserFromReader(reader);
                }
                throw new DalException("Cannot find user by id");
            }
        }
        public void DeleteUserAward(int userId, int awardId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("DeleteUserAward", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter("UserId", userId));
                    command.Parameters.Add(new SqlParameter("AwardId", awardId));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add award to user", exception);
            }
        }

        private User GetUserFromReader(SqlDataReader reader)
        {
            return new User()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                BirthDate = (DateTime)reader["BirthDate"],
                ImageId = reader["ImageId"] as int? ?? null,
            };
        }

    }
}
