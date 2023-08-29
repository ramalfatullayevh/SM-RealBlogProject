using ShahnazMammadova.Models;

namespace ShahnazMammadova.ViewModels
{
    public class MailVM
    {
        public ICollection<Mail> Mails { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
