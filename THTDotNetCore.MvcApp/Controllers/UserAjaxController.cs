using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THTDotNetCore.MvcApp.Models;

namespace THTDotNetCore.MvcApp.Controllers
{
    public class UserAjaxController : Controller
    {
        
       private readonly AppDbContext _appDbContext;

        public UserAjaxController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _appDbContext.Users.OrderByDescending(x => x.UserId).ToListAsync();
            return View(lst);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(LoginDataModel RequestModel)
        {
            await _appDbContext.Users.AddAsync(RequestModel);
            int result = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "saving success" : "saving failed" });
        }


        public async Task<IActionResult> Edit(int id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, LoginDataModel RequestModel)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                return Json(new { Message ="No data found." });
            }

            user.FullName = RequestModel.FullName;
            user.Email = RequestModel.Email;
            user.UserName = RequestModel.UserName;

            int result = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "Updating success" : "Updating failed" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LoginDataModel requestModel)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserId == requestModel.UserId);
            if (user is null)
            {
                return Json(new { Message = "No data found." });
            }

            _appDbContext.Remove(user);
            int result = await _appDbContext.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "Delete success" : "Delete failed" });
        }
    }
}
