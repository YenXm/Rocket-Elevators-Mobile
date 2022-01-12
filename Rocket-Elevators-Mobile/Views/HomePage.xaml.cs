using Rocket_Elevators_Mobile.Models;
using Rocket_Elevators_Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rocket_Elevators_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();

        }

        private async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var elevator = ((Elevator)(sender as ListView).SelectedItem).id;

            //Deselect Item
            ((ListView)sender).SelectedItem = null;

            await Shell.Current.GoToAsync($"ElevatorStatusPage?id={elevator}");

        }
    }
}
