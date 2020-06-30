using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BEUShopNearby;

namespace pryShopNearby.Controllers
{
    public class TiendasController : Controller
    {
        private DBShopNearbyEntities1 db = new DBShopNearbyEntities1();

        // GET: Tiendas
        public ActionResult Index()
        {
            var tienda = db.Tienda.Include(t => t.Ciudad).Include(t => t.Provincia).Include(t => t.Sector);
            return View(tienda.ToList());
        }

        // GET: Tiendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tienda tienda = db.Tienda.Find(id);
            if (tienda == null)
            {
                return HttpNotFound();
            }
            return View(tienda);
        }

        // GET: Tiendas/Create
        public ActionResult Create()
        {
            ViewBag.idCiudad = new SelectList(db.Ciudad, "idCiudad", "Nombre");
            ViewBag.idProvincia = new SelectList(db.Provincia, "idProvincia", "Nombre");
            ViewBag.idSector = new SelectList(db.Sector, "idSector", "Nombre");
            return View();
        }

        // POST: Tiendas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTienda,Nombre,R_Social,N_Telefono,Direccion,Estado,Horario,idProvincia,idCiudad,idSector")] Tienda tienda)
        {
            if (ModelState.IsValid)
            {
                db.Tienda.Add(tienda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCiudad = new SelectList(db.Ciudad, "idCiudad", "Nombre", tienda.idCiudad);
            ViewBag.idProvincia = new SelectList(db.Provincia, "idProvincia", "Nombre", tienda.idProvincia);
            ViewBag.idSector = new SelectList(db.Sector, "idSector", "Nombre", tienda.idSector);
            return View(tienda);
        }

        // GET: Tiendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tienda tienda = db.Tienda.Find(id);
            if (tienda == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCiudad = new SelectList(db.Ciudad, "idCiudad", "Nombre", tienda.idCiudad);
            ViewBag.idProvincia = new SelectList(db.Provincia, "idProvincia", "Nombre", tienda.idProvincia);
            ViewBag.idSector = new SelectList(db.Sector, "idSector", "Nombre", tienda.idSector);
            return View(tienda);
        }

        // POST: Tiendas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTienda,Nombre,R_Social,N_Telefono,Direccion,Estado,Horario,idProvincia,idCiudad,idSector")] Tienda tienda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCiudad = new SelectList(db.Ciudad, "idCiudad", "Nombre", tienda.idCiudad);
            ViewBag.idProvincia = new SelectList(db.Provincia, "idProvincia", "Nombre", tienda.idProvincia);
            ViewBag.idSector = new SelectList(db.Sector, "idSector", "Nombre", tienda.idSector);
            return View(tienda);
        }

        // GET: Tiendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tienda tienda = db.Tienda.Find(id);
            if (tienda == null)
            {
                return HttpNotFound();
            }
            return View(tienda);
        }

        // POST: Tiendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tienda tienda = db.Tienda.Find(id);
            db.Tienda.Remove(tienda);
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
