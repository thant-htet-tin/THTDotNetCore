using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using THTDotNetCore.MvcApp.Models;

namespace THTDotNetCore.MvcApp.Controllers
{

    public class UserController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> IndexAsync()
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
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Update(int id,LoginDataModel RequestModel)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                return RedirectToAction("Index");
            }

            user.FullName = RequestModel.FullName;
            user.Email = RequestModel.Email;
            user.UserName = RequestModel.UserName;

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user is null)
            {
                return RedirectToAction("Index");
            }

            _appDbContext.Remove(user);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
