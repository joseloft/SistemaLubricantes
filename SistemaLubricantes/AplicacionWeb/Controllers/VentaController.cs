using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWeb.Controllers
{
    public class VentaController : Controller
    {
        // GET: Venta
        public ActionResult Ventas()
        {
            return View();
        }
    }
}