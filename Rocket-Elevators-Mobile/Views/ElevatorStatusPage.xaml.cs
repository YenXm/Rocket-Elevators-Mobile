using Rocket_Elevators_Mobile.ViewModels;

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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ElevatorStatusViewModel viewModel = BindingContext as ElevatorStatusViewModel;
            viewModel.StopTimer = true;
        }
    }
}