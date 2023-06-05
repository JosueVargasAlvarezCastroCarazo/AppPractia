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

namespace AppPractia.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersListPage : ContentPage
    {

        UserViewModel ViewModel;

        public UsersListPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserViewModel();
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

        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsersPage());
        }

        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new UsersPage((ListPage.SelectedItem as UserDTO)));
        }

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