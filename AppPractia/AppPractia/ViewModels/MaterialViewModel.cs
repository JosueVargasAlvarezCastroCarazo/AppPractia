using AppPractia.ModelsDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppPractia.ViewModels
{
    public class MaterialViewModel : BaseViewModel
    {



        public MaterialDTO MyMaterial { get; set; }

        public MaterialViewModel()
        {
            MyMaterial = new MaterialDTO();
        }


        //retorna una lista de elementos
        public async Task<List<MaterialDTO>> GetList(
            bool active, string search, int project
        )
        {

            if (IsBusy)
            {
                return new List<MaterialDTO>();
            }
            IsBusy = true;

            try
            {

                List<MaterialDTO> R = await MyMaterial.GetList(active, search, project);

                return R;
            }
            catch (Exception)
            {
                return new List<MaterialDTO>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

        //crea una una nueva localizacion
        public async Task<bool> Create(
            string Name,
            string Description,
            string Notes,
            int Amount,
            bool Active,
            int ProjectId,
            int LocationId
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                
                MyMaterial.Name = Name;
                MyMaterial.Description = Description;
                MyMaterial.Notes = Notes;
                MyMaterial.Amount = Amount;
                MyMaterial.Active = Active;
                MyMaterial.ProjectId = ProjectId;
                MyMaterial.LocationId = LocationId;

                bool R = await MyMaterial.Create();

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
            int MaterialId,
            string Name,
            string Description,
            string Notes,
            int Amount,
            bool Active,
            int ProjectId,
            int LocationId
        )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyMaterial.MaterialId = MaterialId;
                MyMaterial.Name = Name;
                MyMaterial.Description = Description;
                MyMaterial.Notes = Notes;
                MyMaterial.Amount = Amount;
                MyMaterial.Active = Active;
                MyMaterial.ProjectId = ProjectId;
                MyMaterial.LocationId = LocationId;


                bool R = await MyMaterial.Update();

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
