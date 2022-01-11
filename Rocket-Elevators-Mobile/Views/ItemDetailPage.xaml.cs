using Rocket_Elevators_Mobile.ViewModels;
using Xamarin.Forms;

namespace Rocket_Elevators_Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}