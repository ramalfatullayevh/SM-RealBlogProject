namespace ShahnazMammadova.Models
{
    public class UserMail
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser? User { get; set; }

        public int MailId { get; set; }
        public Mail? Mail { get; set; }
    }
}
