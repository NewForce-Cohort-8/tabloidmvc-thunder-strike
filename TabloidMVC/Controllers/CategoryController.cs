using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {

                if (category == null)
                {
                    return NotFound();
                }
            }
            return View(category);
        }

        // GET: CategoryController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id
              
                _categoryRepository.AddCategory(category);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRepository.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            
            {
                var category = _categoryRepository.GetCategoryById(id);

                return View(category);
            }
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRepository.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }
    }
}
