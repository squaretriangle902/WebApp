using System.IO;
using WebApp.BLL.Interfaces;
using WebApp.DAL.Interfaces;

namespace WebApp.Common.Configuration
{
    public class Startup
    {
        public static IUserLogic UserLogic { get; private set; }
        public static IAwardLogic AwardLogic { get; private set; }

        static Startup()
        {
            var awardImageDao = new DAL.SQL.ImageDao(Path.Combine(Constants.ImagesPath, Constants.AwardImagesRelativePath));
            var userImageDao = new DAL.SQL.ImageDao(Path.Combine(Constants.ImagesPath, Constants.UserImagesRelativePath));
            var awardDao = new DAL.SQL.AwardDao();
            var userDao = new DAL.SQL.UserDao(awardDao);
            var awardImageLogic = new BLL.Core.ImageLogic(awardImageDao);
            var userImageLogic = new BLL.Core.ImageLogic(userImageDao);
            AwardLogic = new BLL.Core.AwardLogic(awardDao, awardImageLogic);
            UserLogic = new BLL.Core.UserLogic(AwardLogic, userDao, userImageLogic);
        }
    }
}
