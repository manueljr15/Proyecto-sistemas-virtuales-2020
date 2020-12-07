using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemasVirtuales.Models;

namespace SistemasVirtuales.Controllers
{
    public class HomeController : Controller
    {
        private SistemasVirtualesEntities db = new SistemasVirtualesEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["rol"] = null;
            Session["id"] = null;
            Session["Nombre"] = null;
            return RedirectToAction("Login","Home");
        }

        [HttpPost]
        public ActionResult Login(string pwd,string user)
        {
            var Admin = db.Admins.Where(s=>(s.usuario.Equals(user) || s.correo.Equals(user)) && s.pwd.Equals(pwd)).FirstOrDefault();
            if (Admin != null)
            {
                Session["rol"] = "Admin";
                Session["id"] = Admin.id_Admi;
                Session["Nombre"] = Admin.nombre + " " + Admin.ap_pat + " " + Admin.ap_mat;
                return RedirectToAction("Bienvenido","Admins",new {id= Admin.id_Admi});
                
            }
            else
            {
                var Docente= db.Docentes.Where(s => ((s.nombre+"."+s.ap_pat).Equals(user) || s.correo.Equals(user)) && s.pwd.Equals(pwd)).FirstOrDefault();
                if(Docente != null)
                {
                    Session["rol"] = "Docente";
                    Session["id"] = Docente.id_Docente;
                    Session["Nombre"] = Docente.nombre + " " + Docente.ap_pat + " " + Docente.ap_mat;
                    return RedirectToAction("Bienvenido", "Docentes", new { id = Docente.id_Docente });
                }
            }
            ViewBag.Error = "Usuario no valido";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}