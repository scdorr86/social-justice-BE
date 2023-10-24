namespace social_justice_BE.Models
{
    public class Meetup
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime MeetTime { get; set; } = default(DateTime);
        public string Location { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public List <Member> Members { get; set; }
        public int Attending => Members.Count;
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

    }
}
