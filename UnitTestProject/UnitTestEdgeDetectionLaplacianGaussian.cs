using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageEdgeDetection;
using System.IO;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestEdgeDetectionLaplacianGaussian
    {
        private static Bitmap GetBase()
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("UnitTestProject.base.png");
            return new Bitmap(myStream);
        }
        private static Bitmap GetDetected()
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("UnitTestProject.LaplacianGaussian.png");
            return new Bitmap(myStream);
        }
        Bitmap refOriginal = GetBase();
        Bitmap refFiltered = GetDetected();

        [TestMethod]
        public void TestSizeAfterLaplacianOfGaussian()
        {

            Bitmap result = refOriginal.LaplacianOfGaussianFilter();

            Assert.AreEqual(refFiltered.Size, result.Size);
        }

        [TestMethod]
        public void TestLaplacianOfGaussian()
        {
            Bitmap result = refOriginal.LaplacianOfGaussianFilter();

            for (int y = 0; y < refFiltered.Height; y++)
                for (int x = 0; x < refFiltered.Width; x++)
                    Assert.AreEqual(result.GetPixel(x, y), refFiltered.GetPixel(x, y));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestImageNull()
        {
            Bitmap tempOriginal = null;
            tempOriginal.LaplacianOfGaussianFilter();
        }
    }
}
