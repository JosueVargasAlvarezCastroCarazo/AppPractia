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
    public partial class QuickMovePage : ContentPage
    {

        private int project = 0;
        public LocationDTO SelectedLocation;
        MaterialViewModel ViewModel;

        //entrada general que recibe el id de proyecto necesario
        public QuickMovePage(int projectId)
        {
            InitializeComponent();
            project = projectId;
            this.BindingContext = ViewModel = new MaterialViewModel();
        }

        //carga los materiales una vez que la localizacion fuera seleccionada

        protected async override void OnAppearing()
        {
            if (SelectedLocation != null)
            {
                TxtLocation.Text = SelectedLocation.Name;
                try
                {

                    UserDialogs.Instance.ShowLoading("Cargando..");
                    List<MaterialDTO> list = await ViewModel.GetList(true, "", project);
                    ListPage.ItemsSource = list;

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
        }

        //va a la pantalla de seleccionar localizacion
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationsListPage(this));
        }

        //actualiza la localizacion del material seleccionado
        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            MaterialDTO item = (ListPage.SelectedItem as MaterialDTO);

            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");

                bool R = true;

                if (item != null)
                {


                    R = await ViewModel.Update(
                        item.MaterialId,
                        item.Name,
                        item.Description,
                        item.Notes,
                        item.Amount,
                        (bool)item.Active,
                        item.ProjectId,
                        SelectedLocation.LocationId
                    );

                    if (R)
                    {

                        List<MaterialDTO> list = await ViewModel.GetList(true, "", project);
                        ListPage.ItemsSource = list;

                        await DisplayAlert("Atención", "Proceso fializado correctamente", "Aceptar");

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
    }
}