using AppPractia.ModelsDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPractia.ViewModels
{
    public class UserRolViewModel : BaseViewModel
    {

        public UserRolDTO MyUserRol { get; set; }

        public UserRolViewModel()
        {
            MyUserRol = new UserRolDTO();
        }


        //retorna una lista de elementos de rol de usuario
        public async Task<List<UserRolDTO>> GetList()
        {

            if (IsBusy)
            {
                return new List<UserRolDTO>();
            }
            IsBusy = true;

            try
            {

                List<UserRolDTO> R = await MyUserRol.GetList();

                return R;
            }
            catch (Exception)
            {
                return new List<UserRolDTO>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }


    }
}
