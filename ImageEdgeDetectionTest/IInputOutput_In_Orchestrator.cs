using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEdgeDetection;
using NSubstitute;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class IInputOutput_In_Orchestrator
    {
        private IInputOutput inputOutput = Substitute.For<IInputOutput>();
        private IUserInterface userInterface = Substitute.For<IUserInterface>();
           
        [TestMethod]
        [ExpectedException(typeof (IOException))]
        public void getImage_IOException()
        {
            Orchestrator orchestrator = new Orchestrator(userInterface);
            this.injectInterface(orchestrator);

            inputOutput
                .When(x => x.getImage())
                .Do(x => { throw new IOException(); });

           
            orchestrator.loadOriginaleImage();
            
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void getImage_ReturnNull()
        {
            Orchestrator orchestrator = new Orchestrator(userInterface);
            this.injectInterface(orchestrator);
            Bitmap mimicReturn = null;
            inputOutput.getImage().Returns<Bitmap>(mimicReturn);

            orchestrator.loadOriginaleImage();
        }

        [TestMethod]
        public void getImage_Valide()
        {
            Orchestrator orchestrator = new Orchestrator(userInterface);
            this.injectInterface(orchestrator);

            Bitmap imgTest = Properties.Resources._base;
            inputOutput.getImage().Returns<Bitmap>(imgTest);

            orchestrator.loadOriginaleImage();

            FieldInfo field = typeof(Orchestrator).GetField("originalBitmap", BindingFlags.NonPublic|BindingFlags.Instance);
            Bitmap imgResult = (Bitmap)field.GetValue(orchestrator);

            for (int i = 0; i < imgTest.Width; i++)
            {
                for (int j = 0; j < imgTest.Height; j++)
                {
                    Color pixelRef = imgTest.GetPixel(i, j);
                    Color pixelRes = imgResult.GetPixel(i, j);

                    Assert.AreEqual(pixelRef, pixelRes);
                }

            }

        }

        [TestMethod]
        public void saveImage_NullArg()
        {

            Orchestrator orchestrator = new Orchestrator(userInterface);
            this.injectInterface(orchestrator);
            FieldInfo resultImageField = typeof(Orchestrator).GetField("resultBitmap",
                BindingFlags.NonPublic | BindingFlags.Instance);
            Bitmap nullBitmap= null;
            resultImageField.SetValue(orchestrator, nullBitmap);
            

            orchestrator.saveResultImage();
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void saveImage_IOException()
        {
            Orchestrator orchestrator = new Orchestrator(userInterface);
            this.injectInterface(orchestrator);

            inputOutput
                .When(x => x.saveImage(Arg.Any<Bitmap>()))
                .Do(x => { throw new IOException(); });


            orchestrator.saveResultImage();

        }

        private void injectInterface(Orchestrator target)
        {
            FieldInfo field = typeof(Orchestrator).GetField("ioManager",
                BindingFlags.NonPublic| BindingFlags.Instance);
            field.SetValue(target, inputOutput);
        }
    }
}
