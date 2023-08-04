using System;

namespace Denis.UserList.Common.Entities
{
    public class Award
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }

        public Award()
        {
        }

        public Award(int id, string title)
        {
            Id = id;
            Title = title;
            Image = null;
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
