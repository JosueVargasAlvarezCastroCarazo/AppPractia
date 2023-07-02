using Acr.UserDialogs;
using AppPractia.ModelsDTOs;
using AppPractia.ViewModels;
using AppPractia.Views.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace AppPractia.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        UserViewModel ViewModel;
        public UserDTO CurrentItem = null;

        UserRolViewModel UserRolViewModel;
        List<UserRolDTO> UserRolList;

        //entrada de la pantalla de usuario
        public UsersPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new UserViewModel();
            UserRolViewModel = new UserRolViewModel();
            getInitialList();
        }

        //carga un Elemento para editarlo o eliminarlo
        public UsersPage(UserDTO currentItem,bool watchMode)
        {
            InitializeComponent();
            this.CurrentItem = currentItem;

            this.BindingContext = ViewModel = new UserViewModel();
            UserRolViewModel = new UserRolViewModel();
            getInitialList();

            TxtIdentification.Text = currentItem.Identification;
            TxtName.Text = currentItem.Name;
            TxtPhoneNumber.Text = currentItem.PhoneNumber;
            TxtAddress.Text = currentItem.Address;
            TxtEmail.Text = currentItem.Email;

            LblHintPassword.IsVisible = false;

            BtnActionDelete.IsVisible = true;
            BtnResetPassword.IsVisible = true;

            if ((bool)currentItem.Active)
            {
                BtnActionDelete.Text = "Eliminar";
            }
            else
            {
                BtnActionDelete.Text = "Restaurar";
            }

            if (watchMode)
            {
                TxtIdentification.IsReadOnly = true;
                TxtName.IsReadOnly = true;
                TxtPhoneNumber.IsReadOnly = true;
                TxtAddress.IsReadOnly = true;
                TxtEmail.IsReadOnly = true;
                PckrUserRole.IsEnabled = false;
                BtnActionDelete.IsVisible = false;
                BtnAction.IsVisible = false;
                BtnResetPassword.IsVisible = false;
            }

            ProjectMenuContainer.IsVisible = true;

        }

        //trae los roles iniciales del usuario
        private async void getInitialList()
        {
            UserRolList = await UserRolViewModel.GetList();
            PckrUserRole.ItemsSource = UserRolList;

            if (CurrentItem != null)
            {
                PckrUserRole.SelectedItem = UserRolList.Find(e => e.UserRolId == CurrentItem.UserRolId);
            }

        }


        //actualiza la informacion del perfil o crea uno nuevo
        private async void BtnAction_Clicked(object sender, EventArgs e)
        {
            if (CurrentItem != null && CurrentItem.UserId == Global.user.UserId)
            {
                await DisplayAlert("Atención", "Actualice su información desde la pestaña de mi perfil", "Aceptar");
            }
            else
            {
                if (
                    (TxtIdentification.Text != null && !string.IsNullOrEmpty(TxtIdentification.Text.Trim())) &&
                    (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
                    (TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim())) &&
                    (TxtAddress.Text != null && !string.IsNullOrEmpty(TxtAddress.Text.Trim())) &&
                    (TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim())) &&
                    PckrUserRole.SelectedItem != null
              )
                {

                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");

                        bool R = true;

                        UserDTO checkIdentificationUser = await ViewModel.CheckIdentification(TxtIdentification.Text.Trim());

                        if (CurrentItem != null)
                        {

                            if (checkIdentificationUser.Identification != null && checkIdentificationUser.UserId != CurrentItem.UserId)
                            {
                                await DisplayAlert("Atención", "Esta identificación ya esta registrada", "Aceptar");
                                return;
                            }

                            R = await ViewModel.UpdateProfile(
                                CurrentItem.UserId,
                                TxtIdentification.Text.Trim(),
                                TxtEmail.Text.Trim(),
                                TxtName.Text.Trim(),
                                TxtPhoneNumber.Text.Trim(),
                                TxtAddress.Text.Trim(),
                                CurrentItem.Password,
                                CurrentItem.Active,
                                (PckrUserRole.SelectedItem as UserRolDTO).UserRolId
                            );
                        }
                        else
                        {

                            if (!String.IsNullOrEmpty((await ViewModel.CheckIdentification(TxtIdentification.Text.Trim())).Identification))
                            {
                                await DisplayAlert("Atención", "Esta identificación ya esta registrada", "Aceptar");
                                return;
                            }

                            R = await ViewModel.CreateAccount(
                                TxtIdentification.Text.Trim(),
                                TxtEmail.Text.Trim(),
                                TxtName.Text.Trim(),
                                TxtPhoneNumber.Text.Trim(),
                                TxtAddress.Text.Trim(),
                                "Asp128..M",
                                true,
                                (PckrUserRole.SelectedItem as UserRolDTO).UserRolId
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
        }


        //elimina o restaura un perfil
        private async void BtnActionDelete_Clicked(object sender, EventArgs e)
        {
            if (CurrentItem.UserId == Global.user.UserId)
            {
                await DisplayAlert("Atención", "Actualice su información desde la pestaña de mi perfil", "Aceptar");
            }
            else
            {

                if (
                (TxtIdentification.Text != null && !string.IsNullOrEmpty(TxtIdentification.Text.Trim())) &&
                (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
                (TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim())) &&
                (TxtAddress.Text != null && !string.IsNullOrEmpty(TxtAddress.Text.Trim())) &&
                (TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim())) &&
                PckrUserRole.SelectedItem != null
                )
                {


                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");
                        bool R = await ViewModel.UpdateProfile(
                            CurrentItem.UserId,
                            CurrentItem.Identification,
                            CurrentItem.Email,
                            CurrentItem.Name,
                            CurrentItem.PhoneNumber,
                            CurrentItem.Address,
                            CurrentItem.Password,
                            !CurrentItem.Active,
                            CurrentItem.UserRolId
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
        }


        //cambia la contraseña a la por defecto
        private async void BtnResetPassword_Clicked(object sender, EventArgs e)
        {
            if (CurrentItem.UserId == Global.user.UserId)
            {
                await DisplayAlert("Atención", "Actualice su información desde la pestaña de mi perfil", "Aceptar");
            }
            else
            {

                if (
                (TxtIdentification.Text != null && !string.IsNullOrEmpty(TxtIdentification.Text.Trim())) &&
                (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim())) &&
                (TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim())) &&
                (TxtAddress.Text != null && !string.IsNullOrEmpty(TxtAddress.Text.Trim())) &&
                (TxtEmail.Text != null && !string.IsNullOrEmpty(TxtEmail.Text.Trim())) &&
                PckrUserRole.SelectedItem != null
                )
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Cargando..");
                        bool R = await ViewModel.UpdateProfilePassword(
                            CurrentItem.UserId,
                            CurrentItem.Identification,
                            CurrentItem.Email,
                            CurrentItem.Name,
                            CurrentItem.PhoneNumber,
                            CurrentItem.Address,
                            "Asp128..M",
                            CurrentItem.Active,
                            CurrentItem.UserRolId
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
        }

        //esta funcion se utiliza para poder visualizar los proyectos en los que se encuentra un usuario como miembro de planilla
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new ProjectsListPage(CurrentItem));
        }
    }
}