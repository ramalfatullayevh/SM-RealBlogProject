namespace ShahnazMammadova.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public string MailSubject { get; set; }
        public string MailMessage { get; set; }

        public DateTime CreateDate { get; set; } 
        public DateTime? ReadDate { get; set; }

        public bool IsRead { get; set; }

        public ICollection<UserMail>? UserMail { get; set; }

    }
}
