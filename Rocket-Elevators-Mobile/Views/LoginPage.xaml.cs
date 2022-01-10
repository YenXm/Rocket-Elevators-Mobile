using Rocket_Elevators_Mobile.ViewModels;
using Rocket_Elevators_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rocket_Elevators_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<IClientService, ClientService>();
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}