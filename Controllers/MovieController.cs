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
    public class MovieController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            AddMovieVM model = new AddMovieVM();
            var allGenres = GenreManager.GetAll(); //returns List<Genre>
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var genre in allGenres)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = genre.ID,
                    Display = genre.Name,
                    IsChecked = false //On the add view, no genres are selected by default
                });
            }
            model.Genres = checkBoxListItems;
            return View(model);
        }
    }
}