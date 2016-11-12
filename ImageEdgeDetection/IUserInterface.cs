using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageEdgeDetection
{
    public interface IUserInterface
    {


        //Méthode pour l'affichage de l'image (originale et modifiée)
        void displayImage(Image image);

        //Méthode pour envoyer la liste des types de EdgeDetection à l'utilisateur
        void setEdgeSelection(string [] liste);

        //Méthode pour sélectionner un EdgdeDetection au programme
        string selectEdgeDetection();
    }
}
