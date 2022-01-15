using Rocket_Elevators_Mobile.Views;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ElevatorStatusPage), typeof(ElevatorStatusPage));
        }
    }
}
