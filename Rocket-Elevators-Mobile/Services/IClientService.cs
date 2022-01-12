using System.Net.Http;
using System.Threading.Tasks;
using Rocket_Elevators_Mobile.Models;

namespace Rocket_Elevators_Mobile.Services
{
    public interface IClientService
    {
        Task<bool> VerifyEmployeeEmail(string email);
        ElevatorList GetListOfElevatorOffline();
        string GetElevatorStatus(string id);
        void UpdateElevatorStatus(string id);
    }
}
