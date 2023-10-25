﻿namespace social_justice_BE.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created_at { get; set; } = DateTime.Now;
        public List<Member> Members { get; set; } = new List<Member>();
        public List<Meetup> Meetups { get; set; } = new List<Meetup>();
        public int MemberCount => Members.Count;
        public int MeetupCount => Meetups.Count;
        public string Mission { get; set; }

    }
}