using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBB.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBB.Web.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private CoreBBContext _dbContext;

        public ForumController(CoreBBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Forum GetForum(Forum model)
        {
            var forum = _dbContext.Forum.SingleOrDefault(f => f.Id == model.Id);
            if (forum == null)
            {
                throw new Exception("Forum does not exist.");
            }

            return forum;
        }

        private Forum GetForum(int Id)
        {
            var forum = _dbContext.Forum.Include(f => f.Owner).SingleOrDefault(f => f.Id == Id);
            if (forum == null)
            {
                throw new Exception("Forum does not exist.");
            }

            return forum;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var forums = _dbContext.Forum.Include(u => u.Owner).ToList();
            return View(forums);
        }

        [HttpGet, Authorize(Roles = Roles.Administrator)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet, Authorize(Roles = Roles.Administrator)]
        public IActionResult Lock(int Id)
        {
            var forum = GetForum(Id);

            return View(forum);
        }

        public async Task<IActionResult> Lock(Forum model)
        {
            var forum = GetForum(model);

            forum.IsLocked = !forum.IsLocked;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Detail", new { forum.Id });
        }

        [HttpPost, Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Create(Forum model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid forum information.");
            }

            model.OwnerId = _dbContext.User.SingleOrDefault(u => u.Name == User.Identity.Name).Id;
            model.CreateDateTime = DateTime.Now;
            await _dbContext.Forum.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int Id)
        {
            var forum = GetForum(Id);

            return View(forum);
        }

        [HttpGet, Authorize(Roles = Roles.Administrator)]
        public IActionResult Delete(int Id)
        {
            var forum = GetForum(Id);

            return View(forum);
        }

        [HttpPost, Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Delete(Forum model)
        {
            var forum = GetForum(model);

            _dbContext.Forum.Remove(forum);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet, Authorize(Roles = Roles.Administrator)]
        public IActionResult Edit(int Id)
        {
            var forum = GetForum(Id);

            return View(forum);
        }

        [HttpPost, Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Edit(Forum model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid forum information");
            }

            _dbContext.Forum.Update(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}