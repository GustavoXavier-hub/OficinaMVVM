using OficinaMVVM.Models;
using OficinaMVVM.Services.Atendimentos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OficinaMVVM.ViewModels.Atendimentos
{
    public class FotosCRUDViewModel : BaseViewModel
    {
        private IAtendimentoFotoService afService = new AtendimentoFotoService();
        public ICommand CameraCommand { get; set; }

        public ICommand AlbumCommand { get; set; }

        public ICommand GravarFotoCommand { get; set; }

        public AtendimentoFoto AtendimentoFoto { get; set; }
        public FotosCRUDViewModel(AtendimentoFoto atendimentoFoto
        )
        {
            this.AtendimentoFoto = atendimentoFoto;
            RegistrarCommands();
        }

        public string CaminhoFoto
        {
            get { return this.AtendimentoFoto.CaminhoFoto; }
            set
            {
                this.AtendimentoFoto.CaminhoFoto = value;
                OnPropertyChanged();
            }
        }

        public string NomeArquivo
        {
            get { return this.AtendimentoFoto.NomeArquivo; }

            set
            {
                this.AtendimentoFoto.NomeArquivo = value;
                OnPropertyChanged();
            } 
        }

        public byte[] ConteudoFoto
        {
            get { return this.AtendimentoFoto.ConteudoFoto; }
            set
            {
                this.AtendimentoFoto.ConteudoFoto = value;
                OnPropertyChanged();
            }
        }

        public string Observacoes
        {
            get { return this.AtendimentoFoto.Observacoes; }
            set
            {
                this.AtendimentoFoto.Observacoes = value;
                OnPropertyChanged();
            }
        }

        private void RegistrarCommands()
        {
            CameraCommand = new Command(() =>
            {
                MessagingCenter.Send<AtendimentoFoto>(this.AtendimentoFoto, "Camera");
            });

            AlbumCommand = new Command(() =>
            {
                MessagingCenter.Send<AtendimentoFoto>(this.AtendimentoFoto, "Album");

            });

            GravarFotoCommand = new Command(async () =>
            {
                string url = "http://gustavoxavier.somee.com/api/AtendimentoFotos/FileUpload";

                AtendimentoFoto afImagem = new AtendimentoFoto()
                {
                    ConteudoFoto = AtendimentoFoto.ConteudoFoto,
                    AtendimentoID = AtendimentoFoto.AtendimentoID,
                    CaminhoFoto = AtendimentoFoto.CaminhoFoto,
                    NomeArquivo = AtendimentoFoto.NomeArquivo,
                    Observacoes = AtendimentoFoto.Observacoes
                };

                //Outra referência: https://github.com/CrossGeeks/FileUploaderPlugin
                //List<FilePathItem> listaArquivos = new List<FilePathItem>();
                //FileUploadResponse response = await CrossFileUploader.Current.UploadFileAsync(url, new FileBytesItem("txtFile", afImagem.ConteudoFoto, afImagem.CaminhoFoto), new Dictionary<string, string>()
                //{
                //   {"AtendimentoID" , afImagem.AtendimentoID.ToString()}
                //});

                //https://forums.xamarin.com/discussion/64176/how-to-upload-image-to-the-server-using-api-in-xamarin-forms
                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                HttpClient client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();

                ByteArrayContent baContent = new ByteArrayContent(afImagem.ConteudoFoto);
                StringContent atendimentoIdContent = new StringContent(afImagem.AtendimentoID.ToString());
                StringContent caminhoFotoContent = new StringContent(afImagem.CaminhoFoto);
                StringContent observacoesContent = new StringContent(afImagem.Observacoes);

                content.Add(baContent, afImagem.CaminhoFoto, afImagem.NomeArquivo);
                content.Add(atendimentoIdContent, "AtendimentoID");
                content.Add(caminhoFotoContent, "CaminhoFoto");
                content.Add(observacoesContent, "Observacoes");

                //upload MultipartFormDataContent content async and store response in response var
                var response = await client.PostAsync(url, content);

                //read response result as a string async into json var
                var responsestr = response.Content.ReadAsStringAsync().Result;

                int id = 0;
                int.TryParse(responsestr.Replace("\"", "").Replace(@"\", ""), out id);

                if (id != 0)
                {
                    AtendimentoFoto = new AtendimentoFoto();
                    OnPropertyChanged(nameof(Observacoes));

                    //MessagingCenter.Send<string>("Atualização realizada com sucesso.", "InformacaoCRUD");
                    //MessagingCenter.Send<string>("consultar.png", "AtualizarFoto");
                }
            }
             /*, () =>
             {
                 return (!string.IsNullOrEmpty(Observacoes) && !string.IsNullOrEmpty(CaminhoFoto));
             }*/
             );



        }


    }
}
