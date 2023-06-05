﻿using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
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
	public partial class Login : ContentPage
	{

        UserViewModel userViewModel;

        public Login ()
		{
			InitializeComponent ();
            this.BindingContext = userViewModel = new UserViewModel();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            TxtPassword.IsPassword = !TxtPassword.IsPassword;
        }

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