using System.Threading.Tasks;

namespace Rocket_Elevators_Mobile.Services
{
    internal class MessageService : IMessageService
    {
        public async Task DisplayAlert(string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Rocket_Elevators_Mobile", message, "Ok");
        }
    }
}
