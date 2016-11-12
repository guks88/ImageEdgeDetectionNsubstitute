using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageEdgeDetection;

namespace UnitTestProject
{
    [TestClass]
    public class ImageFilterTest
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NullExceptionOnNullImage()
        {
            Bitmap nulled = null;
            ImageFilters.ApplyFilter(nulled, 1, 1, 1, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidFilterMinusOne()
        {
            Bitmap image = getImage(Color.FromArgb(100, 140, 200, 60));
            ImageFilters.ApplyFilter(image, -1, -1, -1, -1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidFilterzero()
        {
            Bitmap image = getImage(Color.FromArgb(100, 140, 200, 60));
            ImageFilters.ApplyFilter(image, 0, 0, 0, 0);
        }
        [TestMethod]
        public void ValidFilter()
        {
            Color origine = Color.FromArgb(100, 140, 200, 60);
            Color wanted = Color.FromArgb(100, 70, 50, 60);
            Bitmap imageBase = getImage(origine);
            Bitmap filtred = ImageFilters.ApplyFilter(imageBase, 1, 2, 1, 4);
            for (int with = 0; with < filtred.Width; with++)
                for (int height = 0; height < filtred.Height; height++)
                    Assert.AreEqual(filtred.GetPixel(with, height),wanted);


        }

        private Bitmap getImage(Color wantedColor)
        {
            Bitmap bmp = new Bitmap(50, 50);
            for (int with = 0; with < bmp.Width; with++)
                for (int height = 0; height < bmp.Height; height++)
                    bmp.SetPixel(with, height, wantedColor);
            return bmp;
        }
    }
}
