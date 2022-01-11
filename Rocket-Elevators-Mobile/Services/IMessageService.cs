using System.Threading.Tasks;

namespace Rocket_Elevators_Mobile.Services
{
    public interface IMessageService
    {
        Task DisplayAlert(string message);
    }
}
