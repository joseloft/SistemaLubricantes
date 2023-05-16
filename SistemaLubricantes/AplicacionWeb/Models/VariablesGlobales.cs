using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AplicacionWeb.Models
{
    public class VariablesGlobales
    {
        public static string UriApi
        {
            get { return ConfigurationManager.AppSettings["uriApiComercio"]; }
        }
    }
}