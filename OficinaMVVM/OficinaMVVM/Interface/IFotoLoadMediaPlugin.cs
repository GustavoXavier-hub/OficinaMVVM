using System;
using System.Collections.Generic;
using System.Text;

namespace OficinaMVVM.Interface
{
    public interface IFotoLoadMediaPlugin
    {
        string SetPathToPhoto(string caminhoCompleto);

        /*string GetDevicePathToPhoto()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Fotos"); 
        }
        string GetPathToPhoto(string caminhoArmazenado)
        {
            return caminhoArmazenado;
        }*/
    }
}
