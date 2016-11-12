using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageEdgeDetection
{
    public class Orchestrator : IOrchestrator
    {
        private IUserInterface userInterface;
        private IInputOutput ioManager;
        private IFilter edgeDetector;
        private Bitmap originalBitmap = null;
        private Bitmap resultBitmap = null;

        //Constructeur qui  orchestre la mise en place de tous les composants
        public Orchestrator(IUserInterface userInterface)
        {
            this.userInterface = userInterface;
            ioManager = new IOManager();
            edgeDetector = new EdgeDetection();
            userInterface.setEdgeSelection(edgeDetector.getEdgeSelection());
        }



        //Méthode qui orchestre le chargement de l'image d'origine.
        //Cette méthode est appelée par l'user interface quand l'utilisateur a sélectionné l'image à charger.
        public void loadOriginaleImage()
        {
            originalBitmap = ioManager.getImage();
            resultBitmap = new Bitmap(originalBitmap,originalBitmap.Size);
            this.showOriginaleImage();
        }

        //Méthode qui orchestre l'affichage de l'image chargée
        private void showOriginaleImage()
        {
            userInterface.displayImage(originalBitmap);
        }

        //Méthode qui orchestre la sélection un Edge Detection.
        //Doit être appelée par la user interface quand l'utilisateur a choisi le type de Edge Detection à appliquer.
        public void performEdgeDetection()
        {
            this.applyEdgeDetection();
        }

        //Méthode qui orchestre l'application du Edge Detection 
        private void applyEdgeDetection()
        {
            resultBitmap = edgeDetector.applyEdgeDetection(userInterface.selectEdgeDetection(), originalBitmap);
            this.showResultImage();
        }

        //Méthode qui orchestre l'affichage de l'image modifiée
        private void showResultImage()
        {
            this.userInterface.displayImage(resultBitmap);
        }

        //Méthode qui orchestre l'enregistrement de l'image modifiée
        //Doit être appelée après que l'utilisateur ait choisi l'endroit où le résultat doit être sauvegardé.
        public void saveResultImage()
        {
            ioManager.saveImage(resultBitmap);
        }
    }
}
