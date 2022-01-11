using Rocket_Elevators_Mobile.Models;
using Rocket_Elevators_Mobile.ViewModels;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}