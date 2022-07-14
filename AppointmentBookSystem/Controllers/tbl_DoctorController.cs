using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AppointmentBookSystem;

namespace AppointmentBookSystem.Controllers
{
    public class tbl_DoctorController : Controller
    {
        private AppointmentBookSystemEntities db = new AppointmentBookSystemEntities();
        [Authorize(Roles = "Admin,User")]
        // GET: tbl_Doctor
        public ActionResult Index()
        {
            var tbl_Doctor = db.tbl_Doctor.Include(t => t.tbl_specialization);
            return View(db.tbl_Doctor.ToList());
        }
        [HttpGet]
        public async Task<ActionResult> Index(String doctorSearch)
        {
            ViewData["GetDoctorDetails"] = doctorSearch;
            var doctorQuery=from x in db.tbl_Doctor select x;
            if(!String.IsNullOrEmpty(doctorSearch))
            {
                doctorQuery = doctorQuery.Where(x => x.Doctor_Name.Contains(doctorSearch) || x.Doctor_Username.Contains(doctorSearch));

            }
            return View(await doctorQuery.AsNoTracking().ToListAsync());

        }
        // GET: tbl_Doctor/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Doctor tbl_Doctor = db.tbl_Doctor.Find(id);
            if (tbl_Doctor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Doctor);
        }
        [Authorize(Roles = "Admin")]
        // GET: tbl_Doctor/Create
        public ActionResult Create()
        {
            ViewBag.Doctor_Specialization_Id = new SelectList(db.tbl_Specialization, "Specialization_Id", "Specialization_Name");
            
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: tbl_Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Doctor_Id,Doctor_Name,Doctor_Username,Doctor_Password,Doctor_Gender,Doctor_Phone,Doctor_Address,Doctor_IsAvailable,Doctor_Specialization_Id")] tbl_Doctor tbl_Doctor)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Doctor.Add(tbl_Doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Doctor_Specialization_Id = new SelectList(db.tbl_Specialization, "Specialization_Id", "Specialization_Name",tbl_Doctor.Doctor_Specialization_Id);

            return View(tbl_Doctor);
        }
        [Authorize(Roles = "Admin")]
        // GET: tbl_Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Doctor tbl_Doctor = db.tbl_Doctor.Find(id);
            if (tbl_Doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Doctor_Specialization_Id = new SelectList(db.tbl_Specialization, "Specialization_Id", "Specialization_Name", tbl_Doctor.Doctor_Specialization_Id);

            return View(tbl_Doctor);
        }

        // POST: tbl_Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Doctor_Id,Doctor_Name,Doctor_Username,Doctor_Password,Doctor_Gender,Doctor_Phone,Doctor_Address,Doctor_IsAvailable,Doctor_Specialization_Id")] tbl_Doctor tbl_Doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Doctor_Specialization_Id = new SelectList(db.tbl_Specialization, "Specialization_Id", "Specialization_Name", tbl_Doctor.Doctor_Specialization_Id);

            return View(tbl_Doctor);
        }
        [Authorize(Roles = "Admin")]
        // GET: tbl_Doctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Doctor tbl_Doctor = db.tbl_Doctor.Find(id);
            if (tbl_Doctor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Doctor);
        }

        // POST: tbl_Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Doctor tbl_Doctor = db.tbl_Doctor.Find(id);
            db.tbl_Doctor.Remove(tbl_Doctor);
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
