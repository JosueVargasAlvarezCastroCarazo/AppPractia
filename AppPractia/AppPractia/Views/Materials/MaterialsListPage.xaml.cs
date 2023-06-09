﻿using Acr.UserDialogs;
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

namespace AppPractia.Views.Materials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialsListPage : ContentPage
    {
        MaterialViewModel ViewModel;

        private int project = 0;

        //entrada que recibe el id de proyecto
        public MaterialsListPage(int projectId)
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new MaterialViewModel();
            project = projectId;
        }

        //muestra la lista cuando se enseña la pantalla
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<MaterialDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim(), project);
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

        //muestra la lista de materiales activos o inactivos
        private async void SwitchShowDisable_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<MaterialDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim(), project);
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

        //va a crear un nuevo material
        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MaterialsPage(project));
        }

        //boton de busqueda que trae la lista de materiales filtrada
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                List<MaterialDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim(), project);
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

        //va a la pantalla de ver material
        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new MaterialsPage((ListPage.SelectedItem as MaterialDTO), project));
        }

        //va a la pantalla de movimiento rapido de material
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new QuickMovePage(project));
        }
    }
}