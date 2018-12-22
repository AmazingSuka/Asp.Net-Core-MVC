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
    public class MessageController : Controller
    {
        private CoreBBContext _dbContext;

        public MessageController(CoreBBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Message GetMessage(int Id)
        {
            var message = _dbContext.Message.Include(m => m.FromUser).Include(m => m.ToUser)
                .SingleOrDefault(m => m.Id == Id);

            if (message == null)
            {
                throw new Exception("Message does not exist.");
            }

            return message;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var user = _dbContext.User.SingleOrDefault(u => u.Name == User.Identity.Name);
            var messages = _dbContext.Message.Include(m => m.FromUser).Include(m => m.ToUser)
                .Where(t => t.ToUser.Name == user.Name || t.FromUser.Name == user.Name);

            return View(messages);
        }

        [HttpGet]
        public IActionResult Create(string toUserName)
        {
            var ToUser = _dbContext.User.SingleOrDefault(u => u.Name == toUserName);
            if (ToUser == null)
            {
                throw new Exception("User does not exist.");
            }
            var message = new Message { ToUserId = ToUser.Id, ToUser = ToUser };

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Message model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Information for message is invalid.");
            }

            var FromUser = _dbContext.User.SingleOrDefault(u => u.Name == User.Identity.Name);
            model.FromUserId = FromUser.Id;
            model.SendDateTime = DateTime.Now;
            model.IsRead = false;
            await _dbContext.Message.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home"); // ---
        } // ---

        [HttpGet]
        public IActionResult Detail(int Id)
        {
            var message = GetMessage(Id);
            var messages = _dbContext.Message
                .Where(m => (m.FromUserId == message.FromUserId && m.ToUserId == message.ToUserId) || (m.FromUserId == message.ToUserId && m.ToUserId == message.FromUserId))
                .OrderBy(m => m.SendDateTime);

            return View(messages);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var message = GetMessage(Id);

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Message model)
        {
            _dbContext.Message.Remove(model);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}