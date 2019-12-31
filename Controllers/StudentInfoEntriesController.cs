using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ProjectWebApplicationMVC.Models;

namespace ProjectWebApplicationMVC.Controllers
{
    public class StudentInfoEntriesController : Controller
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: StudentInfoEntries
        public async Task<ActionResult> Index()
        {
            return View(await db.StudentInfoEntries.ToListAsync());
        }

        // GET: StudentInfoEntries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInfoEntry studentInfoEntry = await db.StudentInfoEntries.FindAsync(id);
            if (studentInfoEntry == null)
            {
                return HttpNotFound();
            }
            return View(studentInfoEntry);
        }

        // GET: StudentInfoEntries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentInfoEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Student_ID,Student_Name,Enrollment_No,Department,Student_Semester,Student_Contact,Student_Email")] StudentInfoEntry studentInfoEntry)
        {
            if (ModelState.IsValid)
            {
                db.StudentInfoEntries.Add(studentInfoEntry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(studentInfoEntry);
        }

        // GET: StudentInfoEntries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInfoEntry studentInfoEntry = await db.StudentInfoEntries.FindAsync(id);
            if (studentInfoEntry == null)
            {
                return HttpNotFound();
            }
            return View(studentInfoEntry);
        }

        // POST: StudentInfoEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Student_ID,Student_Name,Enrollment_No,Department,Student_Semester,Student_Contact,Student_Email")] StudentInfoEntry studentInfoEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentInfoEntry).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(studentInfoEntry);
        }

        // GET: StudentInfoEntries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentInfoEntry studentInfoEntry = await db.StudentInfoEntries.FindAsync(id);
            if (studentInfoEntry == null)
            {
                return HttpNotFound();
            }
            return View(studentInfoEntry);
        }

        // POST: StudentInfoEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StudentInfoEntry studentInfoEntry = await db.StudentInfoEntries.FindAsync(id);
            db.StudentInfoEntries.Remove(studentInfoEntry);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileResult GenerateReport(string format = "pdf")
        {
            var data = db.vwStudents.ToList();
            ReportDocument rd = new ReportDocument();
            string reportPath = Server.MapPath("~/Reports/StudentReport.rpt");
            rd.Load(reportPath);
            rd.SetDataSource(data);

            string fileExt, fileType;
            ExportFormatType exportType = ExportFormatType.PortableDocFormat;

            if (format == "excel")
            {
                fileExt = ".xls";
                fileType = "application/vnd.ms-excel";
                exportType = ExportFormatType.Excel;
            }
            else if (format == "word")
            {
                fileExt = ".doc";
                fileType = "application/msword";
                exportType = ExportFormatType.WordForWindows;
            }
            else
            {
                fileExt = ".pdf";
                fileType = "application/pdf";
                exportType = ExportFormatType.PortableDocFormat;
            }

            Response.Buffer = false;
            Response.ClearHeaders();
            Response.ClearContent();

            Stream fStream = rd.ExportToStream(exportType);
            fStream.Seek(0, SeekOrigin.Begin);
            return File(fStream, fileType, $"Students Report{fileExt}");
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
