using Rocket_Elevators_Mobile.Views;
using Xamarin.Forms;


namespace Rocket_Elevators_Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string isRunning;
        public string IsRunning
        {
            get => isRunning;
            set => SetProperty(ref isRunning, value);
        }

        public Command LoginCommand { get; }

        private bool ValidateLogin()
        {
            return !string.IsNullOrWhiteSpace(email);
        }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked, ValidateLogin);
            // If email field is empty, button will be disabled.
            PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }

        private async void OnLoginClicked()
        {
            IsRunning = "true";
            // Verify that an employee with that email exist in database
            bool EmailVerification = await ClientService.VerifyEmployeeEmail(email);
            IsRunning = "false";
            if (EmailVerification)
            {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            else
            {
                // Display an alert if the email verification fails
                await MessageService.DisplayAlert("Invalid Agent Email Address");
            }
        }
    }
}
