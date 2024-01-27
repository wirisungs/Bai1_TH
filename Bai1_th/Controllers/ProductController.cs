using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bai1_th.Models;
using System.IO;

namespace Bai1_th.Controllers
{
    public class ProductController : Controller
    {
        private DBQLCLOTHEntities db = new DBQLCLOTHEntities();
        // GET: Products
        public ActionResult Index()
        {
            var SanPhams = db.SanPhams.Include(b => b.LoaiSanPham);
            return View(SanPhams.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        public ActionResult Create()
        {
            ViewBag.IdCate = new SelectList(db.LoaiSanPhams, "IdCate", "NameCate");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPro,NamePro,IdCate,ProImage,Decription,Qty, Price" +
            "CreatedDate, UploadImage")] SanPham sp)
        {
            if (ModelState.IsValid)
            {
                //bo sung doan code de gan duong dan anh cho ProImage va luu anh vao thu muc Images tren server
                if (sp.UploadImage != null)
                {
                    string path = "~/Images/";
                    string filename = Path.GetFileName(sp.UploadImage.FileName);
                    sp.ImagePath = path + filename;
                    sp.UploadImage.SaveAs(Path.Combine(Server.MapPath(path), filename));
                }
                sp.CreatedDate = DateTime.Today;

                //doan code giu nguyen
                db.SanPhams.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCate = new SelectList(db.LoaiSanPhams, "IdCate", "NameCate", sp.IdCate);
            return View(sp);
            //return View();
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCate = new SelectList(db.LoaiSanPhams, "IdCate", "NameCate", sp.IdCate);
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "IdPro,NamePro,IdCate,ImagePath,Decription,Qty,Price" +
            "CreatedDate, UploadImage")] SanPham sp)
        {
            if (ModelState.IsValid)
            {
                if (sp.UploadImage != null)
                {
                    string path = "~/Images/";
                    string filename = Path.GetFileName(sp.UploadImage.FileName);
                    sp.ImagePath = path + filename;
                    sp.UploadImage.SaveAs(Path.Combine(Server.MapPath(path), filename));
                }

                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.LoaiSanPhams, "IdCate", "NameCate", sp.IdCate);
            return View(sp);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sp = db.SanPhams.Find(id);
            db.SanPhams.Remove(sp);
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