using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ReservationFinalProject.DATA.EF;

namespace ReservationFinalProject.UI.MVC.Controllers
{
    [Authorize]
    public class OwnerAssetsController : Controller
    {
        private ReservationProjectEntities db = new ReservationProjectEntities();

        // GET: OwnerAssets
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                var ownerAssets = db.OwnerAssets.Include(x => x.UserDetail);
                return View(ownerAssets.ToList());
            }
            else
            {
                string currentUser = User.Identity.GetUserId();

                var ownerAssets = db.OwnerAssets.Where(x => x.OwnerID == currentUser);
                return View(ownerAssets.ToList());
            }
        }

        // GET: OwnerAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        [Authorize(Roles = "Client, Admin")]
        // GET: OwnerAssets/Create
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.UserDetails, "UserID", "FirstName");
            return View();
        }

        // POST: OwnerAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerAssetID,AssetName,OwnerID,AssetPhoto,SpecialNotes,IsActive,DateAdded")] OwnerAsset ownerAsset, HttpPostedFileBase assetImage)
        {
            if (ModelState.IsValid)
            {
                //default image
                string imageName = "noImage.png";

                //if file was sent
                if (assetImage != null)
                {
                    imageName = assetImage.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf('.'));

                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    //if extension is valid
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imageName = Guid.NewGuid() + ext;
                        assetImage.SaveAs(Server.
                        MapPath("~/Content/images/Photos/" + imageName));
                    }
                    else
                    {
                        imageName = "noImage.png";
                    }
                }

                ownerAsset.AssetPhoto = imageName;

                //Dynamically grabs current logged in user and sets it as the OwnerID if the user is not an Admin
                if (!User.IsInRole("Admin"))
                {
                    string currentUser = User.Identity.GetUserId();

                    ownerAsset.OwnerID = currentUser;
                }

                //Automatically sets the IsActive property to true
                ownerAsset.IsActive = true;

                //Dynamically adds the current date
                ownerAsset.DateAdded = DateTime.Now;

                db.OwnerAssets.Add(ownerAsset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerAsset.OwnerID);
            return View(ownerAsset);
        }

        // GET: OwnerAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerAsset.OwnerID);
            return View(ownerAsset);
        }

        // POST: OwnerAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerAssetID,AssetName,OwnerID,AssetPhoto,SpecialNotes,IsActive,DateAdded")] OwnerAsset ownerAsset, HttpPostedFileBase assetImage)
        {
            if (ModelState.IsValid)
            {
                if (assetImage != null)
                {
                    string imageName = assetImage.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf('.'));

                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imageName = Guid.NewGuid() + ext;
                        assetImage.SaveAs(Server.MapPath("~/Content/images/Photos/" + imageName));

                        ownerAsset.AssetPhoto = imageName;
                    }
                }

                //Dynamically grabs current logged in user and sets it as the OwnerID if the user is not an Admin
                if (!User.IsInRole("Admin"))
                {
                    string currentUser = User.Identity.GetUserId();

                    ownerAsset.OwnerID = currentUser;
                }

                //Automatically sets the IsActive property to true
                ownerAsset.IsActive = true;

                //Dynamically adds the current date
                ownerAsset.DateAdded = DateTime.Now;

                db.Entry(ownerAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerAsset.OwnerID);
            return View(ownerAsset);
        }

        [Authorize(Roles = "Client, Admin")]
        // GET: OwnerAssets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        // POST: OwnerAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            db.OwnerAssets.Remove(ownerAsset);
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
