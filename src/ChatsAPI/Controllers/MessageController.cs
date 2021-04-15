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
        public ActionResult<Message> GetById(int messageId) =>
            _context.Messages.First(c => c.Id == messageId);

        [HttpGet]
        [Route("/Messages/GetByUserIdAndProjectId/{userId}/{projectId}")]
        public ActionResult<IEnumerable<Message>> GetByUserIdAndProjectId(int userId, int projectId) =>
            _context.Messages.Where(c => c.UserId == userId && c.ProjectId == projectId).ToList();

        public class MessageIdFormat
        {
            public int messageId { get; set; }
        }
        [HttpPost]
        [Route("/Messages/Delete")]
        public async Task Delete([FromBody]MessageIdFormat messageIdFromat)
        {
            var message = await _context.Messages.FirstAsync(c => c.Id == messageIdFromat.messageId);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

       
        [HttpPost]
        [Route("/Messages/Add")]
        public async Task Add([FromBody]Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}