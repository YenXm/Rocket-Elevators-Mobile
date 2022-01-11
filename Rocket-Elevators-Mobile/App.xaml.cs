using Rocket_Elevators_Mobile.Services;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<IClientService, ClientService>();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
