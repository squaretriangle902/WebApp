using WebApp.Common.Entities;
using WebApp.DAL.Interfaces;
using System.Collections.Generic;
using System.Drawing;

namespace WebApp.BLL.Interfaces
{
    public interface IAwardLogic
    {
        IEnumerable<Award> GetAllAwards();
        IEnumerable<Award> GetUserAwards(int userId);
        int AddAward(Award award);
        Award GetAward(int awardId);
        void UpdateAward(Award award);
        void DeleteAward(int awardId);
        void DatabaseUpdate();
        void SetImage(Award award, Image image);
        Image GetImage(int imageId, int width, int height);
    }
}