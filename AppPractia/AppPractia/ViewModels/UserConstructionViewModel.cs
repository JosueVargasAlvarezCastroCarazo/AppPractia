using AppPractia.ModelsDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppPractia.ViewModels
{
    internal class UserConstructionViewModel : BaseViewModel
    {

        public UserConstructionDTO MyModel { get; set; }

        public UserConstructionViewModel()
        {
            MyModel = new UserConstructionDTO();
        }


        //retorna una lista de elementos de cuadrilla de proyecto
        public async Task<List<UserConstructionDTO>> GetList(int project)
        {

            if (IsBusy)
            {
                return new List<UserConstructionDTO>();
            }
            IsBusy = true;

            try
            {

                List<UserConstructionDTO> R = await MyModel.GetList(project);

                return R;
            }
            catch (Exception)
            {
                return new List<UserConstructionDTO>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }


        // elimina un elemento de cuadrilla
        public async Task<bool> Delete(
           int id
           )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyModel.UserConstructionId = id;

                bool R = await MyModel.Delete();

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


        //crea un nuevo elemento de la cuadrilla
        public async Task<bool> Create(
           int userId,
           int projectId
           )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyModel.ConstructionId = projectId;
                MyModel.UserId = userId;

                bool R = await MyModel.Create();

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



    }
}
