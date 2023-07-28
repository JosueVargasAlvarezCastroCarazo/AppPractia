using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
using AppPractia.Views.Locations;
using AppPractia.Views.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPractia.Views.Workers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkersListPage : ContentPage
    {

        UserConstructionViewModel ViewModel;
        private int project = 0;


        //pantalla de entrada a la cuadrilla de un proyecto
        public WorkersListPage(int projectId)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserConstructionViewModel();
            project = projectId;

            if (!Global.user.IsAdmin())
            {
                DeleteContainer.IsVisible = false;
            }
        }


        //muestra la lista cuando se enseña la pantalla
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<UserConstructionDTO> list = await ViewModel.GetList(project);
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

        //va a agregar un nuevo trabajador a la cuadrilla
        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkersPage(project));
        }

        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

        //elimina un trabajador de la cuadrilla
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                if ((ListPage.SelectedItem as UserConstructionDTO) == null)
                {
                    await DisplayAlert("Atención", "Seleccione el item antes de borrar por seguridad", "Aceptar");
                }
                else
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    bool R = true;
                    R = await ViewModel.Delete(
                        (ListPage.SelectedItem as UserConstructionDTO).UserConstructionId
                    );

                    List<UserConstructionDTO> list = await ViewModel.GetList(project);
                    ListPage.ItemsSource = list;
                    ListPage.SelectedItem = null; 

                    if (R)
                    {
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

        //visualiza un trabajador de la cuadrilla
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {

                if ((ListPage.SelectedItem as UserConstructionDTO) == null)
                {
                    await DisplayAlert("Atención", "Seleccione un item", "Aceptar");
                }
                else
                {
                    await this.Navigation.PushAsync(new UsersPage((ListPage.SelectedItem as UserConstructionDTO).User, true));
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