using Xamarin.Forms;

namespace forms_geolocation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as MainViewModel).Init();
        }
    }
}
