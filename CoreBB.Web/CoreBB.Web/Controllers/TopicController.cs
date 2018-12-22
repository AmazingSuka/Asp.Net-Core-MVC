using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBB.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBB.Web.Controllers
{
    public class TopicController : Controller
    {
        private CoreBBContext _dbContext;

        public TopicController(CoreBBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Topic GetTopic(int Id)
        {
            var topic = _dbContext.Topic.Include(t => t.Owner).Include(t => t.ModifiedByUser).SingleOrDefault(t => t.Id == Id);
            if (topic == null)
            {
                throw new Exception("Topic does not exist.");
            }
            return topic;
        }

        [HttpGet]
        public IActionResult Index(int forumId)
        {
            var forum = _dbContext.Forum.Include(f => f.Owner).SingleOrDefault(f => f.Id == forumId);
            if (forum == null)
            {
                throw new Exception("Forum does not exist.");
            }

            forum.Topic = _dbContext.Topic.Include(t => t.Owner)
                .Where(t => t.ForumId == forum.Id && t.ReplyToTopicId == null).ToList();
            return View(forum);
        }

        [HttpGet]
        public IActionResult Create(int forumId)
        {
            var forum = _dbContext.Forum.SingleOrDefault(f => f.Id == forumId);
            if (forum == null)
            {
                throw new Exception("Forum does not exist.");
            }

            if (forum.IsLocked && !User.IsInRole(Roles.Administrator))
            {
                throw new Exception("This forum is locked.");
            }

            var topic = new Topic { ForumId = forumId };
            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Topic model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid topic information.");
            }

            var user = _dbContext.User.SingleOrDefault(u => u.Name == User.Identity.Name);
            model.OwnerId = user.Id;
            model.PostDateTime = DateTime.Now;
            await _dbContext.Topic.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            model.RootTopicId = model.Id;
            _dbContext.Topic.Update(model);
            await _dbContext.SaveChangesAsync();
                
            return RedirectToAction("Index", new { model.ForumId });
        }

        [HttpGet]
        public IActionResult Detail(int Id)
        {
            var rootTopic = _dbContext.Topic.Include(t => t.Owner).Include(t => t.ModifiedByUser)
                .SingleOrDefault(t => t.Id == Id);
            if (rootTopic == null)
            {
                throw new Exception("Topic does not exist.");
            }

            rootTopic.InverseReplyToTopic = _dbContext.Topic.Include(t => t.Owner).Include(t => t.ModifiedByUser)
                .Where(t => t.RootTopicId == Id && t.ReplyToTopicId != null).ToList();
            return View(rootTopic);
        }

        [HttpGet]
        public IActionResult Reply(int toId)
        {
            var toTopic = _dbContext.Topic.SingleOrDefault(t => t.Id == toId);
            if (toTopic == null)
            {
                throw new Exception("Topic does not exist.");
            }

            var topic = new Topic
            {
                ReplyToTopicId = toTopic.Id,
                RootTopicId = toTopic.RootTopicId,
                ForumId = toTopic.ForumId,
                ReplyToTopic = toTopic
            };

            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(Topic model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid topic information.");
            }


            var user = _dbContext.User.SingleOrDefault(u => u.Name == User.Identity.Name);
            model.OwnerId = user.Id;
            model.PostDateTime = DateTime.Now;
            await _dbContext.Topic.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { forumId = model.ForumId });
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var topic = GetTopic(Id); 

            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Topic model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid information for topic");
            }
            var user = _dbContext.User.SingleOrDefault(u => u.Name == User.Identity.Name);
            model.ModifiedByUserId = user.Id;
            model.ModifyDateTime = DateTime.Now;
            _dbContext.Topic.Update(model);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { forumId = model.ForumId });
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var topic = GetTopic(Id); 

            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Topic model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid topic information");
            }
            var topic = _dbContext.Topic.SingleOrDefault(t => t.Id == model.Id);
            IQueryable<Topic> replyTopics;
            if (topic.ReplyToTopicId == null)
            {
                replyTopics = _dbContext.Topic.Where(t => t.RootTopicId == topic.Id);
            }
            else
            {
                replyTopics = _dbContext.Topic.Where(t => t.ReplyToTopicId == topic.Id);
            }

            _dbContext.Topic.Remove(topic);
            _dbContext.Topic.RemoveRange(replyTopics);
            await _dbContext.SaveChangesAsync();
    
            return RedirectToAction("Index", new { forumId = topic.ForumId });
        }
    }
}