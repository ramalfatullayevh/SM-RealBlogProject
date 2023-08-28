using ShahnazMammadova.Models;

namespace ShahnazMammadova.ViewModels
{
    public class CreateMailVM
    {
        public List<string> UserIds { get; set; }
        public AppUser? User { get; set; }

        public string MailSubject { get; set; }
        public string MailMessage { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? ReadDate { get; set; }
    }
}
