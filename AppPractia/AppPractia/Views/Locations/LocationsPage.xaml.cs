using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPractia.Views.Locations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationsPage : ContentPage
    {

        LocationViewModel ViewModel;
        public LocationDTO CurrentItem = null;
        public LocationsPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new LocationViewModel();
        }

        public LocationsPage(LocationDTO currentItem)
        {
            InitializeComponent();
            this.CurrentItem = currentItem;
            this.BindingContext = ViewModel = new LocationViewModel();


            TxtName.Text = currentItem.Name;
            TxtDescrìption.Text = currentItem.Description;

            BtnActionDelete.IsVisible = true;

            if ((bool)currentItem.Active)
            {
                BtnActionDelete.Text = "Eliminar";
            }
            else
            {
                BtnActionDelete.Text = "Restaurar";
            }

        }

        private async void BtnAction_Clicked(object sender, EventArgs e)
        {
            if (
              (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
              (TxtDescrìption.Text != null && !string.IsNullOrEmpty(TxtDescrìption.Text.Trim()))
            )
            {

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");

                    bool R = true;

                    if (CurrentItem != null)
                    {


                        R = await ViewModel.Update(
                            CurrentItem.LocationId,
                            TxtName.Text.Trim(),
                            TxtDescrìption.Text.Trim(),
                            CurrentItem.Active
                        );
                    }
                    else
                    {

                        R = await ViewModel.Create(
                            TxtName.Text.Trim(),
                            TxtDescrìption.Text.Trim(),
                            true
                        );
                    }

                    if (R)
                    {
                        await DisplayAlert("Atención", "Proceso fializado correctamente", "Aceptar");
                        await this.Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Error inesperado", "Aceptar");
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
                await DisplayAlert("Atención", "Debe de ingresar todos los datos para continuar", "Aceptar");
            }
        }

        private async void BtnActionDelete_Clicked(object sender, EventArgs e)
        {
            if (
              (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
              (TxtDescrìption.Text != null && !string.IsNullOrEmpty(TxtDescrìption.Text.Trim()))
            )
            {

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");

                    bool R = true;

            
                    R = await ViewModel.Update(
                        CurrentItem.LocationId,
                        CurrentItem.Name,
                        CurrentItem.Description,
                        !CurrentItem.Active
                    );
           

                    if (R)
                    {
                        await DisplayAlert("Atención", "Proceso fializado correctamente", "Aceptar");
                        await this.Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Atención", "Error inesperado", "Aceptar");
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
                await DisplayAlert("Atención", "Debe de ingresar todos los datos para continuar", "Aceptar");
            }
        }
    }
}