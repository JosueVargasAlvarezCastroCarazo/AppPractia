
using RestSharp;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AppPractia.ModelsDTOs
{
    public class UserConstructionDTO
    {
        public UserConstructionDTO()
        {
        }

        public RestRequest Request { get; set; }

        public int UserConstructionId { get; set; }
        public int UserId { get; set; }
        public int ConstructionId { get; set; }

    }
}
