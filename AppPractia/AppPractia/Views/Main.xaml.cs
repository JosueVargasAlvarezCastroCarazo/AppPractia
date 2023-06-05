using AppPractia.Views.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPractia.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Main : ContentPage
	{
		public Main ()
		{
			InitializeComponent ();

            if (Global.user.UserRolId == 2)
            {
                BtnUsers.IsVisible = false;
            }

		}

        //muestra el texto del usuario
        protected override void OnAppearing()
        {
            TxtUserWelcome.Text = "Bienvenido " + Global.user.Name;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new MyProfile());
        }

        private async void BtnUsers_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new UsersListPage());
        }

        private void BtnProyects_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnLocations_Clicked(object sender, EventArgs e)
        {

        }
    }
}