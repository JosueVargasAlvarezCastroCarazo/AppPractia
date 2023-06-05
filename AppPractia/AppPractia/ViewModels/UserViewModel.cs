using AppPractia.ModelsDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPractia.ViewModels
{
    class UserViewModel : BaseViewModel
    {

        public UserDTO MyUser { get; set; }

        public UserViewModel()
        {
            MyUser = new UserDTO();
        }



        //verifica el login ademas de retornar un usuario
        public async Task<UserDTO> ValidateLogin(string identification, string password)
        {

            if (IsBusy)
            {
                return new UserDTO();
            }
            IsBusy = true;

            try
            {
                MyUser.Identification = identification;
                MyUser.Password = password;

                UserDTO R = await MyUser.ValidateLogin();

                return R;
            }
            catch (Exception)
            {
                return new UserDTO();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

        //verifica que la identificacion no exista en la base de datos y devuelve un usuario si existiera
        public async Task<UserDTO> CheckIdentification(string identification)
        {

            if (IsBusy)
            {
                return new UserDTO();
            }
            IsBusy = true;

            try
            {
                MyUser.Identification = identification;

                UserDTO R = await MyUser.CheckIdentification();

                return R;
            }
            catch (Exception)
            {
                return new UserDTO();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

        //devuelve un usuario segun un id
        public async Task<UserDTO> GetUserById(int id)
        {

            if (IsBusy)
            {
                return new UserDTO();
            }
            IsBusy = true;

            try
            {
                UserDTO R = await MyUser.GetUserById(id);

                return R;
            }
            catch (Exception)
            {
                return new UserDTO();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

        //crea una una nueva cuenta
        public async Task<bool> CreateAccount(
            string identification, 
            string email, 
            string name, 
            string phoneNumber, 
            string address, 
            string password, 
            bool? active, 
            int userRolId
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyUser.Identification = identification;
                MyUser.Email = email;
                MyUser.Name = name;
                MyUser.PhoneNumber = phoneNumber;
                MyUser.Address = address;
                MyUser.Password = password;
                MyUser.Active = active;
                MyUser.UserRolId = userRolId;


                bool R = await MyUser.CreateAccount();

                return R;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

        //actualiza el perfil de el usuario
        public async Task<bool> UpdateProfile(
            int id,
            string identification,
            string email,
            string name,
            string phoneNumber,
            string address,
            string password,
            bool? active,
            int userRolId
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyUser.UserId = id;
                MyUser.Identification = identification;
                MyUser.Email = email;
                MyUser.Name = name;
                MyUser.PhoneNumber = phoneNumber;
                MyUser.Address = address;
                MyUser.Password = password;
                MyUser.Active = active;
                MyUser.UserRolId = userRolId;

                bool R = await MyUser.UpdateProfile();

                return R;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

        //actualiza el perfil del usaurio ademas de la contraseña
        public async Task<bool> UpdateProfilePassword(
            int id,
            string identification,
            string email,
            string name,
            string phoneNumber,
            string address,
            string password,
            bool? active,
            int userRolId
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyUser.UserId = id;
                MyUser.Identification = identification;
                MyUser.Email = email;
                MyUser.Name = name;
                MyUser.PhoneNumber = phoneNumber;
                MyUser.Address = address;
                MyUser.Password = password;
                MyUser.Active = active;
                MyUser.UserRolId = userRolId;

                bool R = await MyUser.UpdateProfilePassword();

                return R;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }


        //retorna una lista de usuarios
        public async Task<List<UserDTO>> GetList(
         bool Active,
         string search
         )
        {

            if (IsBusy)
            {
                return new List<UserDTO>();
            }
            IsBusy = true;

            try
            {


                List<UserDTO> R = await MyUser.GetList(Active, search);

                return R;
            }
            catch (Exception)
            {
                return new List<UserDTO>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }


    }
}
