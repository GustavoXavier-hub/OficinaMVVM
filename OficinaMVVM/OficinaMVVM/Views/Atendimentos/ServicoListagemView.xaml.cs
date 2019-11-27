using OficinaMVVM.Models;
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
    public partial class ServicoListagemView : ContentPage
    {
        public ServicoListagemView()
        {
            InitializeComponent();
        }

        public ServicoListagemView(Atendimento atendimento)
        {
            InitializeComponent();
        }
    }
}