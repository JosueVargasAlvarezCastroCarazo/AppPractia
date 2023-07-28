using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
using AppPractia.Views.Materials;
using AppPractia.Views.Users;
using AppPractia.Views.Workers;
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
    public partial class ProjectsPage : ContentPage
    {
        ProjectViewModel ViewModel;
        public ProjectDTO CurrentItem = null;
        List<ConstructionStatusDTO> StatusList;
        public UserDTO SelectedUser;

        //entrada general de pantalla de proyecto
        public ProjectsPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new ProjectViewModel();
            getInitialList();
        }

        //entrada con un proyecto para actualizarlo
        public ProjectsPage(ProjectDTO currentItem)
        {
            InitializeComponent();
            this.CurrentItem = currentItem;
            this.BindingContext = ViewModel = new ProjectViewModel();
            getInitialList();

            ProjectMenuContainer.IsVisible = true;

            TxtName.Text = currentItem.Name;
            TxtDescrìption.Text = currentItem.Description;
            TxtAdministrator.Text = currentItem.User.Name;
            SelectedUser = currentItem.User;

            BtnActionDelete.IsVisible = true;

            if ((bool)currentItem.Active)
            {
                BtnActionDelete.Text = "Eliminar";
            }
            else
            {
                BtnActionDelete.Text = "Restaurar";
            }

            if (!Global.user.IsAdmin())
            {
                BtnActionDelete.IsVisible = false;
            }

        }

        //trae el estado del proyecto y lo carga
        private async void getInitialList()
        {
            StatusList = await ViewModel.GetProjectStatusList();
            PckrStatus.ItemsSource = StatusList;

            if (CurrentItem != null)
            {

                if (StatusList.Find(e => e.ConstructionStatusId == CurrentItem.ConstructionStatusId) != null)
                {
                    PckrStatus.SelectedItem = StatusList.Find(e => e.ConstructionStatusId == CurrentItem.ConstructionStatusId);
                }
 
            }

        }


        //actualiza el administrador del proyecto
        protected async override void OnAppearing()
        {
            if (SelectedUser != null)
            {
                TxtAdministrator.Text = SelectedUser.Name;
            }
        }


        // crea o actualiza un proyecto
        private async void BtnAction_Clicked(object sender, EventArgs e)
        {
            if (
              (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
              (TxtDescrìption.Text != null && !string.IsNullOrEmpty(TxtDescrìption.Text.Trim())) &&
              (TxtAdministrator.Text != null && !string.IsNullOrEmpty(TxtAdministrator.Text.Trim())) &&
               PckrStatus.SelectedItem != null
            )
            {

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");

                    bool R = true;

                    if (CurrentItem != null)
                    {


                        R = await ViewModel.Update(
                            CurrentItem.ProjectId,
                            TxtName.Text.Trim(),
                            TxtDescrìption.Text.Trim(),
                            CurrentItem.Active,
                            (PckrStatus.SelectedItem as ConstructionStatusDTO).ConstructionStatusId,
                            SelectedUser.UserId
                        );
                    }
                    else
                    {

                        R = await ViewModel.Create(
                            TxtName.Text.Trim(),
                            TxtDescrìption.Text.Trim(),
                            true,
                            (PckrStatus.SelectedItem as ConstructionStatusDTO).ConstructionStatusId,
                            Global.user.UserId
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


        //elimina o restaura un proyecto
        private async void BtnActionDelete_Clicked(object sender, EventArgs e)
        {
            if (
              (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
              (TxtDescrìption.Text != null && !string.IsNullOrEmpty(TxtDescrìption.Text.Trim())) &&
              (TxtAdministrator.Text != null && !string.IsNullOrEmpty(TxtAdministrator.Text.Trim())) &&
               PckrStatus.SelectedItem != null
            )
            {

                try
                {
                    UserDialogs.Instance.ShowLoading("Cargando..");

                    bool R = true;


                    R = await ViewModel.Update(
                        CurrentItem.ProjectId,
                        CurrentItem.Name,
                        CurrentItem.Description,
                        !CurrentItem.Active,
                        CurrentItem.ConstructionStatusId,
                        CurrentItem.UserId
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

        //cuadrilla del proyecto
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new WorkersListPage(CurrentItem.ProjectId));
        }

        //Material del proyecto
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new MaterialsListPage(CurrentItem.ProjectId));
        }

        //muestra el detalle de el administrador

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            if (SelectedUser != null)
            {
                await this.Navigation.PushAsync(new UsersPage(SelectedUser, true));
            }
            else
            {
                await DisplayAlert("Atención", "Ingrese un Administrador", "Aceptar");
            }
        }

        //llama a la pantalla de seleccionar administrador
        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new UsersListPage(this));
        }
    }
}