using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
using AppPractia.Views.Materials;
using AppPractia.Views.Users;
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
    public partial class LocationsListPage : ContentPage
    {

        LocationViewModel ViewModel;
        MaterialsPage Page;
        QuickMovePage MovePage;
        bool SelectionMode;

        public LocationsListPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new LocationViewModel();
        }

        public LocationsListPage(MaterialsPage PageMaterial)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new LocationViewModel();
            Page = PageMaterial;
            SelectionMode = true;
            SwitchShowDisable.IsVisible = false;
            SwitchShowDisableLabel.IsVisible = false;
        }

        public LocationsListPage(QuickMovePage Page)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new LocationViewModel();
            MovePage = Page;
            SelectionMode = true;
            SwitchShowDisable.IsVisible = false;
            SwitchShowDisableLabel.IsVisible = false;
        }


        //muestra la lista cuando se enseña la pantalla
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<LocationDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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
                List<LocationDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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
            await Navigation.PushAsync(new LocationsPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<LocationDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim());
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

            if (SelectionMode)
            {

                if (Page == null)
                {
                    MovePage.SelectedLocation = (ListPage.SelectedItem as LocationDTO);
                    await this.Navigation.PopAsync();
                }
                else
                {
                    Page.SelectedLocation = (ListPage.SelectedItem as LocationDTO);
                    await this.Navigation.PopAsync();
                }
            }
            else
            {
                await this.Navigation.PushAsync(new LocationsPage((ListPage.SelectedItem as LocationDTO)));
            }

            
        }
    }
}