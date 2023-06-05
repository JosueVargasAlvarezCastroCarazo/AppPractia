
using RestSharp;
using System;
using System.Collections.Generic;

namespace AppPractia.ModelsDTOs
{
    public class ProjectDTO
    {
        public ProjectDTO()
        {
           
        }

        public RestRequest Request { get; set; }


        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public int ConstructionStatusId { get; set; }
        public int UserId { get; set; }

        public string ConstructionStatusName { get; set; }
    }
}
