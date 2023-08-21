using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using WebApp.DAL.Interfaces;
using WebApp.BLL.Interfaces;

namespace WebApp.BLL.Core
{
    public class ImageLogic : IImageLogic
    {
        private readonly IImageDao imageDao;

        public ImageLogic(IImageDao imageDao)
        {
            this.imageDao = imageDao;
        }

        public int SaveImage(Image image)
        {
            try
            {
                return imageDao.SaveImage(image);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot save image", exception);
            }
        }

        public Image GetImage(int imageId, int width, int height)
        {
            try
            {
                return ResizeImage(imageDao.GetImage(imageId), width, height);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot get image", exception);
            }
        }

        public void UpdateImage(Image image, int imageId)
        {
            try
            {
                imageDao.UpdateImage(image, imageId);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot update image", exception);
            }
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            try
            {
                return image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            }
            catch (Exception exception)
            {
                throw new BLLException("Cannot resize image", exception);
            }
        }

    }
}
