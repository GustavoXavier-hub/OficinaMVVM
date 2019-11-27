using OficinaMVVM.Models;
using OficinaMVVM.Services.Clientes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OficinaMVVM.ViewModels.Usuarios
{
    class LoginViewModel : BaseViewModel
    {

        private IClienteService cService = new ClienteService();
        private Cliente Usuario;
        public ICommand EntrarCommand { get; set; }
        public async Task ConsultarUsuario() //Método que retorna a lista de Clientes completa
        {
            ObservableCollection<Cliente> Clientes = await cService.GetClientesAsync();
            Cliente cli = Clientes.ToList().Find(x => x.EMail == Usuario.EMail && x.Telefone ==
            Usuario.Telefone);
            if (cli == null)
                cli = new Cliente(); //Apenas para evitar erro NULL EXCEPTION
            if (cli.Id != 0)//Envia a mensagem com o objeto cliente para a View Tratar
            {
                MessagingCenter.Send<Cliente>(cli, "InformacaoCRUD");
            }
        }
        private void RegistrarCommands()
        {
            //Evento click da View
            EntrarCommand = new Command(async () =>
            {
                await ConsultarUsuario();
            });
        }
        public LoginViewModel() //Instacia o objeto de nome Usuário e Registra os comandos
        {
            this.Usuario = new Cliente();
            RegistrarCommands();
        }

        public string User //Propriedade Vinculada ao Binding, mas que preenche o Objeto Cliente
        {
            get { return this.Usuario.EMail; }
            set
            {
                this.Usuario.EMail = value;
                OnPropertyChanged();
            }
        }
        public string Senha //Propriedade Vinculada ao Binding, mas que preenche o Objeto Cliente
        {
            get { return this.Usuario.Telefone; }
            set
            {
                this.Usuario.Telefone = value;
                OnPropertyChanged();
            }
        }

    }
}
