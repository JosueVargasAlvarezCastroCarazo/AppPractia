using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
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
    public partial class WorkersPage : ContentPage
    {
        UserViewModel ViewModel;
        UserConstructionViewModel ViewModelGroup;
        private int project = 0;

        public WorkersPage(int projectId)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserViewModel();
            ViewModelGroup = new UserConstructionViewModel();
            project = projectId;
        }

        //muestra la lista cuando se enseña la pantalla
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<UserDTO> list = await ViewModel.GetList(true, TxtSearch.Text.Trim());
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

        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {

                if ((ListPage.SelectedItem as UserDTO) == null)
                {
                    await DisplayAlert("Atención", "Seleccione el item antes de agregarlo por seguridad", "Aceptar");
                }
                else
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");
                    bool R = true;
                    R = await ViewModelGroup.Create(
                        (ListPage.SelectedItem as UserDTO).UserId,
                         project
                    );

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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<UserDTO> list = await ViewModel.GetList(true, TxtSearch.Text.Trim());
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
}