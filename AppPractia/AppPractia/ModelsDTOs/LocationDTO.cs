
using RestSharp;
using System;
using System.Collections.Generic;

namespace AppPractia.ModelsDTOs
{
    public class LocationDTO
    {
        public LocationDTO()
        {
        }

        public RestRequest Request { get; set; }


        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
    }
}
