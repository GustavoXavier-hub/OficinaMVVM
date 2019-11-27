using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OficinaMVVM.Interfaces.Fotos;
using Environment = System.Environment;

namespace OficinaMVVM.Droid.Fotos
{
    public class FotoLoadMediaPlugin : IFotoLoadMediaPlugin
    {
        public string SetPathToPhoto(string caminhoCompleto)
        {
            return caminhoCompleto;
            //Caso fosse IOS:
            //return caminhoCompleto.Substring(caminhoCompleto.LastIndexOf("/") + 1);
        }

        public string GetDevicePathToPhoto()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Fotos");

            //Caso fosse IOS:
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Fotos");
        }
        public string GetPathToPhoto(string caminhoArmazenado)
        {
            return caminhoArmazenado;
            //Caso fosse IOS:
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Fotos", caminhoArmazenado);
        }
    }


}