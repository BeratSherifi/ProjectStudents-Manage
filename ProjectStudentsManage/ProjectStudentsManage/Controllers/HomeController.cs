using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectStudentsManage.Models;

namespace ProjectStudentsManage.Controllers
{
    public class HomeController : Controller
    {
        DBStudentManageEntities2 db = new DBStudentManageEntities2();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

     

        [HttpPost]
        public ActionResult Index(user log)
        {
            var user = db.users.Where(x => x.Username == log.Username && x.Password == log.Password).Count();
            if (user > 0)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InsertTBLClassRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertTBLClassRoom(TBLClassRoom tBLClassRoom)
        {
            db.TBLClassRooms.Add(tBLClassRoom);
            db.SaveChanges();
            return View();
        }

        public PartialViewResult PrinfTBLClassRoom()
        {
            var listClassRoom = db.TBLClassRooms.ToList();
            return PartialView(listClassRoom);
        }

        public ActionResult DeleteTBLClassRoom(int id)
        {
            try
            {
                var DeleteTBLClassRoom = db.TBLClassRooms.Where(x => x.IdCr == id).FirstOrDefault();
                db.TBLClassRooms.Remove(DeleteTBLClassRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult AddTblStudent(int id)
        {
            ViewBag.Test = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddTblStudent(TBLStudent tBLStudent, int id)
        {
            db.TBLStudents.Add(tBLStudent);
            db.SaveChanges();
            return View();
        }

        public ActionResult ListTBLStudent(int id)
        {

            var listStudent = (from cr in db.TBLClassRooms
                               from st in db.TBLStudents
                               where cr.IdCr == st.IdCr && id == st.IdCr
                               select st).ToList();
            return View(listStudent);
        }

        public ActionResult DeleteTBLStudent(int id)
        {
            try
            {
                var DeleteTBLStudent = db.TBLStudents.Where(x => x.IdSt == id).FirstOrDefault();
                db.TBLStudents.Remove(DeleteTBLStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLStudent student = db.TBLStudents.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TBLStudent student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLStudent student = db.TBLStudents.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        //

        public ActionResult ShowAllTBLStudent()
        {
            var ShowAllstudent = db.TBLStudents.ToList();
            return View(ShowAllstudent);
        }

        public ActionResult OrderByAscId()
        {
            var OrderByAscId = db.TBLStudents.OrderBy(x => x.IdSt).ToList();
            return View(OrderByAscId);
        }

        public ActionResult OrderByDscId()
        {
            var OrderByDscId = db.TBLStudents.OrderByDescending(x => x.IdSt).ToList();
            return View(OrderByDscId);
        }

    }
}