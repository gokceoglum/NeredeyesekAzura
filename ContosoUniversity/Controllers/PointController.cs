using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System.ComponentModel;
using System.IO;
using OfficeOpenXml;

namespace ContosoUniversity.Controllers
{
    public class PointController : Controller
    {
        private ProjectContext db = new ProjectContext();
        public List<PointWriter> listCreator()
        {
            List<PointWriter> list = new List<PointWriter>();
            foreach (var p in db.Points)
            {
                PointWriter pw = new PointWriter();
                pw.RestaurantName = db.Restaurants.Find(p.RestaurantID).Name;
                pw.PersonName = db.Persons.Find(p.PersonID).FirstName + " " + db.Persons.Find(p.PersonID).LastName;
                pw.GivenPoint = p.GivenPoint;
                list.Add(pw);
            }
            return list;
        }

        public ActionResult importExcel(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    Console.Write(data);
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        int rowIterator = 2;
                        foreach (var point in db.Points)
                        {
                            int givenPoint = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value.ToString());
                            Point element = point;
                            point.GivenPoint = givenPoint;
                            rowIterator++;
                        }
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index", "Point");
        }

        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }
        public ActionResult exportExcel()
        {
            var data = listCreator();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Contact.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1254");
            Response.Charset = "windows-1254";
            WriteTsv(data, Response.Output);
            Response.End();
            return RedirectToAction("Index", "Point");
        }
        public ActionResult createNewPointTable()
        {
            foreach(var p in db.Points)
            {
                db.Points.Remove(p);
            }
            db.SaveChanges();
            int PointId = 1;
            foreach(var person in db.Persons)
            {
                foreach(var res in db.Restaurants)
                {
                    Point point = new Point();
                    point.ID = PointId;
                    point.RestaurantID = res.ID;
                    point.PersonID = person.ID;
                    point.GivenPoint = 0;
                    db.Points.Add(point);
                    PointId++;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Point");
        }
        // GET: Point
        public ActionResult Index()
        {
            IEnumerable<PointWriter> en = listCreator();
            return View(en);
        }

        // GET: Point/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // GET: Point/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Point/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PersonID,RestaurantID,GivenPoint")] Point point)
        {
            if (ModelState.IsValid)
            {
                db.Points.Add(point);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(point);
        }

        // GET: Point/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // POST: Point/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PersonID,RestaurantID,GivenPoint")] Point point)
        {
            if (ModelState.IsValid)
            {
                db.Entry(point).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(point);
        }

        // GET: Point/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // POST: Point/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Point point = db.Points.Find(id);
            db.Points.Remove(point);
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
