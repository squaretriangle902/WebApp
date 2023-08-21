using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using WebApp.Common.Entities;
using WebApp.DAL.Interfaces;
using System.Data;
using System;

namespace WebApp.DAL.SQL
{
    public class AwardDao : IAwardDao
    {
        private readonly string connectionString;

        public AwardDao()
        {
            connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public int AddAward(Award award)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("CreateAward", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter("Title", award.Title));
                    if (award.ImageId is null)
                    {
                        command.Parameters.Add(new SqlParameter("ImageId", DBNull.Value));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("ImageId", award.ImageId.Value));
                    }
                    command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    connection.Open();
                    command.ExecuteNonQuery();
                    award.Id = (int)command.Parameters["Id"].Value;
                    return award.Id;
                }
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot add award", exception);
            }
        }

        public void DeleteAward(int awardId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("DeleteAward", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter("Id", awardId));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot delete award", exception);
            }
        }

        public IEnumerable<Award> GetAllAwards()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("ReadAwards", connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return GetAwardFromReader(reader);
                }
            }
        }

        public Award GetAward(int awardId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("ReadAward", connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                command.Parameters.Add(new SqlParameter("Id", awardId));
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return GetAwardFromReader(reader);
                }
                throw new DalException("Cannot find award by id");
            }
        }

        public IEnumerable<Award> GetUserAwards(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("ReadUserAwards", connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                command.Parameters.Add(new SqlParameter("Id", userId));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return GetAwardFromReader(reader);
                }
            }
        }

        public void UpdateAward(Award award)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("UpdateAward", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    command.Parameters.Add(new SqlParameter("Id", award.Id));
                    command.Parameters.Add(new SqlParameter("Title", award.Title));
                    if (award.ImageId is null)
                    {
                        command.Parameters.Add(new SqlParameter("ImageId", DBNull.Value));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("ImageId", award.ImageId.Value));
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot delete award", exception);
            }
        }

        private Award GetAwardFromReader(SqlDataReader reader)
        {
            return new Award()
            {
                Id = (int)reader["Id"],
                Title = (string)reader["Title"],
                ImageId = reader["ImageId"] as int? ?? null,
            };
        }
    }
}
