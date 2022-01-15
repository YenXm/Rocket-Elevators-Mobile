using Rocket_Elevators_Mobile.Models;
using System.Threading.Tasks;

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
