using WebApp.Common.Entities;
using WebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using WebApp.BLL.Interfaces;
using System.Drawing;

namespace WebApp.BLL.Core
{
    public class AwardLogic : IAwardLogic
    {
        private readonly IImageLogic imageLogic;
        private readonly IAwardDao awardDao;

        public AwardLogic(IAwardDao awardDao, IImageLogic awardImageLogic)
        {
            this.awardDao = awardDao;
            this.imageLogic = awardImageLogic;
        }

        public IEnumerable<Award> GetAllAwards()
        {
            foreach (var award in awardDao.GetAllAwards())
            {
                yield return award;
            }
        }

        public IEnumerable<Award> GetUserAwards(int userId)
        {
            try
            {
                return awardDao.GetUserAwards(userId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot get awards by user ID", exception);
            }
        }

        public Award GetAward(int awardId)
        {
            try
            {
                return awardDao.GetAward(awardId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot find award by ID", exception);
            }
        }
        public int AddAward(Award award)
        {
            if (!IsAwardValid(award))
            {
                throw new ArgumentException("Award is not valid");
            }
            try
            {
                awardDao.AddAward(award);
                return award.Id;
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot add award", exception);
            }
        }

        public void DeleteAward(int awardId)
        {
            try
            {
                awardDao.DeleteAward(awardId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot delete award by ID", exception);
            }
        }

        public Image GetImage(int imageId, int width, int height)
        {
            return imageLogic.GetImage(imageId, width, height);
        }

        public void SetImage(Award award, Image image)
        {
            if (award.ImageId is int imageId)
            {
                imageLogic.UpdateImage(image, imageId);
                return;
            }
            award.ImageId = imageLogic.SaveImage(image);
        }

        public void UpdateAward(Award award)
        {
            try
            {
                awardDao.UpdateAward(award);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot update award", exception);
            }
        }

        private bool IsAwardValid(Award award)
        {
            return !string.IsNullOrEmpty(award.Title);
        }

        public void DatabaseUpdate()
        {
            throw new NotImplementedException();
        }

    }
}
