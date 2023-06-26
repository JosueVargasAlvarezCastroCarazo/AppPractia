using AppPractia.ModelsDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppPractia.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {

        public ConstructionStatusDTO MyConstructionStatusDTO { get; set; }
        public ProjectDTO MyProjectDTO { get; set; }

        public ProjectViewModel()
        {
            MyConstructionStatusDTO = new ConstructionStatusDTO();
            MyProjectDTO = new ProjectDTO();
        }


        //crea un proyecto
        public async Task<bool> Create(
            string name,
            string description,
            bool? active,
            int constructionStatusId,
            int userId
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyProjectDTO.Name = name;
                MyProjectDTO.Description = description;
                MyProjectDTO.Active = active;
                MyProjectDTO.ConstructionStatusId = constructionStatusId;
                MyProjectDTO.UserId = userId;

                bool R = await MyProjectDTO.Create();

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


        //actualiza un proyecto
        public async Task<bool> Update(
            int id,
            string name,
            string description,
            bool? active,
            int constructionStatusId,
            int userId
            )
        {

            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;

            try
            {
                MyProjectDTO.ProjectId = id;
                MyProjectDTO.Name = name;
                MyProjectDTO.Description = description;
                MyProjectDTO.Active = active;
                MyProjectDTO.ConstructionStatusId = constructionStatusId;
                MyProjectDTO.UserId = userId;

                bool R = await MyProjectDTO.Update();

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


        //retorna una lista de proyectos
        public async Task<List<ProjectDTO>> GetList(
            bool Active, string search, bool admin, int user
         )
        {

            if (IsBusy)
            {
                return new List<ProjectDTO>();
            }
            IsBusy = true;
            try
            {


                List<ProjectDTO> R = await MyProjectDTO.GetList(Active, search,admin,user);

                return R;
            }
            catch (Exception)
            {
                return new List<ProjectDTO>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }


        //retorna una lista de elementos de estatus de proyecto
        public async Task<List<ConstructionStatusDTO>> GetProjectStatusList()
        {

            if (IsBusy)
            {
                return new List<ConstructionStatusDTO>();
            }
            IsBusy = true;

            try
            {

                List<ConstructionStatusDTO> R = await MyConstructionStatusDTO.GetList();

                return R;
            }
            catch (Exception)
            {
                return new List<ConstructionStatusDTO>();
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }
    }
}
