using MvvmHelpers;
using Rocket_Elevators_Mobile.Models;
using System;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        /// <summary>Used in the HomePage ListView of Elevator</summary>
        public ObservableRangeCollection<Elevator> Elevators
        {
            get => elevators;
            set => SetProperty(ref elevators, value);
        }
        // For use inside the ViewModels only.
        private ObservableRangeCollection<Elevator> elevators;

        /// <summary>Current State of the RefreshView</summary>
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }
        // For use inside the ViewModels only.
        private bool isRefreshing;

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
