using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemasVirtuales.Models;

namespace SistemasVirtuales.Controllers
{
    public class Tutores_AlumnosController : Controller
    {
        private SistemasVirtualesEntities db = new SistemasVirtualesEntities();

        // GET: Tutores_Alumnos
        public ActionResult Index()
        {
            var tutores_Alumnos = db.Tutores_Alumnos.Include(t => t.Alumnos).Include(t => t.Docentes).Include(t => t.Semestre);
            return View(tutores_Alumnos.ToList());
        }

        // GET: Tutores_Alumnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutores_Alumnos tutores_Alumnos = db.Tutores_Alumnos.Find(id);
            if (tutores_Alumnos == null)
            {
                return HttpNotFound();
            }
            return View(tutores_Alumnos);
        }

        // GET: Tutores_Alumnos/Create
        public ActionResult Create(int id)
        {
           
            ViewBag.Id = id;
            var semestre = db.Semestre.OrderByDescending(s => s.id_semestre).FirstOrDefault();
            ViewBag.id_Alumno = new SelectList(db.Alumnos.OrderBy(s=>s.matricula), "id_Alumnos", "matricula");
            ViewBag.id_Docente = new SelectList(db.Docentes.OrderBy(s=>s.ap_pat), "id_Docente", "nombre",id);
            ViewBag.id_Semetre = new SelectList(db.Semestre.OrderBy(s=>s.inicio), "id_semestre", "folio");
            ;
            var tutela = db.Tutores_Alumnos.Where(s => s.id_Docente == id && s.id_Semetre == semestre.id_semestre);
            List<Alumnos> Tutelados = new List<Alumnos>();
            foreach (var alumno in tutela)
            {
                Tutelados.Add(db.Alumnos.Find(alumno.id_Alumno));
            }
            ViewBag.Tutelados = Tutelados;
            return View();
        }

        // POST: Tutores_Alumnos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_relacion,id_Alumno,id_Docente,id_Semetre")] Tutores_Alumnos tutores_Alumnos)
        {
            var validar = db.Tutores_Alumnos.Where(s => s.id_Alumno == tutores_Alumnos.id_Alumno && s.id_Semetre == tutores_Alumnos.id_Semetre).FirstOrDefault();
            if (validar == null)
            {
                if (ModelState.IsValid)
                {
                    db.Tutores_Alumnos.Add(tutores_Alumnos);
                    db.SaveChanges();
                   
                }
            }
            return RedirectToAction("Index","Docentes");


            /*
            ViewBag.id_Alumno = new SelectList(db.Alumnos, "id_Alumnos", "matricula", tutores_Alumnos.id_Alumno);
            ViewBag.id_Docente = new SelectList(db.Docentes, "id_Docente", "nombre", tutores_Alumnos.id_Docente);
            ViewBag.id_Semetre = new SelectList(db.Semestre, "id_semestre", "folio", tutores_Alumnos.id_Semetre);
            return View(tutores_Alumnos);*/
        }

        // GET: Tutores_Alumnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutores_Alumnos tutores_Alumnos = db.Tutores_Alumnos.Find(id);
            if (tutores_Alumnos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Alumno = new SelectList(db.Alumnos, "id_Alumnos", "matricula", tutores_Alumnos.id_Alumno);
            ViewBag.id_Docente = new SelectList(db.Docentes, "id_Docente", "nombre", tutores_Alumnos.id_Docente);
            ViewBag.id_Semetre = new SelectList(db.Semestre, "id_semestre", "folio", tutores_Alumnos.id_Semetre);
            return View(tutores_Alumnos);
        }

        // POST: Tutores_Alumnos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_relacion,id_Alumno,id_Docente,id_Semetre")] Tutores_Alumnos tutores_Alumnos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutores_Alumnos).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Alumno = new SelectList(db.Alumnos, "id_Alumnos", "matricula", tutores_Alumnos.id_Alumno);
            ViewBag.id_Docente = new SelectList(db.Docentes, "id_Docente", "nombre", tutores_Alumnos.id_Docente);
            ViewBag.id_Semetre = new SelectList(db.Semestre, "id_semestre", "folio", tutores_Alumnos.id_Semetre);
            return View(tutores_Alumnos);
        }

        // GET: Tutores_Alumnos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutores_Alumnos tutores_Alumnos = db.Tutores_Alumnos.Find(id);
            if (tutores_Alumnos == null)
            {
                return HttpNotFound();
            }
            return View(tutores_Alumnos);
        }

        // POST: Tutores_Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tutores_Alumnos tutores_Alumnos = db.Tutores_Alumnos.Find(id);
            db.Tutores_Alumnos.Remove(tutores_Alumnos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
