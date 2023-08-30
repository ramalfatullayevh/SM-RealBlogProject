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
using System.Threading;

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


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact is null) return NotFound();
            var contactVM = new ContactVM
            {
                Email = contact.Email,
                Name = contact.Name,
                Subject = contact.Subject,
                Message = contact.Message,
                CreatedDate = contact.CreatedDate,
            };

            contact.IsRead = true;
            await _context.SaveChangesAsync();
            return View(contactVM); 
        }

        [HttpPost]
        public async Task<IActionResult>Detail(int? id, string? reply)
        {
            if(reply is null)
            {
                ViewBag.Message = "Mesaj Göndərilmədi";
                return View();
            }
            if (id is null) return BadRequest();
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            contact.Reply = reply;
            await _context.SaveChangesAsync();

            MimeMessage message = new MimeMessage();
            MailboxAddress mailbox = new MailboxAddress("Şahnaz Məmmədova", "tu7ma9clt@code.edu.az");
            message.From.Add(mailbox);
            MailboxAddress mailTo = new MailboxAddress(contact.Name, contact.Email);
            message.To.Add(mailTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Geri Dönüş Mesajı: {contact.Reply}";
            message.Body = bodyBuilder.ToMessageBody();
            message.Subject = contact.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("tu7ma9clt@code.edu.az", "annmioxvuiqhzkxz");
            client.SendAsync(message);
            
            return RedirectToAction(nameof(Arrivals));  

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null) return BadRequest();
            var contact = await _context.Contacts.FindAsync(id);
            if(contact is null) return NotFound();
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Arrivals));
        }


        public async Task<IActionResult> Sents()
        {
            var sents = await _context.Mails.Include(x=>x.UserMail).ThenInclude(x=>x.User).ToListAsync();    
            return View(sents);  
        }


        public async Task<IActionResult> SentDelete(int? id)
        {
            if (id is null) return BadRequest();    
            var mail = await _context.Mails.FindAsync(id);  
            if (mail is null) return NotFound();    
            _context.Mails.Remove(mail);    
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Sents));
        }


        public async Task<IActionResult> SentDetail(int? id)
        {
            if (id is null) return BadRequest();
            var mail = await _context.Mails.FindAsync(id);
            if (mail is null) return NotFound();
            var mailvm = new Mail
            {
                MailMessage = mail.MailMessage,
                MailSubject = mail.MailSubject,

            };

            return View(mailvm);
        }
    }

    
}



