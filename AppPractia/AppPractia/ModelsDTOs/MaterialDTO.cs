
using RestSharp;
using System;
using System.Collections.Generic;

namespace AppPractia.ModelsDTOs
{
    public class MaterialDTO
    {
        public MaterialDTO()
        {
        }

        public RestRequest Request { get; set; }

        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int Amount { get; set; }
        public bool? Active { get; set; }
        public int ProjectId { get; set; }
        public int LocationId { get; set; }

        public string LocationName { get; set; }

    }
}
