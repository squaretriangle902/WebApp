using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using WebApp.DAL.Interfaces;

namespace WebApp.DAL.SQL
{
    public class ImageDao : IImageDao
    {
        private readonly string imageDirectoryPath;
        private int maxImageId;

        private int MaxImageId
        {
            get
            {
                return maxImageId;
            }
            set
            {
                maxImageId = value;
            }
        }

        public ImageDao(string imageDirectoryPath)
        {
            try
            {
                this.imageDirectoryPath = imageDirectoryPath;
                MaxImageId = Directory.GetFiles(imageDirectoryPath).
                    Select(fileName => FileId(fileName)).Max();
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot read image directory", exception);
            }
        }

        public int SaveImage(Image image)
        {
            try
            {
                int imageId = ++MaxImageId;
                var path = ImagePath(imageId);
                image.Save(path);
                return imageId;
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot save image", exception);
            }
        }
        public Image GetImage(int imageId)
        {
            try
            {
                return Image.FromFile(ImagePath(imageId));
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot get image", exception);
            }
        }

        public void UpdateImage(Image image, int imageId)
        {
            try
            {
                var path = ImagePath(imageId);
                if (File.Exists(path))
{
                    File.Delete(path);
                }
                image.Save(path);
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot update image", exception);
            }
        }

        private string ImagePath(int imageId)
        {
            try
            {
                return Path.Combine(imageDirectoryPath, imageId.ToString() + ".png");
            }
            catch (Exception exception)
            {
                throw new DalException("Cannot get image path", exception);
            }
        }

        private int FileId(string fileName)
        {
            var fileIdString = Path.GetFileNameWithoutExtension(fileName);
            return int.TryParse(fileIdString, out int fileId) ? fileId : 0;
        }

    }
}
