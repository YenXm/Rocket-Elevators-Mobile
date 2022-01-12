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
        public Command LogoutCommand { get; set; }

        public HomeViewModel()
        {
            LogoutCommand = new Command(OnLogoutClicked);

            ElevatorList _elevators = ClientService.GetListOfElevatorOffline();
            Elevators = new ObservableRangeCollection<Elevator>(_elevators.elevators);

            DynamicElevatorListUpdate();

        }


        /// <summary> Check for a change in the list of the elevator not currently running.</summary>
        private void DynamicElevatorListUpdate()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
            {
                try
                {
                    Elevators.ReplaceRange(ClientService.GetListOfElevatorOffline().elevators);
                }
                catch (Exception e)
                {
                    // Would mainly happen if the api call failed
                    Console.WriteLine("Failed to load ElevatorList.");
                    Console.WriteLine($"{e} Exception caught.");
                }
                return true;
            });
        }

        private async void OnLogoutClicked()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
