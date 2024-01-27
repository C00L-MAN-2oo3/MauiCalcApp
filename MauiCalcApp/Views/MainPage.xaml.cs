
namespace MauiCalcApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();

            var editEntry = OutputText.Handler.PlatformView as Android.Widget.EditText;
            editEntry.Background = null;
        }
    }
}
