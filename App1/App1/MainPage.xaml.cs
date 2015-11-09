using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
            BindingContext = App.Locator.Main;
        }
    }
}
