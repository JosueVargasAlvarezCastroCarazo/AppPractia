using AppPractia.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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




        //trae una lista de elementos
        public async Task<List<ConstructionStatusDTO>> GetList()
        {
            try
            {
                string RouteSufix = "";

                RouteSufix = string.Format("ConstructionStatus");


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

                    return JsonConvert.DeserializeObject<List<ConstructionStatusDTO>>(response.Content);
                }
                else
                {
                    return new List<ConstructionStatusDTO>();
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
