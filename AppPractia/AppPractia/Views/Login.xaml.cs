using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPractia.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{

        UserViewModel userViewModel;

        int screenCount = 0;

        //entrada de l login
        public Login ()
		{
			InitializeComponent ();
            this.BindingContext = userViewModel = new UserViewModel();


            //si hay credenciales guardades se hace autologin
            if (Preferences.ContainsKey("user"))
            {
                AutoLogin();
            }

        }

        //si se vuelve atras se limpia la sesion
        protected async override void OnAppearing()
        {
            screenCount++;

            if (screenCount > 1)
            {
                Preferences.Clear();
            }
        }

        //ejecuta el autologin
        private async void AutoLogin()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                UserDTO R = await userViewModel.ValidateLogin(Preferences.Get("user", "default_value"), Preferences.Get("password", "default_value"));

                if (R.UserId > 0)
                {

                    if ((bool)R.Active)
                    {
                        Global.user = R;
                        await this.Navigation.PushAsync(new Main());

                    }
                    else
                    {
                        await DisplayAlert("Atención", "Usuario desactivado", "Aceptar");
                    }


                }
                else
                {
                    await DisplayAlert("Atención", "Usuario no Encontrado", "Aceptar");
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        //muestra y esconde la contraseña
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            TxtPassword.IsPassword = !TxtPassword.IsPassword;
        }

        //ejecuta la accion de login
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if (TxtIdentificación.Text != null && !string.IsNullOrEmpty(TxtIdentificación.Text.Trim()))
            {
                if (TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {


                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");
                        UserDTO R = await userViewModel.ValidateLogin(TxtIdentificación.Text.Trim(), TxtPassword.Text.Trim());

                        if (R.UserId > 0)
                        {

                            if ((bool)R.Active)
                            {
                                Global.user = R;
                                Preferences.Set("user", TxtIdentificación.Text.Trim());
                                Preferences.Set("password", TxtPassword.Text.Trim());
                                await this.Navigation.PushAsync(new Main());

                            }
                            else
                            {
                                await DisplayAlert("Atención", "Usuario desactivado", "Aceptar");
                            }


                        }
                        else
                        {
                            await DisplayAlert("Atención", "Usuario no Encontrado", "Aceptar");
                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        UserDialogs.Instance.HideLoading();
                    }

                }
                else
                {
                    await DisplayAlert("Atención", "La contraseña esta en blanco", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Atención", "La identificación esta en blanco", "Aceptar");
            }
        }
    }
}