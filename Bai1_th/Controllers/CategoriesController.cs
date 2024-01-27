using Bai1_th.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bai1_th.Controllers
{
    public class CategoriesController : Controller
    {
        private DBQLCLOTHEntities db = new DBQLCLOTHEntities();
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.LoaiSanPhams.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "IdCate, NameCate")] LoaiSanPham loaiSanPham)
        {
            if(ModelState.IsValid)
            {
                db.LoaiSanPhams.Add(loaiSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(loaiSanPham);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            if(loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "IdCate,NameCate")] LoaiSanPham loaiSanPham)
        {
            if(ModelState.IsValid)
            {
                db.Entry(loaiSanPham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSanPham);
        }

        public ActionResult Details(int id)
        {
            var loaiSanPham = db.LoaiSanPhams.Where(c => c.IdCate == id).FirstOrDefault();
            return View(loaiSanPham);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            if(loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSanPham loaiSanPham = db.LoaiSanPhams.Find(id);
            db.LoaiSanPhams.Remove(loaiSanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}