﻿using System;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile.ViewModels
{
    internal class ElevatorStatusViewModel : BaseViewModel, IQueryAttributable
    {

        // Internal variables
        private string status;
        // External variables
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        // Internal variables
        private string color;
        // External variables
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
        // Internal variables
        private string actionText;
        // External variables
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
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                try
                {
                    string _status = ClientService.GetElevatorStatus(Id);
                    if (_status != status)
                    {
                        Color = _status == "Online" ? "Green" : "Red";
                        Status = _status;
                        ActionText = _status == "Online" ? "Return Home" : "End";
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
