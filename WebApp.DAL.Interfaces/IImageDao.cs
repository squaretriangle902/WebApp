using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WebApp.DAL.Interfaces
{
    public interface IImageDao
    {
        int SaveImage(Image image);
        Image GetImage(int imageId);
        void UpdateImage(Image image, int imageId);
    }
}
