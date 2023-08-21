using WebApp.Common.Entities;
using System.Collections.Generic;

namespace WebApp.DAL.Interfaces
{
    public interface IAwardDao
    {
        IEnumerable<Award> GetAllAwards();
        IEnumerable<Award> GetUserAwards(int userId);
        int AddAward(Award award);
        Award GetAward(int awardId);
        void DeleteAward(int awardId);
        void UpdateAward(Award award);
    }
}