using MvvmHelpers;
using Rocket_Elevators_Mobile.Models;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableRangeCollection<Elevator> elevators;
        public ObservableRangeCollection<Elevator> Elevators
        {
            get => elevators;
            set => SetProperty(ref elevators, value);
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        public Command LogoutCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public HomeViewModel()
        {
            LogoutCommand = new Command(OnLogoutClicked);
            RefreshCommand = new Command(Refresh);


            ElevatorList _elevators = ClientService.GetListOfElevatorOffline();
            Elevators = new ObservableRangeCollection<Elevator>(_elevators.elevators);

        }

        public void Refresh()
        {
            Elevators.ReplaceRange(ClientService.GetListOfElevatorOffline().elevators);
            IsRefreshing = false;
        }

        private async void OnLogoutClicked()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
