using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageEdgeDetection
{
    public interface IFilter
    {
        //Méthode qui permet de récupérer la liste des EdgeDetection possibles
        string[] getEdgeSelection();

        //Méthode qui permet d'appliquer un Edge Detection à une image d'origine
        Bitmap applyEdgeDetection(string edgeName, Bitmap originale);
    }
}
