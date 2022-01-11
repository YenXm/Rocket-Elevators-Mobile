using Rocket_Elevators_Mobile.Services;
using Rocket_Elevators_Mobile.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using Xamarin.Forms;
using MvvmHelpers;
using System.Threading.Tasks;

namespace Rocket_Elevators_Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public ObservableCollection<Elevator> Elevators { get; set; }

        public HomeViewModel()
        {

            var elevators = ClientService.GetListOfElevatorOffline();
            Elevators = new ObservableRangeCollection<Elevator>();
            foreach (var elevator in elevators.elevators)
            {
                Elevators.Add(elevator);
            }
        }
    }
}
