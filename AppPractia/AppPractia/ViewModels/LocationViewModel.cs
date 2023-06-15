using AppPractia.ModelsDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPractia.ViewModels
{
    internal class LocationViewModel : BaseViewModel
    {

        public LocationDTO MyLocation { get; set; }

        public LocationViewModel()
        {
            MyLocation = new LocationDTO();
        }



        //crea una una nueva localizacion
        public async Task<bool> Create(
            string name,
            string description,
            bool? active
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyLocation.Name = name;
                MyLocation.Description = description;
                MyLocation.Active = active;
 
                bool R = await MyLocation.Create();

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


        //crea una una nueva localizacion
        public async Task<bool> Update(
            int id,
            string name,
            string description,
            bool? active
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyLocation.LocationId = id;
                MyLocation.Name = name;
                MyLocation.Description = description;
                MyLocation.Active = active;

                bool R = await MyLocation.Update();

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




        //retorna una lista de localizaciones
        public async Task<List<LocationDTO>> GetList(
         bool Active,
         string search
         )
        {

            if (IsBusy)
            {
                return new List<LocationDTO>();
            }
            IsBusy = true;

            try
            {


                List<LocationDTO> R = await MyLocation.GetList(Active, search);

                return R;
            }
            catch (Exception)
            {
                return new List<LocationDTO>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }








    }
}
