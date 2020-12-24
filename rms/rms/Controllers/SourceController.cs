using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rms.Data;
using rms.Models;
using rms.Repositories;

namespace rms.Controllers {

    public class SourceController : Controller {

        private readonly IAccountRepository _accountRepository;
        private readonly ApplicationDbContext _context;

        public SourceController(IAccountRepository accountRepository, ApplicationDbContext context) {
            _accountRepository = accountRepository;
            _context = context;
        }

        [Authorize]
        public IActionResult CreateSource() {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSource ([Bind("Name,Capacity,Description,Owner")] ApplicationSource sourceModel) {

            if (ModelState.IsValid) {
                var currentUser = User;
                var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;
                var user = await _accountRepository.FindByEmailAsync(currentUserEmail);
                sourceModel.OwnerId = user.Id;

                _context.Add(sourceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MySources));
            }
            return View(sourceModel);
        }

        [Authorize]
        public async Task<IActionResult> MySources() {

            var currentUser = User;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;
            var user = await _accountRepository.FindByEmailAsync(currentUserEmail);

            var sourceList = _context.Sources.Where(s => s.OwnerId == user.Id).ToList();
            user.Sources = sourceList;

            return View(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditSource(int id) {
            var currentUser = User;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;
            var user = await _accountRepository.FindByEmailAsync(currentUserEmail);

            var sourceList = _context.Sources.Where(s => s.OwnerId == user.Id && s.Id == id).ToList();
            if (sourceList.Count > 0) {
                ViewBag.EditSource = sourceList[0];
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateSource([Bind("Name,Capacity,Description")] ApplicationSource sourceModel, int id) {

            var currentUser = User;
            var currentUserEmail = currentUser.FindFirst(ClaimTypes.Email).Value;
            var user = await _accountRepository.FindByEmailAsync(currentUserEmail);

            if (ModelState.IsValid) {
                var data = _context.Sources.Where(x => x.Id == id).FirstOrDefault();

                data.Name = sourceModel.Name;
                data.Capacity = sourceModel.Capacity;
                data.Description = sourceModel.Description;
                data.OwnerId = user.Id;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(MySources));
            }
            return View(sourceModel);
        }

        [Authorize]
        public async Task<IActionResult> DeleteSource(int id) {

            var source = _context.Sources.Where(v => v.Id == id).FirstOrDefault();
            _context.Remove(source);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MySources));
        }
    }
}
