using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoUpload.Models;
using System.Collections.Generic;


using PhotoUpload.Data;


namespace PhotoUpload.Controllers
{
    public class PhotoController : Controller
    {

        private readonly ApplicationDbContext _context;
        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }
      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }
         public IActionResult UploadFiles()
        {
            return View();
        }
        [HttpPost("FileUpload")]
         public async Task<IActionResult> NewStudent(Student p)
        {
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    var uploads = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/studentimages");
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse
                            (file.ContentDisposition).FileName.Trim('"');

                        System.Console.WriteLine(fileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                              
                        p.Image = p.Image +"-"+ file.FileName;
                         }
                   }
                }
            }
             _context.Students.Add(p);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    // process uploaded files
    // Don't rely on or trust the FileName property without validation. }
            public IActionResult All()
            {
                return View(_context.Students.ToList());
            }
              public IActionResult Details(int id)
            {
                
                
                 var data = _context.Students.FirstOrDefault(x=>x.Id==id);
                 string[] filenames = data.Image.Split('-');
                                   return View(data);

            }

    }
}