using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using Practical_14.Models;

namespace Practical_14.Controllers
{
    public class EmployeeController : Controller
    {
        private Practical_14Entities db = new Practical_14Entities();

        // GET: Employee
        //public ActionResult Index()
        //{
        //    return View(db.employees.ToList());
        //}

        public ActionResult Index(int page = 1) 
        {
           //Practical_14Entities EmployeeEntityContext = new Practical_14Entities();
           //List<employee> employee = EmployeeEntityContext.employee.ToList();
           //return View(EmployeeEntityContext.employee.Where(x => x.Name.StartsWith(search) || search == null).ToList().ToPagedList(i ?? 1, 10));
            
            int recordsPerPage = 10;
            using (var context = new Practical_14Entities())
            {
                var pageResult = context.employee.ToList().ToPagedList(page, recordsPerPage);
                return View(pageResult);
            }
           // return View(db.employees.OrderBy(x => x.Id).ToPagedList(1,10));
        }
        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DOB,Age")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            var res = new employee()
            {
                Name = employee.Name,
                DOB = (DateTime)employee.DOB,
                Age = employee.Age
            };
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DOB,Age")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employee employee = db.employees.Find(id);
            db.employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult GetDate(string SearchString, int pageNo)
        {
            if (SearchString == null)
            {
                return Json(db.employees.OrderBy(x => x.Name).ToPagedList(pageNo,10), JsonRequestBehavior.AllowGet);
            }

            var employees = db.employees.Where(x => x.Name.Contains(SearchString)).OrderBy(x =>x.Name).ToPagedList(pageNo,10);

            return Json(employees, JsonRequestBehavior.AllowGet);

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
