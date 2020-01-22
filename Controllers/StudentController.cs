using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tp3_MVC.Models;
using Tp3_MVC.ModelsContext;
using System.Xml.Serialization;
using System.Globalization;
using OfficeOpenXml;
using System.Drawing;

namespace Tp3_MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly GroupDbContext _context;

        public StudentController(GroupDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.codeStudent == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("codeStudent,firstName,lastName,old")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("codeStudent,firstName,lastName,old")] Student student)
        {
            if (id != student.codeStudent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.codeStudent))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.codeStudent == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.codeStudent == id);
        }
        public static MemoryStream ListToCsvFile<T>(IEnumerable<T> data)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var csv = new CsvWriter(writer, CultureInfo.GetCultureInfo("en-US"));
            csv.Configuration.Delimiter = ";";
            csv.WriteRecords(data);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
       
        public IActionResult ExportToCSV()
        {
            var data = _context.Students.ToList();
            var stream = ListToCsvFile(data);
            return File(stream, "text/csv", "Students.csv");
        }
        //[HttpGet]
        public IActionResult ExportToEXCEL()
        {
            string[] colNames = new string[]{
                "Code Student",
                "First Name",
                "Last Name",
                "Old"
            };
            var data = _context.Students.ToList();
            byte[] resut;
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add("Students");
                for (int _i = 0; _i < colNames.Length; _i++)
                {
                    ws.Cells[1, _i + 1].Value = colNames[_i];
                    ws.Cells[1, _i + 1].Style.Font.Bold = true;
                    ws.Cells[1, _i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[1, _i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 243, 214));
                }

                var i = 2;
                foreach (var item in data)
                {
                    //Console.WriteLine(item.codeStudent);
                    ws.Cells["A"+ i].Value = item.codeStudent;
                    ws.Cells["A"+i].Style.Font.Bold = true;
                    ws.Cells["B"+i].Value = item.firstName;
                    ws.Cells["B"+i].Style.Font.Bold = true;
                    ws.Cells["C"+i].Value = item.lastName;
                    ws.Cells["C"+i].Style.Font.Bold = true;
                    ws.Cells["D"+i].Value = item.old;
                    ws.Cells["D"+i].Style.Font.Bold = true;
                    i++;

                }
                resut = pck.GetAsByteArray();
            }
            return File(resut, "application/vnd.md-excel", "Students.xlsx");


        }
    }
}
