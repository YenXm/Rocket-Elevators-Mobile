using Rocket_Elevators_Mobile.Services;
using Rocket_Elevators_Mobile.Views;
using Xamarin.Forms;


namespace Rocket_Elevators_Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private readonly IMessageService _messageService = DependencyService.Get<IMessageService>();
        private readonly IClientService _clientService = DependencyService.Get<IClientService>();

        public Command LoginCommand { get; }
        private string email;

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

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }


        private async void OnLoginClicked()
        {
            // Verify that an employee with that email exist in database
            bool EmailVerification = await _clientService.VerifyEmployeeEmail(email);
            if (EmailVerification)
            {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                // Display an alert if the email verification fails
                await _messageService.DisplayAlert("Invalid Email Address");
            }
        }
    }
}
