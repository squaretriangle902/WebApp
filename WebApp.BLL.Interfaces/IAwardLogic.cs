using Denis.UserList.Common.Entities;
using Denis.UserList.DAL.File;
using System.Collections.Generic;

namespace Denis.UserList.BLL.Core
{
    public interface IAwardLogic
    {
        IAwardDAO AwardDAO { get; }
        IEnumerable<Award> GetAllAwards();
        IEnumerable<Award> GetUserAwards(int userID);
        int AddAward(string name);
        Award GetAward(int awardID);
        void DatabaseUpdate();
    }
}