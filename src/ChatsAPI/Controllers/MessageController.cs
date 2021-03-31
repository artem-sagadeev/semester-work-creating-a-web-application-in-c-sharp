using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatsAPI.Controllers
{
    public class MessageController : Controller
    {
        private readonly ChatsContext _context;
        public MessageController(ChatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Messages/Get")]
        public ActionResult<IEnumerable<Message>> Get() => _context.Messages.ToList();

        [HttpGet]
        [Route("/Messages/GetByProjectId/{projectId}")]
        public ActionResult<IEnumerable<Message>> GetByProjectId(int projectId) =>
            _context.Messages.Where(c => c.ProjectId == projectId).ToList();

        [HttpGet]
        [Route("/Messages/GetById/{messageId}")]
        public ActionResult<IEnumerable<Message>> GetById(int messageId) =>
            _context.Messages.Where(c => c.Id == messageId).ToList();


        [HttpGet]
        [Route("/Messages/GetByUserIdAndProjectId")]
        public ActionResult<IEnumerable<Message>> GetByUserIdAndProjectId(int userId, int projectId) =>
            _context.Messages.Where(c => c.UserId == userId && c.ProjectId == projectId).ToList();


        [HttpPost]
        [Route("/Messages/Delete")]
        public async Task Delete(int messageId)
        {
            var message = await _context.Messages.FirstAsync(c => c.Id == messageId);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Messages/Add")]
        public async Task Add(int projectId, int userId, string text)
        {
            var message = new Message()
            {
                ProjectId = projectId,
                UserId = userId,
                Text = text,
                DateTime = DateTime.Now
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

    }
}
