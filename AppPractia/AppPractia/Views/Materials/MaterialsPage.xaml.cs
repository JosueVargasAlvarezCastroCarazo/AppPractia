using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
using AppPractia.Views.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPractia.Views.Materials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialsPage : ContentPage
    {

        public MaterialDTO CurrentItem = null;
        private int project = 0;
        public LocationDTO SelectedLocation;
        MaterialViewModel ViewModel;

        //entrada de material que recibe un id de proyecto
        public MaterialsPage(int projectId)
        {
            InitializeComponent();
            project = projectId;
            this.BindingContext = ViewModel = new MaterialViewModel();
        }

        //entrada para actualizar un material
        public MaterialsPage(MaterialDTO material, int projectId)
        {
            InitializeComponent();
            CurrentItem = material;
            project = projectId;
            this.BindingContext = ViewModel = new MaterialViewModel();

            TxtName.Text = CurrentItem.Name;
            TxtDescrìption.Text = CurrentItem.Description;
            TxtNotes.Text = CurrentItem.Notes;
            TxtAmount.Text = CurrentItem.Amount+"";
            TxtLocation.Text = CurrentItem.Location.Name;
            SelectedLocation = CurrentItem.Location;

            BtnActionDelete.IsVisible = true;
            BtnAction.Text = "Guardar";


            if ((bool)CurrentItem.Active)
            {
                BtnActionDelete.Text = "Eliminar";
            }
            else
            {
                BtnActionDelete.Text = "Restaurar";
            }
        }

        //cuando aparece la pantalla carga la localizacion
        protected async override void OnAppearing()
        {
            if (SelectedLocation!= null)
            {
                TxtLocation.Text = SelectedLocation.Name;
            }
        }

        //va a la pantalla de localizaciones para seleccionar uno
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationsListPage(this));
        }

        //guarda o actualiza un material
        private async void BtnAction_Clicked(object sender, EventArgs e)
        {
            if (
             (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
             (TxtLocation.Text != null && !string.IsNullOrEmpty(TxtLocation.Text.Trim()))
           )
            {

                if (TxtDescrìption.Text == null || string.IsNullOrEmpty(TxtDescrìption.Text.Trim()))
                {
                    TxtDescrìption.Text = "";
                }

                if (TxtNotes.Text == null || string.IsNullOrEmpty(TxtNotes.Text.Trim()))
                {
                    TxtNotes.Text = "";
                }

                if (TxtAmount.Text == null || string.IsNullOrEmpty(TxtAmount.Text.Trim()))
                {
                    TxtAmount.Text = "1";
                }

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");

                    bool R = true;

                    if (CurrentItem != null)
                    {


                        R = await ViewModel.Update(
                            CurrentItem.MaterialId,
                            TxtName.Text.Trim(),
                            TxtDescrìption.Text.Trim(),
                            TxtNotes.Text.Trim(),
                            int.Parse(TxtAmount.Text.Trim()),
                            (bool)CurrentItem.Active,
                            project,
                            SelectedLocation.LocationId
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
                    else
                    {

                        R = await ViewModel.Create(
                            TxtName.Text.Trim(),
                            TxtDescrìption.Text.Trim(),
                            TxtNotes.Text.Trim(),
                            int.Parse(TxtAmount.Text.Trim()),
                            true,
                            project,
                            SelectedLocation.LocationId
                        );

                        if (R)
                        {
                            await DisplayAlert("Atención", "Material Agregado", "Aceptar");
                            TxtName.Text = "";
                            TxtDescrìption.Text = "";
                            TxtNotes.Text = "";
                            TxtAmount.Text = "1";
                        }
                        else
                        {
                            await DisplayAlert("Atención", "Error inesperado", "Aceptar");
                        }
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
                await DisplayAlert("Atención", "Debe de ingresar Nombre y Localización los datos para continuar", "Aceptar");
            }
        }

        //elimina o restaura un material
        private async void BtnActionDelete_Clicked(object sender, EventArgs e)
        {
            if (
             (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
             (TxtLocation.Text != null && !string.IsNullOrEmpty(TxtLocation.Text.Trim()))
           )
            {

                if (TxtDescrìption.Text == null || string.IsNullOrEmpty(TxtDescrìption.Text.Trim()))
                {
                    TxtDescrìption.Text = "";
                }

                if (TxtNotes.Text == null || string.IsNullOrEmpty(TxtNotes.Text.Trim()))
                {
                    TxtNotes.Text = "";
                }

                if (TxtAmount.Text == null || string.IsNullOrEmpty(TxtAmount.Text.Trim()))
                {
                    TxtAmount.Text = "1";
                }

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");

                    bool R = true;

                    R = await ViewModel.Update(
                        CurrentItem.MaterialId,
                        CurrentItem.Name,
                        CurrentItem.Description,
                        CurrentItem.Notes,
                        CurrentItem.Amount,
                        !(bool)CurrentItem.Active,
                        project,
                        CurrentItem.Location.LocationId
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
                await DisplayAlert("Atención", "Debe de ingresar Nombre y Localización los datos para continuar", "Aceptar");
            }
        }

        //reduce en 1 la cantidad de materiales
        private void BtnMinus_Clicked(object sender, EventArgs e)
        {
            if (int.Parse(TxtAmount.Text) > 0)
            {
                TxtAmount.Text = (int.Parse(TxtAmount.Text) - 1) + "";
            }
           
        }

        //aumenta en 1 la cantidad de materiales
        private void BtnPlus_Clicked(object sender, EventArgs e)
        {
            TxtAmount.Text = (int.Parse(TxtAmount.Text) + 1) + "";
        }


        //metodos que modifican la cantidad de materiales en una cantidad especifica
        private void BtnFive_Clicked(object sender, EventArgs e)
        {
            TxtAmount.Text = 5 + "";
        }

        private void BtnTen_Clicked(object sender, EventArgs e)
        {
            TxtAmount.Text = 10 + "";
        }

        private void BtnFifteen_Clicked(object sender, EventArgs e)
        {
            TxtAmount.Text = 15 + "";
        }

        private void BtnTwenty_Clicked(object sender, EventArgs e)
        {
            TxtAmount.Text = 20 + "";
        }

        private void BtnTwentyFive_Clicked(object sender, EventArgs e)
        {
            TxtAmount.Text = 25 + "";
        }
    }
}