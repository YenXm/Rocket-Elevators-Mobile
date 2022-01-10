using System.Threading.Tasks;

namespace Rocket_Elevators_Mobile.Services
{
    public interface IClientService
    {
        Task<bool> VerifyEmployeeEmail(string email);
    }
}
