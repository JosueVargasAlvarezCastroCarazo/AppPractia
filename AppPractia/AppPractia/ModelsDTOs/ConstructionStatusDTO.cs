using RestSharp;
using System;
using System.Collections.Generic;

namespace AppPractia.ModelsDTOs
{
    public class ConstructionStatusDTO
    {
        public ConstructionStatusDTO()
        {
        }

        public RestRequest Request { get; set; }

        public int ConstructionStatusId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

    }
}
