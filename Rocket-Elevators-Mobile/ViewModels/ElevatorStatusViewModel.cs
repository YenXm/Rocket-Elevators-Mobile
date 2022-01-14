using System;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile.ViewModels
{
    internal class ElevatorStatusViewModel : BaseViewModel, IQueryAttributable
    {

        public bool StopTimer { get; set; }

        private string image;
        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private string status;
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private string color;
        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }

        /*  
        Text for the button that will be either :
        "End" When the elevator status is not "Online"
        and "Return Home" when it is online.
        */
        private string actionText;
        public string ActionText
        {
            get => actionText;
            set => SetProperty(ref actionText, value);
        }

        // Id of the selected Elevator Model.
        private string Id { get; set; }

        public Command BottomButton { get; }
        public ElevatorStatusViewModel()
        {
            BottomButton = new Command(OnButtonClicked);
        }


        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            // The query parameter requires URL decoding.
            string id = HttpUtility.UrlDecode(query["id"]);
            // Only used in the OnButtonClicked method.
            Id = id;
            ActionText = "End";
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
                Image = "Elevator_Not_Working.jpg";

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
            StopTimer = false;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                string _status = ClientService.GetElevatorStatus(Id);
                try
                {

                    if (_status != status)
                    {
                        Color = _status == "Online" ? "Green" : "Red";
                        Status = _status;
                        ActionText = _status == "Online" ? "Return Home" : "End";
                        Image = _status == "Online" ? "AdobeStock_53477661.jpeg" : "Elevator_Not_Working.jpg";
                    }
                }
                catch (Exception e)
                {
                    // Would mainly happen if the api call failed
                    Console.WriteLine("Failed to Update Status.");
                    Console.WriteLine($"{e} Exception caught.");

                }
                return !StopTimer;
            });
        }


        public async void OnButtonClicked()
        {
            if (status == "Online")
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                ClientService.UpdateElevatorStatus(Id);
            }
        }
    }
}
