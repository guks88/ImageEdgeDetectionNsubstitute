using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageEdgeDetection
{
    public interface IInputOutput
    {
        //Méthode qui fournit l'image (Load)
        Bitmap getImage();

        //Méthode sauvegarde l'image (Save)
        void saveImage(Bitmap image);
    }
}
