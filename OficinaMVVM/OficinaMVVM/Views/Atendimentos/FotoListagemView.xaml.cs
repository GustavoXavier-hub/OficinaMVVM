using OficinaMVVM.Models;
using OficinaMVVM.ViewModels.Atendimentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficinaMVVM.Views.Atendimentos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FotoListagemView : ContentPage
	{
        public FotoListagemViewModel viewModel;
		public FotoListagemView ()
		{
			InitializeComponent ();
		}

        public FotoListagemView(Atendimento atendimento)
        {
            BindingContext = viewModel = new FotoListagemViewModel(atendimento);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CarregarFotosAsync();

            MessagingCenter.Subscribe<AtendimentoFoto>(this, "Mostrar", async (foto) => {
                await Navigation.PushAsync(new FotosCRUDView(foto, "Foto Atendimento")); });
        }

        public async void CarregarFotosAsync()
        {
            await viewModel.AtualizarFotosAsync();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<AtendimentoFoto>(this, "Mostrar");
        }

    }
}