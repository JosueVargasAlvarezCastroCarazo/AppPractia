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

namespace AppPractia.Views.Projects
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectsListPage : ContentPage
    {

        ProjectViewModel ViewModel;

        //entrada general a pantalla de lista de proyectos
        public ProjectsListPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ProjectViewModel();

            if (!Global.user.IsAdmin())
            {
                BtnCreate.IsVisible = false;
            }
        }

        //muestra la lista cuando se enseña la pantalla
        protected async override void OnAppearing()
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<ProjectDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim(), Global.user.IsAdmin(), Global.user.UserId);
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

        //filtra la lista por proyectos activos y inactivos
        private async void SwitchShowDisable_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<ProjectDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim(), Global.user.UserRolId == 1 ? true : false, Global.user.UserId);
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

        //va a crear un nuevo proyecto
        private async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProjectsPage());
        }

        //busca en la lista de proyectos
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Cargando..");
                List<ProjectDTO> list = await ViewModel.GetList(!SwitchShowDisable.IsToggled, TxtSearch.Text.Trim(), Global.user.UserRolId == 1 ? true : false, Global.user.UserId);
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


        // va a la lista de proyecto a actualizar el elemento
        private async void ListPage_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new ProjectsPage((ListPage.SelectedItem as ProjectDTO)));
        }
    }
}