using OficinaMVVM.Models;
using OficinaMVVM.Services.Atendimentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OficinaMVVM.ViewModels.Atendimentos
{
    public class FotoListagemViewModel : BaseViewModel
    {
        private IAtendimentoFotoService fotoService = new AtendimentoFotoService();

        private Atendimento Atendimento { get; set; }
        private AtendimentoFoto AtendimentoFoto { get; set; }
        public ICommand NovoCommand { get; set; }        

        private Services.Atendimentos.IAtendimentoService fService = 
            new Services.Atendimentos.AtendimentoService();


        public FotoListagemViewModel(Atendimento atendimento)
        {
            this.Atendimento = atendimento;
            AtendimentoFotos = new ObservableCollection<AtendimentoFoto>();
            RegistrarCommands();
        }

        private void RegistrarCommands()
        {
            NovoCommand = new Command(() =>
            {
                var atendimentoFoto = new AtendimentoFoto() { AtendimentoID = this.Atendimento.AtendimentoID };
                MessagingCenter.Send<AtendimentoFoto>(atendimentoFoto, "Mostrar");
            }, () =>
            {
                return !this.Atendimento.EstaFinalizado;
            });
        }

        public ObservableCollection<AtendimentoFoto> AtendimentoFotos
        {
            get; set;
        }
        public async Task ObterAtendimentoFotosAsync()
        {
            AtendimentoFotos = await fotoService.GetAtendimentoFotosAsync();
            OnPropertyChanged(nameof(AtendimentoFotos));
        }


        public async Task AtualizarFotosAsync()
        {
            ObservableCollection<AtendimentoFoto> listaAtendimentos =
                new ObservableCollection<AtendimentoFoto>(); //await fotoService.GetAtendimentoFotosAsync();            
            Atendimento.Fotos = new List<AtendimentoFoto>(listaAtendimentos);
        }

        public List<AtendimentoFoto> FotosAtendimento
        {
            get { return Atendimento.Fotos; }
        }

    }
}
