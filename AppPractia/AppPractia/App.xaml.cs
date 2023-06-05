using AppPractia.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPractia
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            App.Current.UserAppTheme = OSAppTheme.Light;
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
