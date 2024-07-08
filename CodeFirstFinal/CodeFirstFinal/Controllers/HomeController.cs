using CodeFirstFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Xml;

namespace CodeFirstFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TeacherDBContext _teacherDB;

        public HomeController(TeacherDBContext teacherDB,ILogger<HomeController> logger)
        {
            _logger = logger;
            _teacherDB = teacherDB;
        }

        //----------------------------------------------- Index Page rendering data from database ----------------------------

        [HttpGet]
        public IActionResult Index()
        {
            // Retrieve all teacher records from the database and convert them to a list.
            var teacherData = _teacherDB.Teachers.ToList();

            // Return a view to the client, passing the list of teachers as the model.
            return View(teacherData);
        }

        //----------------------------------------------- Adding data ---------------------------------------------------------

        [HttpGet]
        public IActionResult Create()     //--------Showing page------------
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
/*        The[ValidateAntiForgeryToken] attribute is used to prevent CSRF attacks by 
            validating a unique token included with each form submission.
*/        public async Task<IActionResult> Create(Teacher tObj)   //--------------------Form submission--------------
        {
            if (ModelState.IsValid)
            {
               await _teacherDB.Teachers.AddAsync(tObj);
               await _teacherDB.SaveChangesAsync();
                TempData["insert_success"] = " Data Inserted...";
                return RedirectToAction("Index","Home");
            }
            return View(tObj);
        }

        //------------------------------------------- Details  --------------------------------------------------

        public async Task<IActionResult> Details(int? id) 
            
        {
            if(id == null || _teacherDB.Teachers == null)  // null checking
            {
                return NotFound();
            }
            var teacherData = await _teacherDB.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if (teacherData == null)
            {
                return NotFound();
            }
            return View(teacherData);
        }

        //------------------------------------------- Edit --------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _teacherDB.Teachers == null)  // null checking
            {
                return NotFound();
            }
            var teacherData = await _teacherDB.Teachers.FindAsync(id);
            if (teacherData == null)
            {
                return NotFound();
            }
            return View(teacherData);  //fetching that id data and showing on the page
        }

        //---------------- After Clicking Save Button ------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Teacher teaObj)  // that id , new data in obj
        {
            if (id != teaObj.Id)  //checking if id is same in obj as selected id
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                _teacherDB.Teachers.Update(teaObj);
                 await _teacherDB.SaveChangesAsync();
                TempData["update_success"] = " Data updated...";
                return RedirectToAction("Index","Home");
            }
            return View(teaObj);
        }

        //------------------------------------------- Delete -------------------------------------------------


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)     //--------Showing data to be deleted ------------
        {
            if (id == null || _teacherDB.Teachers == null)  // null checking
            {
                return NotFound();
            }
            var teacherData = await _teacherDB.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if(teacherData == null)
            {

            return NotFound(); 
            }
            return View(teacherData);
        }

        //------------- After Clicking Delete Button confirming deletion

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var teacherData = await _teacherDB.Teachers.FindAsync(id);
            if(teacherData != null)
            {
                _teacherDB.Teachers.Remove(teacherData);

            }
            await _teacherDB.SaveChangesAsync();
            TempData["delete_success"] = " Data deleted...";
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
