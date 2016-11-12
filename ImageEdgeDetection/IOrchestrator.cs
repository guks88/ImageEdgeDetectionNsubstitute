using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageEdgeDetection
{
    interface IOrchestrator
    {
        //Méthode qui orchestre le chargement de l'image d'origine.
        void loadOriginaleImage();

        //Méthode qui orchestre la sélection d'un Edge Detection.
        void performEdgeDetection();

        //Méthode qui orchestre l'enregistrement de l'image modifiée
        void saveResultImage();
    }
}
