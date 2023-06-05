
using AppPractia.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AppPractia.ModelsDTOs
{
    public class UserRolDTO
    {
        public UserRolDTO()
        {
        }

        public RestRequest Request { get; set; }


        public int UserRolId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }


        //trae una lista de elementos
        public async Task<List<UserRolDTO>> GetList()
        {
            try
            {
                string RouteSufix = "";

                RouteSufix = string.Format("UserRols");
              

                string URL = APIConnection.ProductionUrlPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                Request.AddHeader(APIConnection.ApiKeyName, APIConnection.ApiKey);
                Request.AddHeader(APIConnection.ContentType, APIConnection.MimeType);

                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (
                    statusCode == HttpStatusCode.OK ||
                    statusCode == HttpStatusCode.Created ||
                    statusCode == HttpStatusCode.NoContent
                    )
                {

                    return JsonConvert.DeserializeObject<List<UserRolDTO>>(response.Content);
                }
                else
                {
                    return new List<UserRolDTO>();
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                throw;
            }
        }

    }
}
