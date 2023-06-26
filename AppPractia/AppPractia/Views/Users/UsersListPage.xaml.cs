using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
using AppPractia.Views.Materials;
using AppPractia.Views.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPractia.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersListPage : ContentPage
    {

        UserViewModel ViewModel;
        ProjectsPage Page;
        bool SelectionMode;

        //entrada inicial a la lista de usuarios
        public UsersListPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserViewModel();
        }

        //entrada desde la pagina de proyecto para seleccionar el administrador
        public UsersListPage(ProjectsPage PageForSelect )
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserViewModel();
            Page = PageForSelect;
            SelectionMode = true;
            SwitchShowDisableStack.IsVisible = false;
        }

        //muestra la lista cuando se enseña la pantalla
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<UserDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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

        //filtra la lista entre activos y inactivos

        private async void SwitchShowDisable_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<UserDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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

        //va a la pantalla de crear nuevo usuario
        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsersPage());
        }

        //accion al toca un item de la lista
        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            if (SelectionMode)
            {
                Page.SelectedUser = (ListPage.SelectedItem as UserDTO);
                await this.Navigation.PopAsync();
            }
            else
            {
                await this.Navigation.PushAsync(new UsersPage((ListPage.SelectedItem as UserDTO), false));
            }
        }

        //boton de buscar que filtra la lista
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<UserDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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