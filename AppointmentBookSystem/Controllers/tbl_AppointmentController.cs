using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppointmentBookSystem;

namespace AppointmentBookSystem.Controllers
{
    [Authorize]
    public class tbl_AppointmentController : Controller
    {
        private AppointmentBookSystemEntities db = new AppointmentBookSystemEntities();
        [Authorize(Roles ="Admin,Doctor,User")]

        // GET: tbl_Appointment
        public ActionResult Index()
        {
            var tbl_Appointment = db.tbl_Appointment.Include(t => t.tbl_Appointment_Status).Include(t => t.tbl_Doctor).Include(t => t.tbl_Patient);
            return View(db.tbl_Appointment.ToList());
        }
        [Authorize(Roles = "Admin,Doctor")]
        // GET: tbl_Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Appointment tbl_Appointment = db.tbl_Appointment.Find(id);
            if (tbl_Appointment == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Appointment);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: tbl_Appointment/Create
        public ActionResult Create()
        {
            ViewBag.Appoitment_Status = new SelectList(db.tbl_Appointment_Status, "Status_Id", "Status_Name");
            ViewBag.Appoitment_Doctor_Id = new SelectList(db.tbl_Doctor, "Doctor_Id", "Doctor_Name");
            ViewBag.Appoitment_Patient_Id = new SelectList(db.tbl_Patient, "Patient_Id", "Patient_Name");

            return View();
        }

        // POST: tbl_Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public ActionResult Create([Bind(Include = "Appoitment_Id,Appoitment_Date,Appoitment_Time,Appoitment_Details,Appoitment_Status,Appoitment_Patient_Id,Appoitment_Doctor_Id")] tbl_Appointment tbl_Appointment)
        {
            if (ModelState.IsValid)
            {
                tbl_Appointment.Appoitment_Status = 1;
                db.tbl_Appointment.Add(tbl_Appointment);
                db.SaveChanges();
                return RedirectToAction("Create","tbl_Appointment");
            }
            ViewBag.Appoitment_Status = new SelectList(db.tbl_Appointment_Status, "Status_Id", "Status_Name",tbl_Appointment.Appoitment_Patient_Id);
            ViewBag.Appoitment_Doctor_Id = new SelectList(db.tbl_Doctor, "Doctor_Id", "Doctor_Name", tbl_Appointment.Appoitment_Doctor_Id);
            ViewBag.Appoitment_Patient_Id = new SelectList(db.tbl_Patient, "Patient_Id", "Patient_Name", tbl_Appointment.Appoitment_Patient_Id);

            return View(tbl_Appointment);
        }
        [Authorize(Roles = "Admin,Doctor")]
        // GET: tbl_Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Appointment tbl_Appointment = db.tbl_Appointment.Find(id);
            if (tbl_Appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Appoitment_Status = new SelectList(db.tbl_Appointment_Status, "Status_Id", "Status_Name", tbl_Appointment.Appoitment_Patient_Id);
            ViewBag.Appoitment_Doctor_Id = new SelectList(db.tbl_Doctor, "Doctor_Id", "Doctor_Name", tbl_Appointment.Appoitment_Doctor_Id);
            ViewBag.Appoitment_Patient_Id = new SelectList(db.tbl_Patient, "Patient_Id", "Patient_Name", tbl_Appointment.Appoitment_Patient_Id);

            return View(tbl_Appointment);
        }

        // POST: tbl_Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public ActionResult Edit([Bind(Include = "Appoitment_Id,Appoitment_Date,Appoitment_Time,Appoitment_Details,Appoitment_Status,Appoitment_Patient_Id,Appoitment_Doctor_Id")] tbl_Appointment tbl_Appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Appoitment_Status = new SelectList(db.tbl_Appointment_Status, "Status_Id", "Status_Name", tbl_Appointment.Appoitment_Patient_Id);
            ViewBag.Appoitment_Doctor_Id = new SelectList(db.tbl_Doctor, "Doctor_Id", "Doctor_Name", tbl_Appointment.Appoitment_Doctor_Id);
            ViewBag.Appoitment_Patient_Id = new SelectList(db.tbl_Patient, "Patient_Id", "Patient_Name", tbl_Appointment.Appoitment_Patient_Id);

            return View(tbl_Appointment);
        }
        [Authorize(Roles = "Admin")]
        // GET: tbl_Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Appointment tbl_Appointment = db.tbl_Appointment.Find(id);
            if (tbl_Appointment == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Appointment);
        }
        [Authorize(Roles = "Admin")]
        // POST: tbl_Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Appointment tbl_Appointment = db.tbl_Appointment.Find(id);
            db.tbl_Appointment.Remove(tbl_Appointment);
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
