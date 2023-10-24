namespace social_justice_BE.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? MemberSince { get; set; } = DateTime.Now;
        public string ImageUrl { get; set; }
        public List <Meetup> Meetups { get; set; }
        public int Uid { get; set; }
    }
}
