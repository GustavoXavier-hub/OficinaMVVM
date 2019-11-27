using OficinaMVVM.Models;
using OficinaMVVM.ViewModels.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficinaMVVM.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
       
        LoginViewModel loginViewModel;
        Cliente cliente;
        public LoginView()
        {
            InitializeComponent();
            cliente = new Cliente();
            loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "InformacaoCrud", async (c) =>
            {
                if (cliente.Id != 0)
                {
                    //NOVO: Guardando o Id e o nome do usuario para uso futuro
                    Application.Current.Properties["UsuarioId"] = cliente.Id;
                    Application.Current.Properties["UsuarioNome"] = cliente.Nome;

                    string mensagem = string.Format("Bem-Vindo {0} ", cliente.Nome);
                    await DisplayAlert("Informacao", mensagem, "OK");
                    await Navigation.PushModalAsync(new MainPageView());

                }
                else
                    await DisplayAlert("Informaçao", "Dados incorretos!!! =(", "OK");
            });

            }
          
        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "InformacaoCRUD");
        }

        
    }
}