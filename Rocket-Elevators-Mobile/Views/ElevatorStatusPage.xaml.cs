using Rocket_Elevators_Mobile.ViewModels;
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
    public partial class ElevatorStatusPage : ContentPage
    {
        public ElevatorStatusPage()
        {
            InitializeComponent();
            BindingContext = new ElevatorStatusViewModel();
        }
    }
}