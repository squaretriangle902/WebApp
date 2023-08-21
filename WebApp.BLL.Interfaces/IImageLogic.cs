using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WebApp.BLL.Interfaces
{
    public interface IImageLogic
    {
        int SaveImage(Image image);
        Image GetImage(int imageId, int width, int height);
        void UpdateImage(Image image, int imageId);
    }
}
