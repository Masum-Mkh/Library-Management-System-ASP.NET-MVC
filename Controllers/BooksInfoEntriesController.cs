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
    public class BooksInfoEntriesController : Controller
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: BooksInfoEntries
        public async Task<ActionResult> Index()
        {
            return View(await db.BooksInfoEntries.ToListAsync());
        }

        // GET: BooksInfoEntries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksInfoEntry booksInfoEntry = await db.BooksInfoEntries.FindAsync(id);
            if (booksInfoEntry == null)
            {
                return HttpNotFound();
            }
            return View(booksInfoEntry);
        }

        // GET: BooksInfoEntries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksInfoEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Book_ID,Book_Name,Author_Name,Publication,Purchase_Date,Book_Price,Books_Quantity")] BooksInfoEntry booksInfoEntry)
        {
            if (ModelState.IsValid)
            {
                db.BooksInfoEntries.Add(booksInfoEntry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(booksInfoEntry);
        }

        // GET: BooksInfoEntries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksInfoEntry booksInfoEntry = await db.BooksInfoEntries.FindAsync(id);
            if (booksInfoEntry == null)
            {
                return HttpNotFound();
            }
            return View(booksInfoEntry);
        }

        // POST: BooksInfoEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Book_ID,Book_Name,Author_Name,Publication,Purchase_Date,Book_Price,Books_Quantity")] BooksInfoEntry booksInfoEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksInfoEntry).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(booksInfoEntry);
        }

        // GET: BooksInfoEntries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksInfoEntry booksInfoEntry = await db.BooksInfoEntries.FindAsync(id);
            if (booksInfoEntry == null)
            {
                return HttpNotFound();
            }
            return View(booksInfoEntry);
        }

        // POST: BooksInfoEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BooksInfoEntry booksInfoEntry = await db.BooksInfoEntries.FindAsync(id);
            db.BooksInfoEntries.Remove(booksInfoEntry);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileResult GenerateReport(string format = "pdf")
        {
            var data = db.vwBooks.ToList();
            ReportDocument rd = new ReportDocument();
            string reportPath = Server.MapPath("~/Reports/BookReport.rpt");
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
