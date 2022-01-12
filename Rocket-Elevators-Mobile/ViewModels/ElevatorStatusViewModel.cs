using System;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile.ViewModels
{
    internal class ElevatorStatusViewModel : BaseViewModel, IQueryAttributable
    {


        /// <summary> Used in ElevevatorStatusPage for Binding on Status.</summary>
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
        // For use inside the ViewModels only.
        private string status;


        /// <summary>Used in ElevevatorStatusPage for Binding on Color.</summary>
        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }
        // For use inside the ViewModels only.
        private string color;

        /// <summary>Id of the selected Elevator Model.</summary>
        private string Id { get; set; }

        public Command EndIntervention { get; }
        public ElevatorStatusViewModel()
        {
            EndIntervention = new Command(OnButtonClicked);
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            // The query parameter requires URL decoding.
            string id = HttpUtility.UrlDecode(query["id"]);
            // Only used in the OnButtonClicked method.
            Id = id;
            LoadElevator(id);
        }

        /// <summary>
        /// Initialize the Status text then, update it every second.
        /// Status is queried in the database via the api.
        /// </summary>
        /// <param name="id"></param>
        private void LoadElevator(string id)
        {
            try
            {
                // Initialize the Status text.
                Status = ClientService.GetElevatorStatus(id);
                // Initialize the Status textColor.
                Color = "Red";

                //Start the dynamic Status update
                DynamicStatusUpdate();
            }
            catch (Exception e)
            {
                // Would mainly happen if the api call failed
                Console.WriteLine("Failed to load Status.");
                Console.WriteLine($"{e} Exception caught.");
            }
        }

        /// <summary> Check for a change in the status of the elevator in the database.</summary>
        private void DynamicStatusUpdate()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
            {
                try
                {
                    string _status = ClientService.GetElevatorStatus(Id);
                    if (_status != status)
                    {
                        Color = _status == "Online" ? "Green" : "Red";
                        Status = _status;
                    }
                }
                catch (Exception e)
                {
                    // Would mainly happen if the api call failed
                    Console.WriteLine("Failed to Update Status.");
                    Console.WriteLine($"{e} Exception caught.");

                }
                return true;
            });
        }


        public void OnButtonClicked()
        {
            ClientService.UpdateElevatorStatus(Id);
        }
    }
}
