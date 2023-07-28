using System;
using System.Collections.Generic;
using System.Text;

namespace AppPractia.Services
{
    public static class APIConnection
    {

        //content type para enviar jsons
        public static string MimeType = "application/json";
        public static string ContentType = "Content-Type";


        //urls para conectar api
        public static string ProductionUrlPrefix = "http://192.168.0.15:45455/api/";
        public static string TestingUrlPrefix = "http://192.168.0.15:45455/api/";


        //key de seguridad para los servicios
        public static string ApiKey = "JosueVargasAQWERTQWEQWEQWEQWE";
        public static string ApiKeyName = "P6ApiKey";
    }
}
