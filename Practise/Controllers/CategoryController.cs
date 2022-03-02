using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practise.DataAccessLayer;
using Practise.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace P320Practise.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
                return NotFound();

            var selectedCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
            if (selectedCategory == null)
            {
                return NotFound();
            }

            var categories = await _dbContext.Categories
                .Where(x => x.IsDeleted == false && x.IsMain)
                .Include(x => x.Children.Where(y => y.IsDeleted == false))
                .ToListAsync();

            return View(new CategoryViewModel
            {
                SelectedCategory = selectedCategory,
                Categories = categories
            });
        }
    }
}

