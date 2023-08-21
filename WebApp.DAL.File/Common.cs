using System.IO;

namespace WebApp.DAL.File
{
    public static class Common
    {
        public const string UserFileLocation = @"E:\UserList\Database\user.csv";
        public const string UsersAwardsFileLocation = @"E:\UserList\Database\usersAwards.csv";
        public const string AwardFileLocation = @"E:\UserList\Database\award.csv";

        public static void CreateTableIfNotExists(string location)
        {
            if (System.IO.File.Exists(location))
            {
                return;
            }

            FileStream fileStream = null;
            try
            {
                fileStream = System.IO.File.Create(location);
            }
            finally
            {
                if (fileStream != null)
                {
                    try
                    {
                        fileStream.Close();
                    }
                    catch
                    {
                    }
                }
            }

            //if (!System.IO.File.Exists(location))
            //{
            //    using var file = System.IO.File.Create(location);
            //}
        }
    }
}
