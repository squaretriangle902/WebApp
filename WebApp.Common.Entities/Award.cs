using System;

namespace WebApp.Common.Entities
{
    public class Award
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ImageId { get; set; }

        public Award()
        {
        }

        public Award(int id, string title)
        {
            Id = id;
            Title = title;
            ImageId = null;
        }

        public override int GetHashCode() 
        {
            return Id + Title.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Award award && award.Id == Id && award.Title == Title;
        }
    }
}
