using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ShahnazMammadova.DataAccessLayer.Context;
using ShahnazMammadova.Models;
using ShahnazMammadova.ViewModels;

namespace ShahnazMammadova.Areas.shahnazm.Controllers
{
    [Area("shahnazm")]
    public class EmailController : Controller
    {
        readonly AppDBContext _context;
        readonly UserManager<AppUser> _userManager;
        public EmailController(AppDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Arrivals()
        {
            var contacts = await _context.Contacts.OrderByDescending(x => x.CreatedDate).ToListAsync();
            return View(contacts);
        }


        [HttpGet]
        public async Task<IActionResult> CreateMessage()
        {
            ViewBag.Users = new SelectList(_context.AppUser.Where(u => u.IsSubscribed == true), nameof(AppUser.Id), nameof(AppUser.Email));

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMailVM create)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = new SelectList(_context.AppUser.Where(u => u.IsSubscribed == true), nameof(AppUser.Id), nameof(AppUser.Email));
                return View(create);
            }

            var selectedUserIds = create.UserIds;
            var users = await _context.AppUser.Where(user => selectedUserIds.Contains(user.Id)).ToListAsync();

            Mail mail = new Mail
            {
                CreateDate = DateTime.Now,
                MailMessage = create.MailMessage,
                MailSubject = create.MailSubject,
            };

            foreach (var user in users)
            {
                MimeMessage message = new MimeMessage();
                MailboxAddress mailbox = new MailboxAddress("Şahnaz Məmmədova", "tu7ma9clt@code.edu.az");
                message.From.Add(mailbox);
                MailboxAddress mailTo = new MailboxAddress(user.UserName, user.Email);
                message.To.Add(mailTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = create.MailMessage;
                message.Body = bodyBuilder.ToMessageBody();
                message.Subject = create.MailSubject;

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("tu7ma9clt@code.edu.az", "annmioxvuiqhzkxz");
                    await client.SendAsync(message);
                }

                await _context.UserMails.AddAsync(new UserMail { Mail = mail, UserId = user.Id });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact is null) return NotFound();
            var contactVM = new Contact
            {
                Email = contact.Email,
                Name = contact.Name,
                Subject = contact.Subject,
                Message = contact.Message,
                CreatedDate = contact.CreatedDate,
                IsRead = true
            };

            return View(contactVM); 
        }
    }
}



