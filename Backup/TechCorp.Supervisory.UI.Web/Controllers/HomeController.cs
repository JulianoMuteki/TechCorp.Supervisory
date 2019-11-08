using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechCorp.Supervisory.UI.Web.Models;

namespace TechCorp.Supervisory.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();

        }

        public JsonResult ButtonRelePress(string comando)
        {
            Fachada fachada = new Fachada();
          var retorno =  fachada.ButtonRelePress(comando);
             return Json( retorno , JsonRequestBehavior.AllowGet ); 

        }

        public JsonResult CheckSwitch(int numberSwitch)
        {
            Fachada fachada = new Fachada();
            var retorno = fachada.CheckSwitch(numberSwitch);

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

    }
}
